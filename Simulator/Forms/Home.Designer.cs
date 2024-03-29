﻿
namespace Simulator.Forms
{
    partial class Home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.panel1 = new System.Windows.Forms.Panel();
            this.settingsUserControlButton = new System.Windows.Forms.Button();
            this.purchaseUserControlButton = new System.Windows.Forms.Button();
            this.reversalUserControlButton = new System.Windows.Forms.Button();
            this.preAuthUserControlButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.settingsUserControl1 = new Simulator.Forms.SettingsUserControl();
            this.reversalUserControl1 = new Simulator.Forms.ReversalUserControl();
            this.purchaseUserControl1 = new Simulator.Forms.PurchaseUserControl();
            this.preAuthUserControl1 = new Simulator.Forms.PreAuthUserControl();
            this.initialUserControl1 = new Simulator.Forms.InitialUserControl();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.settingsUserControlButton);
            this.panel1.Controls.Add(this.purchaseUserControlButton);
            this.panel1.Controls.Add(this.reversalUserControlButton);
            this.panel1.Controls.Add(this.preAuthUserControlButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 665);
            this.panel1.TabIndex = 0;
            // 
            // settingsUserControlButton
            // 
            this.settingsUserControlButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.settingsUserControlButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsUserControlButton.Location = new System.Drawing.Point(43, 471);
            this.settingsUserControlButton.Name = "settingsUserControlButton";
            this.settingsUserControlButton.Size = new System.Drawing.Size(185, 44);
            this.settingsUserControlButton.TabIndex = 5;
            this.settingsUserControlButton.Text = "Settings";
            this.settingsUserControlButton.UseVisualStyleBackColor = false;
            this.settingsUserControlButton.Click += new System.EventHandler(this.settingsUserControlButton_Click);
            // 
            // purchaseUserControlButton
            // 
            this.purchaseUserControlButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.purchaseUserControlButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.purchaseUserControlButton.Location = new System.Drawing.Point(43, 121);
            this.purchaseUserControlButton.Name = "purchaseUserControlButton";
            this.purchaseUserControlButton.Size = new System.Drawing.Size(185, 44);
            this.purchaseUserControlButton.TabIndex = 1;
            this.purchaseUserControlButton.Text = "Purchase";
            this.purchaseUserControlButton.UseVisualStyleBackColor = false;
            this.purchaseUserControlButton.Click += new System.EventHandler(this.purchaseUserControlButton_Click);
            // 
            // reversalUserControlButton
            // 
            this.reversalUserControlButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.reversalUserControlButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reversalUserControlButton.Location = new System.Drawing.Point(43, 298);
            this.reversalUserControlButton.Name = "reversalUserControlButton";
            this.reversalUserControlButton.Size = new System.Drawing.Size(185, 44);
            this.reversalUserControlButton.TabIndex = 4;
            this.reversalUserControlButton.Text = "Reversal";
            this.reversalUserControlButton.UseVisualStyleBackColor = false;
            this.reversalUserControlButton.Click += new System.EventHandler(this.reversalUserControlButton_Click);
            // 
            // preAuthUserControlButton
            // 
            this.preAuthUserControlButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.preAuthUserControlButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preAuthUserControlButton.Location = new System.Drawing.Point(43, 211);
            this.preAuthUserControlButton.Name = "preAuthUserControlButton";
            this.preAuthUserControlButton.Size = new System.Drawing.Size(185, 44);
            this.preAuthUserControlButton.TabIndex = 2;
            this.preAuthUserControlButton.Text = "Pre-Auth";
            this.preAuthUserControlButton.UseVisualStyleBackColor = false;
            this.preAuthUserControlButton.Click += new System.EventHandler(this.preAuthUserControlButton_Click);
            // 
            // panel2
            // 
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel2.Controls.Add(this.initialUserControl1);
            this.panel2.Controls.Add(this.settingsUserControl1);
            this.panel2.Controls.Add(this.reversalUserControl1);
            this.panel2.Controls.Add(this.purchaseUserControl1);
            this.panel2.Controls.Add(this.preAuthUserControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(274, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(880, 665);
            this.panel2.TabIndex = 1;
            // 
            // settingsUserControl1
            // 
            this.settingsUserControl1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.settingsUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsUserControl1.Location = new System.Drawing.Point(0, 0);
            this.settingsUserControl1.Name = "settingsUserControl1";
            this.settingsUserControl1.Size = new System.Drawing.Size(880, 665);
            this.settingsUserControl1.TabIndex = 5;
            // 
            // reversalUserControl1
            // 
            this.reversalUserControl1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.reversalUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reversalUserControl1.Location = new System.Drawing.Point(0, 0);
            this.reversalUserControl1.Name = "reversalUserControl1";
            this.reversalUserControl1.Size = new System.Drawing.Size(880, 665);
            this.reversalUserControl1.TabIndex = 4;
            // 
            // purchaseUserControl1
            // 
            this.purchaseUserControl1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.purchaseUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.purchaseUserControl1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.purchaseUserControl1.Location = new System.Drawing.Point(0, 0);
            this.purchaseUserControl1.Name = "purchaseUserControl1";
            this.purchaseUserControl1.Size = new System.Drawing.Size(880, 665);
            this.purchaseUserControl1.TabIndex = 1;
            // 
            // preAuthUserControl1
            // 
            this.preAuthUserControl1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.preAuthUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preAuthUserControl1.Location = new System.Drawing.Point(0, 0);
            this.preAuthUserControl1.Name = "preAuthUserControl1";
            this.preAuthUserControl1.Size = new System.Drawing.Size(880, 665);
            this.preAuthUserControl1.TabIndex = 2;
            // 
            // initialUserControl1
            // 
            this.initialUserControl1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.initialUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.initialUserControl1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.initialUserControl1.Location = new System.Drawing.Point(0, 0);
            this.initialUserControl1.Name = "initialUserControl1";
            this.initialUserControl1.Size = new System.Drawing.Size(880, 665);
            this.initialUserControl1.TabIndex = 1;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1154, 665);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OPI Simulator v1.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Home_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button purchaseUserControlButton;
        private System.Windows.Forms.Button reversalUserControlButton;
        private System.Windows.Forms.Button preAuthUserControlButton;
        private System.Windows.Forms.Button settingsUserControlButton;
        private PurchaseUserControl purchaseUserControl1;
        private PreAuthUserControl preAuthUserControl1;
        private InitialUserControl initialUserControl1;
        private ReversalUserControl reversalUserControl1;
        private SettingsUserControl settingsUserControl1;
        private System.Windows.Forms.Panel panel2;
    }
}