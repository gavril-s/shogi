namespace ShogiGUI
{
    partial class GameForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ScreenBoard = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // ScreenBoard
            // 
            this.ScreenBoard.Location = new System.Drawing.Point(207, 12);
            this.ScreenBoard.Name = "ScreenBoard";
            this.ScreenBoard.Size = new System.Drawing.Size(576, 576);
            this.ScreenBoard.TabIndex = 0;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 621);
            this.Controls.Add(this.ScreenBoard);
            this.Name = "GameForm";
            this.Text = "Сёги";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ScreenBoard;
    }
}

