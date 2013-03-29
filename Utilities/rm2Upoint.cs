using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ptsCogo;
using Bentley.Interop.MicroStationDGN;

namespace rm21Ustn.Utilities
{
   public class rm2Upoint
   {
      public static ptsPoint CreatePtsPoint(Point3d pt)
      {
         return new ptsPoint(pt.X, pt.Y, pt.Z);
      }

      public static Point3d CreateUstnPoint(ptsPoint pts)
      {
         Point3d returnPoint;
         returnPoint.X = pts.x; returnPoint.Y = pts.y;
         returnPoint.Z = pts.z;

         return returnPoint;
      }
   }
}
