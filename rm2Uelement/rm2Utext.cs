using Bentley.Interop.MicroStationDGN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rm21Ustn.rm2Uelement
{
   public class rm2Utext : rm2UtextBase
   {
      public rm2Utext() : base() { }

      public rm2Utext(Element el) : base(el) { }

      public TextElement EL
      {
         get { return (TextElement)el; }
         private set { }
      }
   }
}
