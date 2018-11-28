﻿namespace CL_Main
{
    partial class UCCANConfig
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCANStart = new System.Windows.Forms.Button();
            this.lbCANK = new System.Windows.Forms.Label();
            this.tbxCANIndex = new System.Windows.Forms.TextBox();
            this.tbxCANName = new System.Windows.Forms.TextBox();
            this.cbxCANBaudRate = new System.Windows.Forms.ComboBox();
            this.lbCANBaudRate = new System.Windows.Forms.Label();
            this.cbxCANMode = new System.Windows.Forms.ComboBox();
            this.lbCANMode = new System.Windows.Forms.Label();
            this.lbCANName = new System.Windows.Forms.Label();
            this.lbCANIndex = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCANStart
            // 
            this.btnCANStart.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCANStart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCANStart.Location = new System.Drawing.Point(303, 190);
            this.btnCANStart.Name = "btnCANStart";
            this.btnCANStart.Size = new System.Drawing.Size(157, 30);
            this.btnCANStart.TabIndex = 19;
            this.btnCANStart.Text = "配置并启动CAN";
            this.btnCANStart.UseVisualStyleBackColor = true;
            // 
            // lbCANK
            // 
            this.lbCANK.AutoSize = true;
            this.lbCANK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCANK.Location = new System.Drawing.Point(218, 113);
            this.lbCANK.Name = "lbCANK";
            this.lbCANK.Size = new System.Drawing.Size(19, 20);
            this.lbCANK.TabIndex = 18;
            this.lbCANK.Text = "K";
            // 
            // tbxCANIndex
            // 
            this.tbxCANIndex.Location = new System.Drawing.Point(91, 9);
            this.tbxCANIndex.Name = "tbxCANIndex";
            this.tbxCANIndex.ReadOnly = true;
            this.tbxCANIndex.Size = new System.Drawing.Size(50, 27);
            this.tbxCANIndex.TabIndex = 17;
            this.tbxCANIndex.Text = "CANx";
            // 
            // tbxCANName
            // 
            this.tbxCANName.Location = new System.Drawing.Point(91, 43);
            this.tbxCANName.Name = "tbxCANName";
            this.tbxCANName.Size = new System.Drawing.Size(200, 27);
            this.tbxCANName.TabIndex = 12;
            // 
            // cbxCANBaudRate
            // 
            this.cbxCANBaudRate.FormattingEnabled = true;
            this.cbxCANBaudRate.Items.AddRange(new object[] {
            "1000",
            "800",
            "666",
            "500",
            "400",
            "250",
            "200",
            "125",
            "100",
            "80",
            "50",
            "40",
            "20",
            "10",
            "5",
            "其他"});
            this.cbxCANBaudRate.Location = new System.Drawing.Point(91, 110);
            this.cbxCANBaudRate.Name = "cbxCANBaudRate";
            this.cbxCANBaudRate.Size = new System.Drawing.Size(121, 28);
            this.cbxCANBaudRate.TabIndex = 16;
            // 
            // lbCANBaudRate
            // 
            this.lbCANBaudRate.AutoSize = true;
            this.lbCANBaudRate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCANBaudRate.Location = new System.Drawing.Point(14, 113);
            this.lbCANBaudRate.Name = "lbCANBaudRate";
            this.lbCANBaudRate.Size = new System.Drawing.Size(69, 20);
            this.lbCANBaudRate.TabIndex = 15;
            this.lbCANBaudRate.Text = "波特率：";
            // 
            // cbxCANMode
            // 
            this.cbxCANMode.FormattingEnabled = true;
            this.cbxCANMode.Location = new System.Drawing.Point(91, 76);
            this.cbxCANMode.Name = "cbxCANMode";
            this.cbxCANMode.Size = new System.Drawing.Size(121, 28);
            this.cbxCANMode.TabIndex = 14;
            // 
            // lbCANMode
            // 
            this.lbCANMode.AutoSize = true;
            this.lbCANMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCANMode.Location = new System.Drawing.Point(14, 79);
            this.lbCANMode.Name = "lbCANMode";
            this.lbCANMode.Size = new System.Drawing.Size(54, 20);
            this.lbCANMode.TabIndex = 13;
            this.lbCANMode.Text = "模式：";
            // 
            // lbCANName
            // 
            this.lbCANName.AutoSize = true;
            this.lbCANName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCANName.Location = new System.Drawing.Point(14, 46);
            this.lbCANName.Name = "lbCANName";
            this.lbCANName.Size = new System.Drawing.Size(54, 20);
            this.lbCANName.TabIndex = 11;
            this.lbCANName.Text = "名称：";
            // 
            // lbCANIndex
            // 
            this.lbCANIndex.AutoSize = true;
            this.lbCANIndex.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCANIndex.Location = new System.Drawing.Point(14, 12);
            this.lbCANIndex.Name = "lbCANIndex";
            this.lbCANIndex.Size = new System.Drawing.Size(54, 20);
            this.lbCANIndex.TabIndex = 10;
            this.lbCANIndex.Text = "通道：";
            // 
            // UCCANConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCANStart);
            this.Controls.Add(this.lbCANK);
            this.Controls.Add(this.tbxCANIndex);
            this.Controls.Add(this.tbxCANName);
            this.Controls.Add(this.cbxCANBaudRate);
            this.Controls.Add(this.lbCANBaudRate);
            this.Controls.Add(this.cbxCANMode);
            this.Controls.Add(this.lbCANMode);
            this.Controls.Add(this.lbCANName);
            this.Controls.Add(this.lbCANIndex);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UCCANConfig";
            this.Size = new System.Drawing.Size(474, 229);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCANStart;
        private System.Windows.Forms.Label lbCANK;
        private System.Windows.Forms.TextBox tbxCANIndex;
        private System.Windows.Forms.TextBox tbxCANName;
        private System.Windows.Forms.ComboBox cbxCANBaudRate;
        private System.Windows.Forms.Label lbCANBaudRate;
        private System.Windows.Forms.ComboBox cbxCANMode;
        private System.Windows.Forms.Label lbCANMode;
        private System.Windows.Forms.Label lbCANName;
        private System.Windows.Forms.Label lbCANIndex;
    }
}
