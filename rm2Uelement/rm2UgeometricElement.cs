using Bentley.Interop.MicroStationDGN;
using rm21Ustn.Horizontal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rm21Ustn.rm2Uelement
{
   public abstract class rm2UgeometricElement : rm2UgraphicalElement
   {
      public rm2UgeometricElement() : base() { }

      public rm2UgeometricElement(Element el) : base(el) { }

   }
}
