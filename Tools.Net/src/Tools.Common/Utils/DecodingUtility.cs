using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Reflection;
using System.ComponentModel;


namespace Tools.Common.Utils
{
    /// <summary>
    /// Decodes place holders with actual values
    /// </summary>
    public static class DecodingUtility
    {
        public static string ParseToString(IDictionary scriptParams, string parseString)
        {
            if (scriptParams == null) return parseString;

            string tparseString = parseString;
            foreach (object key in scriptParams.Keys)
            {
                if (scriptParams[key] != null)
                    tparseString = tparseString.Replace("{" + key.ToString() + "}", scriptParams[key].ToString());
            }
            return tparseString;
        }
        public static string ParseToString(object scriptParams, string parseString)
        {
            return ParseToString(ReflectionUtility.GetObjectPropertiesDictionary(scriptParams), 
                parseString);
        }
    }
}