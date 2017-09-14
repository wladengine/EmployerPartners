namespace EmployerPartners
{
    partial class NewAppVersion
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
            this.label1 = new System.Windows.Forms.Label();
            this.BtnRunNewVersion = new System.Windows.Forms.Button();
            this.btnDontRunNewVersion = new System.Windows.Forms.Button();
            this.btnStopTimer = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(23, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имеется новая версия программы";
            // 
            // BtnRunNewVersion
            // 
            this.BtnRunNewVersion.Location = new System.Drawing.Point(23, 90);
            this.BtnRunNewVersion.Name = "BtnRunNewVersion";
            this.BtnRunNewVersion.Size = new System.Drawing.Size(167, 24);
            this.BtnRunNewVersion.TabIndex = 1;
            this.BtnRunNewVersion.Text = "Запустить новую версию";
            this.BtnRunNewVersion.UseVisualStyleBackColor = true;
            this.BtnRunNewVersion.Click += new System.EventHandler(this.BtnRunNewVersion_Click);
            // 
            // btnDontRunNewVersion
            // 
            this.btnDontRunNewVersion.Location = new System.Drawing.Point(231, 90);
            this.btnDontRunNewVersion.Name = "btnDontRunNewVersion";
            this.btnDontRunNewVersion.Size = new System.Drawing.Size(170, 24);
            this.btnDontRunNewVersion.TabIndex = 2;
            this.btnDontRunNewVersion.Text = "Остаться в текущей версии";
            this.btnDontRunNewVersion.UseVisualStyleBackColor = true;
            this.btnDontRunNewVersion.Click += new System.EventHandler(this.btnDontRunNewVersion_Click);
            // 
            // btnStopTimer
            // 
            this.btnStopTimer.Location = new System.Drawing.Point(435, 90);
            this.btnStopTimer.Name = "btnStopTimer";
            this.btnStopTimer.Size = new System.Drawing.Size(188, 24);
            this.btnStopTimer.TabIndex = 3;
            this.btnStopTimer.Text = "Не выводить это сообщение";
            this.btnStopTimer.UseVisualStyleBackColor = true;
            this.btnStopTimer.Click += new System.EventHandler(this.btnStopTimer_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(290, 26);
            this.label2.TabIndex = 4;
            this.label2.Text = "При следующем запуске программы информирование \r\nо наличии новой версии будет воз" +
    "обновлено.";
            // 
            // NewAppVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 224);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnStopTimer);
            this.Controls.Add(this.btnDontRunNewVersion);
            this.Controls.Add(this.BtnRunNewVersion);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewAppVersion";
            this.Text = "Новая версия";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnRunNewVersion;
        private System.Windows.Forms.Button btnDontRunNewVersion;
        private System.Windows.Forms.Button btnStopTimer;
        private System.Windows.Forms.Label label2;
    }
}