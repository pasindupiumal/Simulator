
namespace Simulator.Forms
{
    partial class ReversalUserControl
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
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.urlLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.tranDetailsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.respDetailsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.reqDetailsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.reverseLastButton = new System.Windows.Forms.Button();
            this.respDetailsLabel = new System.Windows.Forms.Label();
            this.reqDetailsLabel = new System.Windows.Forms.Label();
            this.transDetCopyButton = new System.Windows.Forms.Button();
            this.restDetCopyButton = new System.Windows.Forms.Button();
            this.reqDetCopyButton = new System.Windows.Forms.Button();
            this.tranDetCopyLabel = new System.Windows.Forms.Label();
            this.respDetCopyLabel = new System.Windows.Forms.Label();
            this.reqDetCopyLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // urlTextBox
            // 
            this.urlTextBox.Location = new System.Drawing.Point(77, 35);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(279, 20);
            this.urlTextBox.TabIndex = 46;
            // 
            // urlLabel
            // 
            this.urlLabel.AutoSize = true;
            this.urlLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.urlLabel.Location = new System.Drawing.Point(24, 37);
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
            this.tranDetailsRichTextBox.Location = new System.Drawing.Point(27, 86);
            this.tranDetailsRichTextBox.Name = "tranDetailsRichTextBox";
            this.tranDetailsRichTextBox.ReadOnly = true;
            this.tranDetailsRichTextBox.Size = new System.Drawing.Size(838, 267);
            this.tranDetailsRichTextBox.TabIndex = 43;
            this.tranDetailsRichTextBox.Text = "";
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
            this.respDetailsRichTextBox.TabIndex = 34;
            this.respDetailsRichTextBox.Text = "";
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
            // reverseLastButton
            // 
            this.reverseLastButton.Enabled = false;
            this.reverseLastButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reverseLastButton.Location = new System.Drawing.Point(403, 28);
            this.reverseLastButton.Name = "reverseLastButton";
            this.reverseLastButton.Size = new System.Drawing.Size(170, 33);
            this.reverseLastButton.TabIndex = 36;
            this.reverseLastButton.Text = "Reverse Last";
            this.reverseLastButton.UseVisualStyleBackColor = true;
            this.reverseLastButton.Click += new System.EventHandler(this.reverseLastButton_Click);
            // 
            // respDetailsLabel
            // 
            this.respDetailsLabel.AutoSize = true;
            this.respDetailsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.respDetailsLabel.Location = new System.Drawing.Point(481, 370);
            this.respDetailsLabel.Name = "respDetailsLabel";
            this.respDetailsLabel.Size = new System.Drawing.Size(116, 16);
            this.respDetailsLabel.TabIndex = 42;
            this.respDetailsLabel.Text = "Response Details";
            // 
            // reqDetailsLabel
            // 
            this.reqDetailsLabel.AutoSize = true;
            this.reqDetailsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reqDetailsLabel.Location = new System.Drawing.Point(54, 370);
            this.reqDetailsLabel.Name = "reqDetailsLabel";
            this.reqDetailsLabel.Size = new System.Drawing.Size(104, 16);
            this.reqDetailsLabel.TabIndex = 41;
            this.reqDetailsLabel.Text = "Request Details";
            // 
            // transDetCopyButton
            // 
            this.transDetCopyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.transDetCopyButton.BackColor = System.Drawing.Color.Transparent;
            this.transDetCopyButton.Image = global::Simulator.Properties.Resources.Copy_Small1;
            this.transDetCopyButton.Location = new System.Drawing.Point(818, 95);
            this.transDetCopyButton.Name = "transDetCopyButton";
            this.transDetCopyButton.Size = new System.Drawing.Size(25, 19);
            this.transDetCopyButton.TabIndex = 56;
            this.transDetCopyButton.UseVisualStyleBackColor = false;
            this.transDetCopyButton.Click += new System.EventHandler(this.transDetCopyButton_Click);
            // 
            // restDetCopyButton
            // 
            this.restDetCopyButton.BackColor = System.Drawing.Color.Transparent;
            this.restDetCopyButton.Image = global::Simulator.Properties.Resources.Copy_Small1;
            this.restDetCopyButton.Location = new System.Drawing.Point(454, 369);
            this.restDetCopyButton.Name = "restDetCopyButton";
            this.restDetCopyButton.Size = new System.Drawing.Size(25, 19);
            this.restDetCopyButton.TabIndex = 55;
            this.restDetCopyButton.UseVisualStyleBackColor = false;
            this.restDetCopyButton.Click += new System.EventHandler(this.respDetCopyButton_Click);
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
            this.tranDetCopyLabel.Location = new System.Drawing.Point(761, 96);
            this.tranDetCopyLabel.Name = "tranDetCopyLabel";
            this.tranDetCopyLabel.Size = new System.Drawing.Size(92, 15);
            this.tranDetCopyLabel.TabIndex = 59;
            this.tranDetCopyLabel.Text = "transDetCopy";
            this.tranDetCopyLabel.Visible = false;
            // 
            // respDetCopyLabel
            // 
            this.respDetCopyLabel.AutoSize = true;
            this.respDetCopyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.respDetCopyLabel.Location = new System.Drawing.Point(593, 369);
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
            this.reqDetCopyLabel.Location = new System.Drawing.Point(155, 369);
            this.reqDetCopyLabel.Name = "reqDetCopyLabel";
            this.reqDetCopyLabel.Size = new System.Drawing.Size(86, 15);
            this.reqDetCopyLabel.TabIndex = 57;
            this.reqDetCopyLabel.Text = "ReqDetCopy";
            this.reqDetCopyLabel.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(610, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 60;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ReversalUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tranDetCopyLabel);
            this.Controls.Add(this.respDetCopyLabel);
            this.Controls.Add(this.reqDetCopyLabel);
            this.Controls.Add(this.transDetCopyButton);
            this.Controls.Add(this.restDetCopyButton);
            this.Controls.Add(this.reqDetCopyButton);
            this.Controls.Add(this.urlTextBox);
            this.Controls.Add(this.urlLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.tranDetailsRichTextBox);
            this.Controls.Add(this.respDetailsRichTextBox);
            this.Controls.Add(this.reqDetailsRichTextBox);
            this.Controls.Add(this.reverseLastButton);
            this.Controls.Add(this.respDetailsLabel);
            this.Controls.Add(this.reqDetailsLabel);
            this.Name = "ReversalUserControl";
            this.Size = new System.Drawing.Size(897, 777);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.Label urlLabel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.RichTextBox tranDetailsRichTextBox;
        private System.Windows.Forms.RichTextBox respDetailsRichTextBox;
        private System.Windows.Forms.RichTextBox reqDetailsRichTextBox;
        private System.Windows.Forms.Button reverseLastButton;
        private System.Windows.Forms.Label respDetailsLabel;
        private System.Windows.Forms.Label reqDetailsLabel;
        private System.Windows.Forms.Button transDetCopyButton;
        private System.Windows.Forms.Button restDetCopyButton;
        private System.Windows.Forms.Button reqDetCopyButton;
        private System.Windows.Forms.Label tranDetCopyLabel;
        private System.Windows.Forms.Label respDetCopyLabel;
        private System.Windows.Forms.Label reqDetCopyLabel;
        private System.Windows.Forms.Button button1;
    }
}
