using BMW=Bentley.MicroStation.WinForms;
using BMI=Bentley.MicroStation.InteropServices;
using BCOM=Bentley.Interop.MicroStationDGN;
using rm21Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bentley.Interop.MicroStationDGN;

namespace rm21Ustn
{
   public class rm2Uproject
   {
      public RM21Project rm21CoreProject {get; set;}

      public rm2Uproject(ModelReference modelRef)
      {
         if (null == modelRef) throw new NullReferenceException();

      }
   }
}
