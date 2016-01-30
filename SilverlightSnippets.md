## On Virtual scroll for RadGridView ##

in works for Q3 2010:

http://www.telerik.com/community/forums/silverlight/gridview/virtual-scrolling-in-silverlight-radgridview.aspx

## On ItemsControl ##

```
		<ItemsControl ItemsSource="{Binding Path=CurrentPath}" >
	<ItemsControl.Template>
	<ControlTemplate>
		
	</ControlTemplate>
	</ItemsControl.Template>

	<ItemsControl.ItemsPanel>
		<ItemsPanelTemplate>
			<StackPanel Orientation="Horizontal"/>                                           
		</ItemsPanelTemplate>  
	</ItemsControl.ItemsPanel>
<ItemsControl.ItemTemplate>
<DataTemplate>
			                            	<StackPanel Orientation="Horizontal">
			                            		<Button Background="White" BorderBrush="{x:Null}">
			                            			<Button.Content>
			                            				 <TextBlock Text="{Binding Title}" VerticalAlignment="Center" TextWrapping="Wrap" MaxWidth="200"/>                 			
													</Button.Content>                   		
												</Button>
											<Image Width="8" Height="8" VerticalAlignment="Center"  Source="../Images/arrow_state_blue_right.png" />
										</StackPanel>
</DataTemplate>
</ItemsControl.ItemTemplate>
</ItemsControl>
```

http://compilewith.net/2008/03/wpf-listbox-itemspaneltemplate-and.html

## Binding to the parent (or any) element data context ##
Happens when for example binding items in the list but command should be executed in the view viewmodel. In the sample bellow HelpViewControl corresponds to x:Name="HelpViewControl" of the user control itself.
```

Command="{Binding ElementName=HelpViewControl, Path=DataContext.DownloadResourceCommand}" CommandParameter="{Binding DownloadQuery}"

```