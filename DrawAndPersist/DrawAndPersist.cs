using Bentley.Interop.MicroStationDGN;
using ptsCogo;
using ptsCogo.coordinates.CurvilinearCoordinates;
using ptsCogo.Horizontal;
using rm21Ustn.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rm21Ustn.DrawAndPersist
{
   class DrawAndPersist : rm2Ubase, IPersistantDrawer
   {

      // todo: DrawAndPersist technical debt: Does not yet assign RM21 named group stuff.
      // todo: DrawAndPersist technical debt: Does not yet place cross slope elements.

      public rm21Feature featureSymbology { get; protected set; }

      public void setCaddSymbologyForRibbon(rm21Feature feature)
      {
         featureSymbology = feature;
      }

      public void PlaceArc(rm21HorArc arc, 
         ptsPoint startPoint, StationOffsetElevation startSOE, 
         ptsPoint endPoint, StationOffsetElevation endSOE)
      {
         Point3d uPtStart = rm2Upoint.CreateUstnPoint(startPoint);
         Point3d uPtEnd = rm2Upoint.CreateUstnPoint(endPoint);
         Point3d uCenterPt = rm2Upoint.CreateUstnPoint(arc.ArcCenterPt);

         ArcElement newArc = null;
         if (arc.Deflection.getAsRadians() < 0.0)
         {
             newArc =
               app.CreateArcElement1(null, ref uPtStart, ref uCenterPt, ref uPtEnd);
         }
         else
         {
            newArc =
               app.CreateArcElement1(null, ref uPtEnd, ref uCenterPt, ref uPtStart);
            newArc.Reverse();
         }

         processNewElement(newArc, arc);
      }
      
      public void PlaceLine(rm21HorLineSegment lineSegment,
         ptsPoint startPoint, StationOffsetElevation startSOE,
         ptsPoint endPoint, StationOffsetElevation endSOE)
      {
         Point3d uPtStart = rm2Upoint.CreateUstnPoint(startPoint);
         Point3d uPtEnd = rm2Upoint.CreateUstnPoint(endPoint);

         LineElement newLine = app.CreateLineElement2(null,
            ref uPtStart, ref uPtEnd);

         processNewElement(newLine, lineSegment);
      }

      private void processNewElement(Element newElement, HorizontalAlignmentBase rm21GoverningElement)
      {
         activeModelRef.AddElement(newElement);
         newElement.IsLocked = true;
         newElement.Rewrite();

         //rm21GoverningElement.
      }

   }
}
