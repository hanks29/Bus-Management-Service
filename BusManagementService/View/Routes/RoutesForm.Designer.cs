
namespace BusManagementService.View
{
    partial class RoutesForm
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
            this.cbo_TenTuyenDuong = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvroutes = new System.Windows.Forms.DataGridView();
            this.tram = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_tram = new System.Windows.Forms.TextBox();
            this.txt_diadiem = new System.Windows.Forms.TextBox();
            this.txt_quangduong = new System.Windows.Forms.TextBox();
            this.txt_thoigian = new System.Windows.Forms.TextBox();
            this.btnthem = new System.Windows.Forms.Button();
            this.btnsua = new System.Windows.Forms.Button();
            this.btnxoa = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btn_Tim = new System.Windows.Forms.Button();
            this.btn_themRoute = new System.Windows.Forms.Button();
            this.btn_suaRoute = new System.Windows.Forms.Button();
            this.btn_xoaRoute = new System.Windows.Forms.Button();
            this.dgvhocsinh = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvroutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvhocsinh)).BeginInit();
            this.SuspendLayout();
            // 
            // cbo_TenTuyenDuong
            // 
            this.cbo_TenTuyenDuong.FormattingEnabled = true;
            this.cbo_TenTuyenDuong.Location = new System.Drawing.Point(212, 38);
            this.cbo_TenTuyenDuong.Margin = new System.Windows.Forms.Padding(4);
            this.cbo_TenTuyenDuong.Name = "cbo_TenTuyenDuong";
            this.cbo_TenTuyenDuong.Size = new System.Drawing.Size(523, 28);
            this.cbo_TenTuyenDuong.TabIndex = 0;
            this.cbo_TenTuyenDuong.SelectedIndexChanged += new System.EventHandler(this.cbo_TenTuyenDuong_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tên tuyến đường";
            // 
            // dgvroutes
            // 
            this.dgvroutes.AllowUserToAddRows = false;
            this.dgvroutes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvroutes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvroutes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tram,
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvroutes.Location = new System.Drawing.Point(42, 122);
            this.dgvroutes.Margin = new System.Windows.Forms.Padding(5);
            this.dgvroutes.Name = "dgvroutes";
            this.dgvroutes.RowHeadersWidth = 51;
            this.dgvroutes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvroutes.Size = new System.Drawing.Size(830, 283);
            this.dgvroutes.TabIndex = 3;
            this.dgvroutes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvroutes_CellClick);
            // 
            // tram
            // 
            this.tram.HeaderText = "Trạm";
            this.tram.MinimumWidth = 6;
            this.tram.Name = "tram";
            this.tram.Width = 125;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Thời gian";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 400F;
            this.Column2.HeaderText = "Địa điểm";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 400;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Quảng đường";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 125;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(108, 471);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Trạm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(651, 470);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Thời gian";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(81, 550);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Địa điểm";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(620, 549);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Quảng đường";
            // 
            // txt_tram
            // 
            this.txt_tram.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tram.Location = new System.Drawing.Point(165, 468);
            this.txt_tram.Margin = new System.Windows.Forms.Padding(5);
            this.txt_tram.Name = "txt_tram";
            this.txt_tram.Size = new System.Drawing.Size(338, 27);
            this.txt_tram.TabIndex = 8;
            // 
            // txt_diadiem
            // 
            this.txt_diadiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_diadiem.Location = new System.Drawing.Point(165, 546);
            this.txt_diadiem.Margin = new System.Windows.Forms.Padding(5);
            this.txt_diadiem.Name = "txt_diadiem";
            this.txt_diadiem.Size = new System.Drawing.Size(338, 27);
            this.txt_diadiem.TabIndex = 9;
            // 
            // txt_quangduong
            // 
            this.txt_quangduong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_quangduong.Location = new System.Drawing.Point(746, 545);
            this.txt_quangduong.Margin = new System.Windows.Forms.Padding(5);
            this.txt_quangduong.Name = "txt_quangduong";
            this.txt_quangduong.Size = new System.Drawing.Size(338, 27);
            this.txt_quangduong.TabIndex = 10;
            // 
            // txt_thoigian
            // 
            this.txt_thoigian.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_thoigian.Location = new System.Drawing.Point(746, 464);
            this.txt_thoigian.Margin = new System.Windows.Forms.Padding(5);
            this.txt_thoigian.Name = "txt_thoigian";
            this.txt_thoigian.Size = new System.Drawing.Size(338, 27);
            this.txt_thoigian.TabIndex = 11;
            // 
            // btnthem
            // 
            this.btnthem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnthem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnthem.Location = new System.Drawing.Point(1276, 435);
            this.btnthem.Margin = new System.Windows.Forms.Padding(5);
            this.btnthem.Name = "btnthem";
            this.btnthem.Size = new System.Drawing.Size(150, 60);
            this.btnthem.TabIndex = 12;
            this.btnthem.Text = "Thêm";
            this.btnthem.UseVisualStyleBackColor = false;
            this.btnthem.Click += new System.EventHandler(this.btnthem_Click);
            // 
            // btnsua
            // 
            this.btnsua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnsua.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsua.Location = new System.Drawing.Point(1276, 528);
            this.btnsua.Margin = new System.Windows.Forms.Padding(5);
            this.btnsua.Name = "btnsua";
            this.btnsua.Size = new System.Drawing.Size(150, 60);
            this.btnsua.TabIndex = 13;
            this.btnsua.Text = "Sửa";
            this.btnsua.UseVisualStyleBackColor = false;
            this.btnsua.Click += new System.EventHandler(this.btnsua_Click);
            // 
            // btnxoa
            // 
            this.btnxoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnxoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnxoa.Location = new System.Drawing.Point(1276, 618);
            this.btnxoa.Margin = new System.Windows.Forms.Padding(5);
            this.btnxoa.Name = "btnxoa";
            this.btnxoa.Size = new System.Drawing.Size(150, 60);
            this.btnxoa.TabIndex = 14;
            this.btnxoa.Text = "Xóa";
            this.btnxoa.UseVisualStyleBackColor = false;
            this.btnxoa.Click += new System.EventHandler(this.btnxoa_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 639);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(142, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Tìm kiếm theo tên";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(165, 635);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(370, 27);
            this.txtSearch.TabIndex = 18;
            // 
            // btn_Tim
            // 
            this.btn_Tim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_Tim.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Tim.Location = new System.Drawing.Point(591, 625);
            this.btn_Tim.Margin = new System.Windows.Forms.Padding(5);
            this.btn_Tim.Name = "btn_Tim";
            this.btn_Tim.Size = new System.Drawing.Size(137, 49);
            this.btn_Tim.TabIndex = 19;
            this.btn_Tim.Text = "Tìm";
            this.btn_Tim.UseVisualStyleBackColor = false;
            this.btn_Tim.Click += new System.EventHandler(this.btn_Tim_Click);
            // 
            // btn_themRoute
            // 
            this.btn_themRoute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_themRoute.Location = new System.Drawing.Point(951, 30);
            this.btn_themRoute.Margin = new System.Windows.Forms.Padding(5);
            this.btn_themRoute.Name = "btn_themRoute";
            this.btn_themRoute.Size = new System.Drawing.Size(133, 42);
            this.btn_themRoute.TabIndex = 20;
            this.btn_themRoute.Text = "Thêm";
            this.btn_themRoute.UseVisualStyleBackColor = false;
            this.btn_themRoute.Click += new System.EventHandler(this.btn_themRoute_Click);
            // 
            // btn_suaRoute
            // 
            this.btn_suaRoute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_suaRoute.Location = new System.Drawing.Point(1133, 30);
            this.btn_suaRoute.Margin = new System.Windows.Forms.Padding(5);
            this.btn_suaRoute.Name = "btn_suaRoute";
            this.btn_suaRoute.Size = new System.Drawing.Size(133, 42);
            this.btn_suaRoute.TabIndex = 21;
            this.btn_suaRoute.Text = "Sửa";
            this.btn_suaRoute.UseVisualStyleBackColor = false;
            this.btn_suaRoute.Click += new System.EventHandler(this.btn_suaRoute_Click);
            // 
            // btn_xoaRoute
            // 
            this.btn_xoaRoute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_xoaRoute.Location = new System.Drawing.Point(1315, 30);
            this.btn_xoaRoute.Margin = new System.Windows.Forms.Padding(5);
            this.btn_xoaRoute.Name = "btn_xoaRoute";
            this.btn_xoaRoute.Size = new System.Drawing.Size(133, 42);
            this.btn_xoaRoute.TabIndex = 22;
            this.btn_xoaRoute.Text = "Xóa";
            this.btn_xoaRoute.UseVisualStyleBackColor = false;
            this.btn_xoaRoute.Click += new System.EventHandler(this.btn_xoaRoute_Click);
            // 
            // dgvhocsinh
            // 
            this.dgvhocsinh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvhocsinh.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3});
            this.dgvhocsinh.Location = new System.Drawing.Point(935, 122);
            this.dgvhocsinh.Margin = new System.Windows.Forms.Padding(5);
            this.dgvhocsinh.Name = "dgvhocsinh";
            this.dgvhocsinh.RowHeadersWidth = 51;
            this.dgvhocsinh.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvhocsinh.Size = new System.Drawing.Size(513, 283);
            this.dgvhocsinh.TabIndex = 23;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Mã học sinh";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 400F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Tên học sinh";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 400;
            // 
            // RoutesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1675, 782);
            this.Controls.Add(this.dgvhocsinh);
            this.Controls.Add(this.btn_xoaRoute);
            this.Controls.Add(this.btn_suaRoute);
            this.Controls.Add(this.btn_themRoute);
            this.Controls.Add(this.btn_Tim);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnxoa);
            this.Controls.Add(this.btnsua);
            this.Controls.Add(this.btnthem);
            this.Controls.Add(this.txt_thoigian);
            this.Controls.Add(this.txt_quangduong);
            this.Controls.Add(this.txt_diadiem);
            this.Controls.Add(this.txt_tram);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvroutes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbo_TenTuyenDuong);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RoutesForm";
            this.Text = "Route";
            this.Load += new System.EventHandler(this.RoutesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvroutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvhocsinh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbo_TenTuyenDuong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvroutes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_tram;
        private System.Windows.Forms.TextBox txt_diadiem;
        private System.Windows.Forms.TextBox txt_quangduong;
        private System.Windows.Forms.TextBox txt_thoigian;
        private System.Windows.Forms.Button btnthem;
        private System.Windows.Forms.Button btnsua;
        private System.Windows.Forms.Button btnxoa;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btn_Tim;
        private System.Windows.Forms.DataGridViewTextBoxColumn tram;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Button btn_themRoute;
        private System.Windows.Forms.Button btn_suaRoute;
        private System.Windows.Forms.Button btn_xoaRoute;
        private System.Windows.Forms.DataGridView dgvhocsinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}