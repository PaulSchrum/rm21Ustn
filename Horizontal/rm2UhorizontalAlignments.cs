using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMW = Bentley.MicroStation.WinForms;
using BMI = Bentley.MicroStation.InteropServices;
using BCOM = Bentley.Interop.MicroStationDGN;
using Bentley.Interop.MicroStationDGN;
using ptsCogo;
using rm21Ustn.Utilities;
using ptsCogo.Horizontal;
using rm21Ustn.Horizontal;

namespace rm21Ustn
{
   class rm2UhorizontalAlignments
   {
      public static void promoteSelectionSetToHA(String requestedName)
      {
         BCOM.Application app = BMI.Utilities.ComApp;

         if (false == app.ActiveModelReference.AnyElementsSelected)
            return;

         if (false == selectedElementsAreValidForPromotion(app))
            return;

         promoteSelectionSetToHorizAlignment(app);
      }

      private static void promoteSelectionSetToHorizAlignment(BCOM.Application app)
      {
         Element elem;

         if (null == app) throw new ArgumentNullException();

         BCOM.ElementEnumerator selectionSet = app.ActiveModelReference.GetSelectedElements();
         var ssElements = selectionSet.BuildArrayFromContents().ToList<Element>();
         if (ssElements.Count > 1) throw new NotImplementedException();

         rm21HorizontalAlignment newHA = CreateRm21HA(ssElements);
      }

      private static rm21HorizontalAlignment CreateRm21HA(List<Element> selectedElements)
      {
         List<IRM21fundamentalGeometry> funGeometryList = new List<IRM21fundamentalGeometry>();
         foreach (var element in selectedElements)
         {
            if (true == element.IsLineElement())
            {
               LineElement Line = (LineElement)element;
               ptsCogo.ptsPoint beginPt = rm2Upoint.CreatePtsPoint(Line.StartPoint);
               ptsCogo.ptsPoint endPt = rm2Upoint.CreatePtsPoint(Line.StartPoint);
               addToFunGeomList(new List<ptsPoint>() { beginPt, endPt },
                  expectedType.LineSegment, funGeometryList);
            }
         }
         
         if (null == funGeometryList) return null;

         return new rm21HorizontalAlignment(funGeometryList, null, null);
      }

      private static void addToFunGeomList(List<ptsPoint> pointList, expectedType geomType, List<IRM21fundamentalGeometry> funGeomList)
      {
         if (null == pointList || null == funGeomList) throw new NullReferenceException();

         var funGeom = new rm2UfunGeom();
         funGeom.ptList = pointList;
         funGeom.expType = geomType;

         funGeomList.Add(funGeom);
      }

      private static bool selectedElementsAreValidForPromotion(BCOM.Application app)
      {
         return true;
      }


   }

}
