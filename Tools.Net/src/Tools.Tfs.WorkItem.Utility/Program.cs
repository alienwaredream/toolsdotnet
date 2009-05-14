using System;

namespace Tools.Tfs.WorkItem.Utility
{
    class Program
    {
        static void Main(string[] args)
        {
            FieldsDeleteUtility util = new FieldsDeleteUtility();

            util.DeleteField("test", "test");


        }
    }
}