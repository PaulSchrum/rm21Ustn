﻿using Bentley.Interop.MicroStationDGN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCOM = Bentley.Interop.MicroStationDGN;
using rm21Ustn.Utilities;

namespace rm21Ustn.rm2Uelement
{
   public class rm2UNamedGroup : rm2UgraphicalElement
   {
      public rm2UNamedGroup() : base() { }

      public BCOM.NamedGroupElement EL
      {
         get { return (BCOM.NamedGroupElement)el; }
         private set { }
      }

      public String Name 
      { 
         get 
         {
            BCOM.NamedGroupElement me = el.AsNamedGroupElement();
            return me.Name;
         } 
         
         private set { } 
      }

      public static List<rm2UNamedGroup> getAllNamedGroupsInModelRef
         (Bentley.Interop.MicroStationDGN.ModelReference modRef)
      {
         ElementScanCriteria scanCriteria = new ElementScanCriteriaClass();

         scanCriteria.ExcludeAllTypes();
         scanCriteria.IncludeType(MsdElementType.NamedGroupHeader);
         scanCriteria.IncludeType(MsdElementType.NamedGroupComponent);
         ElementEnumerator elEnum = modRef.Scan(scanCriteria);

         if (null == elEnum) return null;

         List<rm2UNamedGroup> returnList = new List<rm2UNamedGroup>();

         while (elEnum.MoveNext())
         {
            rm2UNamedGroup newEL = new rm2UNamedGroup();
            newEL.el = elEnum.Current;
            returnList.Add(newEL);
         }

         return returnList;
      }

      public static bool isRM21UnaffiliatedHAnamedGroup(NamedGroupElement nge)
      {
         String keyString = "RM21::";
         return nge.Name.StartsWith(keyString);
      }

      public static bool isRM21NamedGroup(NamedGroupElement nge)
      {
         String keyString = "RM21:";
         return nge.Name.StartsWith(keyString);
      }

   }
}
