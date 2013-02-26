/*--------------------------------------------------------------------------------------+
|
|     $Source: /miscdev-root/miscdev/vault/VisualStudioWizards/MicroStationAddInWizard/Templates/1033/PlacementCmd.cs,v $
|    $RCSfile: PlacementCmd.cs,v $
|   $Revision: 1.1 $
|
|  $Copyright: (c) 2011 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/
#region "System Namespaces"
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SRI=System.Runtime.InteropServices;
#endregion 

#region "Bentley Namespaces"
using BMW = Bentley.MicroStation.WinForms;
using BMI = Bentley.MicroStation.InteropServices;
using BCOM = Bentley.Interop.MicroStationDGN;
using BM = Bentley.MicroStation;
#endregion

namespace rm21Ustn
{
	/// <summary>
	/// </summary>
	internal class rm21UstnPlacementCmd : BCOM.IPrimitiveCommandEvents
	{

		#region "Private Variables"

		// These will get used over and over, so just get them once.
		private Bentley.MicroStation.AddIn  m_AddIn;
		private BCOM.Application m_App;
		 		//the form....
		rm21Ustnform myForm;
		
		#endregion

		#region "Constructors"
		
		private rm21UstnPlacementCmd()
		{
			//  Make sure only the IDE uses this.
		}
		
		internal rm21UstnPlacementCmd(Bentley.MicroStation.AddIn addIn)
		{
			//Initialize class variables
			m_AddIn = addIn;
			m_App = rm21Ustn.ComApp;
			

			//  Set the controls to the values from active settings.
			BCOM.Settings settings = m_App.ActiveSettings;
    
			//  Initialize to active settings
		}
		
		#endregion

		/// <summary>
		/// Starts the primitive command to place the Route
		/// </summary>
		internal static void StartPlacementCommand (Bentley.MicroStation.AddIn addIn)
		{
			//These are needed because it is a static method
			Bentley.MicroStation.AddIn pAddIn = addIn;
			BCOM.Application pApp = rm21Ustn.ComApp;

			//Create a PlaceRouteCommand object
			rm21UstnPlacementCmd command = new rm21UstnPlacementCmd (pAddIn);

			BCOM.CommandState commandState = pApp.CommandState;
			

			pApp.CommandState.StartPrimitive (command, false);

			// Record the name that is saved in the Undo buffer and shown as the prompt.
			pApp.CommandState.CommandName = "Placement Command";

			//Optional Start Dynamics b/c we are ready to show elements 
			//pApp.CommandState.StartDynamics();
		}



		/* --------------------------------------------------------------
		 * The goal of this command is to:
		 * 
		 * ---------------------------------------------------------------------*/
		#region "IPrimitiveCommandEvents"
		public void Start()
		{
			//Instantiate the Form and Tell Microstation it is a tool settings
            			myForm = new rm21Ustnform(m_AddIn);
			myForm.AttachToToolSettings (m_AddIn);
            
			//Show Prompts etc.
			m_App.ShowCommand("Place First Point");
			m_App.ShowPrompt("Enter Point");

			//Enables Accusnap for this command if the user has it enabled in Microstation
			m_App.CommandState.EnableAccuSnap();

		}


		public void DataPoint(ref BCOM.Point3d Point, BCOM.View View)
		{

			/*--------------------------------------------------------
			 * ------------------------------------------------------*/
		}
	
		
		public void Keyin(string Keyin)
		{
			//Do Nothing
			;
		}

			
		public void Dynamics(ref BCOM.Point3d Point, BCOM.View View, BCOM.MsdDrawingMode DrawMode)
		{
			/*--------------------------------------------------------
			 *  
			 * -------------------------------------------------------*/
		}


		public void Reset()
		{
                     try
            {
                myForm.DetachFromMicroStation();
                myForm.Close();
                myForm.Dispose();
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("no window" + e.Message);
            }
        			m_App.CommandState.StartDefaultCommand();
		}


		public void Cleanup()
		{

                     try
            {
                myForm.DetachFromMicroStation();
                myForm.Dispose();
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message + "cleanup window failed");
            }

            myForm = null;
				}

		#endregion


	}
}