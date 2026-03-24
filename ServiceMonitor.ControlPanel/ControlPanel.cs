using ServiceMonitor.Shared.Models;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text.Json;
using System.Security.Principal;


namespace ServiceMonitor.ControlPanel
{
    public partial class ControlPanel : Form
    {
        private System.Windows.Forms.Timer timerStatus = new System.Windows.Forms.Timer();
        private string _monitoringServiceName = "SrvsMonSrv";
        private string _monitoringServiceDisplayName = "Services Monitoring Service";
        public ControlPanel()
        {
            InitializeComponent();

            // Configure and start the status timer
            timerStatus.Interval = 3000; // 3 seconds
            timerStatus.Tick += TimerStatus_Tick;
            timerStatus.Tick += MonitoringServiceStatus_Tick;
            timerStatus.Tick += async (s, e) => await MonitoringServiceInstallState();
            timerStatus.Start();
        }

        private async Task LoadConfigAsync()
        {
            try
            {
                using var client = new HttpClient();
                var json = await client.GetStringAsync("http://localhost:8080/config");
                var config = JsonSerializer.Deserialize<MonitorConfig>(json);

                if (config == null) return;

                port_box.Text = config.HttpPort.ToString();

                monitoredServicesList.Items.Clear();
                foreach (var svc in config.Services)
                {
                    monitoredServicesList.Items.Add(svc.ServiceName);
                }

                // Optional: load branding logo here if applicable
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,"Failed to load config from service: " + ex.Message,"Service Monitor");
            }
        }

        private async Task MonitoringServiceInstallState()
        {
            var svcInfo = ServiceController.GetServices()
         .FirstOrDefault(s => s.DisplayName == _monitoringServiceDisplayName || s.ServiceName == _monitoringServiceName);

            if (svcInfo == null)
            {
                install_monit_btn.Text = "Install";
                start_monit_btn.Enabled = false;
                stop_monit_btn.Enabled = false;
                restart_monit_btn.Enabled = false;
                uninstall_monit_btn.Enabled = false;
                install_monit_btn.Enabled = true; 
                install_monit_btn.BackColor = Color.Salmon; 
            }
            else
            {

                install_monit_btn.Text = "Installed";
                start_monit_btn.Enabled = true; 
                stop_monit_btn.Enabled = true;
                restart_monit_btn.Enabled = true;
                install_monit_btn.Enabled = false;
                uninstall_monit_btn.Enabled = true; 
                install_monit_btn.BackColor = Color.DarkSeaGreen;

            }

        }

        private bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        private async void ControlPanel_Load(object sender, EventArgs e)
        {
            comboBoxServices.Items.Clear();

            if (!IsAdministrator())
            {
                MessageBox.Show(this, "This application must be run as administrator to manage services.", "Service Monitor");
                return;
            }

            await MonitoringServiceInstallState(); // Ensure completion

            var services = ServiceController.GetServices();
            foreach (var svc in services)
            {
                comboBoxServices.Items.Add(svc.ServiceName);
            }

            if (string.IsNullOrWhiteSpace(port_box.Text))
            {
                port_box.Text = "8080";
            }
            else
            {
                if (!int.TryParse(port_box.Text, out int port) || port < 1 || port > 65535)
                {
                    MessageBox.Show(this, "Invalid port number in config. Resetting to default (8080).", "Service Monitor");
                    port_box.Text = "8080";
                }

                await LoadConfigAsync();
            }
        }


