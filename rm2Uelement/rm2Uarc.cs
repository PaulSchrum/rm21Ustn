using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bentley.Interop.MicroStationDGN;
using rm21Ustn.Horizontal;
using ptsCogo;
using rm21Ustn.Utilities;

namespace rm21Ustn.rm2Uelement
{
   public class rm2Uarc : rm2Upath
   {
      public rm2Uarc() : base() { }
      public rm2Uarc(Element el) : base(el) { }

      public ArcElement EL
      {
         get { return (ArcElement)el; }
         private set { }
      }

      internal override rm2UfunGeom getAsFundamentalGeometry()
      {
         rm2UfunGeom returnItem = new rm2UfunGeom();
         ArcElement Arc = this.EL;

         returnItem.ptList = new List<ptsPoint>();
         returnItem.ptList.Add(rm2Upoint.CreatePtsPoint(Arc.StartPoint));
         returnItem.ptList.Add(rm2Upoint.CreatePtsPoint(Arc.CenterPoint));
         returnItem.ptList.Add(rm2Upoint.CreatePtsPoint(Arc.EndPoint));

         if (Math.Abs(ptsAngle.degreesFromRadians(Arc.SweepAngle)) < 180.0)
            returnItem.expType = ptsCogo.Horizontal.expectedType.ArcSegmentInsideSolution;
         else if (Math.Abs(ptsAngle.degreesFromRadians(Arc.SweepAngle)) > 180.0)
            returnItem.expType = ptsCogo.Horizontal.expectedType.ArcSegmentOutsideSoluion;
         else
            returnItem.expType = ptsCogo.Horizontal.expectedType.ArcHalfCircle;

         returnItem.DeflectionDirection = -1 * Math.Sign(Arc.SweepAngle);

         return returnItem;
      }
   }
}
