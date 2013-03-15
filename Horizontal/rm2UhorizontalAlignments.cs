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
         if (null == app) throw new ArgumentNullException();

         //BCOM.ElementEnumerator selectionSet = app.ActiveModelReference.GetSelectedElements();
         //var ssElements = selectionSet.BuildArrayFromContents().ToList<Element>();

         var selectionSet = rm2Uelements.returnOnlyPathElements(
            rm2Uelements.convertElEnumToRm2UList(app.ActiveModelReference.GetSelectedElements()));
         if (selectionSet.Count > 1) throw new NotImplementedException();
         rm21HorizontalAlignment newHA = CreateRm21HA(selectionSet, name);

         if (null == rm21Ustn.MyAddin.rm21UstnProject) rm21Ustn.MyAddin.rm21UstnProject = new rm2Uproject();

         // throws HorzontalAlignment_NameAlreadyExists
         rm21Ustn.MyAddin.rm21UstnProject.AddUnaffiliatedHA(newHA, name, selectionSet);
      }

      private static rm21HorizontalAlignment CreateRm21HA(List<rm2Upath> selectionSet, string name)
      {
         List<IRM21fundamentalGeometry> funGeometryList = new List<IRM21fundamentalGeometry>();
         foreach (var element in selectionSet)
         {
            rm2UfunGeom funGeomItem = element.getAsFundamentalGeometry();
            if (null != funGeomItem)
               funGeometryList.Add(funGeomItem);
         }
         
         if (null == funGeometryList) return null;
         if (0 == funGeometryList.Count) return null;

         return new rm21HorizontalAlignment(funGeometryList, name, null);
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
