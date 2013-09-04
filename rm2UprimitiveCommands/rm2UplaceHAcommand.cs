using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIM = Bentley.Interop.MicroStationDGN;
using ptsCogo.Horizontal;
using rm21Ustn.Utilities;
using ptsCogo;
using ptsCogo.Angle;
using rm21Ustn.GUI.Forms;

namespace rm21Ustn.rm2UprimitiveCommands
{
   public class rm2UplaceHAcommand : rm2UprimitiveCommandBase, ILinearElementDrawer
   {
      private CmdState_ internalState;
      private double defaultRadius;
      private rm21HorizontalAlignment newHA { get; set; }
      private ptsCogo.ptsPoint startPt { get; set; }
      private DrawingState drawingState { get; set; }
      private int dyncounter;
      private frm_HorizontalAlignmentTable HAtable = null;
      System.Windows.Forms.DataGridView grid = null;


      public override void Start()
      {
         defaultRadius = 117.0;
         internalState = CmdState_.initiate;
         drawingState = DrawingState.temporary;
         dyncounter = 0;
         app.ShowPrompt("Data Point to initiate HA placement");
         HAtable = new frm_HorizontalAlignmentTable();
         grid = HAtable.Controls["dgv_HorizontalAlignmentElements"] as System.Windows.Forms.DataGridView;
         HAtable.Show();
      }

      public override void Dynamics(ref BIM.Point3d Point, BIM.View View, BIM.MsdDrawingMode DrawMode)
      {
         var nextPt = rm2Upoint.CreatePtsPoint(Point);
         if (internalState == CmdState_.initiate)
         {
            app.ShowPrompt("Dynamics state:Initiate");
         }
         else if (internalState == CmdState_.initialTan)
         {
            newHA = new rm21HorizontalAlignment();
            newHA.reset(startPt, nextPt);
            newHA.drawTemporary(this);
         }
         else if (internalState == CmdState_.appendArc)
         {
            if ((nextPt - newHA.EndPoint).Length < 0.00001) return;
            newHA.appendArc(nextPt, this.defaultRadius);
            newHA.drawTemporary(this);
            //dyncounter++;
            //app.ShowPrompt("Dynamics state:AppendArc  Defl: " + newHA.getDeflectionOfFinalArc());
            newHA.removeFinalChildItem();
         }
         else if (internalState == CmdState_.appendTan)
         {
            if ((nextPt - newHA.EndPoint).Length < 0.00001) return;
            newHA.appendTangent(nextPt);
            newHA.drawTemporary(this);
            //app.ShowPrompt("Dynamics state:AppendTan  Defl: " + newHA.getDeflectionOfFinalArc());
            newHA.removeFinalChildItem();
         }
         else if (internalState == CmdState_.acceptAndDone)
         {

            app.ShowPrompt("Dynamics state:AcceptAndDone");
         }
         else
         {
            throw new Exception("rm21 PlaceHAinteractive: unexpected internal state.");
         }
      }

      public override void DataPoint(ref BIM.Point3d Point, BIM.View View)
      {
         var nextPt = rm2Upoint.CreatePtsPoint(Point);
         if (internalState == CmdState_.initiate)
         {
            this.startPt = rm2Upoint.CreatePtsPoint(Point);
            internalState = CmdState_.initialTan;
            app.ShowPrompt("Data Point state:Initiate");
            app.CommandState.StartDynamics();
         }
         else if (internalState == CmdState_.initialTan)
         {
            newHA = new rm21HorizontalAlignment();
            newHA.reset(startPt, nextPt);
            newHA.drawTemporary(this);
            //app.ShowPrompt("Data Point state:InitialTan");
            internalState = CmdState_.appendArc;
            app.CommandState.StartDynamics();
         }
         else if (internalState == CmdState_.appendArc)
         {
            if ((nextPt - newHA.EndPoint).Length < 0.00001) return;
            newHA.appendArc(nextPt, this.defaultRadius);
            newHA.drawTemporary(this);
            //app.ShowPrompt("Data Point state:AppendArc");
            internalState = CmdState_.appendTan;
            app.CommandState.StartDynamics();
         }
         else if (internalState == CmdState_.appendTan)
         {
            if ((nextPt - newHA.EndPoint).Length < 0.00001) return;
            newHA.appendTangent(nextPt);
            newHA.drawTemporary(this);

            app.ShowPrompt("Data Point state:AppendTan");
            internalState = CmdState_.appendArc;
            app.CommandState.StartDynamics();
         }
         else if (internalState == CmdState_.acceptAndDone)
         {

            app.ShowPrompt("Data Point state:AcceptAndDone");
         }
         else
         {
            throw new Exception("rm21 PlaceHAinteractive: unexpected internal state.");
         }
      }

