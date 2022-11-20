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
            Program.GetMenuForm().Show();
            destroyed = true;
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ScreenBoard = new System.Windows.Forms.Panel();
            this.BlackHand = new System.Windows.Forms.Panel();
            this.WhiteHand = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.WhiteHandLabel = new System.Windows.Forms.Label();
            this.BlackHandLabel = new System.Windows.Forms.Label();
            this.HistoryLabel = new System.Windows.Forms.Label();
            this.GameStateLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ScreenBoard
            // 
            this.ScreenBoard.Location = new System.Drawing.Point(312, 60);
            this.ScreenBoard.Name = "ScreenBoard";
            this.ScreenBoard.Size = new System.Drawing.Size(576, 576);
            this.ScreenBoard.TabIndex = 0;
            // 
            // BlackHand
            // 
            this.BlackHand.Location = new System.Drawing.Point(20, 60);
            this.BlackHand.Name = "BlackHand";
            this.BlackHand.Size = new System.Drawing.Size(256, 256);
            this.BlackHand.TabIndex = 1;
            // 
            // WhiteHand
            // 
            this.WhiteHand.Location = new System.Drawing.Point(20, 380);
            this.WhiteHand.Name = "WhiteHand";
            this.WhiteHand.Size = new System.Drawing.Size(256, 256);
            this.WhiteHand.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(935, 336);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 300);
            this.panel3.TabIndex = 4;
            // 
            // WhiteHandLabel
            // 
            this.WhiteHandLabel.AutoSize = true;
            this.WhiteHandLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.WhiteHandLabel.Location = new System.Drawing.Point(20, 364);
            this.WhiteHandLabel.Name = "WhiteHandLabel";
            this.WhiteHandLabel.Size = new System.Drawing.Size(38, 13);
            this.WhiteHandLabel.TabIndex = 5;
            this.WhiteHandLabel.Text = "Сброс";
            this.WhiteHandLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // BlackHandLabel
            // 
            this.BlackHandLabel.AutoSize = true;
            this.BlackHandLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.BlackHandLabel.Location = new System.Drawing.Point(20, 44);
            this.BlackHandLabel.Name = "BlackHandLabel";
            this.BlackHandLabel.Size = new System.Drawing.Size(38, 13);
            this.BlackHandLabel.TabIndex = 6;
            this.BlackHandLabel.Text = "Сброс";
            this.BlackHandLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // HistoryLabel
            // 
            this.HistoryLabel.AutoSize = true;
            this.HistoryLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.HistoryLabel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.HistoryLabel.Location = new System.Drawing.Point(932, 320);
            this.HistoryLabel.Name = "HistoryLabel";
            this.HistoryLabel.Size = new System.Drawing.Size(76, 13);
            this.HistoryLabel.TabIndex = 7;
            this.HistoryLabel.Text = "Запись ходов";
            // 
            // GameStateLabel
            // 
            this.GameStateLabel.AutoSize = true;
            this.GameStateLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.GameStateLabel.Location = new System.Drawing.Point(309, 24);
            this.GameStateLabel.Name = "GameStateLabel";
            this.GameStateLabel.Size = new System.Drawing.Size(0, 13);
            this.GameStateLabel.TabIndex = 8;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.GameStateLabel);
            this.Controls.Add(this.HistoryLabel);
            this.Controls.Add(this.BlackHandLabel);
            this.Controls.Add(this.WhiteHandLabel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.WhiteHand);
            this.Controls.Add(this.BlackHand);
            this.Controls.Add(this.ScreenBoard);
            this.Name = "GameForm";
            this.Text = "Сёги";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ScreenBoard;
        private System.Windows.Forms.Panel BlackHand;
        private System.Windows.Forms.Panel WhiteHand;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label WhiteHandLabel;
        private System.Windows.Forms.Label BlackHandLabel;
        private System.Windows.Forms.Label HistoryLabel;
        private System.Windows.Forms.Label GameStateLabel;
    }
}

