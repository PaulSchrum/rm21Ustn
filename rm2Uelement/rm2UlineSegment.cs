using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCOM = Bentley.Interop.MicroStationDGN;

namespace rm21Ustn.rm2Uelement
{
   public class rm2UlineSegment : rm2Upath
   {
      public BCOM.LineElement EL
      {
         get { return (BCOM.LineElement)el; }
         private set { }
      }
   }
}
