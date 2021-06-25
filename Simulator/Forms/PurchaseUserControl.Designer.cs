
namespace Simulator.Forms
{
    partial class PurchaseUserControl
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
            this.respDetailsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.purchaseButton = new System.Windows.Forms.Button();
            this.currCodesLabel = new System.Windows.Forms.Label();
            this.amountLabel = new System.Windows.Forms.Label();
            this.amountTextBox = new System.Windows.Forms.TextBox();
            this.reqDetailsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.reversalButton = new System.Windows.Forms.Button();
            this.respDetLabel = new System.Windows.Forms.Label();
            this.reqDetLabel = new System.Windows.Forms.Label();
            this.tranDetailsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.urlLabel = new System.Windows.Forms.Label();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.currCodesComboBox = new System.Windows.Forms.ComboBox();
            this.tranDetCopyButton = new System.Windows.Forms.Button();
            this.respDetCopyButton = new System.Windows.Forms.Button();
            this.reqDetCopyButton = new System.Windows.Forms.Button();
            this.tranDetCopyLabel = new System.Windows.Forms.Label();
            this.respDetCopyLabel = new System.Windows.Forms.Label();
            this.reqDetCopyLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // respDetailsRichTextBox
            // 
            this.respDetailsRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.respDetailsRichTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.respDetailsRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.respDetailsRichTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.respDetailsRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.respDetailsRichTextBox.Location = new System.Drawing.Point(455, 388);
            this.respDetailsRichTextBox.Name = "respDetailsRichTextBox";
            this.respDetailsRichTextBox.ReadOnly = true;
            this.respDetailsRichTextBox.Size = new System.Drawing.Size(410, 328);
            this.respDetailsRichTextBox.TabIndex = 15;
            this.respDetailsRichTextBox.Text = "";
            // 
            // purchaseButton
            // 
            this.purchaseButton.Enabled = false;
            this.purchaseButton.Location = new System.Drawing.Point(408, 26);
            this.purchaseButton.Name = "purchaseButton";
            this.purchaseButton.Size = new System.Drawing.Size(170, 33);
            this.purchaseButton.TabIndex = 17;
            this.purchaseButton.Text = "Purchase";
            this.purchaseButton.UseVisualStyleBackColor = true;
            this.purchaseButton.Click += new System.EventHandler(this.purchaseButton_Click);
            // 
            // currCodesLabel
            // 
            this.currCodesLabel.AutoSize = true;
            this.currCodesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currCodesLabel.Location = new System.Drawing.Point(24, 92);
            this.currCodesLabel.Name = "currCodesLabel";
            this.currCodesLabel.Size = new System.Drawing.Size(97, 16);
            this.currCodesLabel.TabIndex = 22;
            this.currCodesLabel.Text = "Currency Code";
            // 
            // amountLabel
            // 
            this.amountLabel.AutoSize = true;
            this.amountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amountLabel.Location = new System.Drawing.Point(24, 55);
            this.amountLabel.Name = "amountLabel";
            this.amountLabel.Size = new System.Drawing.Size(53, 16);
            this.amountLabel.TabIndex = 21;
            this.amountLabel.Text = "Amount";
            // 
            // amountTextBox
            // 
            this.amountTextBox.Location = new System.Drawing.Point(135, 52);
            this.amountTextBox.Multiline = true;
            this.amountTextBox.Name = "amountTextBox";
            this.amountTextBox.Size = new System.Drawing.Size(220, 19);
            this.amountTextBox.TabIndex = 20;
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
            this.reqDetailsRichTextBox.TabIndex = 19;
            this.reqDetailsRichTextBox.Text = "";
            // 
            // reversalButton
            // 
            this.reversalButton.Enabled = false;
            this.reversalButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reversalButton.Location = new System.Drawing.Point(408, 72);
            this.reversalButton.Name = "reversalButton";
            this.reversalButton.Size = new System.Drawing.Size(170, 33);
            this.reversalButton.TabIndex = 18;
            this.reversalButton.Text = "Reversal";
            this.reversalButton.UseVisualStyleBackColor = true;
            this.reversalButton.Click += new System.EventHandler(this.reversalButton_Click);
            // 
            // respDetLabel
            // 
            this.respDetLabel.AutoSize = true;
            this.respDetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.respDetLabel.Location = new System.Drawing.Point(480, 370);
            this.respDetLabel.Name = "respDetLabel";
            this.respDetLabel.Size = new System.Drawing.Size(116, 16);
            this.respDetLabel.TabIndex = 27;
            this.respDetLabel.Text = "Response Details";
            // 
            // reqDetLabel
            // 
            this.reqDetLabel.AutoSize = true;
            this.reqDetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reqDetLabel.Location = new System.Drawing.Point(54, 370);
            this.reqDetLabel.Name = "reqDetLabel";
            this.reqDetLabel.Size = new System.Drawing.Size(104, 16);
            this.reqDetLabel.TabIndex = 26;
            this.reqDetLabel.Text = "Request Details";
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
            this.tranDetailsRichTextBox.TabIndex = 28;
            this.tranDetailsRichTextBox.Text = "";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(615, 742);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(250, 23);
            this.progressBar.TabIndex = 29;
            // 
            // urlLabel
            // 
            this.urlLabel.AutoSize = true;
            this.urlLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.urlLabel.Location = new System.Drawing.Point(24, 16);
            this.urlLabel.Name = "urlLabel";
            this.urlLabel.Size = new System.Drawing.Size(35, 16);
            this.urlLabel.TabIndex = 31;
            this.urlLabel.Text = "URL";
            // 
            // urlTextBox
            // 
            this.urlTextBox.Location = new System.Drawing.Point(135, 12);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(220, 20);
            this.urlTextBox.TabIndex = 32;
            // 
            // currCodesComboBox
            // 
            this.currCodesComboBox.FormattingEnabled = true;
            this.currCodesComboBox.Location = new System.Drawing.Point(135, 91);
            this.currCodesComboBox.Name = "currCodesComboBox";
            this.currCodesComboBox.Size = new System.Drawing.Size(220, 21);
            this.currCodesComboBox.TabIndex = 33;
            // 
            // tranDetCopyButton
            // 
            this.tranDetCopyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tranDetCopyButton.BackColor = System.Drawing.Color.Transparent;
            this.tranDetCopyButton.Image = global::Simulator.Properties.Resources.Copy_Small1;
            this.tranDetCopyButton.Location = new System.Drawing.Point(818, 142);
            this.tranDetCopyButton.Name = "tranDetCopyButton";
            this.tranDetCopyButton.Size = new System.Drawing.Size(25, 19);
            this.tranDetCopyButton.TabIndex = 56;
            this.tranDetCopyButton.UseVisualStyleBackColor = false;
            this.tranDetCopyButton.Click += new System.EventHandler(this.tranDetCopyButton_Click);
            // 
            // respDetCopyButton
            // 
            this.respDetCopyButton.BackColor = System.Drawing.Color.Transparent;
            this.respDetCopyButton.Image = global::Simulator.Properties.Resources.Copy_Small1;
            this.respDetCopyButton.Location = new System.Drawing.Point(454, 369);
            this.respDetCopyButton.Name = "respDetCopyButton";
            this.respDetCopyButton.Size = new System.Drawing.Size(25, 19);
            this.respDetCopyButton.TabIndex = 55;
            this.respDetCopyButton.UseVisualStyleBackColor = false;
            this.respDetCopyButton.Click += new System.EventHandler(this.respDetCopyButton_Click);
            // 
            // reqDetCopyButton
            // 
            this.reqDetCopyButton.BackColor = System.Drawing.Color.Transparent;
            this.reqDetCopyButton.Image = global::Simulator.Properties.Resources.Copy_Small1;
            this.reqDetCopyButton.Location = new System.Drawing.Point(26, 369);
            this.reqDetCopyButton.Name = "reqDetCopyButton";
            this.reqDetCopyButton.Size = new System.Drawing.Size(25, 19);
            this.reqDetCopyButton.TabIndex = 54;
            this.reqDetCopyButton.UseVisualStyleBackColor = false;
            this.reqDetCopyButton.Click += new System.EventHandler(this.reqDetCopyButton_Click);
            // 
            // tranDetCopyLabel
            // 
            this.tranDetCopyLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tranDetCopyLabel.AutoSize = true;
            this.tranDetCopyLabel.BackColor = System.Drawing.SystemColors.Control;
            this.tranDetCopyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tranDetCopyLabel.Location = new System.Drawing.Point(764, 143);
            this.tranDetCopyLabel.Name = "tranDetCopyLabel";
            this.tranDetCopyLabel.Size = new System.Drawing.Size(96, 15);
            this.tranDetCopyLabel.TabIndex = 59;
            this.tranDetCopyLabel.Text = "TransDetCopy";
            this.tranDetCopyLabel.Visible = false;
            // 
            // respDetCopyLabel
            // 
            this.respDetCopyLabel.AutoSize = true;
            this.respDetCopyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.respDetCopyLabel.Location = new System.Drawing.Point(622, 370);
            this.respDetCopyLabel.Name = "respDetCopyLabel";
            this.respDetCopyLabel.Size = new System.Drawing.Size(93, 15);
            this.respDetCopyLabel.TabIndex = 58;
            this.respDetCopyLabel.Text = "RespDetCopy";
            this.respDetCopyLabel.Visible = false;
            // 
            // reqDetCopyLabel
            // 
            this.reqDetCopyLabel.AutoSize = true;
            this.reqDetCopyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reqDetCopyLabel.Location = new System.Drawing.Point(155, 370);
            this.reqDetCopyLabel.Name = "reqDetCopyLabel";
            this.reqDetCopyLabel.Size = new System.Drawing.Size(86, 15);
            this.reqDetCopyLabel.TabIndex = 57;
            this.reqDetCopyLabel.Text = "ReqDetCopy";
            this.reqDetCopyLabel.Visible = false;
            // 
            // PurchaseUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tranDetCopyLabel);
            this.Controls.Add(this.respDetCopyLabel);
            this.Controls.Add(this.reqDetCopyLabel);
            this.Controls.Add(this.tranDetCopyButton);
            this.Controls.Add(this.respDetCopyButton);
            this.Controls.Add(this.reqDetCopyButton);
            this.Controls.Add(this.currCodesComboBox);
            this.Controls.Add(this.urlTextBox);
            this.Controls.Add(this.urlLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.tranDetailsRichTextBox);
            this.Controls.Add(this.respDetailsRichTextBox);
            this.Controls.Add(this.purchaseButton);
            this.Controls.Add(this.currCodesLabel);
            this.Controls.Add(this.amountLabel);
            this.Controls.Add(this.amountTextBox);
            this.Controls.Add(this.reqDetailsRichTextBox);
            this.Controls.Add(this.reversalButton);
            this.Controls.Add(this.respDetLabel);
            this.Controls.Add(this.reqDetLabel);
            this.Name = "PurchaseUserControl";
            this.Size = new System.Drawing.Size(897, 777);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox respDetailsRichTextBox;
        private System.Windows.Forms.Button purchaseButton;
        private System.Windows.Forms.Label currCodesLabel;
        private System.Windows.Forms.Label amountLabel;
        private System.Windows.Forms.TextBox amountTextBox;
        private System.Windows.Forms.RichTextBox reqDetailsRichTextBox;
        private System.Windows.Forms.Button reversalButton;
        private System.Windows.Forms.Label respDetLabel;
        private System.Windows.Forms.Label reqDetLabel;
        private System.Windows.Forms.RichTextBox tranDetailsRichTextBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label urlLabel;
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.ComboBox currCodesComboBox;
        private System.Windows.Forms.Button tranDetCopyButton;
        private System.Windows.Forms.Button respDetCopyButton;
        private System.Windows.Forms.Button reqDetCopyButton;
        private System.Windows.Forms.Label tranDetCopyLabel;
        private System.Windows.Forms.Label respDetCopyLabel;
        private System.Windows.Forms.Label reqDetCopyLabel;
    }
}
