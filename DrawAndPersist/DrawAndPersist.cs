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

      public void PlaceArc(rm21HorArc arc, 
         ptsPoint startPoint, StationOffsetElevation startSOE, 
         ptsPoint endPoint, StationOffsetElevation endSOE)
      {
         int i = 9;
      }

      public void PlaceLine(rm21HorLineSegment lineSegment,
         ptsPoint startPoint, StationOffsetElevation startSOE,
         ptsPoint endPoint, StationOffsetElevation endSOE)
      {
         Point3d uPtStart = rm2Upoint.CreateUstnPoint(startPoint);
         Point3d uPtEnd = rm2Upoint.CreateUstnPoint(endPoint);

         LineElement newLine = app.CreateLineElement2(null,
            ref uPtStart, ref uPtEnd);

         activeModelRef.AddElement(newLine);
      }

   }
}
