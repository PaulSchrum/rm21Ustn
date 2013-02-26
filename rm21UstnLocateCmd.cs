using System;
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
    internal class rm21UstnLocateCmd : BCOM.ILocateCommandEvents
       {

        #region "private variables"
        // These will get used over and over, so just get them once.
        private Bentley.MicroStation.AddIn m_AddIn;
        private BCOM.Application m_App;
                //the form....
        rm21Ustnform myForm;
        
        #endregion

        internal static void StartLocateCommand(BM.AddIn pAddIn)
        {
            //These are needed because it is a static method
            Bentley.MicroStation.AddIn m_AddIn = pAddIn;
			BCOM.Application m_App = rm21Ustn.ComApp;
            rm21UstnLocateCmd command = new rm21UstnLocateCmd(m_AddIn);
            BCOM.CommandState commandState = m_App.CommandState;
            commandState.StartLocate(command);
        }

        #region "constructors"
        
        private rm21UstnLocateCmd() { }
        public rm21UstnLocateCmd(BM.AddIn pAddIn)
        {
            m_App = rm21Ustn.ComApp;
            m_AddIn = pAddIn;
        }
        
        #endregion

        #region "ILocateCommandEvents"
        public void Start(){}
        public void Accept(BCOM.Element pElement, ref BCOM.Point3d pPoint, BCOM.View iView){}
        public void Cleanup (){}
        public void Dynamics(ref BCOM.Point3d pPoint,BCOM.View iView,BCOM.MsdDrawingMode drawMode) {}
        public void LocateFailed () {}
        public void LocateFilter (BCOM.Element pElement,ref BCOM.Point3d pPoint,ref bool accept){}
        public void LocateReset () { }
        #endregion ILocateCommandEvents
    }
}
