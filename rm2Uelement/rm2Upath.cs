using Bentley.Interop.MicroStationDGN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rm21Ustn.rm2Uelement
{
   public abstract class rm2Upath : rm2UgeometricElement
   {
      public rm2Upath() : base() { }

      public rm2Upath(Element el) : base(el) { }
   }
}
