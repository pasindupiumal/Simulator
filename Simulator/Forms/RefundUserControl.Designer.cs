
namespace Simulator.Forms
{
    partial class RefundUserControl
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
            this.currCodeTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // currCodeTextBox
            // 
            this.currCodeTextBox.Location = new System.Drawing.Point(251, 230);
            this.currCodeTextBox.Multiline = true;
            this.currCodeTextBox.Name = "currCodeTextBox";
            this.currCodeTextBox.Size = new System.Drawing.Size(220, 20);
            this.currCodeTextBox.TabIndex = 24;
            this.currCodeTextBox.Text = "752";
            // 
            // RefundUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.currCodeTextBox);
            this.Name = "RefundUserControl";
            this.Size = new System.Drawing.Size(723, 480);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox currCodeTextBox;
    }
}
