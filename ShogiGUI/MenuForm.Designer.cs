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
            this.SuspendLayout();
            // 
            // ToTheGameButton
            // 
            this.ToTheGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ToTheGameButton.Location = new System.Drawing.Point(12, 21);
            this.ToTheGameButton.Name = "ToTheGameButton";
            this.ToTheGameButton.Size = new System.Drawing.Size(212, 36);
            this.ToTheGameButton.TabIndex = 0;
            this.ToTheGameButton.Text = "Игрок (1 окно)";
            this.ToTheGameButton.UseVisualStyleBackColor = true;
            // 
            // RulesButton
            // 
            this.RulesButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RulesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.RulesButton.Location = new System.Drawing.Point(12, 117);
            this.RulesButton.Name = "RulesButton";
            this.RulesButton.Size = new System.Drawing.Size(212, 36);
            this.RulesButton.TabIndex = 1;
            this.RulesButton.Text = "Правила";
            this.RulesButton.UseVisualStyleBackColor = true;
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(236, 165);
            this.Controls.Add(this.RulesButton);
            this.Controls.Add(this.ToTheGameButton);
            this.Name = "MenuForm";
            this.Text = "Меню";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ToTheGameButton;
        private System.Windows.Forms.Button RulesButton;
    }
}