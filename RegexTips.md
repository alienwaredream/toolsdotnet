# Replace with negative look ahead #
```
\[[\s\t]*assembly[\s\t]*\:[\s\t]*AssemblyVersion(Attribute){0,1}[\s\t]*\([\s\t]*".*"[\s\t]*\)[\s\t]*\](?![\s\t]*--[\s\t]*Skip[\s\t]*replace)
```

# Distinguish matches in the regex grep #
```
\d{1,2}\smatch[es]?.*?(?=\d{1,2}\smatch[es]?)
```

# Find/Replace source control elements in the project files #
```
<SccProjectName>.\s*?</SccProjectName>\r\n\s*<SccLocalPath>.*?</SccLocalPath>\r\n\s*<SccAuxPath>.*?</SccAuxPath>\r\n\s*<SccProvider>.*?</SccProvider>
```

# Groups #

```
^:61:.{10}(D|RC|RD)
```
```
:61:0903310331C6179
:61:0903310331D6179
:61:0903310331RC6179
:61:0903310331RD6179
:61:0903310331D6179

```

# Match the whole word only #

```

javascript:element.className.match(/\bdateFormat\b/)

```
http://answers.oreilly.com/topic/217-how-to-match-whole-words-with-a-regular-expression/

# date formats validation regexes #

```

dateTimeFormats = [
    {formatName: "mm/dd/yy", msgFormat: "mm/dd/yyyy", regex: /^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$/},
    {formatName: "mm/dd/y", msgFormat: "mm/dd/yy", regex: /^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.]\d\d$/},
    {formatName: "dd/mm/yy", msgFormat: "dd/mm/yyyy", regex: /^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$/},
	{formatName: "dd/mm/y", msgFormat: "dd/mm/yy", regex: /^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.]\d\d$/}
    ];

```

# Add custom tool to VS project items where not added yet #

```

    <None Include="DataAccess\ActionPlanning\ActionPlanningDAC.sp">
      <Generator>StoreProcAdapterGenerator</Generator>
      <LastGenOutput>ActionPlanningDAC.sp.cs</LastGenOutput>
    </None>
    <None Include="DataAccess\Administration\ActionPlanning\EngagementSiteSettingsDAC.sp" />
    <None Include="DataAccess\Administration\ActionPlanning\EscalationGroupDAC.sp" />

```
Match regex:
```

<None\sInclude="(.*)?\\(.*)?\.sp"\s?(/)>

```
Replace formula:
```
<None Include="$1\\$2.sp" ><Generator>StoreProcAdapterGenerator</Generator><LastGenOutput>$2.sp.cs</LastGenOutput></None>
```

And now we need to add DependentUpon for our generated files:

```

    <Compile Include="DataAccess\Presentation\DisplayOptionsDAC.sp.cs">
      <DependentUpon>DisplayOptionsDAC.sp</DependentUpon>
      <AutoGen>True</AutoGen>
      <DesignTime>True</DesignTime>
    </Compile>
    <Compile Include="DataAccess\ReportingManager\ReportDAC.sp.cs" />
    <Compile Include="DataAccess\ReportingManager\ReportGroupDAC.sp.cs" />

```

regex to match:
```
<Compile\sInclude="(.*)?\\(.*)?\.sp.cs"\s?(/)>
```
Replacement text:
```
<Compile Include="$1\\$2.sp.cs"><DependentUpon>$2.sp</DependentUpon><AutoGen>True</AutoGen><DesignTime>True</DesignTime></Compile>
```

# Regex to match uri path up to the last slash #
```
^(.*)(?=/)/
```
to match with first capture

http://localhost/Portal30Web/

in

http://localhost/Portal30Web/Portal30.aspx

## regex for sca perf parsing ##
```
^\[(.*?)\]\..*?\[(.*?)\]
```

## Check 8 fields zeparated by tab with digit as a last field ##
```
(.*?\t){8}\d
```

## Get comma separated list of cities ##
Source of cities list: http://www.mongabay.com/cities_pop_01.htm
From:
```
TOKYO, Japan	1	34,400,000	7,835	0.15	C / B	Yokohama [2] 	8,489,653	621	12,576,601	2,187	CDFC	2005-10-01	(61),(62),(63),(65)
JAKARTA, Indonesia	2	21,800,000	2,720	2.38	F / B	[3]	8,640,184	740	...	...	ESDF	2003-07-01	
```

Pick the first until the tab:
` ^([^\t].*?),\s?(.*?)\t `
With replace pattern:
`$1,$2`

Then from the list like:
```
TOKYO,Japan
New York (NY),United States
MANILA,Philippines
```

With pattern: `^(.*)?\r\n`
Getting the bash for list with replace pattern of:
`"$1" `

For the output to look like:

```
"TOKYO,Japan" "New York (NY),United States" "MANILA,Philippines" 
```

## List of emails ##
```
^(([A-Za-z0-9_\+\-]+\.)*[A-Za-z0-9_\+\-]+@([A-Za-z0-9]+\.)+([A-Za-z]{2,4})(\s*(;)\s*))*([A-Za-z0-9_\+\-]+\.)*[A-Za-z0-9_\+\-]+@([A-Za-z0-9]+\.)+([A-Za-z]{2,4})$
```