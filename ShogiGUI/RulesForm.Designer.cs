namespace ShogiGUI
{
    partial class RulesForm
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
            OnDestroy();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RulesForm));
            this.RulesBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // RulesBox
            // 
            this.RulesBox.Font = new System.Drawing.Font("Arial", 10F);
            this.RulesBox.Location = new System.Drawing.Point(10, 0);
            this.RulesBox.Multiline = true;
            this.RulesBox.Name = "RulesBox";
            this.RulesBox.Size = new System.Drawing.Size(750, 750);
            this.RulesBox.TabIndex = 0;
            this.RulesBox.Text = resources.GetString("RulesBox.Text");
            // 
            // RulesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.RulesBox);
            this.Name = "RulesForm";
            this.Text = "Правила";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox RulesBox;
    }
}