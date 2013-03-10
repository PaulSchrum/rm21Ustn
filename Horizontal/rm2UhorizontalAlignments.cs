﻿using System;
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
using rm21Ustn.rm2Uelement;

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

         if (true == doesHAofThisNameExist(requestedName))
            throw new HorzontalAlignment_NameAlreadyExists();

         promoteSelectionSetToHorizAlignment(app, requestedName);
      }

      private static bool doesHAofThisNameExist(string requestedName)
      {
         return false;
      }

      private static void promoteSelectionSetToHorizAlignment(BCOM.Application app, String name)
      {
         Element elem;

         if (null == app) throw new ArgumentNullException();

         BCOM.ElementEnumerator selectionSet = app.ActiveModelReference.GetSelectedElements();
         var ssElements = selectionSet.BuildArrayFromContents().ToList<Element>();
         if (ssElements.Count > 1) throw new NotImplementedException();

         rm21HorizontalAlignment newHA = CreateRm21HA(ssElements, name);

         if (null == rm21Ustn.MyAddin.rm21UstnProject) rm21Ustn.MyAddin.rm21UstnProject = new rm2Uproject();

         // throws HorzontalAlignment_NameAlreadyExists
         rm21Ustn.MyAddin.rm21UstnProject.AddUnaffiliatedHA(newHA, name, ssElements);
      }

      //private static rm2UbridgeHAs rm2UbridgeHAs()
      //{
      //   throw new NotImplementedException();
      //}

      private static rm21HorizontalAlignment CreateRm21HA(List<Element> selectedElements, String name)
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

         return new rm21HorizontalAlignment(funGeometryList, name, null);
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
         var selectedElements = rm2Uelements.convertElEnumToRm2UList(app.ActiveModelReference.GetSelectedElements());

         bool allowedElements = elementsAreAllOfAllowedType(selectedElements);
         bool notAlreadyRM21 = elementsAreNotAlreadyRM21(selectedElements);

         bool returnBool = allowedElements && notAlreadyRM21;

         return returnBool;
      }

      private static bool elementsAreAllOfAllowedType(List<rm2UgraphicalElement> selectedElements)
      {
         Type elType = null;
         Type lineSegType = null;
         Type arcType = null;

         bool isLine = false;
         bool isArc = false;
         bool returnBool = false;

         foreach (var el in selectedElements)
         {
            elType = el.GetType();
            lineSegType = Type.GetType("rm21Ustn.rm2Uelement.rm2UlineSegment");
            arcType = Type.GetType("rm21Ustn.rm2Uelement.rm2Uarc");
            isLine = elType == lineSegType;
            isArc = elType == arcType;
            returnBool = returnBool || (isLine || isArc);
         }

         return returnBool;

      }

      private static bool elementsAreNotAlreadyRM21(List<rm2UgraphicalElement> selectedElements)
      {
         return true;
      }



   }

}
