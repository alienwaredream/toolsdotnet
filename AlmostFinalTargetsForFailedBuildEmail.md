
```

<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">

  <!--
=== PROPERTY GROUP ============================================================================
BranchLocalRoot
 - Sets BranchLocalRoot used on the build server
MSBuildSitronicsExtensionsPath
 - Sets MSBuildSitronicsExtensionsPath used on the build server
===============================================================================================
    -->
  <PropertyGroup>
	<SkipGetChangesetsAndUpdateWorkItems>true</SkipGetChangesetsAndUpdateWorkItems>
    <BranchLocalRoot Condition="'$(BranchLocalRoot)'==''">$(MSBuildProjectDirectory)\..\B</BranchLocalRoot>
    <BranchLocalRoot Condition=" '$(BranchLocalRoot)' != '' and !HasTrailingSlash('$(BranchLocalRoot)') ">$(BranchLocalRoot)\</BranchLocalRoot>
    <MSBuildSitronicsExtensionsPath Condition="'$(MSBuildSitronicsExtensionsPath)'==''">$(BranchLocalRoot)\Tools\MSBuild\Extensions\Quick\</MSBuildSitronicsExtensionsPath>
  </PropertyGroup>

  <!--
=== IMPORT ====================================================================================
MSBuild Community Tasks Targets
 - Imports the targets file.
===============================================================================================
  -->
  <Import Project="$(MSBuildExtensionsPath)\MSBuildCommunityTasks\MSBuild.Community.Tasks.Targets" />

  <!--
=== IMPORT ====================================================================================
Microsoft.TeamFoundation.Build.targets
 - Imports the Microsoft Team Build targets file.
===============================================================================================
  -->
  <Import Project="$(MSBuildExtensionsPath)\Microsoft\VisualStudio\TeamBuild\Microsoft.TeamFoundation.Build.targets" />

  <!--
=== USING TASK ================================================================================
Sitronics Tasks
 - Imports the following tasks provided by the Sitronics.Tasks.dll assembly.
Tasks:
 - GenerateVersionNumber - Generates build number in accord with the versioning strategy
 - SetEnvVar - Sets enviroment variable.
===============================================================================================
  -->
  <UsingTask AssemblyFile="$(MSBuildExtensionsPath)\SitronicsTeamBuild.Quick\Sitronics.Tasks.dll" TaskName="Sitronics.Tasks.SetEnvVar" />
  <UsingTask AssemblyFile="$(MSBuildExtensionsPath)\SitronicsTeamBuild.Quick\tools.tfs.utils.exe" TaskName="Tools.TeamBuild.Tasks.ResolveUser" /> 
  <UsingTask AssemblyFile="$(MSBuildExtensionsPath)\SitronicsTeamBuild.Quick\tools.tfs.utils.exe" TaskName="Tools.TeamBuild.Tasks.BuildGateKeeper" /> 
  <!--
=== PROPERTY GROUP ============================================================================
SolutionRoot
 - Sets Solution root to equal to BranchLocalRoot.
SkipSetEnvironment
 - Sets default value of SkipSetEnvironment to false.
ForceGet
 - Overrides the ForceGet property to new value equal to true.
TfCommand
 - Specifies a location of the TF command.
===============================================================================================
    -->
  <PropertyGroup>

    <SolutionRoot>$(BranchLocalRoot)</SolutionRoot>

    <!-- Set this property to true to skip SetEnvironment target -->
    <SkipSetEnvironment Condition=" '$(SkipSetEnvironment)'=='' " >false</SkipSetEnvironment>

    <TfCommand>"$(ProgramFiles)\Microsoft Visual Studio 9.0\Common7\IDE\TF.EXE"</TfCommand>

  </PropertyGroup>

<!--
=== TARGET ====================================================================================
FailCurrentBuildStep
 - Fails current build step. Call it from OnError if needed
Inputs:
Outputs:  
===============================================================================================
-->
  <Target Name="FailCurrentBuildStep">
    <BuildStep TeamFoundationServerUrl="$(TeamFoundationServerUrl)" BuildUri="$(BuildUri)" Id="$(CurrentBuildStepId)" Status="Failed" />
  </Target>


  <!--
=== TARGET ====================================================================================
SetEnvironment
 - Sets development environment.
Inputs:
Outputs:  
===============================================================================================
  -->
  <PropertyGroup>
    <SetEnvironmentDependsOn>
    </SetEnvironmentDependsOn>
  </PropertyGroup>
  <!-- CoreGet -->
  <Target Name="SetEnvironment"
          DependsOnTargets="$(SetEnvironmentDependsOn)"
          Condition="'$(SkipSetEnvironment)'!='true'">
		  
	
    <BuildStep TeamFoundationServerUrl="$(TeamFoundationServerUrl)" BuildUri="$(BuildUri)" Name="Set environment" Message="Setting environment variables and properties">
      <Output TaskParameter="Id" PropertyName="CurrentBuildStepId" />
    </BuildStep>

    <SetEnvVar Variable="BranchLocalRoot" Value="$(BranchLocalRoot)" />

    <CreateProperty Value="$(SolutionRoot)">
      <Output
                TaskParameter="Value"
                PropertyName="BranchLocalRoot" />
    </CreateProperty>

    <BuildStep TeamFoundationServerUrl="$(TeamFoundationServerUrl)" BuildUri="$(BuildUri)" Id="$(CurrentBuildStepId)" Status="Succeeded" />

    <OnError ExecuteTargets="FailCurrentBuildStep" />
  </Target>
	
  <!--
=== TARGET ====================================================================================
RunTestWithConfiguration
 -  Overrides RunTestWithConfiguration Microsoft target. It runs NCover and NUnit tasks.
Inputs:
 - IsDesktopBuild
 - Flavor
 - Platform
 - BinariesRoot
 - SearchPathRoot
Outputs:  
 - BuildNumber - the new BuildNumber
===============================================================================================
  -->  
  <Target Name="RunTestWithConfiguration" >

    <Message Text="Run NCOVER and NUNIT" />
<!--
    <TeamBuildMessage
          Tag="Configuration"
          Condition=" '$(IsDesktopBuild)'!='true' "
          Value="$(Flavor)" />

    <TeamBuildMessage
          Tag="Platform"
          Condition=" '$(IsDesktopBuild)'!='true' "
          Value="$(Platform)" />
-->
    <!-- SearchPathRoot for not Any CPU -->
    <CreateProperty
          Condition=" '$(Platform)'!='Any CPU' "
          Value="$(BinariesRoot)\$(Platform)\$(Flavor)\" >
      <Output TaskParameter="Value" PropertyName="SearchPathRoot" />
    </CreateProperty>

    <!-- SearchPathRoot for Any CPU -->
    <CreateProperty
          Condition=" '$(Platform)'=='Any CPU' "
          Value="$(BinariesRoot)\$(Flavor)\" >
      <Output TaskParameter="Value" PropertyName="SearchPathRoot" />
    </CreateProperty>

    <CreateItem include="$(SearchPathRoot)*.*">
      <Output TaskParameter="include" ItemName="AssembliesToTest" />
    </CreateItem>

    <Message Text="Assemblies to Test: @(AssembliesToTest)" />

  </Target>

  <PropertyGroup>
    <ProgressMessage>Build started...</ProgressMessage>
  </PropertyGroup>
  
  <PropertyGroup>
    <BeforeCompileDependsOn>
    </BeforeCompileDependsOn>
  </PropertyGroup>
  <Target Name="BeforeCompile"
          DependsOnTargets="$(BeforeCompileDependsOn)"
          Inputs="@(DirectoryToDrop)"
          Outputs="%(DirectoryToDrop.Identity)">

    <!-- Combine SourceDirectoryToDrop -->
    <CombinePath BasePath="$(BranchLocalRoot)" Paths="%(DirectoryToDrop.Identity)">
      <Output TaskParameter="CombinedPaths" PropertyName="SourceDirectoryToDrop"/>
    </CombinePath>

    <MakeDir Directories="$(DropLocation)\$(BuildNumber)" Condition="!Exists('$(DropLocation)\$(BuildNumber)')"></MakeDir>
    <WriteLinesToFile File="$(DropLocation)\$(BuildNumber)\Build.Progress" Lines="$(ProgressMessage)" Overwrite="true" />

  </Target>

  <Target Name="CallCompile"
         DependsOnTargets="$(CoreCompileDependsOn)"
         Inputs="%(ConfigurationToBuild.PlatformToBuild);%(ConfigurationToBuild.FlavorToBuild)"
         Outputs="%(ConfigurationToBuild.PlatformToBuild);%(ConfigurationToBuild.FlavorToBuild)" 
         >
			<Message Importance="high" Text="**$(RequestedFor)" />
			
    	<ResolveUser WindowsAccountName="$(RequestedFor)">
			<Output TaskParameter="DisplayName" PropertyName="Requestor" />
			<Output TaskParameter="MailAddress" PropertyName="MailAddress" />		
		</ResolveUser>
		
    <BuildStep TeamFoundationServerUrl="$(TeamFoundationServerUrl)" BuildUri="$(BuildUri)" Name="Compile configuration" Message="Compiling configuration %(ConfigurationToBuild.Identity)">
		<Output TaskParameter="Id" PropertyName="CurrentBuildStepId" />
    </BuildStep>

    <!-- RunCodeAnalysis option -->
    <CreateProperty
          Condition=" '$(RunCodeAnalysis)'=='Always' "
          Value="RunCodeAnalysis=true" >
      <Output TaskParameter="Value" PropertyName="CodeAnalysisOption" />
    </CreateProperty>

    <!-- Second part of VCOverride file when RunCodeAnalysis is always -->
    <CreateProperty
          Condition=" '$(RunCodeAnalysis)'=='Always' "
          Value="%09%3CTool Name=%22VCCLCompilerTool%22 EnablePREfast=%22true%22 /%3E%0D%0A%09%3CTool Name=%22VCFxCopTool%22 EnableFxCop=%22true%22 /%3E%0D%0A" >
      <Output TaskParameter="Value" PropertyName="VCOverridesString2"/>
    </CreateProperty>

    <CreateProperty
          Condition=" '$(RunCodeAnalysis)'=='Never' "
          Value="RunCodeAnalysis=false" >
      <Output TaskParameter="Value" PropertyName="CodeAnalysisOption" />
    </CreateProperty>

    <!-- Second part of VCOverride file when RunCodeAnalysis is never -->
    <CreateProperty
          Condition=" '$(RunCodeAnalysis)'=='Never' "
          Value="%09%3CTool Name=%22VCCLCompilerTool%22 EnablePREfast=%22false%22 /%3E%0D%0A%09%3CTool Name=%22VCFxCopTool%22 EnableFxCop=%22false%22 /%3E%0D%0A" >
      <Output TaskParameter="Value" PropertyName="VCOverridesString2"/>
    </CreateProperty>

    <!-- Generate VCOverride file for C++ projects -->
    <WriteLinesToFile
          File="TFSBuild.vsprops"
          Lines="$(VCOverridesString1)$(VCOverridesString2)$(AdditionalVCOverrides)$(VCOverridesString3)"
          Overwrite="true" />


    <!-- Build using MSBuild task -->
    <!-- Workaround to build only BM for x64 - to remove it - just remove condition after And-->
    <!--'%(ConfigurationToBuild.PlatformToBuild)'!='x64' Or '%(SolutionToBuild.Filename)'=='BalanceManagement'-->
	<!-- <Error Text="errjfvjndvjkndfkvjfdnv" /> -->
	
    <MSBuild
          Condition=" '@(SolutionToBuild)'!=''"
          Projects="@(SolutionToBuild)"
          Properties="Configuration=%(ConfigurationToBuild.FlavorToBuild);Platform=%(ConfigurationToBuild.PlatformToBuild);SkipInvalidConfigurations=true;VCBuildOverride=$(MSBuildProjectDirectory)\TFSBuild.vsprops;FxCopDir=$(FxCopDir);ReferencePath=$(ReferencePath);TeamBuildConstants=$(TeamBuildConstants);$(CodeAnalysisOption);Version=$(Version);BranchLocalRoot=$(BranchLocalRoot);MSBuildSitronicsExtensionsPath=$(MSBuildSitronicsExtensionsPath);SkipLocalization=$(SkipLocalization);SkipDocumentation=$(SkipDocumentation)"
          Targets="Build">
      <Output TaskParameter="TargetOutputs" ItemName="FilesToDrop" />
    </MSBuild>

	    <CreateProperty
          Value="Success" >
			<Output TaskParameter="Value" PropertyName="BuildStatus" />
		</CreateProperty>
		
		<BuildGateKeeper BuildStatus="$(BuildStatus)" StateFilePath="c:\Build\$(BuildDefinition).txt" RequestorMailAddress="$(MailAddress)" RequestorDisplayName="$(Requestor)">		
		</BuildGateKeeper>
	
		<Mail SmtpServer="YourSmtpServer" IsBodyHtml="true" Username="xxx\xxx" Password="xxx" To="$(MailAddress)" CC="controlfreak@controlfreak.com" From="build@sitronicsts.com" Body="Successful check-in! Your checkin to SP5 Dev branch was built successfuly. Check-in number and comments to be provided soon. Build log: %3CBR%2F%3E $(DropLocation)\$(BuildNumber)\BuildLog.txt;" Subject="Your checkin to SP5 Dev branch was built successfuly."></Mail>

	    <BuildStep TeamFoundationServerUrl="$(TeamFoundationServerUrl)" BuildUri="$(BuildUri)" Id="$(CurrentBuildStepId)" Status="Succeeded" />
    	   <SetBuildProperties TeamFoundationServerUrl="$(TeamFoundationServerUrl)"
			  BuildUri="$(BuildUri)"
			  CompilationStatus="Succeeded"
			  TestStatus="Succeeded" />
	  
	      <OnError ExecuteTargets="FailCurrentBuildStep;CreateWorkItem" />

  </Target>

  <PropertyGroup>
    <ErrorMessage>Build finished with errors.</ErrorMessage>
  </PropertyGroup>

  <Target Name="CreateFileSignalizeError">
    <WriteLinesToFile File="$(DropLocation)\$(BuildNumber)\Build.Error" Lines="$(ErrorMessage)" Overwrite="true" />
    <Delete Files="$(DropLocation)\$(BuildNumber)\Build.Progress" />
  </Target>

  <!--
=== TARGET ====================================================================================
CreateWorkItem
 -  Overrides CreateWorkItem Microsoft target. The target create a bug if the build is failed.
Inputs:
Outputs:  
===============================================================================================
  -->
  <Target Name="CreateWorkItem" Condition=" '$(SkipWorkItemCreation)'!='true' and '$(IsDesktopBuild)'!='true'  and '$(UpdateAssociatedWorkItems)'=='true'">
	<CreateProperty Value="Dvoychenko Stanislav" Condition="'$(Requestor)' == ''">
      <Output TaskParameter="Value" PropertyName="Requestor" />
    </CreateProperty>
    <CreateProperty Value="$(WorkItemTitle) $(BuildNumber)">
      <Output TaskParameter="Value" PropertyName="WorkItemTitle" />
    </CreateProperty>
    <CreateProperty Value="$(BuildlogText) &lt;a href='file:///$(DropLocation)\$(BuildNumber)\BuildLog.txt' &gt; $(DropLocation)\$(BuildNumber)\BuildLog.txt &lt;/a &gt;.">
      <Output TaskParameter="Value" PropertyName="BuildlogText" />
    </CreateProperty>
    <CreateProperty Value="$(ErrorWarningLogText) &lt;a href='file:///$(DropLocation)\$(BuildNumber)\ErrorsWarningsLog.txt' &gt; $(DropLocation)\$(BuildNumber)\ErrorsWarningsLog.txt  &lt;/a &gt;. ">
      <Output TaskParameter="Value" PropertyName="ErrorWarningLogText" Condition="Exists('$(DropLocation)\$(BuildNumber)\ErrorsWarningsLog.txt')" />
    </CreateProperty>
    <CreateProperty Value="$(DescriptionText) %3CBR%2F%3E $(BuildlogText) %3CBR%2F%3E ">
      <Output TaskParameter="Value" PropertyName="WorkItemDescription" />
    </CreateProperty>
    <CreateProperty Value="$(DescriptionText) %3CBR%2F%3E test">
      <Output TaskParameter="Value" PropertyName="WorkItemDescription2" />
    </CreateProperty>
    <!-- backup basic item. vlk. -->
    <!-- 
    <CreateProperty Value="$(DescriptionText) %3CBR%2F%3E $(BuildlogText) %3CBR%2F%3E $(ErrorWarningLogText)">
      <Output TaskParameter="Value" PropertyName="WorkItemDescription" />
    </CreateProperty> 
    -->
    <!--System.IterationPath=$(System.IterationPath);-->
    <!--Microsoft.VSTS.CMMI.StepsToReproduce= ???? ;
    System.HyperLinkCount= ???? ;-->
    <CreateProperty Value="System.Title=Build - $(BuildNumber); Strom.Foris.Bug.Type=Configuration; Strom.BugCategory=Internal; System.AssignedTo=$(Requestor); Microsoft.VSTS.Common.Severity=Critical; System.State=Proposed; System.Reason=Build Failure; System.Description=Build $(BuildNumber) failed.; Microsoft.VSTS.CMMI.Symptom=Build $(BuildNumber) failed. %3CBR%2F%3E $(DropLocation)\$(BuildNumber)\BuildLog.txt; Microsoft.VSTS.CMMI.StepsToReproduce=Run team build Test Team Build; System.HyperLinkCount=$(DropLocation)\$(BuildNumber)\BuildLog.txt ">
      <Output TaskParameter="Value" PropertyName="WorkItemFieldValues" />
    </CreateProperty>
    <!-- System.Reason=Build falure;-->
    <!-- Create WorkItem for build failure -->
	<!--
    <CreateNewWorkItem BuildId="$(BuildNumber)" Description="$(WorkItemDescription)" TeamProject="$(TeamProject)" TeamFoundationServerUrl="$(TeamFoundationServerUrl)" Title="$(WorkItemTitle)" WorkItemFieldValues="$(WorkItemFieldValues)" WorkItemType="$(WorkItemType)" ContinueOnError="true" />
	-->
	<Copy Condition="Exists('$(MSBuildProjectDirectory)\build.errors.txt')"
		SourceFiles="$(MSBuildProjectDirectory)\build.errors.txt" DestinationFolder="$(DropLocation)\$(BuildNumber)\" />
	
	<ReadLinesFromFile Condition="Exists('$(DropLocation)\$(BuildNumber)\build.errors.txt')"
            File="$(DropLocation)\$(BuildNumber)\build.errors.txt" >
            <Output
                TaskParameter="Lines"
                ItemName="BuildErrorLines"/>
        </ReadLinesFromFile>
		
	<CreateProperty
          Value="Failure" >
			<Output TaskParameter="Value" PropertyName="BuildStatus" />
	</CreateProperty>
		
		<BuildGateKeeper BuildStatus="$(BuildStatus)" StateFilePath="c:\Build\$(BuildDefinition).txt" RequestorMailAddress="$(MailAddress)" RequestorDisplayName="$(Requestor)">
			<Output TaskParameter="BreakerMailAddress" PropertyName="BreakerMailAddress" />
			<Output TaskParameter="BreakerDisplayName" PropertyName="BreakerDisplayName" />		
			<Output TaskParameter="BreakTimeStamp" PropertyName="BreakTimeStamp" />					
		</BuildGateKeeper>

	<Mail SmtpServer="YourSmtpServer" IsBodyHtml="true" Username="xxx\xxx" Password="xxx" To="$(MailAddress)" CC="controlfreak@controlfreak.com;$(BreakerMailAddress)" From="build@sitronicsts.com" Body="You got this email message because the code you checked in recently was not compiled successfully during verification build. Changeset number and comments to be provided soon. &lt;br/&gt; The build was broken first by $(BreakerDisplayName) at $(BreakTimeStamp). &lt;br/&gt;&lt;br/&gt; Errors log: &lt;br/&gt; &lt;b&gt; @(BuildErrorLines) &lt;/b&gt; &lt;br/&gt; &lt;br/&gt; More detailed info: &lt;a href='file:///$(DropLocation)\$(BuildNumber)\BuildLog.txt' &gt; $(DropLocation)\$(BuildNumber)\BuildLog.txt &lt;/a &gt;. &lt;br/&gt; If you believe this informatin is false positive or need help with fixing check-in, please reply to the Dvoychenko Stanislav." Subject="FAILURE! Build - $(BuildNumber) - changes your checked in were not compiled successfuly."></Mail>
  
  </Target>

  <!--
=== TARGET ====================================================================================
SetBuildFailed
 -  Sets BuildFailed to true if the build is failed.
Inputs:
Outputs:  
===============================================================================================
  -->
  <PropertyGroup>
    <OnBuildBreakDependsOn>
      $(OnBuildBreakDependsOn);
      SetBuildFailed
    </OnBuildBreakDependsOn>
    <TeamBuildDependsOn>
      $(TeamBuildDependsOn);  
    </TeamBuildDependsOn>
  </PropertyGroup>

  <Target Name="SetBuildFailed">
    <CreateProperty Value="true" >
      <Output TaskParameter="Value" PropertyName="BuildFailed"/>
    </CreateProperty>    
  </Target>

<!--
=== TARGET ====================================================================================
RunSourceServerIndexing
 -  Indexes pdb files and copies them to symbol server.
Inputs:
Outputs:  
===============================================================================================
  -->

  <PropertyGroup>
    <SourceServerIndexing Condition="'$(SourceServerIndexing)' == ''">false</SourceServerIndexing>
    <SymbolsProduction Condition="'$(SymbolsProduction)' == ''">false</SymbolsProduction>
  </PropertyGroup>
  
  <Target Name="RunSourceServerIndexing" Condition="'$(SourceServerIndexing)' == 'true'">

    <BuildStep TeamFoundationServerUrl="$(TeamFoundationServerUrl)" BuildUri="$(BuildUri)" Name="Source server" Message="Source server indexing">
		<Output TaskParameter="Id" PropertyName="CurrentBuildStepId" />
    </BuildStep>

    <!-- Index pdb files -->    
    <Exec Command="&quot;C:\Program Files\Debugging Tools for Windows (x64)\srcsrv\tfsindex.cmd&quot; &quot;$(BranchLocalRoot)Build&quot; > &quot;$(DropLocation)\$(BuildNumber)\SourceServerIndexing.log&quot;" WorkingDirectory="$(SolutionRoot)"/>
    <Exec Command="tf.exe workfold" WorkingDirectory="$(SolutionRoot)" ContinueOnError="True"/>
    <!-- Check the index log file for errors -->
    <ReadLinesFromFile File="$(DropLocation)\$(BuildNumber)\SourceServerIndexing.log" >
      <Output TaskParameter="Lines" ItemName="SourceServerIndexingLines"/>
    </ReadLinesFromFile>

    <RegexMatch Input="@(SourceServerIndexingLines)" Expression="Skipping|Error|Failed" Options="IgnoreCase">
      <Output TaskParameter="Output" ItemName ="SourceServerIndexingErrors" />
    </RegexMatch>

    <WriteLinesToFile File="$(DropLocation)\$(BuildNumber)\SourceServerIndexing.err" Lines="@(SourceServerIndexingErrors)" Overwrite="true"/>

	<!--<Error Text="Source server indexing failed!" Condition="'@(SourceServerIndexingErrors)' != ''" />-->


    <!-- Safe pdb files to symbol server-->
    <Exec Condition="'$(SymbolsProduction)' == 'true'" Command="&quot;C:\Program Files\Debugging Tools for Windows (x86)\symstore.exe&quot; add /compress /f &quot;$(BranchLocalRoot)Build\%(ConfigurationToBuild.PlatformToBuild)\Release\*.pdb&quot; /r /o /s \\build\symbols\production /t &quot;$(BuildNumber)&quot; >> &quot;$(DropLocation)\$(BuildNumber)\SymbolServerPublish.log&quot;" />
    <Exec Condition="'$(SymbolsProduction)' != 'true'" Command="&quot;C:\Program Files\Debugging Tools for Windows (x86)\symstore.exe&quot; add /compress /f &quot;$(BranchLocalRoot)Build\%(ConfigurationToBuild.PlatformToBuild)\Release\*.pdb&quot; /r /o /s \\build\symbols\development /t &quot;$(BuildNumber)&quot; >> &quot;$(DropLocation)\$(BuildNumber)\SymbolServerPublish.log&quot;" />


    <BuildStep TeamFoundationServerUrl="$(TeamFoundationServerUrl)" BuildUri="$(BuildUri)" Id="$(CurrentBuildStepId)" Status="Succeeded" />
    
    <OnError ExecuteTargets="FailCurrentBuildStep" />

  </Target>
  
  <PropertyGroup>
    <SuccessMessage>Build finished successfuly.</SuccessMessage>
	<SkipLabel>true</SkipLabel>
  </PropertyGroup>
</Project>

```