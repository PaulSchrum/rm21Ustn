using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bentley.Interop.MicroStationDGN;

namespace rm21Ustn.rm2Uelement
{
   public static class rm2UelementFactory
   {
      public static rm2UgeometricElement CreateGeometricElement(Element inElement)
      {
         rm2UgeometricElement outElement = null;

         if (true == inElement.IsLineElement())
         {
            outElement = new rm2UlineSegment();
            outElement.el = inElement;
         }
         else if (true == inElement.IsArcElement())
         {
            outElement = new rm2Uarc();
            outElement.el = inElement;
         }
         else
         {
            outElement = new rm2UgeometricElementUndeveloped(inElement);
         }

         return outElement;
      }

      public static rm2UgraphicalElement CreateGraphicalElement(Element inElement)
      {
         rm2UgraphicalElement outElement = null;

         if (true == inElement.IsLineElement())
         {
            outElement = new rm2UlineSegment();
            outElement.el = inElement;
         }
         else if (true == inElement.IsArcElement())
         {
            outElement = new rm2Uarc();
            outElement.el = inElement;
         }
         else if (true == inElement.IsTextElement())
         {
            outElement = new rm2Utext();
            outElement.el = inElement;
         }
         else if (true == inElement.IsTextNodeElement())
         {
            outElement = new rm2UtextNode();
            outElement.el = inElement;
         }
         else
         {
            outElement = new rm2UgeometricElementUndeveloped(inElement);
         }

         return outElement;
      }
   }
}
