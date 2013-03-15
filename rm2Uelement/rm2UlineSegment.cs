using Bentley.Interop.MicroStationDGN;
using ptsCogo;
using rm21Ustn.Horizontal;
using rm21Ustn.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using BCOM = Bentley.Interop.MicroStationDGN;

namespace rm21Ustn.rm2Uelement
{
   public class rm2UlineSegment : rm2Upath
   {
      public rm2UlineSegment() : base() { }

      public rm2UlineSegment(Element el) : base(el) { }

      public LineElement EL
      {
         get { return (LineElement)el; }
         private set { }
      }

      internal override rm2UfunGeom getAsFundamentalGeometry()
      {
         rm2UfunGeom returnItem = new rm2UfunGeom();
         LineElement Line = this.EL;

         returnItem.ptList = new List<ptsPoint>();
         returnItem.ptList.Add(rm2Upoint.CreatePtsPoint(Line.StartPoint));
         returnItem.ptList.Add(rm2Upoint.CreatePtsPoint(Line.EndPoint));
         returnItem.expType = ptsCogo.Horizontal.expectedType.LineSegment;

         return returnItem;
      }
   }
}
