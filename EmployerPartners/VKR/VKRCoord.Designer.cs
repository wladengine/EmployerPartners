namespace EmployerPartners
{
    partial class VKRCoord
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
            this.label5 = new System.Windows.Forms.Label();
            this.cbVKRYear = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvFaculty = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvSection = new System.Windows.Forms.DataGridView();
            this.ColumnDiv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDelSection = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnEditSection = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvCoordinator = new System.Windows.Forms.DataGridView();
            this.ColumnDiv1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDelCoordinator = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnEditCoordinator = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFaculty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSection)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoordinator)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(330, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Год";
            // 
            // cbVKRYear
            // 
            this.cbVKRYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVKRYear.FormattingEnabled = true;
            this.cbVKRYear.Location = new System.Drawing.Point(429, 16);
            this.cbVKRYear.Name = "cbVKRYear";
            this.cbVKRYear.Size = new System.Drawing.Size(121, 21);
            this.cbVKRYear.TabIndex = 18;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1082, 537);
            this.tabControl1.TabIndex = 17;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvFaculty);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.dgvSection);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1074, 511);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Координаторы верхнего уровня";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvFaculty
            // 
            this.dgvFaculty.AllowUserToAddRows = false;
            this.dgvFaculty.AllowUserToDeleteRows = false;
            this.dgvFaculty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFaculty.Location = new System.Drawing.Point(25, 284);
            this.dgvFaculty.Name = "dgvFaculty";
            this.dgvFaculty.ReadOnly = true;
            this.dgvFaculty.Size = new System.Drawing.Size(518, 200);
            this.dgvFaculty.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 259);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Направления \"куста\"";
            // 
            // dgvSection
            // 
            this.dgvSection.AllowUserToAddRows = false;
            this.dgvSection.AllowUserToDeleteRows = false;
            this.dgvSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSection.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDiv,
            this.ColumnDelSection,
            this.ColumnEditSection});
            this.dgvSection.Location = new System.Drawing.Point(25, 42);
            this.dgvSection.Name = "dgvSection";
            this.dgvSection.ReadOnly = true;
            this.dgvSection.Size = new System.Drawing.Size(1030, 200);
            this.dgvSection.TabIndex = 1;
            // 
            // ColumnDiv
            // 
            this.ColumnDiv.Frozen = true;
            this.ColumnDiv.HeaderText = "";
            this.ColumnDiv.Name = "ColumnDiv";
            this.ColumnDiv.ReadOnly = true;
            this.ColumnDiv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnDiv.Width = 10;
            // 
            // ColumnDelSection
            // 
            this.ColumnDelSection.Frozen = true;
            this.ColumnDelSection.HeaderText = "Действие";
            this.ColumnDelSection.Name = "ColumnDelSection";
            this.ColumnDelSection.ReadOnly = true;
            this.ColumnDelSection.Text = "Удалить";
            this.ColumnDelSection.UseColumnTextForButtonValue = true;
            // 
            // ColumnEditSection
            // 
            this.ColumnEditSection.Frozen = true;
            this.ColumnEditSection.HeaderText = "Действие";
            this.ColumnEditSection.Name = "ColumnEditSection";
            this.ColumnEditSection.ReadOnly = true;
            this.ColumnEditSection.Text = "Изменить";
            this.ColumnEditSection.UseColumnTextForButtonValue = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Группы направлений (\"кусты\")";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvCoordinator);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1074, 511);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Координаторы образовательных программ";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvCoordinator
            // 
            this.dgvCoordinator.AllowUserToAddRows = false;
            this.dgvCoordinator.AllowUserToDeleteRows = false;
            this.dgvCoordinator.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCoordinator.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCoordinator.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDiv1,
            this.ColumnDelCoordinator,
            this.ColumnEditCoordinator});
            this.dgvCoordinator.Location = new System.Drawing.Point(25, 42);
            this.dgvCoordinator.Name = "dgvCoordinator";
            this.dgvCoordinator.ReadOnly = true;
            this.dgvCoordinator.Size = new System.Drawing.Size(1030, 441);
            this.dgvCoordinator.TabIndex = 1;
            // 
            // ColumnDiv1
            // 
            this.ColumnDiv1.Frozen = true;
            this.ColumnDiv1.HeaderText = "";
            this.ColumnDiv1.Name = "ColumnDiv1";
            this.ColumnDiv1.ReadOnly = true;
            this.ColumnDiv1.Width = 10;
            // 
            // ColumnDelCoordinator
            // 
            this.ColumnDelCoordinator.Frozen = true;
            this.ColumnDelCoordinator.HeaderText = "Действие";
            this.ColumnDelCoordinator.Name = "ColumnDelCoordinator";
            this.ColumnDelCoordinator.ReadOnly = true;
            this.ColumnDelCoordinator.Text = "Удалить";
            this.ColumnDelCoordinator.UseColumnTextForButtonValue = true;
            // 
            // ColumnEditCoordinator
            // 
            this.ColumnEditCoordinator.Frozen = true;
            this.ColumnEditCoordinator.HeaderText = "Действие";
            this.ColumnEditCoordinator.Name = "ColumnEditCoordinator";
            this.ColumnEditCoordinator.ReadOnly = true;
            this.ColumnEditCoordinator.Text = "Изменить";
            this.ColumnEditCoordinator.UseColumnTextForButtonValue = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Координаторы";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "ВКР: Координаторы";
            // 
            // VKRCoord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 602);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbVKRYear);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Name = "VKRCoord";
            this.Text = "VKRCoord";
            this.Load += new System.EventHandler(this.VKRCoord_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFaculty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSection)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoordinator)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbVKRYear;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvFaculty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvSection;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDiv;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnDelSection;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnEditSection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvCoordinator;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDiv1;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnDelCoordinator;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnEditCoordinator;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}