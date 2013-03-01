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

namespace rm21Ustn
{
   class rmUhorizontalAlignments
   {
      public static void promoteSelectionSetToHA()
      {
         BCOM.Application app = BMI.Utilities.ComApp;

         if (false == app.ActiveModelReference.AnyElementsSelected)
            return;

         if (false == selectedElementsValidForPromotion(app))
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

         CreateRm21HA(ssElements);
      }

      private static void CreateRm21HA(List<Element> selectedElements)
      {
         foreach (var element in selectedElements)
         {
            if (true == element.IsLineElement())
            {
               LineElement Line = (LineElement)element;
               ptsCogo.ptsPoint beginPt = rmUpoint.CreatePtsPoint(Line.StartPoint);
               ptsCogo.ptsPoint endPt = rmUpoint.CreatePtsPoint(Line.StartPoint);
               
            }
         }
      }

      private static bool selectedElementsValidForPromotion(BCOM.Application app)
      {
         return true;
      }

   }
}
