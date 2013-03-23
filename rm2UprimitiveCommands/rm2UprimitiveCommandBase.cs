using Bentley.Interop.MicroStationDGN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rm21Ustn.rm2UprimitiveCommands
{
   public abstract class rm2UprimitiveCommandBase : rm2Ubase, IPrimitiveCommandEvents
   {
      String testString;

      public virtual void Cleanup()
      {
         
      }

      public virtual void DataPoint(ref Point3d Point, View View)
      {
         app.CommandState.StartDefaultCommand();
      }

      public virtual void Dynamics(ref Point3d Point, View View, MsdDrawingMode DrawMode)
      {
         
      }

      public virtual void Keyin(string Keyin)
      {
         
      }

      public virtual void Reset()
      {
         app.CommandState.StartDefaultCommand();
      }

      public virtual void Start()
      {
         
      }
   }
}
