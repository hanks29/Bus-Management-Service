namespace BusManagementService.View
{
    partial class Drivers
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
            this.dgvdrives = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdrives)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvdrives
            // 
            this.dgvdrives.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvdrives.Location = new System.Drawing.Point(12, 202);
            this.dgvdrives.Name = "dgvdrives";
            this.dgvdrives.Size = new System.Drawing.Size(651, 367);
            this.dgvdrives.TabIndex = 0;
            // 
            // Drives
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 581);
            this.Controls.Add(this.dgvdrives);
            this.Name = "Drives";
            this.Text = "Drives";
            ((System.ComponentModel.ISupportInitialize)(this.dgvdrives)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvdrives;
    }
}