      public override void Keyin(string Keyin)
      {
         Double newRad;
         if(true ==Double.TryParse(Keyin, out newRad))
         {
            this.defaultRadius = newRad;
            app.ShowPrompt("Radius set to " + newRad.ToString());
         }
      }

      public override void Reset()
      {
         newHA.drawPermanent(this);
         app.CommandState.StartDefaultCommand();
      }

      private enum CmdState_
      {
         initiate,
         initialTan,
         appendArc,
         appendTan,
         acceptAndDone
      }

      private enum DrawingState { temporary, permanent }

      public void drawArcSegment(ptsPoint startPt, ptsPoint centerPt, ptsPoint endPt, Double deflection)
      {
         app.ShowPrompt(String.Format("Defl: {0:0.00}, endPt: {1:0.00}, {2:0.00}", 
            deflection.AsPtsDegree().getAsDouble(), endPt.x, endPt.y));
         var uCenterPt = rm2Upoint.CreateUstnPoint(centerPt);
         BIM.Point3d uStartPt; BIM.Point3d uEndPt;
         if (deflection > 0)
         {
            uStartPt = rm2Upoint.CreateUstnPoint(startPt);
            uEndPt = rm2Upoint.CreateUstnPoint(endPt);
         }
         else
         {
            uStartPt = rm2Upoint.CreateUstnPoint(endPt);
            uEndPt = rm2Upoint.CreateUstnPoint(startPt);
         }
         BIM.ArcElement newArc = null;
         try
         {
            newArc = app.CreateArcElement1(null,
               ref uEndPt, ref uCenterPt, ref uStartPt);
         }
         catch (ArcException arcEx) { return; }
         newArc.Redraw(BIM.MsdDrawingMode.Temporary);
         if (this.drawingState == DrawingState.permanent)
         {
            app.ActiveModelReference.AddElement(newArc);
         }
      }

      public void drawLineSegment(ptsPoint startPt, ptsPoint endPt)
      {
         var newLine = app.CreateLineElement2(null,
            rm2Upoint.CreateUstnPoint(startPt), rm2Upoint.CreateUstnPoint(endPt));

         newLine.Redraw(BIM.MsdDrawingMode.Temporary);
         if (this.drawingState == DrawingState.permanent)
         {
            app.ActiveModelReference.AddElement(newLine);
         }
      }

      public void setDrawingStatePermanent()
      {
         this.drawingState = DrawingState.permanent;
      }

      public void setDrawingStateTemporary()
      {
         this.drawingState = DrawingState.temporary;
      }

      public void setAlignmentValues(
         int itemIndex, 
         String BegSta, 
         String Length, 
         String Azimuth, 
         String Radius, 
         String Deflection)
      {
         int placeholder = 0;
         if (grid.Rows.Count == 0)
            grid.Rows.Add();
         while (itemIndex >= grid.Rows.Count)
         {  grid.Rows.Add();  }
         grid.Rows[itemIndex].Cells["BeginStation"].Value = BegSta;
         grid.Rows[itemIndex].Cells["Length"].Value = Length;
         grid.Rows[itemIndex].Cells["Radius"].Value = Radius;
         grid.Rows[itemIndex].Cells["Deflection"].Value = Deflection;
         grid.Refresh();
      }
   }

}
