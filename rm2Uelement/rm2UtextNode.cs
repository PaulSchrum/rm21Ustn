using Bentley.Interop.MicroStationDGN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rm21Ustn.rm2Uelement
{
   public class rm2UtextNode : rm2UtextBase
   {
      public rm2UtextNode() : base() { }

      public rm2UtextNode(Element el) : base(el) { }

      public TextNodeElement EL
      {
         get { return (TextNodeElement)el; }
         private set { }
      }
   }
}
