using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Common.Utils
{
    public static class CompareUtility
    {
        /// <summary>
        /// Returns true only if all of the objects in the params list
        /// are set to some representable as string value.
        /// Check is different from String.IsEmptyOrNull as regardless of the object type, it
        /// is just ToString representation of the object that is checked.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool AllValuesSetAsString<T>(params T[] args)
        {
            if (args == null)
                throw new ArgumentNullException(
                    "Parameter args of the CompareUtility.AllValuesSetAsString<T> can't be null!");

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == null || args[i].ToString().Length == 0)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Return true if only one of the two objects is null.
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool ObjectsAreExclusivelyNull(object obj1, object obj2)
        {
            if ((obj1 == null && obj2 != null) || (obj1 != null && obj2 == null))
                return false;
            return true;
        }
		public static bool AreEqualIdentifiers<IDType>(IIdentifierHolder<IDType> iHolder1, IIdentifierHolder<IDType> iHolder2)
        {
            //The bellow will return true even if child was deleted from the parent association.
            if ((iHolder1 == null && iHolder2 == null) || (iHolder1 != null && iHolder2 == null))
                return true;

            if (!ObjectsAreExclusivelyNull(iHolder1, iHolder2))
                return false;
            if (iHolder1 != null && iHolder2 != null && iHolder1.Id.Equals(iHolder2.Id))
                return true;
			return false;
      
        }
    }
}
