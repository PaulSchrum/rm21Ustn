namespace rm21Ustn.GUI.Forms
{
   partial class frm_HorizontalAlignmentTable
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.dgv_HorizontalAlignmentElements = new System.Windows.Forms.DataGridView();
         this.BeginStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Radius = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Deflection = new System.Windows.Forms.DataGridViewTextBoxColumn();
         ((System.ComponentModel.ISupportInitialize)(this.dgv_HorizontalAlignmentElements)).BeginInit();
         this.SuspendLayout();
         // 
         // dgv_HorizontalAlignmentElements
         // 
         this.dgv_HorizontalAlignmentElements.AllowUserToAddRows = false;
         this.dgv_HorizontalAlignmentElements.AllowUserToDeleteRows = false;
         this.dgv_HorizontalAlignmentElements.AllowUserToOrderColumns = true;
         this.dgv_HorizontalAlignmentElements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.dgv_HorizontalAlignmentElements.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BeginStation,
            this.Length,
            this.Radius,
            this.Deflection});
         this.dgv_HorizontalAlignmentElements.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
         this.dgv_HorizontalAlignmentElements.Location = new System.Drawing.Point(0, 0);
         this.dgv_HorizontalAlignmentElements.Name = "dgv_HorizontalAlignmentElements";
         this.dgv_HorizontalAlignmentElements.ReadOnly = true;
         this.dgv_HorizontalAlignmentElements.RowTemplate.Height = 24;
         this.dgv_HorizontalAlignmentElements.Size = new System.Drawing.Size(599, 267);
         this.dgv_HorizontalAlignmentElements.TabIndex = 0;
         // 
         // BeginStation
         // 
         this.BeginStation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
         this.BeginStation.HeaderText = "Beg Sta";
         this.BeginStation.Name = "BeginStation";
         this.BeginStation.ReadOnly = true;
         this.BeginStation.Width = 160;
         // 
         // Length
         // 
         this.Length.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
         this.Length.HeaderText = "Length";
         this.Length.Name = "Length";
         this.Length.ReadOnly = true;
         // 
         // Radius
         // 
         this.Radius.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
         this.Radius.HeaderText = "Radius";
         this.Radius.Name = "Radius";
         this.Radius.ReadOnly = true;
         this.Radius.Width = 120;
         // 
         // Deflection
         // 
         this.Deflection.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
         this.Deflection.HeaderText = "Deflection";
         this.Deflection.Name = "Deflection";
         this.Deflection.Width = 175;
         // 
         // frm_HorizontalAlignmentTable
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(601, 269);
         this.Controls.Add(this.dgv_HorizontalAlignmentElements);
         this.Name = "frm_HorizontalAlignmentTable";
         this.Text = "Horizontal Alignment Table";
         this.Load += new System.EventHandler(this.HorizontalAlignmentTable_Load);
         ((System.ComponentModel.ISupportInitialize)(this.dgv_HorizontalAlignmentElements)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.DataGridView dgv_HorizontalAlignmentElements;
      private System.Windows.Forms.DataGridViewTextBoxColumn BeginStation;
      private System.Windows.Forms.DataGridViewTextBoxColumn Length;
      private System.Windows.Forms.DataGridViewTextBoxColumn Radius;
      private System.Windows.Forms.DataGridViewTextBoxColumn Deflection;
   }
}