/*--------------------------------------------------------------------------------------+
|
|     $Source: /miscdev-root/miscdev/vault/VisualStudioWizards/MicroStationAddInWizard/Templates/1033/KeyinCommands.cs,v $
|    $RCSfile: KeyinCommands.cs,v $
|   $Revision: 1.1 $
|
|  $Copyright: (c) 2011 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using rm21Ustn.Corridor;
using rm21Ustn.rm2UprimitiveCommands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using BCOM = Bentley.Interop.MicroStationDGN;
using BMI = Bentley.MicroStation.InteropServices;

namespace rm21Ustn
{
   /// <summary>Class used for running key-ins.  The key-ins
   /// XML file commands.xml provides the class name and the method names.
   /// </summary>
   internal class KeyinCommands
   {
    
      public static void rm21UstnCommand (System.String unparsed)
      {
         
      }

      public static void rm21UstnIssueDPstationOffset(System.String unparsed)
      {
         rm2UhorizontalAlignments.issueDPstationOffset(unparsed);
      }

      public static void rm21UstnDisplayStationOffsetStartPrimitiveCommand(System.String unparsed)
      {
         rm2UhorizontalAlignments.displayStationOffsetStartPrimitive(unparsed);
      }

      public static void rm21UstnPromoteElementsToHA(System.String unparsed)
      {
         rm2UhorizontalAlignments.promoteSelectionSetToHA((unparsed.Split(' '))[0]);
      }

      public static void rm21UstnCreateNewCorridor(System.String unparsed)
      {
         //try 
         //{ 
            rm2Ucorridor.CreateNewCorridor(unparsed); 
         //}
         //catch (Exception e)
         //{
         //   if (e is ArgumentNullException)
         //   {
         //      Debugger.Break();
         //   }
         //   else if (e is NullReferenceException)
         //   {
         //      Debugger.Break();
         //   }
         //   else
         //   {
         //      Debugger.Break();
         //   }
         //   throw e;
         //}
      }

      public static void rm21UstnPlaceHAinteractive(System.String unparsed)
      {
         rm2UhorizontalAlignments.placeHAinteractively("stream");
      }

      public static void rm21UstnPlacementCommand(System.String unparsed)
      {
         rm21UstnPlacementCmd.StartPlacementCommand(rm21Ustn.MyAddin);
      }

      public static void rm21UstnLocateCommand(System.String unparsed)
      {
         int i = 0;
      }

   }  // End of KeyinCommands

}  // End of the namespace
