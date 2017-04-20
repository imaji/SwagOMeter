namespace SwagOMeter.Config
{
    partial class ConfigForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.save = new System.Windows.Forms.Button();
            this.swagGrid = new System.Windows.Forms.DataGridView();
            this.company = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Swag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveSwag = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.swagGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 28);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(325, 399);
            this.textBox1.TabIndex = 0;
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(12, 433);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(91, 23);
            this.save.TabIndex = 1;
            this.save.Text = "Save People";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // swagGrid
            // 
            this.swagGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.swagGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.company,
            this.Swag});
            this.swagGrid.Location = new System.Drawing.Point(382, 28);
            this.swagGrid.Name = "swagGrid";
            this.swagGrid.Size = new System.Drawing.Size(370, 399);
            this.swagGrid.TabIndex = 2;
            // 
            // company
            // 
            this.company.HeaderText = "Company";
            this.company.Name = "company";
            // 
            // Swag
            // 
            this.Swag.HeaderText = "Swag";
            this.Swag.Name = "Swag";
            // 
            // saveSwag
            // 
            this.saveSwag.Location = new System.Drawing.Point(382, 433);
            this.saveSwag.Name = "saveSwag";
            this.saveSwag.Size = new System.Drawing.Size(91, 23);
            this.saveSwag.TabIndex = 1;
            this.saveSwag.Text = "Save Swag";
            this.saveSwag.UseVisualStyleBackColor = true;
            this.saveSwag.Click += new System.EventHandler(this.saveSwag_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 468);
            this.Controls.Add(this.swagGrid);
            this.Controls.Add(this.saveSwag);
            this.Controls.Add(this.save);
            this.Controls.Add(this.textBox1);
            this.Name = "ConfigForm";
            this.Text = "ConfigForm";
            this.Load += new System.EventHandler(this.configForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.swagGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.DataGridView swagGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn company;
        private System.Windows.Forms.DataGridViewTextBoxColumn Swag;
        private System.Windows.Forms.Button saveSwag;
    }
}