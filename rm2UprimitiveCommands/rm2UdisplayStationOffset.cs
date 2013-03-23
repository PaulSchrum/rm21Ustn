using Bentley.Interop.MicroStationDGN;
using ptsCogo;
using ptsCogo.coordinates.CurvilinearCoordinates;
using ptsCogo.Horizontal;
using rm21Ustn.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rm21Ustn.rm2UprimitiveCommands
{
   class rm2UdisplayStationOffset : rm2UprimitiveCommandBase
   {
      List<rm21HorizontalAlignment> allAlignments = null;
      rm21HorizontalAlignment anAlignment = null;
      StationOffsetElevation soe = null;
      long count = 0;

      public override void Dynamics(ref Point3d ustnPoint, View View, MsdDrawingMode DrawMode)
      {
         if (null == anAlignment) return;

         soe = anAlignment.getStationOffsetElevation(rm2Upoint.CreatePtsPoint(ustnPoint)).FirstOrDefault();
         app.ShowStatus(CogoStation.getStationOffsetElevationAsString(soe));
      }

      public override void Start()
      {
         allAlignments = proj.getAllHorizontalAlignments();
         if (null == allAlignments) return;
         anAlignment = allAlignments.FirstOrDefault();
      }
   }
}
