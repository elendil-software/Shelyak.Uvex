using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ASCOM.ShelyakUvex.Shared;
using ASCOM.Utilities;

namespace ASCOM.ShelyakUvex.FilterWheel
{
    [ComVisible(false)] // Form not registered for COM!
    public partial class SetupDialogForm : SetupDialogFormBase
    {
        TraceLogger tl;

        public SetupDialogForm(TraceLogger tlDriver)
        {
            InitializeComponent(); 
            tl = tlDriver;
            
            SetComPortComboBox(comboBoxComPort);
            InitHttpClient(FilterWheelHardwareSettings.uvexApiUrl, FilterWheelHardwareSettings.uvexApiPort);
            
            InitUI();
        }

        private void CmdOK_Click(object sender, EventArgs e)
        {
            tl.Enabled = chkTrace.Checked;
            FilterWheelHardwareSettings.uvexApiUrl = textBoxUvexWebApi.Text;
            FilterWheelHardwareSettings.uvexApiPort = (int)numericUpPort.Value;
            UpdateApiPort();
        }

        private void CmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void InitUI()
        {
            chkTrace.Checked = tl.Enabled;
            textBoxUvexWebApi.Text = FilterWheelHardwareSettings.uvexApiUrl;
            numericUpPort.Value = FilterWheelHardwareSettings.uvexApiPort;
            
            ReloadComPorts();
        }

        private void SetupDialogForm_Load(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            else
            {
                TopMost = true;
                Focus();
                BringToFront();
                TopMost = false;
            }
        }
    }
}