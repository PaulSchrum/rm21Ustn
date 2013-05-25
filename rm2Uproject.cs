using BMW=Bentley.MicroStation.WinForms;
using BMI=Bentley.MicroStation.InteropServices;
using BCOM=Bentley.Interop.MicroStationDGN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Bentley.Interop.MicroStationDGN;
using ptsCogo.Horizontal;
using rm21Ustn.Horizontal;
using Bentley.Internal.MicroStation;
using System.Runtime.InteropServices;
using rm21Ustn.Utilities;
using rm21Ustn.rm2Uelement;
using ptsCogo;
using System.Text.RegularExpressions;

namespace rm21Ustn
{
   public class rm2Uproject : rm2Ubase
   {
      public RM21Project rm21CoreProject {get; set;}
      internal DesignFile TypicalSectionDgnLib { get; set; }
      private readonly String defaultTypicalSectionLibraryName = "RM21 3D Typical Sections.dgnlib";
      internal Dictionary<Type, Element> XSlevelMap { get; set; }
      internal Dictionary<Type, Element> PlanLevelMap { get; set; }
      List<String> TypicalSectionList { get; set; }

      private List<rm2UbridgeHAs> horizAlignmentSoftBridge;
      private List<rm2UbridgeCorridors> corridorsBridge;

      public rm2Uproject()
      {
         Bentley.Interop.MicroStationDGN.ModelReference modelRef = BMI.Utilities.ComApp.ActiveModelReference;
         if (null == modelRef) throw new NullReferenceException();

         corridorsBridge = null;
         horizAlignmentSoftBridge = null;
         rm21CoreProject = null;
         LoadTypicalSectionLibrary();
         //CreateLevelMappingToRibbon();
         instantiateRM21project(modelRef);
      }

      private void instantiateRM21project(Bentley.Interop.MicroStationDGN.ModelReference modelRef)
      {
         rm21CoreProject = new RM21Project();
      }

      public rm21HorizontalAlignment getUnaffiliatedHorizontalAlignment(String name)
      {
         String nameToSearchFor = getNamedGroupNameForUnaffiliatedHA(name);
         foreach (var item in horizAlignmentSoftBridge)
         {
            if (item.Name == name)
               return item.rm21HA;
         }
         return null;
      }

      public void removeFromHAlist(String name)
      {
         String nameToSearchFor = getNamedGroupNameForUnaffiliatedHA(name);
         foreach (var item in horizAlignmentSoftBridge)
         {
            if (item.Name == name)
            {
               horizAlignmentSoftBridge.Remove(item);
               return;
            }
         }
      }

      private class rm2UbridgeCorridors 
      {
         public String Name { get; set; }
         public NamedGroupElement namedGroup { get; set; }
         public rm21Corridor theCorridor;
      }
      
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

      private NamedGroupElement CreateCorridor_NamedGroup(string name, List<rm2Upath> HAdgnItems)
      {
         String ngName = getNamedGroupNameForCorridor(name);
         return Create_NamedGroup(ngName, HAdgnItems);
      }

      private NamedGroupElement CreateHA_NamedGroup(string name, List<rm2Upath> selectionSet)
      {
         String ngName = getNamedGroupNameForUnaffiliatedHA(name);
         return Create_NamedGroup(ngName, selectionSet);
      }

      private NamedGroupElement Create_NamedGroup(String ngName, List<rm2Upath> HAelements)
      {
         IntPtr activeModelRef = Bentley.Internal.MicroStation.ModelReference.Active.DgnModelRefIntPtr;
         IntPtr nullElemTemplateRef = (IntPtr) 0;
         NamedGroupElement returnNG = null;

         returnNG = 
            Bentley.MicroStation.InteropServices.Utilities.ComApp.ActiveModelReference.AddNewNamedGroup(ngName);
         returnNG.Name = ngName;

         addAllElementsToNamedGroup(returnNG, HAelements);
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

      public String getNamedGroupNameForCorridor(String inputName)
      {
         return "RM21:" + inputName;
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

      internal void AddCorridor(rm21Corridor newCorridor, List<rm2Upath> governingAlignmentElements)
      {
         bool shouldCreateNewNG = true;
         if (null == corridorsBridge) corridorsBridge = new List<rm2UbridgeCorridors>();
         var newCordrBridge = new rm2UbridgeCorridors();
         newCordrBridge.Name = this.getNamedGroupNameForCorridor(newCorridor.Name);
         newCordrBridge.theCorridor = newCorridor;
         corridorsBridge.Add(newCordrBridge);

         var item = governingAlignmentElements.FirstOrDefault();
         {
            var containers = item.el.GetContainingNamedGroups();
            foreach (var ng in containers)
            {
               if (rm2UNamedGroup.isRM21UnaffiliatedHAnamedGroup(ng) == true)
               {
                  ng.Name = getNamedGroupNameForCorridor(newCorridor.Name);
                  ng.Rewrite();
                  newCordrBridge.namedGroup = ng;
                  shouldCreateNewNG = false;
                  break;
               }
            }
         }

         if (true == shouldCreateNewNG)
         {
            newCordrBridge.namedGroup = CreateCorridor_NamedGroup(newCorridor.Name, governingAlignmentElements);
         }

         // Todo: Technical Debt: What happens when the lane and ep lines are in a different dgn model
         //    from a referenced unaffiliated HA?  We have to handle this scenario.
      }

      private void LoadTypicalSectionLibrary()
      {
         // technical debt: look for file listed in enviromental variable
         // if found, load then return;

         // look for the file in the current directory
         String findString = app.ActiveDesignFile.Path + Path.DirectorySeparatorChar + defaultTypicalSectionLibraryName;
         if (true == File.Exists(findString))
         {
            app.ShowStatus("RM21: 3D Typical Section dgnlib found . . .");
            TypicalSectionDgnLib = app.OpenDesignFileForProgram(findString);
            GenerateLevelMapping();
            BuildTypicalSectionList();
            app.ShowStatus("RM21: 3D Typical Section dgnlib parsed");
         }

         // technical debt: 
         // look for the default library delivere in the RM21 installation directory
      }

      private void GenerateLevelMapping()
      {
         BCOM.ModelReference levelMappingModel = TypicalSectionDgnLib.Models["Level Mapping"];
         if (null == levelMappingModel) throw new Exception("Can't find Level Mapping model.");

         ElementScanCriteria textScanCriteria = new ElementScanCriteriaClass();
         textScanCriteria.ExcludeAllTypes();
         textScanCriteria.IncludeType(MsdElementType.Text);
         textScanCriteria.IncludeType(MsdElementType.TextNode);

         var elements = rm2Uelements.convertElEnumToRm2UList(levelMappingModel.Scan(textScanCriteria));
         List<rm2UtextBase> textElements = (from e in elements
                            where e is rm2UtextBase
                            select (e as rm2UtextBase)).ToList<rm2UtextBase>();

         Regex xsRegex = new Regex(".*\\[XS\\]");
         List<rm2UtextBase> xsMappingTextElements = new List<rm2UtextBase>();
         Regex planRegex = new Regex(".*\\[Plan\\]");
         List<rm2UtextBase> PlanMappingTextElements = new List<rm2UtextBase>();

         foreach (var el in textElements)
         {
            if (xsRegex.IsMatch(el.getTextValue()))
               xsMappingTextElements.Add(el);
            else if (planRegex.IsMatch(el.getTextValue()))
               PlanMappingTextElements.Add(el);
         }

      }

      private void BuildTypicalSectionList()
      {
         
      }

   }

   

}
