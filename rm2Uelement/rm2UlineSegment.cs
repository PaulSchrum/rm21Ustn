using Bentley.Interop.MicroStationDGN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using BCOM = Bentley.Interop.MicroStationDGN;

namespace rm21Ustn.rm2Uelement
{
   public class rm2UlineSegment : rm2Upath
   {
      public rm2UlineSegment() : base() { }

      public rm2UlineSegment(Element el) : base(el) { }

      public LineElement EL
      {
         get { return (LineElement)el; }
         private set { }
      }
   }
}
