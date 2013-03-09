using Bentley.Interop.MicroStationDGN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMW = Bentley.MicroStation.WinForms;
//using BWW = Bentley.Windowing.WindowManager;
using Bentley.Interop.MicroStationDGN;
using Bentley.MicroStation.Application;
using rm21Ustn.Utilities;
using System.Collections.Generic;
//  The Primary Interop Assembley (PIA) for MicroStation's COM object
//  model uses the namespace Bentley.Interop.MicroStationDGN
using BCOM = Bentley.Interop.MicroStationDGN;
//  The InteropServices namespace contains utilities to simplify using 
//  COM object model.
using BMI = Bentley.MicroStation.InteropServices;
using Bentley.Internal.MicroStation;
using System.Runtime.InteropServices;
using rm21Ustn.rm2Uelement;

namespace rm21Ustn.Utilities
{
   public static class rm2Uelements
   {
      public static List<rm2UElementTypeTuple> convertElEnumToTupleList(ElementEnumerator elEnum)
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

      public static List<rm2UgraphicalElement> convertElEnumToRm2UList(ElementEnumerator elEnum)
      {
         if (null == elEnum) return null;
         /* */
         List<rm2UgraphicalElement> returnList = null;

         while (elEnum.MoveNext())
         {
            Element el = elEnum.Current;
            rm2UgraphicalElement graphicEL = rm2UgraphicalElement.Factory(el);

            if (null != graphicEL)
            {
               if(returnList == null)
                  returnList = new List<rm2UgraphicalElement>();
               returnList.Add(graphicEL);
            }
         } /* */

         return returnList;
      }

   }

   public class rm2UElementTypeTuple
   {
      public Element element{get; set;}
      public MsdElementType ustnType { get; set; }
   }

      //[DllImport("stdmdlbltin.dll")]
      //static extern int mdlNamedGroup_addMember(IntPtr namedGroup, IntPtr elementId, IntPtr modelRef, int memberFlags);


}
