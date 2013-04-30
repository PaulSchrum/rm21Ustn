using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rm21Ustn.Utilities
{
   public static class stringExtensionMethods
   {
      public static bool StartsWith(this string s, String compareTo)
      {
         int lngth = compareTo.Length;
         return s.Substring(0, lngth) == compareTo;
      }
   }
}
