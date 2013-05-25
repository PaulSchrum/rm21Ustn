using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bentley.Interop.MicroStationDGN;

namespace rm21Ustn
{
   public class rm2Ubase
   {
      internal static Application app
      {
         get { return Bentley.MicroStation.InteropServices.Utilities.ComApp; }
         private set { }
      }

      internal static ModelReference activeModelRef
      {
         get { return Bentley.MicroStation.InteropServices.Utilities.ComApp.ActiveModelReference; }
         private set { }
      }

      internal static rm21Ustn myAdIn
      {
         get { return rm21Ustn.MyAddin; }
         private set { }
      }

      internal static rm2Uproject proj
      {
         get { return rm21Ustn.MyAddin.rm21UstnProject; }
         private set { }
      }

   }
}
