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

   public static class NamedGroups_extensionMethods
   {
      public static void AddMember(this NamedGroup ng, Element el)
      {
         IntPtr exNamedGroup = ng.ElementRef;
         IntPtr exElemId = (IntPtr) el.MdlElementRef();
         IntPtr mdlRef = Bentley.Internal.MicroStation.ModelReference.Active.DgnModelRefIntPtr;
         mdlNamedGroup_addMember(exNamedGroup, exElemId, mdlRef, 0);
      }

      [DllImport("stdmdlbltin.dll")]
      static extern int mdlNamedGroup_addMember(IntPtr namedGroup, IntPtr elementId, IntPtr modelRef, int memberFlags);

      public static void WriteToFile(this NamedGroupElement nge)
      {
         //NamedGroup ng = nge.;
         //IntPtr exNamedGroup = ng.ElementRef;
         //mdlNamedGroup_writeToFile(exNamedGroup, 0);
      }

      [DllImport("stdmdlbltin.dll")]
      static extern int mdlNamedGroup_writeToFile(IntPtr namedGroup, int overwriteExisting); 



   }
}
