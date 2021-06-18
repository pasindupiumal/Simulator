
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.settingsUserControlButton = new System.Windows.Forms.Button();
            this.purchaseUserControlButton = new System.Windows.Forms.Button();
            this.reversalUserControlButton = new System.Windows.Forms.Button();
            this.preAuthUserControlButton = new System.Windows.Forms.Button();
            this.refundUserControlButton = new System.Windows.Forms.Button();
            this.settingsUserControl1 = new Simulator.Forms.SettingsUserControl();
            this.reversalUserControl1 = new Simulator.Forms.ReversalUserControl();
            this.refundUserControl1 = new Simulator.Forms.RefundUserControl();
            this.preAuthUserControl1 = new Simulator.Forms.PreAuthUserControl();
            this.purchaseUserControl1 = new Simulator.Forms.PurchaseUserControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.settingsUserControlButton);
            this.panel1.Controls.Add(this.purchaseUserControlButton);
            this.panel1.Controls.Add(this.reversalUserControlButton);
            this.panel1.Controls.Add(this.preAuthUserControlButton);
            this.panel1.Controls.Add(this.refundUserControlButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 665);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // settingsUserControlButton
            // 
            this.settingsUserControlButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsUserControlButton.Location = new System.Drawing.Point(43, 471);
            this.settingsUserControlButton.Name = "settingsUserControlButton";
            this.settingsUserControlButton.Size = new System.Drawing.Size(185, 44);
            this.settingsUserControlButton.TabIndex = 5;
            this.settingsUserControlButton.Text = "Settings";
            this.settingsUserControlButton.UseVisualStyleBackColor = true;
            this.settingsUserControlButton.Click += new System.EventHandler(this.settingsUserControlButton_Click);
            // 
            // purchaseUserControlButton
            // 
            this.purchaseUserControlButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.purchaseUserControlButton.Location = new System.Drawing.Point(43, 121);
            this.purchaseUserControlButton.Name = "purchaseUserControlButton";
            this.purchaseUserControlButton.Size = new System.Drawing.Size(185, 44);
            this.purchaseUserControlButton.TabIndex = 1;
            this.purchaseUserControlButton.Text = "Purchase";
            this.purchaseUserControlButton.UseVisualStyleBackColor = true;
            this.purchaseUserControlButton.Click += new System.EventHandler(this.purchaseUserControlButton_Click);
            // 
            // reversalUserControlButton
            // 
            this.reversalUserControlButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reversalUserControlButton.Location = new System.Drawing.Point(43, 386);
            this.reversalUserControlButton.Name = "reversalUserControlButton";
            this.reversalUserControlButton.Size = new System.Drawing.Size(185, 44);
            this.reversalUserControlButton.TabIndex = 4;
            this.reversalUserControlButton.Text = "Reversal";
            this.reversalUserControlButton.UseVisualStyleBackColor = true;
            this.reversalUserControlButton.Click += new System.EventHandler(this.reversalUserControlButton_Click);
            // 
            // preAuthUserControlButton
            // 
            this.preAuthUserControlButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preAuthUserControlButton.Location = new System.Drawing.Point(43, 211);
            this.preAuthUserControlButton.Name = "preAuthUserControlButton";
            this.preAuthUserControlButton.Size = new System.Drawing.Size(185, 44);
            this.preAuthUserControlButton.TabIndex = 2;
            this.preAuthUserControlButton.Text = "Pre-Auth";
            this.preAuthUserControlButton.UseVisualStyleBackColor = true;
            this.preAuthUserControlButton.Click += new System.EventHandler(this.preAuthUserControlButton_Click);
            // 
            // refundUserControlButton
            // 
            this.refundUserControlButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refundUserControlButton.Location = new System.Drawing.Point(43, 300);
            this.refundUserControlButton.Name = "refundUserControlButton";
            this.refundUserControlButton.Size = new System.Drawing.Size(185, 44);
            this.refundUserControlButton.TabIndex = 3;
            this.refundUserControlButton.Text = "Refund";
            this.refundUserControlButton.UseVisualStyleBackColor = true;
            this.refundUserControlButton.Click += new System.EventHandler(this.refundUserControlButton_Click);
            // 
            // settingsUserControl1
            // 
            this.settingsUserControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.settingsUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsUserControl1.Location = new System.Drawing.Point(0, 0);
            this.settingsUserControl1.Name = "settingsUserControl1";
            this.settingsUserControl1.Size = new System.Drawing.Size(878, 631);
            this.settingsUserControl1.TabIndex = 5;
            this.settingsUserControl1.Load += new System.EventHandler(this.settingsUserControl1_Load);
            // 
            // reversalUserControl1
            // 
            this.reversalUserControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.reversalUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reversalUserControl1.Location = new System.Drawing.Point(0, 0);
            this.reversalUserControl1.Name = "reversalUserControl1";
            this.reversalUserControl1.Size = new System.Drawing.Size(878, 631);
            this.reversalUserControl1.TabIndex = 4;
            this.reversalUserControl1.Load += new System.EventHandler(this.reversalUserControl1_Load);
            // 
            // refundUserControl1
            // 
            this.refundUserControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.refundUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.refundUserControl1.Location = new System.Drawing.Point(0, 0);
            this.refundUserControl1.Name = "refundUserControl1";
            this.refundUserControl1.Size = new System.Drawing.Size(878, 631);
            this.refundUserControl1.TabIndex = 3;
            // 
            // preAuthUserControl1
            // 
            this.preAuthUserControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.preAuthUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preAuthUserControl1.Location = new System.Drawing.Point(0, 0);
            this.preAuthUserControl1.Name = "preAuthUserControl1";
            this.preAuthUserControl1.Size = new System.Drawing.Size(878, 631);
            this.preAuthUserControl1.TabIndex = 2;
            this.preAuthUserControl1.Load += new System.EventHandler(this.preAuthUserControl1_Load);
            // 
            // purchaseUserControl1
            // 
            this.purchaseUserControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.purchaseUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.purchaseUserControl1.Location = new System.Drawing.Point(0, 0);
            this.purchaseUserControl1.Name = "purchaseUserControl1";
            this.purchaseUserControl1.Size = new System.Drawing.Size(878, 631);
            this.purchaseUserControl1.TabIndex = 1;
            this.purchaseUserControl1.Load += new System.EventHandler(this.purchaseUserControl1_Load);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.settingsUserControl1);
            this.panel2.Controls.Add(this.reversalUserControl1);
            this.panel2.Controls.Add(this.refundUserControl1);
            this.panel2.Controls.Add(this.purchaseUserControl1);
            this.panel2.Controls.Add(this.preAuthUserControl1);
            this.panel2.Location = new System.Drawing.Point(274, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(880, 633);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.progressBar1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(274, 634);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(880, 31);
            this.panel3.TabIndex = 2;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(628, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(247, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 665);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Home_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button purchaseUserControlButton;
        private System.Windows.Forms.Button reversalUserControlButton;
        private System.Windows.Forms.Button preAuthUserControlButton;
        private System.Windows.Forms.Button refundUserControlButton;
        private System.Windows.Forms.Button settingsUserControlButton;
        private PurchaseUserControl purchaseUserControl1;
        private PreAuthUserControl preAuthUserControl1;
        private RefundUserControl refundUserControl1;
        private ReversalUserControl reversalUserControl1;
        private SettingsUserControl settingsUserControl1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}