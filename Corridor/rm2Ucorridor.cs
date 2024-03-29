﻿using ptsCogo.Angle;
using ptsCogo.Horizontal;
//using rm21Core;
using rm21Ustn.rm2Uelement;
using rm21Ustn.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using ptsCogo;
using ptsCogo.CorridorTypes;
using ptsCogo.Ribbons;
using rm21Ustn.DrawAndPersist;

namespace rm21Ustn.Corridor
{
   public class rm2Ucorridor : rm2Ubase
   {
      

      internal static void CreateNewCorridor(string unparsed)
      {
         IPersistantDrawer drawAndPersist = new DrawAndPersist.DrawAndPersist();
         bool shouldRemoveHAfromUnaffiliatedList = false;
         String corridorName = "";
         String typicalSectionName1 = "";
         String typicalSectionName2 = "";
         String[] parsed = unparsed.Split(' ');
         if (parsed.Length >= 1)
            corridorName = parsed[0];
         if (parsed.Length >= 2)
            typicalSectionName1 = parsed[1];
         if (parsed.Length >= 3)
            typicalSectionName2 = parsed[2];

         var theseHAs = new Dictionary<rm21HorizontalAlignment, List<rm2Upath>>();
         //rm21HorizontalAlignment theHA = null;
         List<rm2Uelement.rm2Upath> selectedPathElements = rm2Uelements.getSelectedPathElements();
         if (null == selectedPathElements)
         {
            rm21HorizontalAlignment anHA = proj.getUnaffiliatedHorizontalAlignment(corridorName);
            if (null == anHA) return;
            List<rm2Upath> constiuentElements = rm2Upath.getDGNelementsByHAname(corridorName);
            if (null == constiuentElements) return;
            theseHAs.Add(anHA, constiuentElements);
            shouldRemoveHAfromUnaffiliatedList = true;
         }
         else
         {
            // TODO: CreateNewCorridor technical debt:
            // case: selected elements are not HA: promote to HA and proceed
            // case: selected elements are one HA:
            //             Use as Governing Alignment (mend namedGroup) and name the corridor with corridorName
            // case: selected elements are multiple HAs: 
            //             Use as Governing Alignments foreach
            //                and treat parsed[0] as typicalSection1Name
         }

         // TODO: CreateNewCorridor technical debt:
         // - develop a real Typical Section class
         // - allow partial station ranges such that previously extruded typicals
         //       do not get overwritten outside the station range of the new typical section

         foreach (var theHA in theseHAs)
         {
            var newCorridor = new rm21OpenChannelCorridor(corridorName);
            // TODO: CreateNewCorridor technical debt: handle case where this name already exists
            newCorridor.GoverningAlignment = theHA.Key;
            //if (true == shouldRemoveHAfromUnaffiliatedList)
               //proj.removeFromHAlist(corridorName);

            //proj.AddCorridor(newCorridor, theHA.Value);

            // TODO: CreateNewCorridor technical debt: handle typicalSectionName2
            extrudeTypicalSections(newCorridor, typicalSectionName1);

            newCorridor.PersistantDraw(drawAndPersist);
         }

      }

      internal static void extrudeTypicalSections(rm21Corridor newCorridor, 
         String typicalSections)
      {
         if (null == newCorridor) throw new NullReferenceException("newCorridor");
         if (null == newCorridor.GoverningAlignment) throw new NullReferenceException("newCorridor.GoverningAlignment");

         tempTestingBuildSimpleCorridor(newCorridor);
      }

      private static void tempTestingBuildSimpleCorridor(rm21Corridor newCorridor)
      {
         PGLGrouping pglGrLT = new PGLGrouping(-1);
         PGLGrouping pglGrRT = new PGLGrouping(1);

         newCorridor.addPGLgrouping(pglGrLT);
         newCorridor.addPGLgrouping(pglGrRT);

         pglGrLT.addOutsideRibbon(new RoadwayLane(pglGrLT, 12.0, new Slope(-0.02)));
         pglGrLT.addOutsideRibbon(new RoadwayLane(pglGrLT, 12.0, new Slope(-0.02)));
         pglGrLT.addOutsideRibbon(new Shoulder(pglGrLT, 8.0, new Slope(-0.08)));

         pglGrRT.addOutsideRibbon(new RoadwayLane(pglGrRT, 12.0, new Slope(-0.02)));
         pglGrRT.addOutsideRibbon(new RoadwayLane(pglGrRT, 12.0, new Slope(-0.02)));
         pglGrRT.addOutsideRibbon(new Shoulder(pglGrRT, 8.0, new Slope(-0.08)));

      }
   }
}

// TODO: rm2Ucorridor technical debt:
//    method to delete a corridor
//    method to drop a corridor
