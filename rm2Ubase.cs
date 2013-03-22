using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rm21Ustn
{
   public class rm2Ubase
   {
      internal static rm21Ustn myAdIn
      {
         get { return rm21Ustn.MyAddin; }
         private set { }
      }

      internal static rm2Uproject proj
      {
         get { return rm21Ustn.MyAddin.rm21UstnProject; }
         private set { }
      }

   }
}
