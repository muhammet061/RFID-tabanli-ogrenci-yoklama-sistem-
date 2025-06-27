namespace RFID_YOKLAMA
{
    partial class Form3
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.kisilerDataSet = new RFID_YOKLAMA.kisilerDataSet();
            this.ogrencilistesiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ogrencilistesiTableAdapter = new RFID_YOKLAMA.kisilerDataSetTableAdapters.ogrencilistesiTableAdapter();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.kisilerDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogrencilistesiBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Lime;
            this.button1.Location = new System.Drawing.Point(355, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 52);
            this.button1.TabIndex = 2;
            this.button1.Text = "Bağlan";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(55, 72);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 4;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(209, 72);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 24);
            this.comboBox2.TabIndex = 5;
            // 
            // kisilerDataSet
            // 
            this.kisilerDataSet.DataSetName = "kisilerDataSet";
            this.kisilerDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ogrencilistesiBindingSource
            // 
            this.ogrencilistesiBindingSource.DataMember = "ogrencilistesi";
            this.ogrencilistesiBindingSource.DataSource = this.kisilerDataSet;
            // 
            // ogrencilistesiTableAdapter
            // 
            this.ogrencilistesiTableAdapter.ClearBeforeFill = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(206, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Lütfen Kartı Okutunuz";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(257, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Kartı okutmak için önce arduinoya bağlan !";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(484, 57);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 52);
            this.button2.TabIndex = 8;
            this.button2.Text = "Bağlantıyı Kes";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 398);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kisilerDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogrencilistesiBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private kisilerDataSet kisilerDataSet;
        private System.Windows.Forms.BindingSource ogrencilistesiBindingSource;
        private kisilerDataSetTableAdapters.ogrencilistesiTableAdapter ogrencilistesiTableAdapter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
    }
}