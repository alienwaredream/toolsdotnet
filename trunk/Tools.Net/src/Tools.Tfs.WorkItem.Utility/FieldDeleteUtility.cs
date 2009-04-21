using System;
using System.Collections.Generic;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace Tools.Tfs.WorkItem.Utility
{
    class FieldDeleteUtility
    {
        private static Dictionary<int, List<WorkItemType>> usageMap;
 

        private static Dictionary<int, List<WorkItemType>> UsageMap
        {
            get
            {
                if (usageMap == null)
                {
                    Dictionary<int, List<WorkItemType>> dictionary = new Dictionary<int, List<WorkItemType>>(Fields.Count);

                    ProjectCollection projects = Store.Projects;

                    for (int i = 0; i < Fields.Count; i++)
                    {
                        FieldDefinitionClass class2 = Fields[i];
                        if (!CoreFieldsMap.ContainsKey(class2.ID))
                        {
                            List<WorkItemType> list = new List<WorkItemType>();
                            dictionary[class2.ID] = list;
                            for (int j = 0; j < projects.Count; j++)
                            {
                                Project project = projects[j];
                                for (int k = 0; k < project.WorkItemTypes.Count; k++)
                                {
                                    WorkItemType item = project.WorkItemTypes[k];
                                    if (item.FieldDefinitions.Contains(class2.ID))
                                    {
                                        list.Add(item);
                                    }
                                }
                            }
                        }
                    }
                    usageMap = dictionary;
                }
                return usageMap;
            }
        }

        private static void DeleteField(string refName, string tfsUrl)
        {

            if (!ValidationMethods.IsValidReferenceFieldName(refName))
            {
                throw new ArgumentException(
                    String.Format("Invalid reference name [{0}]", refName));
            }

            FieldDefinitionClass fieldByName = GetFieldByName(nextArgument);

            if (IsCoreField(fieldByName.ID))
            {
                throw new ApplicationException(ResourceStrings.Format("ErrorCannotDeleteCoreField", new object[] { fieldByName.ReferenceName }));
            }
            List<WorkItemType> list = UsageMap[fieldByName.ID];
            if (list.Count > 0)
            {
                throw new ApplicationException(ResourceStrings.Format("ErrorCannotDeleteFieldInUse", new object[] { fieldByName.ReferenceName, GetFieldUsages(fieldByName.ID) }));
            }
            if (Confirm("ConfirmDelete", new object[] { fieldByName.ReferenceName }))
            {
                UpdatePackage batch = new UpdatePackage(Store);
                batch.DeleteField(fieldByName.ID);
                Update(batch);
                if (fieldByName.IsReportable)
                {
                    Console.WriteLine();
                    Console.WriteLine(string.Format(CultureInfo.InvariantCulture, ResourceStrings.Get("DeletedReportableFieldWarning"), new object[] { fieldByName.ReferenceName }));
                }
            }
        }
    }
}
