namespace EmployerPartners 
{
    partial class ListOrganizations
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnAddPartner = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbOwnership = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbActivityGoal = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbNationalityAffilation = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbCountry = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbRegion = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbActivityArea = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(15, 125);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(1108, 393);
            this.dgv.TabIndex = 0;
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(1006, 524);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(117, 24);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Открыть";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnAddPartner
            // 
            this.btnAddPartner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPartner.Location = new System.Drawing.Point(883, 95);
            this.btnAddPartner.Name = "btnAddPartner";
            this.btnAddPartner.Size = new System.Drawing.Size(117, 24);
            this.btnAddPartner.TabIndex = 1;
            this.btnAddPartner.Text = "Добавить";
            this.btnAddPartner.UseVisualStyleBackColor = true;
            this.btnAddPartner.Click += new System.EventHandler(this.btnAddPartner_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(1006, 95);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(117, 24);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Форма собственности:";
            // 
            // cbOwnership
            // 
            this.cbOwnership.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOwnership.FormattingEnabled = true;
            this.cbOwnership.Location = new System.Drawing.Point(161, 6);
            this.cbOwnership.Name = "cbOwnership";
            this.cbOwnership.Size = new System.Drawing.Size(354, 21);
            this.cbOwnership.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Цель деятельности:";
            // 
            // cbActivityGoal
            // 
            this.cbActivityGoal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActivityGoal.FormattingEnabled = true;
            this.cbActivityGoal.Location = new System.Drawing.Point(161, 33);
            this.cbActivityGoal.Name = "cbActivityGoal";
            this.cbActivityGoal.Size = new System.Drawing.Size(354, 21);
            this.cbActivityGoal.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Национальн. принадлежн.:";
            // 
            // cbNationalityAffilation
            // 
            this.cbNationalityAffilation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNationalityAffilation.FormattingEnabled = true;
            this.cbNationalityAffilation.Location = new System.Drawing.Point(161, 60);
            this.cbNationalityAffilation.Name = "cbNationalityAffilation";
            this.cbNationalityAffilation.Size = new System.Drawing.Size(354, 21);
            this.cbNationalityAffilation.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(571, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Страна:";
            // 
            // cbCountry
            // 
            this.cbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCountry.FormattingEnabled = true;
            this.cbCountry.Location = new System.Drawing.Point(623, 6);
            this.cbCountry.Name = "cbCountry";
            this.cbCountry.Size = new System.Drawing.Size(179, 21);
            this.cbCountry.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(571, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Регион:";
            // 
            // cbRegion
            // 
            this.cbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRegion.FormattingEnabled = true;
            this.cbRegion.Location = new System.Drawing.Point(623, 33);
            this.cbRegion.Name = "cbRegion";
            this.cbRegion.Size = new System.Drawing.Size(179, 21);
            this.cbRegion.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(141, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Осн. сфера деятельности:";
            // 
            // cbActivityArea
            // 
            this.cbActivityArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActivityArea.FormattingEnabled = true;
            this.cbActivityArea.Location = new System.Drawing.Point(161, 87);
            this.cbActivityArea.Name = "cbActivityArea";
            this.cbActivityArea.Size = new System.Drawing.Size(354, 21);
            this.cbActivityArea.TabIndex = 3;
            // 
            // ListOrganizations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 556);
            this.Controls.Add(this.cbRegion);
            this.Controls.Add(this.cbActivityArea);
            this.Controls.Add(this.cbNationalityAffilation);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbCountry);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbActivityGoal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbOwnership);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddPartner);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.dgv);
            this.Name = "ListOrganizations";
            this.Text = "ListOrganizations";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnAddPartner;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbOwnership;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbActivityGoal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbNationalityAffilation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbCountry;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbRegion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbActivityArea;
    }
}