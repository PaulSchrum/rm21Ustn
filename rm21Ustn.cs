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
using System;

//  Provides access to adapters needed to use forms and controls
//  from System.Windows.Forms in MicroStation
using BMW=Bentley.MicroStation.WinForms;

//  Provides access to classes used to make forms dockable in MicroStation
using BWW=Bentley.Windowing.WindowManager;

//  The Primary Interop Assembley (PIA) for MicroStation's COM object
//  model uses the namespace Bentley.Interop.MicroStationDGN
using BCOM=Bentley.Interop.MicroStationDGN;

//  The InteropServices namespace contains utilities to simplify using 
//  COM object model.
using BMI=Bentley.MicroStation.InteropServices;
using Bentley.Interop.MicroStationDGN;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using rm21Ustn.Utilities;
using Bentley.MicroStation.Application;

namespace rm21Ustn
{
   /// <summary>When loading an AddIn MicroStation looks for a class
   /// derived from AddIn.</summary>

   [Bentley.MicroStation.AddInAttribute(MdlTaskID="rm21Ustn", KeyinTree="rm21Ustn.commands.xml")]
   internal sealed class rm21Ustn : Bentley.MicroStation.AddIn
   {
      private static rm21Ustn s_addin = null;
      private static BCOM.Application  s_comApp = null;
      public rm2Uproject rm21UstnProject = null;

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

         rm21UstnProject = BuildProjectFromDgnFile(s_comApp);

         //  Register reload and unload events, and show the form
         this.ReloadEvent += new ReloadEventHandler(rm21Ustn_ReloadEvent);
         this.UnloadedEvent += new UnloadedEventHandler(rm21Ustn_UnloadedEvent);
         

         return 0;
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
      }



      private rm2Uproject BuildProjectFromDgnFile(BCOM.Application comApp)
      {
         //comApp.ActiveModelReference;
         Bentley.Internal.MicroStation.NamedGroup topRM21NG = getTopLevelRM21NG(comApp.ActiveModelReference);

         return null;
      }

      private Bentley.Internal.MicroStation.NamedGroup getTopLevelRM21NG(BCOM.ModelReference modelReference)
      {
         ElementScanCriteria scanCriteria = new ElementScanCriteriaClass();
         //scanCriteria.ExcludeNonGraphical();
         scanCriteria.ExcludeAllTypes();
         scanCriteria_IncludeAllGraphicalTypesOfInterest(scanCriteria);
         scanCriteria.IncludeType(MsdElementType.NamedGroupHeader);
         //scanCriteria.IncludeType(MsdElementType.NamedGroupComponent);
         

         System.Console.WriteLine("Test");
         ElementEnumerator elEnum = modelReference.Scan(scanCriteria);

         List<Utilities.rm2UElementTypeTuple> elList = rm2Uelements.convertElEnumToList(elEnum);

         foreach (var el in elList)
            MessageCenter.StatusMessage = el.element.Type.ToString();
         //Note: This partial development is suspended for the time being.
         //   return here when ready to proceed with EU2, open a dgn file with rm21 peristance.
         return null;
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