        private void MonitoringServiceStatus_Tick(object? sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(_monitoringServiceName))
                    throw new ArgumentException("Service name cannot be null or empty.", nameof(_monitoringServiceName));
                using var svc = new ServiceController(_monitoringServiceName);
                monitoring_service_state.BackColor = svc.Status == ServiceControllerStatus.Running ? Color.LimeGreen : Color.Red;
            }
            catch
            {
                monitoring_service_state.BackColor = Color.Red;
            }
        }

        private void add_service_to_list_Click(object sender, EventArgs e)
        {
            var selectedService = comboBoxServices.Text;
            if (string.IsNullOrWhiteSpace(selectedService))
            {
                MessageBox.Show(this, "Please select a service to add.", "Service Monitor");
                return;
            }
            if (!monitoredServicesList.Items.Contains(selectedService))
            {
                monitoredServicesList.Items.Add(selectedService);
            }
        }

        private void del_service_from_list_Click(object sender, EventArgs e)
        {
            if (monitoredServicesList.SelectedItem != null)
            {
                monitoredServicesList.Items.Remove(monitoredServicesList.SelectedItem);
            }
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(port_box.Text, out int port) || port < 1 || port > 65535)
            {
                MessageBox.Show(this, "Please enter a valid port number (1-65535).", "Service Monitor");
                return;
            }

            try
            {
                var config = new MonitorConfig
                {
                    HttpPort = port,
                    Services = monitoredServicesList.Items.Cast<string>()
                                .Select(s => new MonitoredService { ServiceName = s })
                                .ToList()
                };

                var json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("config.json", json);

                MessageBox.Show(this, "Configuration saved. Please restart the service to apply changes.", "Service Monitor");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to save config: " + ex.Message, "Service Monitor");
            }
        }

        private void restart_btn_Click(object sender, EventArgs e)
        {
            if (monitoredServicesList.SelectedItem == null)
            {
                MessageBox.Show(this, "Please select a monitored service to restart.", "Service Monitor");
                return;
            }

            var selectedName = monitoredServicesList.SelectedItem.ToString();

            try
            {
                var svcInfo = ServiceController.GetServices()
                    .FirstOrDefault(s => s.DisplayName == selectedName || s.ServiceName == selectedName);

                if (svcInfo == null)
                {
                    MessageBox.Show(this, "Service not found.", "Service Monitor");
                    return;
                }

                using var svc = new ServiceController(svcInfo.ServiceName);

                svc.Refresh();

                if (svc.Status != ServiceControllerStatus.Stopped &&
                    svc.Status != ServiceControllerStatus.StopPending)
                {
                    svc.Stop();
                    svc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
                }

                svc.Start();
                svc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to restart service: {ex.Message}", "Service Monitor");
            }
        }

        private void stop_btn_Click(object sender, EventArgs e)
        {
            if (monitoredServicesList.SelectedItem == null)
            {
                MessageBox.Show(this, "Please select a monitored service to stop.", "Service Monitor");
                return;
            }

            var selectedName = monitoredServicesList.SelectedItem.ToString();

            try
            {
                var svcInfo = ServiceController.GetServices()
                    .FirstOrDefault(s => s.DisplayName == selectedName || s.ServiceName == selectedName);

                if (svcInfo == null)
                {
                    MessageBox.Show(this, "Service not found.", "Service Monitor");
                    return;
                }

                using var svc = new ServiceController(svcInfo.ServiceName);
                svc.Refresh();

                if (svc.Status != ServiceControllerStatus.Stopped &&
                    svc.Status != ServiceControllerStatus.StopPending)
                {
                    svc.Stop();
                    svc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(40));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to stop service: {ex.Message}", "Service Monitor");
            }
        }
        private void TimerStatus_Tick(object? sender, EventArgs e)
        {
            if (monitoredServicesList.SelectedItem == null)
            {
                panelStatus.BackColor = Color.Gray;
                return;
            }

            var serviceName = monitoredServicesList.SelectedItem.ToString();

            try
            {
                if (string.IsNullOrEmpty(serviceName))
                    throw new ArgumentException("Service name cannot be null or empty.", nameof(serviceName));
                using var svc = new ServiceController(serviceName);
                panelStatus.BackColor = svc.Status == ServiceControllerStatus.Running ? Color.LimeGreen : Color.Red;
            }
            catch
            {
                panelStatus.BackColor = Color.Red;
            }
        }


        private void restart_monit_btn_Click(object sender, EventArgs e)
        {

            try
            {
                var svcInfo = ServiceController.GetServices()
                   .FirstOrDefault(s => s.DisplayName == _monitoringServiceDisplayName || s.ServiceName == _monitoringServiceName);

                if (svcInfo == null)
                {
                    MessageBox.Show(this, "Service not found.", "Service Monitor");
                    return;
                }

                using var svc = new ServiceController(svcInfo.ServiceName);

                svc.Refresh();

                if (svc.Status != ServiceControllerStatus.Stopped &&
                    svc.Status != ServiceControllerStatus.StopPending)
                {
                    svc.Stop();
                    svc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
                }

                svc.Start();
                svc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to restart service: {ex.Message}", "Service Monitor");
            }
        }

        private void start_monit_btn_Click(object sender, EventArgs e)
        {

            try
            {
                var svcInfo = ServiceController.GetServices()
                   .FirstOrDefault(s => s.DisplayName == _monitoringServiceDisplayName || s.ServiceName == _monitoringServiceName);

                if (svcInfo == null)
                {
                    MessageBox.Show(this, "Service not found.", "Service Monitor");
                    return;
                }

                using var svc = new ServiceController(svcInfo.ServiceName);

                svc.Refresh();

                svc.Start();
                svc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to start service: {ex.Message}", "Service Monitor");
            }
        }

        private void stop_monit_btn_Click(object sender, EventArgs e)
        {

            try
            {
                var svcInfo = ServiceController.GetServices()
                   .FirstOrDefault(s => s.DisplayName == _monitoringServiceDisplayName || s.ServiceName == _monitoringServiceName);

                if (svcInfo == null)
                {
                    MessageBox.Show(this, "Service not found.", "Service Monitor");
                    return;
                }

                using var svc = new ServiceController(svcInfo.ServiceName);

                svc.Refresh(); 

                svc.Stop();
                svc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(40));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to stop service: {ex.Message}", "Service Monitor");
            }
        }

        private void install_monit_btn_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "ServiceMonitor.Service.exe");


            try
            {
                if (File.Exists(path))
                {
                    var process = new Process();
                    process.StartInfo.FileName = "sc";
                    process.StartInfo.Arguments = $"create {_monitoringServiceName} binPath= \"{path}\" displayName= \"{_monitoringServiceDisplayName}\"";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.Verb = "runas";
                    process.Start();
                    process.WaitForExit();

                    MessageBox.Show(this, "Service installed!", "Service Monitor");
                }
                else
                {
                    MessageBox.Show(this, "Executable not found at: " + path, "Service Monitor");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to install monitoring service: {ex.Message}", "Service Monitor");
            }


        }

        private void uninstall_monit_btn_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "ServiceMonitor.Service.exe");


            try
            {
                var process = new Process();
                process.StartInfo.FileName = "sc";
                process.StartInfo.Arguments = $"delete {_monitoringServiceName}";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.Verb = "runas";
                process.Start();
                process.WaitForExit(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to uninstall monitoring service: {ex.Message}", "Service Monitor");
            }

        }
    }
}