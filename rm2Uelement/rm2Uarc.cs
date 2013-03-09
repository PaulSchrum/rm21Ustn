using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bentley.Interop.MicroStationDGN;

namespace rm21Ustn.rm2Uelement
{
   public class rm2Uarc : rm2Upath
   {
      public rm2Uarc() : base() { }
      public rm2Uarc(Element el) : base(el) { }

      public ArcElement EL
      {
         get { return (ArcElement)el; }
         private set { }
      }
   }
}
