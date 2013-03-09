using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCOM = Bentley.Interop.MicroStationDGN;

namespace rm21Ustn.rm2Uelement
{
   public static class rm2UelementFactory
   {
      public static rm2UgeometricElement Create(BCOM.Element inElement)
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

         return outElement;
      }
   }
}
