using Bentley.Interop.MicroStationDGN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCOM = Bentley.Interop.MicroStationDGN;
using BMI = Bentley.MicroStation.InteropServices;
using BMW = Bentley.MicroStation.WinForms;
using BWW = Bentley.Windowing.WindowManager;


namespace rm21Ustn.Utilities
{
   public static class rm2Uelements
   {
      public static List<rm2UElementTypeTuple> convertElEnumToList(ElementEnumerator elEnum)
      {
         if (null == elEnum) return null;
         /* */
         var returnList = new List<rm2UElementTypeTuple>();

         while (elEnum.MoveNext())
         {
            Element el = elEnum.Current;
            rm2UElementTypeTuple newEntry = new rm2UElementTypeTuple();
            newEntry.element = el;
            newEntry.ustnType = el.Type;
            returnList.Add(newEntry);
         } /* */

         return returnList;
      }
   }

   public class rm2UElementTypeTuple
   {
      public Element element{get; set;}
      public MsdElementType ustnType { get; set; }
   }
}
