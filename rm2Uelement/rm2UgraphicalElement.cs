using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bentley.Interop.MicroStationDGN;

namespace rm21Ustn.rm2Uelement
{
   public abstract class rm2UgraphicalElement : rm2Uelement
   {
      public rm2UgraphicalElement() : base() { }

      public rm2UgraphicalElement(Element el) : base(el) { }

      internal static rm2UgraphicalElement Factory(Element el)
      {
         if (false == el.IsGraphical) return null;

         if (true == el.IsLineElement())
            return new rm2UlineSegment(el);

         if (true == el.IsArcElement())
            return new rm2Uarc(el);

         return null;
      }
   }
}
