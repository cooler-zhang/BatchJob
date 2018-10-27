namespace BatchJob.WindowsService
{
    partial class FormServiceManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnInstallService = new System.Windows.Forms.Button();
            this.btnUnInstallService = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblServiceName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblServiceStatus = new System.Windows.Forms.Label();
            this.pbxServiceStatus = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbxServiceStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInstallService
            // 
            this.btnInstallService.Location = new System.Drawing.Point(12, 12);
            this.btnInstallService.Name = "btnInstallService";
            this.btnInstallService.Size = new System.Drawing.Size(75, 23);
            this.btnInstallService.TabIndex = 0;
            this.btnInstallService.Text = "安装服务";
            this.btnInstallService.UseVisualStyleBackColor = true;
            this.btnInstallService.Click += new System.EventHandler(this.btnInstallService_Click);
            // 
            // btnUnInstallService
            // 
            this.btnUnInstallService.Location = new System.Drawing.Point(93, 12);
            this.btnUnInstallService.Name = "btnUnInstallService";
            this.btnUnInstallService.Size = new System.Drawing.Size(75, 23);
            this.btnUnInstallService.TabIndex = 1;
            this.btnUnInstallService.Text = "卸载服务";
            this.btnUnInstallService.UseVisualStyleBackColor = true;
            this.btnUnInstallService.Click += new System.EventHandler(this.btnUnInstallService_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "服务：";
            // 
            // lblServiceName
            // 
            this.lblServiceName.AutoSize = true;
            this.lblServiceName.Location = new System.Drawing.Point(50, 48);
            this.lblServiceName.Name = "lblServiceName";
            this.lblServiceName.Size = new System.Drawing.Size(29, 12);
            this.lblServiceName.TabIndex = 3;
            this.lblServiceName.Text = "暂无";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "状态：";
            // 
            // lblServiceStatus
            // 
            this.lblServiceStatus.AutoSize = true;
            this.lblServiceStatus.Location = new System.Drawing.Point(50, 71);
            this.lblServiceStatus.Name = "lblServiceStatus";
            this.lblServiceStatus.Size = new System.Drawing.Size(29, 12);
            this.lblServiceStatus.TabIndex = 5;
            this.lblServiceStatus.Text = "暂无";
            // 
            // pbxServiceStatus
            // 
            this.pbxServiceStatus.Location = new System.Drawing.Point(90, 67);
            this.pbxServiceStatus.Name = "pbxServiceStatus";
            this.pbxServiceStatus.Size = new System.Drawing.Size(20, 20);
            this.pbxServiceStatus.TabIndex = 6;
            this.pbxServiceStatus.TabStop = false;
            this.pbxServiceStatus.Click += new System.EventHandler(this.pbxServiceStatus_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormServiceManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 132);
            this.Controls.Add(this.pbxServiceStatus);
            this.Controls.Add(this.lblServiceStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblServiceName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUnInstallService);
            this.Controls.Add(this.btnInstallService);
            this.Name = "FormServiceManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务管理";
            ((System.ComponentModel.ISupportInitialize)(this.pbxServiceStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInstallService;
        private System.Windows.Forms.Button btnUnInstallService;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblServiceName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblServiceStatus;
        private System.Windows.Forms.PictureBox pbxServiceStatus;
        private System.Windows.Forms.Timer timer1;
    }
}