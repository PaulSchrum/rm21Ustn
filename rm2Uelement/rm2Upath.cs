using Bentley.Interop.MicroStationDGN;
using rm21Ustn.Horizontal;
using rm21Ustn.Utilities;
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

      internal abstract rm2UfunGeom getAsFundamentalGeometry();

      public static List<rm2Upath> getDGNelementsByHAname(string nameToSeek)
      {
         String searchName = proj.getNamedGroupNameForUnaffiliatedHA(nameToSeek);
         NamedGroupElement nge = app.ActiveModelReference.GetNamedGroup(searchName);
         List<rm2Upath> pathList = null;
         if (null != nge)
         {
            pathList = rm2Uelements.convertElEnumToPathList(nge.GetElements(true));
         }
         else
         {
            searchName = proj.getNamedGroupNameForCorridor(nameToSeek);
            nge = app.ActiveModelReference.GetNamedGroup(searchName);
            if (null != nge)
            {
               pathList = rm2Uelements.convertElEnumToPathList(nge.GetElements(true));
            }
         }

         return pathList;
      }


      public bool IsSingleHorizontalAlignment()
      {
         bool returnBool = true;



         return returnBool;
      }

   }
}
