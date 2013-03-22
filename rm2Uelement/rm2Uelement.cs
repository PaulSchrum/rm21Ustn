using Bentley.Interop.MicroStationDGN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCOM = Bentley.Interop.MicroStationDGN;

namespace rm21Ustn.rm2Uelement
{
   public abstract class rm2Uelement : rm2Ubase
   {
      public rm2Uelement() { }

      public rm2Uelement(Element newEL) : this()
      {
         el = newEL;
      }

      internal BCOM.Element el;
   }
}
