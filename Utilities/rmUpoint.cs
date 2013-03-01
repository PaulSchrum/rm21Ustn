using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ptsCogo;
using Bentley.Interop.MicroStationDGN;

namespace rm21Ustn.Utilities
{
   public class rmUpoint
   {
      public static ptsPoint CreatePtsPoint(Point3d pt)
      {
         return new ptsPoint(pt.X, pt.Y, pt.Z);
      }
   }
}
