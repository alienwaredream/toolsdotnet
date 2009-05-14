using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tools.Tfs.WorkItem.Utility
{
    class FieldsDeleteUtility
    {
        internal void DeleteField(string refName, string tfsUrl, string[] args)
        {
             
            Type witFieldType = Type.GetType("Microsoft.TeamFoundation.WorkItemTracking.Client.Provision.CommandLineTools.WITFields, witfields");

            MethodInfo deleteMethod = witFieldType.GetMethod("");
        }

    }
}
