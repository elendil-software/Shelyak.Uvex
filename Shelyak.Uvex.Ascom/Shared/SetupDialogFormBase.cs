using System.Linq;
using System.Windows.Forms;
using ASCOM.ShelyakUvex.Shared;

namespace ASCOM.ShelyakUvex.Shared
{
    public class SetupDialogFormBase : Form
    {
        private ConfigHttpClient _httpClient;
        private ComboBox _comboBoxComPort;
        
        protected SetupDialogFormBase()
        {
            
        }
        
        protected void SetComPortComboBox(ComboBox comboBoxComPort)
        {
            _comboBoxComPort = comboBoxComPort;
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
    }
}