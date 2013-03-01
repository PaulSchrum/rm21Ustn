using ptsCogo;
using ptsCogo.Horizontal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rm21Ustn.Horizontal
{
   internal class rm2UfunGeom : IRM21fundamentalGeometry
   {
      public List<ptsPoint> ptList
      {
         get;
         set;
      }

      public expectedType expType { get; set; }

      public double getBeginningDegreeOfCurve()
      {
         return 0.0;
      }

      public double getEndingDegreeOfCurve()
      {
         return 0.0;
      }

      public expectedType getExpectedType()
      {
         return expType;
      }

      public List<ptsPoint> getPointList()
      {
         return ptList;
      }
   }
}
