using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SRI=System.Runtime.InteropServices;

//  Bentley namespaces
using BMW=Bentley.MicroStation.WinForms;
using BMI=Bentley.MicroStation.InteropServices;
using BCOM=Bentley.Interop.MicroStationDGN;



namespace rm21Ustn.GUI.Forms
{
   public partial class frm_HorizontalAlignmentTable : 
      //Form
      BMW.Adapter
   {
      public frm_HorizontalAlignmentTable()
      {
         InitializeComponent();
      }

      private void HorizontalAlignmentTable_Load(object sender, EventArgs e)
      {

      }
   }
}
