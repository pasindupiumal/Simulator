
namespace Simulator.Forms
{
    partial class PreAuthUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.resDetailsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.preAuthButton = new System.Windows.Forms.Button();
            this.currCodesComboBox = new System.Windows.Forms.ComboBox();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.urlLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.tranDetailsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.currCodeLabel = new System.Windows.Forms.Label();
            this.amountLabel = new System.Windows.Forms.Label();
            this.amountTextBox = new System.Windows.Forms.TextBox();
            this.reqDetailsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.preAuthReversalButton = new System.Windows.Forms.Button();
            this.resDetailsLabel = new System.Windows.Forms.Label();
            this.reqDetailsLabel = new System.Windows.Forms.Label();
            this.incPreAuthButton = new System.Windows.Forms.Button();
            this.preAuthCompButton = new System.Windows.Forms.Button();
            this.preAuthCancelButton = new System.Windows.Forms.Button();
            this.copyTranDetButton = new System.Windows.Forms.Button();
            this.copyResDetButton = new System.Windows.Forms.Button();
            this.copyReqDetButton = new System.Windows.Forms.Button();
            this.reqDelCopyLabel = new System.Windows.Forms.Label();
            this.resDetCopyLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // resDetailsRichTextBox
            // 
            this.resDetailsRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resDetailsRichTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.resDetailsRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.resDetailsRichTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.resDetailsRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resDetailsRichTextBox.Location = new System.Drawing.Point(455, 388);
            this.resDetailsRichTextBox.Name = "resDetailsRichTextBox";
            this.resDetailsRichTextBox.ReadOnly = true;
            this.resDetailsRichTextBox.Size = new System.Drawing.Size(410, 328);
            this.resDetailsRichTextBox.TabIndex = 34;
            this.resDetailsRichTextBox.Text = "";
            // 
            // preAuthButton
            // 
            this.preAuthButton.Enabled = false;
            this.preAuthButton.Location = new System.Drawing.Point(408, 2);
            this.preAuthButton.Name = "preAuthButton";
            this.preAuthButton.Size = new System.Drawing.Size(170, 33);
            this.preAuthButton.TabIndex = 35;
            this.preAuthButton.Text = "Pre-Auth";
            this.preAuthButton.UseVisualStyleBackColor = true;
            this.preAuthButton.Click += new System.EventHandler(this.preAuthButton_Click);
            // 
            // currCodesComboBox
            // 
            this.currCodesComboBox.FormattingEnabled = true;
            this.currCodesComboBox.Location = new System.Drawing.Point(135, 91);
            this.currCodesComboBox.Name = "currCodesComboBox";
            this.currCodesComboBox.Size = new System.Drawing.Size(220, 21);
            this.currCodesComboBox.TabIndex = 47;
            // 
            // urlTextBox
            // 
            this.urlTextBox.Location = new System.Drawing.Point(135, 12);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(220, 20);
            this.urlTextBox.TabIndex = 46;
            // 
            // urlLabel
            // 
            this.urlLabel.AutoSize = true;
            this.urlLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.urlLabel.Location = new System.Drawing.Point(24, 16);
            this.urlLabel.Name = "urlLabel";
            this.urlLabel.Size = new System.Drawing.Size(35, 16);
            this.urlLabel.TabIndex = 45;
            this.urlLabel.Text = "URL";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(615, 742);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(250, 23);
            this.progressBar.TabIndex = 44;
            // 
            // tranDetailsRichTextBox
            // 
            this.tranDetailsRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tranDetailsRichTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.tranDetailsRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tranDetailsRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tranDetailsRichTextBox.Location = new System.Drawing.Point(27, 137);
            this.tranDetailsRichTextBox.Name = "tranDetailsRichTextBox";
            this.tranDetailsRichTextBox.ReadOnly = true;
            this.tranDetailsRichTextBox.Size = new System.Drawing.Size(838, 216);
            this.tranDetailsRichTextBox.TabIndex = 43;
            this.tranDetailsRichTextBox.Text = "";
            // 
            // currCodeLabel
            // 
            this.currCodeLabel.AutoSize = true;
            this.currCodeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currCodeLabel.Location = new System.Drawing.Point(24, 92);
            this.currCodeLabel.Name = "currCodeLabel";
            this.currCodeLabel.Size = new System.Drawing.Size(97, 16);
            this.currCodeLabel.TabIndex = 40;
            this.currCodeLabel.Text = "Currency Code";
            // 
            // amountLabel
            // 
            this.amountLabel.AutoSize = true;
            this.amountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amountLabel.Location = new System.Drawing.Point(24, 55);
            this.amountLabel.Name = "amountLabel";
            this.amountLabel.Size = new System.Drawing.Size(53, 16);
            this.amountLabel.TabIndex = 39;
            this.amountLabel.Text = "Amount";
            // 
            // amountTextBox
            // 
            this.amountTextBox.Location = new System.Drawing.Point(135, 52);
            this.amountTextBox.Multiline = true;
            this.amountTextBox.Name = "amountTextBox";
            this.amountTextBox.Size = new System.Drawing.Size(220, 19);
            this.amountTextBox.TabIndex = 38;
            // 
            // reqDetailsRichTextBox
            // 
            this.reqDetailsRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.reqDetailsRichTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.reqDetailsRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.reqDetailsRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reqDetailsRichTextBox.Location = new System.Drawing.Point(27, 388);
            this.reqDetailsRichTextBox.Name = "reqDetailsRichTextBox";
            this.reqDetailsRichTextBox.ReadOnly = true;
            this.reqDetailsRichTextBox.Size = new System.Drawing.Size(408, 328);
            this.reqDetailsRichTextBox.TabIndex = 37;
            this.reqDetailsRichTextBox.Text = "";
            // 
            // preAuthReversalButton
            // 
            this.preAuthReversalButton.Enabled = false;
            this.preAuthReversalButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preAuthReversalButton.Location = new System.Drawing.Point(629, 87);
            this.preAuthReversalButton.Name = "preAuthReversalButton";
            this.preAuthReversalButton.Size = new System.Drawing.Size(170, 33);
            this.preAuthReversalButton.TabIndex = 36;
            this.preAuthReversalButton.Text = "Reversal";
            this.preAuthReversalButton.UseVisualStyleBackColor = true;
            this.preAuthReversalButton.Click += new System.EventHandler(this.preAuthReversalButton_Click);
            // 
            // resDetailsLabel
            // 
            this.resDetailsLabel.AutoSize = true;
            this.resDetailsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resDetailsLabel.Location = new System.Drawing.Point(453, 369);
            this.resDetailsLabel.Name = "resDetailsLabel";
            this.resDetailsLabel.Size = new System.Drawing.Size(116, 16);
            this.resDetailsLabel.TabIndex = 42;
            this.resDetailsLabel.Text = "Response Details";
            // 
            // reqDetailsLabel
            // 
            this.reqDetailsLabel.AutoSize = true;
            this.reqDetailsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reqDetailsLabel.Location = new System.Drawing.Point(25, 369);
            this.reqDetailsLabel.Name = "reqDetailsLabel";
            this.reqDetailsLabel.Size = new System.Drawing.Size(104, 16);
            this.reqDetailsLabel.TabIndex = 41;
            this.reqDetailsLabel.Text = "Request Details";
            // 
            // incPreAuthButton
            // 
            this.incPreAuthButton.Location = new System.Drawing.Point(408, 44);
            this.incPreAuthButton.Name = "incPreAuthButton";
            this.incPreAuthButton.Size = new System.Drawing.Size(170, 33);
            this.incPreAuthButton.TabIndex = 48;
            this.incPreAuthButton.Text = "Inc. Pre-Auth";
            this.incPreAuthButton.UseVisualStyleBackColor = true;
            this.incPreAuthButton.Click += new System.EventHandler(this.incPreAuthButton_Click);
            // 
            // preAuthCompButton
            // 
            this.preAuthCompButton.Location = new System.Drawing.Point(408, 87);
            this.preAuthCompButton.Name = "preAuthCompButton";
            this.preAuthCompButton.Size = new System.Drawing.Size(170, 33);
            this.preAuthCompButton.TabIndex = 49;
            this.preAuthCompButton.Text = "Pre-Auth Completion";
            this.preAuthCompButton.UseVisualStyleBackColor = true;
            this.preAuthCompButton.Click += new System.EventHandler(this.preAuthCompButton_Click);
            // 
            // preAuthCancelButton
            // 
            this.preAuthCancelButton.Location = new System.Drawing.Point(629, 23);
            this.preAuthCancelButton.Name = "preAuthCancelButton";
            this.preAuthCancelButton.Size = new System.Drawing.Size(170, 33);
            this.preAuthCancelButton.TabIndex = 50;
            this.preAuthCancelButton.Text = "Cancelation";
            this.preAuthCancelButton.UseVisualStyleBackColor = true;
            this.preAuthCancelButton.Visible = false;
            this.preAuthCancelButton.Click += new System.EventHandler(this.preAuthCancelButton_Click);
            // 
            // copyTranDetButton
            // 
            this.copyTranDetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.copyTranDetButton.BackColor = System.Drawing.Color.Transparent;
            this.copyTranDetButton.FlatAppearance.BorderSize = 0;
            this.copyTranDetButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.copyTranDetButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.copyTranDetButton.Image = global::Simulator.Properties.Resources.Copy_Small1;
            this.copyTranDetButton.Location = new System.Drawing.Point(818, 143);
            this.copyTranDetButton.Name = "copyTranDetButton";
            this.copyTranDetButton.Size = new System.Drawing.Size(25, 19);
            this.copyTranDetButton.TabIndex = 53;
            this.copyTranDetButton.UseVisualStyleBackColor = false;
            this.copyTranDetButton.Click += new System.EventHandler(this.tranDetCopyButton_Click);
            // 
            // copyResDetButton
            // 
            this.copyResDetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.copyResDetButton.BackColor = System.Drawing.Color.Transparent;
            this.copyResDetButton.FlatAppearance.BorderSize = 0;
            this.copyResDetButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.copyResDetButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.copyResDetButton.Image = global::Simulator.Properties.Resources.Copy_Small1;
            this.copyResDetButton.Location = new System.Drawing.Point(818, 394);
            this.copyResDetButton.Name = "copyResDetButton";
            this.copyResDetButton.Size = new System.Drawing.Size(25, 19);
            this.copyResDetButton.TabIndex = 52;
            this.copyResDetButton.UseVisualStyleBackColor = false;
            this.copyResDetButton.Click += new System.EventHandler(this.respDetCopyButton_Click);
            // 
            // copyReqDetButton
            // 
            this.copyReqDetButton.BackColor = System.Drawing.Color.Transparent;
            this.copyReqDetButton.FlatAppearance.BorderSize = 0;
            this.copyReqDetButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.copyReqDetButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.copyReqDetButton.Image = global::Simulator.Properties.Resources.Copy_Small1;
            this.copyReqDetButton.Location = new System.Drawing.Point(390, 394);
            this.copyReqDetButton.Name = "copyReqDetButton";
            this.copyReqDetButton.Size = new System.Drawing.Size(25, 19);
            this.copyReqDetButton.TabIndex = 51;
            this.copyReqDetButton.UseVisualStyleBackColor = false;
            this.copyReqDetButton.Click += new System.EventHandler(this.reqDetCopyButton_Click);
            // 
            // reqDelCopyLabel
            // 
            this.reqDelCopyLabel.AutoSize = true;
            this.reqDelCopyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reqDelCopyLabel.Location = new System.Drawing.Point(126, 369);
            this.reqDelCopyLabel.Name = "reqDelCopyLabel";
            this.reqDelCopyLabel.Size = new System.Drawing.Size(86, 15);
            this.reqDelCopyLabel.TabIndex = 54;
            this.reqDelCopyLabel.Text = "ReqDetCopy";
            this.reqDelCopyLabel.Visible = false;
            // 
            // resDetCopyLabel
            // 
            this.resDetCopyLabel.AutoSize = true;
            this.resDetCopyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resDetCopyLabel.Location = new System.Drawing.Point(565, 369);
            this.resDetCopyLabel.Name = "resDetCopyLabel";
            this.resDetCopyLabel.Size = new System.Drawing.Size(85, 15);
            this.resDetCopyLabel.TabIndex = 55;
            this.resDetCopyLabel.Text = "ResDetCopy";
            this.resDetCopyLabel.Visible = false;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(766, 144);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 15);
            this.label8.TabIndex = 56;
            this.label8.Text = "TranDetCopy";
            this.label8.Visible = false;
            // 
            // PreAuthUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.resDetCopyLabel);
            this.Controls.Add(this.reqDelCopyLabel);
            this.Controls.Add(this.copyTranDetButton);
            this.Controls.Add(this.copyResDetButton);
            this.Controls.Add(this.copyReqDetButton);
            this.Controls.Add(this.preAuthCancelButton);
            this.Controls.Add(this.preAuthCompButton);
            this.Controls.Add(this.incPreAuthButton);
            this.Controls.Add(this.resDetailsRichTextBox);
            this.Controls.Add(this.preAuthButton);
            this.Controls.Add(this.currCodesComboBox);
            this.Controls.Add(this.urlTextBox);
            this.Controls.Add(this.urlLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.tranDetailsRichTextBox);
            this.Controls.Add(this.currCodeLabel);
            this.Controls.Add(this.amountLabel);
            this.Controls.Add(this.amountTextBox);
            this.Controls.Add(this.reqDetailsRichTextBox);
            this.Controls.Add(this.preAuthReversalButton);
            this.Controls.Add(this.resDetailsLabel);
            this.Controls.Add(this.reqDetailsLabel);
            this.Name = "PreAuthUserControl";
            this.Size = new System.Drawing.Size(897, 777);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox resDetailsRichTextBox;
        private System.Windows.Forms.Button preAuthButton;
        private System.Windows.Forms.ComboBox currCodesComboBox;
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.Label urlLabel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.RichTextBox tranDetailsRichTextBox;
        private System.Windows.Forms.Label currCodeLabel;
        private System.Windows.Forms.Label amountLabel;
        private System.Windows.Forms.TextBox amountTextBox;
        private System.Windows.Forms.RichTextBox reqDetailsRichTextBox;
        private System.Windows.Forms.Button preAuthReversalButton;
        private System.Windows.Forms.Label resDetailsLabel;
        private System.Windows.Forms.Label reqDetailsLabel;
        private System.Windows.Forms.Button incPreAuthButton;
        private System.Windows.Forms.Button preAuthCompButton;
        private System.Windows.Forms.Button preAuthCancelButton;
        private System.Windows.Forms.Button copyReqDetButton;
        private System.Windows.Forms.Button copyResDetButton;
        private System.Windows.Forms.Button copyTranDetButton;
        private System.Windows.Forms.Label reqDelCopyLabel;
        private System.Windows.Forms.Label resDetCopyLabel;
        private System.Windows.Forms.Label label8;
    }
}
