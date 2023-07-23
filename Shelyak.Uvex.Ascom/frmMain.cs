using System.Windows.Forms;

namespace ASCOM.LocalServer
{
    public partial class FrmMain : Form
    {
        private delegate void SetTextCallback(string text);

        public FrmMain()
        {
            InitializeComponent();
            ShowInTaskbar = false;
            Visible = false;
        }

    }
}