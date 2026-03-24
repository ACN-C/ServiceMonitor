namespace ServiceMonitor.ControlPanel
{
    partial class ControlPanel
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBoxServices = new ComboBox();
            port_box = new TextBox();
            restart_btn = new Button();
            stop_btn = new Button();
            save_btn = new Button();
            monitoredServicesList = new ListBox();
            add_service_to_list = new Button();
            del_service_from_list = new Button();
            panelStatus = new Panel();
            start_monit_btn = new Button();
            panel1 = new Panel();
            restart_monit_btn = new Button();
            stop_monit_btn = new Button();
            uninstall_monit_btn = new Button();
            monitoring_service_state = new Panel();
            install_monit_btn = new Button();
            monitored_services = new Panel();
            lb_logo = new PictureBox();
            panel1.SuspendLayout();
            monitored_services.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lb_logo).BeginInit();
            SuspendLayout();
            // 
            // comboBoxServices
            // 
            comboBoxServices.BackColor = SystemColors.Control;
            comboBoxServices.FormattingEnabled = true;
            comboBoxServices.Location = new Point(8, 10);
            comboBoxServices.Margin = new Padding(4, 3, 4, 3);
            comboBoxServices.Name = "comboBoxServices";
            comboBoxServices.Size = new Size(242, 23);
            comboBoxServices.TabIndex = 0;
            // 
            // port_box
            // 
            port_box.AcceptsReturn = true;
            port_box.BackColor = SystemColors.ControlLightLight;
            port_box.Cursor = Cursors.IBeam;
            port_box.Location = new Point(9, 108);
            port_box.Margin = new Padding(5, 5, 5, 5);
            port_box.Name = "port_box";
            port_box.PlaceholderText = "8080";
            port_box.Size = new Size(110, 23);
            port_box.TabIndex = 1;
            port_box.TextAlign = HorizontalAlignment.Center;
            // 
            // restart_btn
            // 
            restart_btn.BackColor = Color.WhiteSmoke;
            restart_btn.FlatStyle = FlatStyle.Flat;
            restart_btn.Location = new Point(260, 138);
            restart_btn.Margin = new Padding(5, 5, 5, 5);
            restart_btn.Name = "restart_btn";
            restart_btn.RightToLeft = RightToLeft.Yes;
            restart_btn.Size = new Size(120, 22);
            restart_btn.TabIndex = 2;
            restart_btn.Text = "Restart";
            restart_btn.UseVisualStyleBackColor = false;
            restart_btn.Click += restart_btn_Click;
            // 
            // stop_btn
            // 
            stop_btn.BackColor = Color.WhiteSmoke;
            stop_btn.FlatStyle = FlatStyle.Flat;
            stop_btn.Location = new Point(260, 105);
            stop_btn.Margin = new Padding(5, 5, 5, 5);
            stop_btn.Name = "stop_btn";
            stop_btn.Size = new Size(120, 22);
            stop_btn.TabIndex = 3;
            stop_btn.Text = "Stop";
            stop_btn.UseVisualStyleBackColor = false;
            stop_btn.Click += stop_btn_Click;
            // 
            // save_btn
            // 
            save_btn.BackColor = Color.WhiteSmoke;
            save_btn.FlatStyle = FlatStyle.Flat;
            save_btn.Location = new Point(10, 140);
            save_btn.Margin = new Padding(5, 5, 5, 5);
            save_btn.Name = "save_btn";
            save_btn.Size = new Size(110, 22);
            save_btn.TabIndex = 4;
            save_btn.Text = "Save";
            save_btn.UseVisualStyleBackColor = false;
            save_btn.Click += save_btn_Click;
            // 
            // monitoredServicesList
            // 
            monitoredServicesList.BorderStyle = BorderStyle.FixedSingle;
            monitoredServicesList.FormattingEnabled = true;
            monitoredServicesList.Location = new Point(10, 42);
            monitoredServicesList.Margin = new Padding(5, 5, 5, 5);
            monitoredServicesList.Name = "monitoredServicesList";
            monitoredServicesList.Size = new Size(240, 122);
            monitoredServicesList.TabIndex = 9;
            monitoredServicesList.Tag = "";
            // 
            // add_service_to_list
            // 
            add_service_to_list.BackColor = Color.WhiteSmoke;
            add_service_to_list.FlatStyle = FlatStyle.Flat;
            add_service_to_list.Location = new Point(260, 10);
            add_service_to_list.Margin = new Padding(5, 5, 5, 5);
            add_service_to_list.Name = "add_service_to_list";
            add_service_to_list.Size = new Size(120, 22);
            add_service_to_list.TabIndex = 11;
            add_service_to_list.Text = "Add";
            add_service_to_list.UseVisualStyleBackColor = false;
            add_service_to_list.Click += add_service_to_list_Click;
            // 
            // del_service_from_list
            // 
            del_service_from_list.BackColor = Color.WhiteSmoke;
            del_service_from_list.FlatStyle = FlatStyle.Flat;
            del_service_from_list.Location = new Point(260, 42);
            del_service_from_list.Margin = new Padding(5, 5, 5, 5);
            del_service_from_list.Name = "del_service_from_list";
            del_service_from_list.RightToLeft = RightToLeft.Yes;
            del_service_from_list.Size = new Size(120, 22);
            del_service_from_list.TabIndex = 10;
            del_service_from_list.Text = "Remove";
            del_service_from_list.UseVisualStyleBackColor = false;
            del_service_from_list.Click += del_service_from_list_Click;
            // 
            // panelStatus
            // 
            panelStatus.BackColor = SystemColors.ControlLight;
            panelStatus.BorderStyle = BorderStyle.Fixed3D;
            panelStatus.Location = new Point(260, 74);
            panelStatus.Margin = new Padding(4, 3, 4, 3);
            panelStatus.Name = "panelStatus";
            panelStatus.Size = new Size(119, 24);
            panelStatus.TabIndex = 12;
            // 
            // start_monit_btn
            // 
            start_monit_btn.BackColor = Color.WhiteSmoke;
            start_monit_btn.FlatStyle = FlatStyle.Flat;
            start_monit_btn.Location = new Point(183, 42);
            start_monit_btn.Margin = new Padding(4, 3, 4, 3);
            start_monit_btn.Name = "start_monit_btn";
            start_monit_btn.Size = new Size(55, 22);
            start_monit_btn.TabIndex = 13;
            start_monit_btn.Text = "Start";
            start_monit_btn.UseVisualStyleBackColor = false;
            start_monit_btn.Click += start_monit_btn_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Silver;
            panel1.BackgroundImageLayout = ImageLayout.None;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(port_box);
            panel1.Controls.Add(save_btn);
            panel1.Controls.Add(restart_monit_btn);
            panel1.Controls.Add(stop_monit_btn);
            panel1.Controls.Add(uninstall_monit_btn);
            panel1.Controls.Add(monitoring_service_state);
            panel1.Controls.Add(install_monit_btn);
            panel1.Controls.Add(start_monit_btn);
            panel1.Location = new Point(435, 20);
            panel1.Margin = new Padding(10, 10, 10, 10);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(5, 5, 5, 5);
            panel1.Size = new Size(250, 176);
            panel1.TabIndex = 14;
            // 
            // restart_monit_btn
            // 
            restart_monit_btn.BackColor = Color.WhiteSmoke;
            restart_monit_btn.FlatStyle = FlatStyle.Flat;
            restart_monit_btn.Location = new Point(183, 110);
            restart_monit_btn.Margin = new Padding(4, 3, 4, 3);
            restart_monit_btn.Name = "restart_monit_btn";
            restart_monit_btn.Size = new Size(55, 22);
            restart_monit_btn.TabIndex = 20;
            restart_monit_btn.Text = "Restart";
            restart_monit_btn.UseVisualStyleBackColor = false;
            restart_monit_btn.Click += restart_monit_btn_Click;
            // 
            // stop_monit_btn
            // 
            stop_monit_btn.BackColor = Color.WhiteSmoke;
            stop_monit_btn.FlatStyle = FlatStyle.Flat;
            stop_monit_btn.Location = new Point(183, 76);
            stop_monit_btn.Margin = new Padding(4, 3, 4, 3);
            stop_monit_btn.Name = "stop_monit_btn";
            stop_monit_btn.Size = new Size(55, 22);
            stop_monit_btn.TabIndex = 19;
            stop_monit_btn.Text = "Stop";
            stop_monit_btn.UseVisualStyleBackColor = false;
            stop_monit_btn.Click += stop_monit_btn_Click;
            // 
            // uninstall_monit_btn
            // 
            uninstall_monit_btn.BackColor = Color.WhiteSmoke;
            uninstall_monit_btn.FlatStyle = FlatStyle.Flat;
            uninstall_monit_btn.Location = new Point(10, 76);
            uninstall_monit_btn.Margin = new Padding(4, 3, 4, 3);
            uninstall_monit_btn.Name = "uninstall_monit_btn";
            uninstall_monit_btn.Size = new Size(110, 22);
            uninstall_monit_btn.TabIndex = 18;
            uninstall_monit_btn.Text = "Uninstall";
            uninstall_monit_btn.UseVisualStyleBackColor = false;
            uninstall_monit_btn.Click += uninstall_monit_btn_Click;
            // 
            // monitoring_service_state
            // 
            monitoring_service_state.BackColor = SystemColors.ControlLight;
            monitoring_service_state.BorderStyle = BorderStyle.Fixed3D;
            monitoring_service_state.Location = new Point(10, 10);
            monitoring_service_state.Margin = new Padding(4, 3, 4, 3);
            monitoring_service_state.Name = "monitoring_service_state";
            monitoring_service_state.Size = new Size(228, 24);
            monitoring_service_state.TabIndex = 17;
            // 
            // install_monit_btn
            // 
            install_monit_btn.BackColor = Color.WhiteSmoke;
            install_monit_btn.FlatStyle = FlatStyle.Flat;
            install_monit_btn.Location = new Point(10, 44);
            install_monit_btn.Margin = new Padding(4, 3, 4, 3);
            install_monit_btn.Name = "install_monit_btn";
            install_monit_btn.Size = new Size(110, 22);
            install_monit_btn.TabIndex = 14;
            install_monit_btn.Text = "Install";
            install_monit_btn.UseVisualStyleBackColor = false;
            install_monit_btn.Click += install_monit_btn_Click;
            // 
            // monitored_services
            // 
            monitored_services.BackColor = Color.Silver;
            monitored_services.BorderStyle = BorderStyle.Fixed3D;
            monitored_services.Controls.Add(monitoredServicesList);
            monitored_services.Controls.Add(comboBoxServices);
            monitored_services.Controls.Add(del_service_from_list);
            monitored_services.Controls.Add(panelStatus);
            monitored_services.Controls.Add(restart_btn);
            monitored_services.Controls.Add(add_service_to_list);
            monitored_services.Controls.Add(stop_btn);
            monitored_services.Location = new Point(20, 20);
            monitored_services.Margin = new Padding(10, 10, 10, 10);
            monitored_services.Name = "monitored_services";
            monitored_services.Padding = new Padding(5, 5, 5, 5);
            monitored_services.Size = new Size(395, 176);
            monitored_services.TabIndex = 15;
            // 
            // lb_logo
            // 
            lb_logo.BackColor = Color.DimGray;
            lb_logo.BackgroundImageLayout = ImageLayout.Center;
            lb_logo.Location = new Point(20, 215);
            lb_logo.Margin = new Padding(10);
            lb_logo.Name = "lb_logo";
            lb_logo.Size = new Size(665, 48);
            lb_logo.SizeMode = PictureBoxSizeMode.Zoom;
            lb_logo.TabIndex = 16;
            lb_logo.TabStop = false;
            // 
            // ControlPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(704, 280);
            Controls.Add(lb_logo);
            Controls.Add(monitored_services);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(4, 3, 4, 3);
            Name = "ControlPanel";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Service Status Checker - Control Panel";
            TopMost = true;
            Load += ControlPanel_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            monitored_services.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)lb_logo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox comboBoxServices;
        private TextBox port_box;
        private Button restart_btn;
        private Button stop_btn;
        private Button save_btn;
        private ListBox monitoredServicesList;
        private Button add_service_to_list;
        private Button del_service_from_list;
        private Panel panelStatus;
        private Button start_monit_btn;
        private Panel panel1;
        private Panel monitored_services;
        private Button install_monit_btn;
        private Panel monitoring_service_state;
        private Button restart_monit_btn;
        private Button stop_monit_btn;
        private Button uninstall_monit_btn;
        private PictureBox lb_logo;
    }
}
