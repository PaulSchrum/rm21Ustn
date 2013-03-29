/*--------------------------------------------------------------------------------------+
|
|     $Source: /miscdev-root/miscdev/vault/VisualStudioWizards/MicroStationAddInWizard/Templates/1033/KeyinCommands.cs,v $
|    $RCSfile: KeyinCommands.cs,v $
|   $Revision: 1.1 $
|
|  $Copyright: (c) 2011 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
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
