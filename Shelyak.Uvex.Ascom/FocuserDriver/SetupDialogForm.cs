using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ASCOM.Utilities;

namespace ASCOM.ShelyakUvex.Focuser
{
    [ComVisible(false)] // Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        TraceLogger tl;

        public SetupDialogForm(TraceLogger tlDriver)
        {
            InitializeComponent();
            tl = tlDriver;
            InitUI();
        }

        private void CmdOK_Click(object sender, EventArgs e)
        {
            tl.Enabled = chkTrace.Checked;
            FocuserHardware.uvexApiUrl = textBoxUvexWebApi.Text;
        }

        private void CmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InitUI()
        {
            chkTrace.Checked = tl.Enabled;
            textBoxUvexWebApi.Text = FocuserHardware.uvexApiUrl;
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