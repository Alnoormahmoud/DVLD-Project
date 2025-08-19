namespace DVLD
{
    partial class UCInfoWithFillter
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
            this.components = new System.ComponentModel.Container();
            this.gbFillter = new System.Windows.Forms.GroupBox();
            this.ptnSerch = new System.Windows.Forms.PictureBox();
            this.btnAdd = new System.Windows.Forms.PictureBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ucInfo1 = new DVLD.UCInfo();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbFillter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptnSerch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            this.SuspendLayout();
            // 
            // gbFillter
            // 
            this.gbFillter.Controls.Add(this.ptnSerch);
            this.gbFillter.Controls.Add(this.btnAdd);
            this.gbFillter.Controls.Add(this.cbFilterBy);
            this.gbFillter.Controls.Add(this.txtFilterValue);
            this.gbFillter.Controls.Add(this.label1);
            this.gbFillter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFillter.Location = new System.Drawing.Point(9, 3);
            this.gbFillter.Name = "gbFillter";
            this.gbFillter.Size = new System.Drawing.Size(1198, 110);
            this.gbFillter.TabIndex = 1;
            this.gbFillter.TabStop = false;
            this.gbFillter.Text = "Fillter";
            // 
            // ptnSerch
            // 
            this.ptnSerch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ptnSerch.Image = global::DVLD.Properties.Resources.SearchPerson1;
            this.ptnSerch.InitialImage = null;
            this.ptnSerch.Location = new System.Drawing.Point(644, 33);
            this.ptnSerch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ptnSerch.Name = "ptnSerch";
            this.ptnSerch.Size = new System.Drawing.Size(51, 51);
            this.ptnSerch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptnSerch.TabIndex = 102;
            this.ptnSerch.TabStop = false;
            this.ptnSerch.Click += new System.EventHandler(this.ptnSerch_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Image = global::DVLD.Properties.Resources.Add_Person_72;
            this.btnAdd.InitialImage = null;
            this.btnAdd.Location = new System.Drawing.Point(703, 33);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(51, 51);
            this.btnAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnAdd.TabIndex = 101;
            this.btnAdd.TabStop = false;
            this.btnAdd.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "Person ID",
            "National No."});
            this.cbFilterBy.Location = new System.Drawing.Point(129, 40);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(210, 34);
            this.cbFilterBy.TabIndex = 100;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilterValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterValue.ForeColor = System.Drawing.Color.Red;
            this.txtFilterValue.Location = new System.Drawing.Point(358, 40);
            this.txtFilterValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(256, 30);
            this.txtFilterValue.TabIndex = 99;
            this.txtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterValue_KeyPress);
            this.txtFilterValue.Validating += new System.ComponentModel.CancelEventHandler(this.txtFilterValue_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 29);
            this.label1.TabIndex = 98;
            this.label1.Text = "Filter By:";
            // 
            // ucInfo1
            // 
            this.ucInfo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucInfo1.Location = new System.Drawing.Point(9, 114);
            this.ucInfo1.Name = "ucInfo1";
            this.ucInfo1.Size = new System.Drawing.Size(1198, 533);
            this.ucInfo1.TabIndex = 0;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // UCInfoWithFillter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbFillter);
            this.Controls.Add(this.ucInfo1);
            this.Name = "UCInfoWithFillter";
            this.Size = new System.Drawing.Size(1210, 650);
            this.Load += new System.EventHandler(this.UCInfoWithFillter_Load);
            this.gbFillter.ResumeLayout(false);
            this.gbFillter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptnSerch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UCInfo ucInfo1;
        private System.Windows.Forms.GroupBox gbFillter;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox ptnSerch;
        private System.Windows.Forms.PictureBox btnAdd;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
    }
}
