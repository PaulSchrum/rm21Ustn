﻿using BMW=Bentley.MicroStation.WinForms;
using BMI=Bentley.MicroStation.InteropServices;
using BCOM=Bentley.Interop.MicroStationDGN;
using rm21Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bentley.Interop.MicroStationDGN;
using ptsCogo.Horizontal;
using rm21Ustn.Horizontal;
using Bentley.Internal.MicroStation;
using System.Runtime.InteropServices;
using rm21Ustn.Utilities;
using rm21Ustn.rm2Uelement;

namespace rm21Ustn
{
   public class rm2Uproject
   {
      public RM21Project rm21CoreProject {get; set;}
      private List<rm2UbridgeHAs> horizAlignmentSoftBridge;
      private List<rm2UbridgeCorridors> corridorsBridge;

      public rm2Uproject()
      {
         Bentley.Interop.MicroStationDGN.ModelReference modelRef = BMI.Utilities.ComApp.ActiveModelReference;
         if (null == modelRef) throw new NullReferenceException();

         corridorsBridge = null;
         horizAlignmentSoftBridge = null;
         rm21CoreProject = null;

         instantiateRM21project(modelRef);
      }

      private void instantiateRM21project(Bentley.Interop.MicroStationDGN.ModelReference modelRef)
      {
         rm21CoreProject = new RM21Project();
      }

      private rm21HorizontalAlignment getUnaffiliatedHorizontalAlignment(String name)
      {
         String nameToSearchFor = "RM21::" + name;
         foreach (var item in horizAlignmentSoftBridge)
         {
            if (item.namedGroup.Name == nameToSearchFor)
               return item.rm21HA;
         }
         return null;
      }

      private class rm2UbridgeCorridors { }
      
      private class rm2UbridgeHAs
      {
         public String Name { get; set; }
         public NamedGroupElement namedGroup { get; set; }
         public rm21HorizontalAlignment rm21HA;
      }

      internal void AddUnaffiliatedHA(rm21HorizontalAlignment newHA, string name, List<rm2Upath> selectionSet)
      {
         validateNameForUnaffiliatedHA(name);  // may throw HorzontalAlignment_NameAlreadyExists.
         if (null == horizAlignmentSoftBridge) horizAlignmentSoftBridge = new List<rm2UbridgeHAs>();
         var haBridgeEntry = new rm2UbridgeHAs();
         haBridgeEntry.rm21HA = newHA;
         haBridgeEntry.Name = name;
         if(null != selectionSet)
            haBridgeEntry.namedGroup = CreateHA_NamedGroup(name, selectionSet);
         if (null == haBridgeEntry) return;  // failed.  dont know why.

         if (null == horizAlignmentSoftBridge) horizAlignmentSoftBridge = new List<rm2UbridgeHAs>();
         horizAlignmentSoftBridge.Add(haBridgeEntry);
      }

      private NamedGroupElement CreateHA_NamedGroup(string name, List<rm2Upath> selectionSet)
      {
         IntPtr activeModelRef = Bentley.Internal.MicroStation.ModelReference.Active.DgnModelRefIntPtr;
         IntPtr nullElemTemplateRef = (IntPtr) 0;
         NamedGroupElement returnNG = null;

         String ngName = getNamedGroupNameForUnaffiliatedHA(name);
         returnNG = 
            Bentley.MicroStation.InteropServices.Utilities.ComApp.ActiveModelReference.AddNewNamedGroup(ngName);
         returnNG.Name = ngName;

         addAllElementsToNamedGroup(returnNG, selectionSet);
         returnNG.Rewrite();

         return returnNG;
      }

      private void addAllElementsToNamedGroup(NamedGroupElement namedGroup, List<rm2Upath> selectionSet)
      {
         foreach (var item in selectionSet)
         {
            item.el.IsLocked = true;
            namedGroup.AddMember(item.el);
            item.el.Rewrite();
         }
      }

      public List<rm21HorizontalAlignment> getAllHorizontalAlignments()
      {
         return (from brItem in horizAlignmentSoftBridge
                       select brItem.rm21HA).ToList();
      }

      public String getNamedGroupNameForUnaffiliatedHA(String inputName)
      {
         return "RM21::" + inputName;
      }

      private void validateNameForUnaffiliatedHA(string inputName)
      {
         if (null == horizAlignmentSoftBridge) return;

         foreach (var entry in horizAlignmentSoftBridge)
            if (true == entry.Name.Equals(inputName))
               throw new HorzontalAlignment_NameAlreadyExists();

         return;
      }

      //public static 


   }


}
