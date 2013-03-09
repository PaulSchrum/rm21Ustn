using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCOM = Bentley.Interop.MicroStationDGN;

namespace rm21Ustn.rm2Uelement
{
   public class rm2Uarc : rm2Upath
   {
      public BCOM.ArcElement EL
      {
         get { return (BCOM.ArcElement)el; }
         private set { }
      }
   }
}
