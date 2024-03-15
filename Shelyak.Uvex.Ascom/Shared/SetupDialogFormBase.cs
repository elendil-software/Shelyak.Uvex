using System;
using System.Linq;
using System.Windows.Forms;

namespace ASCOM.ShelyakUvex.Shared
{
    public class SetupDialogFormBase : Form
    {
        private ConfigHttpClient _httpClient;
        private ComboBox _comboBoxComPort;
        private TextBox _textBoxUvexWebApiUrl;
        private NumericUpDown _numericUpPort;
        
        protected SetupDialogFormBase()
        {
            
        }
        
        protected void SetComPortComboBox(ComboBox comboBoxComPort, TextBox textBoxUvexWebApiUrl, NumericUpDown numericUpPort)
        {
            _comboBoxComPort = comboBoxComPort;
            _textBoxUvexWebApiUrl = textBoxUvexWebApiUrl;
            _numericUpPort = numericUpPort;
        }

        protected void InitHttpClient(string uvexApiUrl, int uvexApiPort)
        {
            _httpClient = UvexHttpClientHelper.CreateConfigHttpClient(
                UvexHttpClientHelper.BuildUvexUrl(uvexApiUrl, uvexApiPort, UvexApiParameter.defaultApiConfigPath));
        }
        
        protected void UpdateApiPort()
        {
            _httpClient.UpdatePort(_comboBoxComPort.SelectedItem.ToString());
        }
        
        protected void ReloadComPorts()
        {
            _comboBoxComPort.Items.Clear();
            _comboBoxComPort.Items.Add("");
            var ports = _httpClient.GetAvailablePorts();
            var configuredPort = _httpClient.GetConfiguredPort();
            _comboBoxComPort.Items.AddRange(ports);
            
            if (ports.Contains(configuredPort))
            {
                _comboBoxComPort.SelectedItem = configuredPort;
            }
        }
        
        protected void OnUvexWebApiUrlChange()
        {
            if (!_textBoxUvexWebApiUrl.Text.StartsWith("http"))
            {
                _textBoxUvexWebApiUrl.Text = "http://" + _textBoxUvexWebApiUrl.Text;
            }
            
            //check if the URL is valid
            if (!Uri.IsWellFormedUriString(_textBoxUvexWebApiUrl.Text, UriKind.Absolute))
            {
                MessageBox.Show("Invalid URL", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _textBoxUvexWebApiUrl.Focus();
            }
            
            InitHttpClient(_textBoxUvexWebApiUrl.Text, (int)_numericUpPort.Value);

            try
            {   
                ReloadComPorts();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}