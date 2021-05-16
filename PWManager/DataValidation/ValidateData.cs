using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWManager.DataValidation
{
    public static class ValidateData
    {
        /// <summary>
        /// validates a string of text
        /// </summary>
        /// <param name="strToValidate">string to validate</param>
        /// <returns>true if the string is empty or null</returns>
        public static bool IsEmpty(string strToValidate)
        {
            return string.IsNullOrEmpty(strToValidate);
        }

        /// <summary>
        /// validates a string of text and checks the length
        /// </summary>
        /// <param name="strToValidate"></param>
        /// <returns>the length of the string</returns>
        public static int IsLengthValid(string strToValidate)
        {
            return strToValidate.Length;
        }

        /// <summary>
        /// Compares two strings of text and confirms they match
        /// </summary>
        /// <param name="strCompare">first string to compare</param>
        /// <param name="strCompareTo">second string to compare</param>
        /// <returns>true if the strings match</returns>
        public static bool IsEqual(string strCompare, string strCompareTo)
        {
            return strCompare.Equals(strCompareTo);
        }
    }
}
