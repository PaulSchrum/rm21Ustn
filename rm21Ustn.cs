/*--------------------------------------------------------------------------------------+
|
|     $Source: /miscdev-root/miscdev/vault/VisualStudioWizards/MicroStationAddInWizard/Templates/1033/root.cs,v $
|    $RCSfile: root.cs,v $
|   $Revision: 1.1 $
|       $Date: 2011/08/09 13:12:41 $
|
|  $Copyright: (c) 2011 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

//  Provides access to adapters needed to use forms and controls
//  from System.Windows.Forms in MicroStation

//  Provides access to classes used to make forms dockable in MicroStation

using Bentley.Interop.MicroStationDGN;
using Bentley.MicroStation.Application;
using ptsCogo.Horizontal;
using rm21Ustn.Utilities;
using System;
using System.Collections.Generic;
//  The Primary Interop Assembley (PIA) for MicroStation's COM object
//  model uses the namespace Bentley.Interop.MicroStationDGN
using BCOM = Bentley.Interop.MicroStationDGN;
//  The InteropServices namespace contains utilities to simplify using 
//  COM object model.
using BMI = Bentley.MicroStation.InteropServices;

namespace rm21Ustn
{
   /// <summary>When loading an AddIn MicroStation looks for a class
   /// derived from AddIn.</summary>

   [Bentley.MicroStation.AddInAttribute(MdlTaskID="rm21Ustn", KeyinTree="rm21Ustn.commands.xml")]
   internal sealed class rm21Ustn : Bentley.MicroStation.AddIn
   {
      private static rm21Ustn s_addin = null;
      private static BCOM.Application  s_comApp = null;
      public rm2Uproject rm21UstnProject { get; internal set; }
      public ModelReference activeModelRef;

      /// <summary>Private constructor required for all AddIn classes derived from 
      /// Bentley.MicroStation.AddIn.</summary>
      private rm21Ustn(System.IntPtr mdlDesc) : base (mdlDesc)
      {   s_addin = this;    }

      /// <summary>The AddIn loader creates an instance of a class 
      /// derived from Bentley.MicroStation.AddIn and invokes Run.
      /// </summary>
      protected override int Run(System.String[] commandLine)
      {
         s_comApp = BMI.Utilities.ComApp;
         activeModelRef = s_comApp.ActiveModelReference;

         //  Register reload and unload events, and show the form
         this.ReloadEvent += new ReloadEventHandler(rm21Ustn_ReloadEvent);
         this.UnloadedEvent += new UnloadedEventHandler(rm21Ustn_UnloadedEvent);

         //rm21UstnProject = new rm2Uproject();
         if (null == rm21Ustn.MyAddin.rm21UstnProject) rm21Ustn.MyAddin.rm21UstnProject = new rm2Uproject();
         rm2UDeserializeProjectFromUstn();

         return 0;
      }

      private void rm2UDeserializeProjectFromUstn()
      {
         List<String> nameList = new List<string>();
         var namedGroups = getAllRM21NamedGroups(s_comApp.ActiveModelReference, nameList);

         foreach(var namedGroup in namedGroups)
         {
            var parsed = namedGroup.Name.Split(':');
            if (parsed[0] != "RM21") continue;
            if (parsed[1].Length == 0)
            {
               addUnafilliatedHAtoProject(namedGroup, parsed[2]);
            }
            // to later do: handle corridors
         }
      }

      private void addUnafilliatedHAtoProject(NamedGroupElement namedGroup, string HAname)
      {
         rm21HorizontalAlignment newHA = rm2UhorizontalAlignments.CreateRm21HA(
            rm2Uelements.returnOnlyPathElements(rm2Uelements.convertElEnumToRm2UList(namedGroup.GetElements(true))), 
            HAname);

         if (null == rm21Ustn.MyAddin.rm21UstnProject) rm21Ustn.MyAddin.rm21UstnProject = new rm2Uproject();

         // throws HorzontalAlignment_NameAlreadyExists
         rm21UstnProject.AddUnaffiliatedHA(newHA, HAname, null);
      }

      /// <summary>Static property that the rest of the application uses 
      /// get the reference to the AddIn.</summary>
      internal static rm21Ustn MyAddin
      {      get { return s_addin; }      }

      /// <summary>Static property that the rest of the application uses to
      /// get the reference to the COM object model's main application object.</summary>
      internal static BCOM.Application ComApp
      {      get { return s_comApp; }      }

      /// <summary>Handles MDL LOAD requests after the application has been loaded.
      /// </summary>
      private void rm21Ustn_ReloadEvent(Bentley.MicroStation.AddIn sender, ReloadEventArgs eventArgs)
      { //  mainForm.ShowForm (this);
         int i;
         i = 0;
      }

      private void rm21Ustn_UnloadedEvent(Bentley.MicroStation.AddIn sender, UnloadedEventArgs eventArgs)
      {
      }

      protected override void OnUnloading(UnloadingEventArgs eventArgs)
      {
         //  CellControl.CloseForm ();
         base.OnUnloading (eventArgs);
         eventArgs.ResponseCode = UnloadingEventArgs.Response.UnloadProceed;
      }

      private List<BCOM.NamedGroupElement> getAllRM21NamedGroups(BCOM.ModelReference modelReference, List<String> nameList)
      {
         var returnList = getAllRM21NamedGroupPerModel(modelReference);
         var otherList = new List<BCOM.NamedGroupElement>();


         foreach (Attachment refFile in modelReference.Attachments)
         {
            var refAsModelRef = ComApp.MdlGetModelReferenceFromModelRefP(refFile.MdlModelRefP());
            otherList.AddRange(getAllRM21NamedGroups(refAsModelRef, nameList));
         }

         returnList.AddRange(otherList);
         return returnList;
      }

      private List<BCOM.NamedGroupElement> getAllRM21NamedGroupPerModel(BCOM.ModelReference modelReference)
      {
         List<BCOM.NamedGroupElement> returnList = new List<BCOM.NamedGroupElement>();
         ElementScanCriteria scanCriteria = new ElementScanCriteriaClass();
         scanCriteria.ExcludeAllTypes();
         //scanCriteria_IncludeAllGraphicalTypesOfInterest(scanCriteria);
         scanCriteria.IncludeType(MsdElementType.NamedGroupHeader);
         scanCriteria.IncludeType(MsdElementType.NamedGroupComponent);
         
         ElementEnumerator elEnum = modelReference.Scan(scanCriteria);
         List<Utilities.rm2UElementTypeTuple> elList = rm2Uelements.convertElEnumToTupleList(elEnum);

         foreach (var el in elList)
         {
            NamedGroupElement nge = el.element.AsNamedGroupElement();
            if("RM21:" == nge.Name.Substring(0,5))
               returnList.Add(nge);
         }

         return returnList;
      }

      private void scanCriteria_IncludeAllGraphicalTypesOfInterest(ElementScanCriteria scanCriteria)
      {
         scanCriteria.IncludeType(MsdElementType.Arc);
         scanCriteria.IncludeType(MsdElementType.Line);
         scanCriteria.IncludeType(MsdElementType.BsplineCurve);
         scanCriteria.IncludeType(MsdElementType.Conic);
         scanCriteria.IncludeType(MsdElementType.Curve);
      }

      

   }
}
