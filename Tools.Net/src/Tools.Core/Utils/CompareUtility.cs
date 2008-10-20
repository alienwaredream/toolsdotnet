using System;

namespace Tools.Core.Utils
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
        public static bool AllValuesSetAsString<T>(params T[] args) where T : class
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
    }
}