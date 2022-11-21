namespace ShogiGUI
{
    partial class MenuForm
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
            this.ToTheGameButton = new System.Windows.Forms.Button();
            this.RulesButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ToTheGameButton
            // 
            this.ToTheGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.ToTheGameButton.Location = new System.Drawing.Point(125, 140);
            this.ToTheGameButton.Name = "ToTheGameButton";
            this.ToTheGameButton.Size = new System.Drawing.Size(150, 75);
            this.ToTheGameButton.TabIndex = 0;
            this.ToTheGameButton.Text = "К игре";
            this.ToTheGameButton.UseVisualStyleBackColor = true;
            // 
            // RulesButton
            // 
            this.RulesButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RulesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.RulesButton.Location = new System.Drawing.Point(125, 250);
            this.RulesButton.Name = "RulesButton";
            this.RulesButton.Size = new System.Drawing.Size(150, 75);
            this.RulesButton.TabIndex = 1;
            this.RulesButton.Text = "Правила";
            this.RulesButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F);
            this.label1.Location = new System.Drawing.Point(110, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 76);
            this.label1.TabIndex = 2;
            this.label1.Text = "Сёги";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RulesButton);
            this.Controls.Add(this.ToTheGameButton);
            this.Name = "MenuForm";
            this.Text = "Меню";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ToTheGameButton;
        private System.Windows.Forms.Button RulesButton;
        private System.Windows.Forms.Label label1;
    }
}