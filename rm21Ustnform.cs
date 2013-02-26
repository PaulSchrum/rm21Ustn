/*--------------------------------------------------------------------------------------+
|
|     $Source: /miscdev-root/miscdev/vault/VisualStudioWizards/MicroStationAddInWizard/Templates/1033/form.cs,v $
|    $RCSfile: form.cs,v $
|   $Revision: 1.1 $
|
|  $Copyright: (c) 2011 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/
//  System namespaces
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SRI=System.Runtime.InteropServices;

//  Bentley namespaces
using BMW=Bentley.MicroStation.WinForms;
using BMI=Bentley.MicroStation.InteropServices;
using BCOM=Bentley.Interop.MicroStationDGN;

namespace rm21Ustn
{
/// <summary>The rm21Ustn class 
/// implements a form that is embedded in MicroStation's
/// 
/// </summary>
internal class rm21Ustnform : 
                BMW.Adapter        //  For the form embedded in Tool Settings
{
private rm21Ustn          m_addIn;


/// <summary>
/// Required designer variable.
/// </summary>
private System.ComponentModel.Container components = null;

/// <summary>The IDE requires the constructor with no arguments.</summary>
private rm21Ustnform()
    {
    //  Make sure only the IDE uses this.
    System.Diagnostics.Debug.Assert (this.DesignMode == true);
    InitializeComponent();
    }

/// <summary>Constructor</summary>
internal rm21Ustnform(Bentley.MicroStation.AddIn addIn)
    {
    //  Invoke the IDE-generated code
    InitializeComponent();

    m_addIn       = rm21Ustn.MyAddin;
   
    
    //  Set the controls to the values from active settings.
    BCOM.Application   app = rm21Ustn.ComApp;
   
    ProcessToolSettingsControls ();
    }
    
/// <summary>Clean up any resources being used. 
/// </summary>
protected override void Dispose( bool disposing )
    {
    if( disposing )
        {
        //  This is an explicit call to Dispose. It is not
        //  the result of the object being garbage collected.
        if(components != null)
            {
            components.Dispose();
            }
        }

    base.Dispose( disposing );
    }

#region Windows Form Designer generated code
/// <summary>
/// Required method for Designer support - do not modify
/// the contents of this method with the code editor.
/// </summary>
private void InitializeComponent()
    {
    // 
    // PlaceCell
    // 
    this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
    this.ClientSize = new System.Drawing.Size(176, 133);
    this.ResumeLayout(false);

}
#endregion

    
private void ProcessToolSettingsControls ()
    {
    BCOM.Application    app = rm21Ustn.ComApp;

    }


}   // End of class 
}   // End of namespace
