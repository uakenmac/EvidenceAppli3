namespace EvidenceAppli
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Comment = new System.Windows.Forms.TextBox();
            this.capture_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.douitu_radioButton = new System.Windows.Forms.RadioButton();
            this.sheetgoto_radioButton = new System.Windows.Forms.RadioButton();
            this.clear_button = new System.Windows.Forms.Button();
            this.captuerTargetInfo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.close_button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rect_radioButton = new System.Windows.Forms.RadioButton();
            this.form_radioButton = new System.Windows.Forms.RadioButton();
            this.select_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.sheetName_textBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Comment
            // 
            this.Comment.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Comment.Location = new System.Drawing.Point(15, 186);
            this.Comment.Multiline = true;
            this.Comment.Name = "Comment";
            this.Comment.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Comment.Size = new System.Drawing.Size(465, 125);
            this.Comment.TabIndex = 0;
            // 
            // capture_button
            // 
            this.capture_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.capture_button.Location = new System.Drawing.Point(264, 323);
            this.capture_button.Name = "capture_button";
            this.capture_button.Size = new System.Drawing.Size(217, 32);
            this.capture_button.TabIndex = 1;
            this.capture_button.Text = "キャプチャ";
            this.capture_button.UseVisualStyleBackColor = true;
            this.capture_button.Click += new System.EventHandler(this.capture_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.douitu_radioButton);
            this.groupBox1.Controls.Add(this.sheetgoto_radioButton);
            this.groupBox1.Location = new System.Drawing.Point(15, 317);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(160, 44);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "挿入先";
            // 
            // douitu_radioButton
            // 
            this.douitu_radioButton.AutoSize = true;
            this.douitu_radioButton.Checked = true;
            this.douitu_radioButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.douitu_radioButton.Location = new System.Drawing.Point(3, 15);
            this.douitu_radioButton.Name = "douitu_radioButton";
            this.douitu_radioButton.Size = new System.Drawing.Size(75, 26);
            this.douitu_radioButton.TabIndex = 10;
            this.douitu_radioButton.TabStop = true;
            this.douitu_radioButton.Text = "同一シート";
            this.douitu_radioButton.UseVisualStyleBackColor = true;
            // 
            // sheetgoto_radioButton
            // 
            this.sheetgoto_radioButton.AutoSize = true;
            this.sheetgoto_radioButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.sheetgoto_radioButton.Location = new System.Drawing.Point(89, 15);
            this.sheetgoto_radioButton.Name = "sheetgoto_radioButton";
            this.sheetgoto_radioButton.Size = new System.Drawing.Size(68, 26);
            this.sheetgoto_radioButton.TabIndex = 11;
            this.sheetgoto_radioButton.TabStop = true;
            this.sheetgoto_radioButton.Text = "シートごと";
            this.sheetgoto_radioButton.UseVisualStyleBackColor = true;
            // 
            // clear_button
            // 
            this.clear_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clear_button.Location = new System.Drawing.Point(405, 56);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(76, 42);
            this.clear_button.TabIndex = 3;
            this.clear_button.Text = "クリア";
            this.clear_button.UseVisualStyleBackColor = true;
            this.clear_button.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // captuerTargetInfo
            // 
            this.captuerTargetInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.captuerTargetInfo.ForeColor = System.Drawing.Color.Red;
            this.captuerTargetInfo.Location = new System.Drawing.Point(3, 53);
            this.captuerTargetInfo.Name = "captuerTargetInfo";
            this.captuerTargetInfo.Size = new System.Drawing.Size(396, 48);
            this.captuerTargetInfo.TabIndex = 6;
            this.captuerTargetInfo.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "キャプチャ対象";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(69, 164);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(106, 16);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "このコメントを残す";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.close_button, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.save_button, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(254, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(223, 34);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // close_button
            // 
            this.close_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.close_button.Location = new System.Drawing.Point(114, 3);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(106, 28);
            this.close_button.TabIndex = 13;
            this.close_button.Text = "保存しないで終了";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // save_button
            // 
            this.save_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.save_button.Location = new System.Drawing.Point(3, 3);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(105, 28);
            this.save_button.TabIndex = 14;
            this.save_button.Text = "保存して終了";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Location = new System.Drawing.Point(7, 364);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(477, 34);
            this.panel2.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "コメント欄";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.05115F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel3.Controls.Add(this.captuerTargetInfo, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.clear_button, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.select_button, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(7, 12);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(484, 101);
            this.tableLayoutPanel3.TabIndex = 20;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(396, 47);
            this.panel4.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rect_radioButton);
            this.groupBox2.Controls.Add(this.form_radioButton);
            this.groupBox2.Location = new System.Drawing.Point(82, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(146, 38);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "タイプ";
            // 
            // rect_radioButton
            // 
            this.rect_radioButton.AutoSize = true;
            this.rect_radioButton.Location = new System.Drawing.Point(72, 19);
            this.rect_radioButton.Name = "rect_radioButton";
            this.rect_radioButton.Size = new System.Drawing.Size(47, 16);
            this.rect_radioButton.TabIndex = 1;
            this.rect_radioButton.Text = "矩形";
            this.rect_radioButton.UseVisualStyleBackColor = true;
            // 
            // form_radioButton
            // 
            this.form_radioButton.AutoSize = true;
            this.form_radioButton.Checked = true;
            this.form_radioButton.Location = new System.Drawing.Point(7, 19);
            this.form_radioButton.Name = "form_radioButton";
            this.form_radioButton.Size = new System.Drawing.Size(59, 16);
            this.form_radioButton.TabIndex = 0;
            this.form_radioButton.TabStop = true;
            this.form_radioButton.Text = "フォーム";
            this.form_radioButton.UseVisualStyleBackColor = true;
            // 
            // select_button
            // 
            this.select_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.select_button.Location = new System.Drawing.Point(405, 3);
            this.select_button.Name = "select_button";
            this.select_button.Size = new System.Drawing.Size(76, 47);
            this.select_button.TabIndex = 8;
            this.select_button.Text = "選択開始";
            this.select_button.UseVisualStyleBackColor = true;
            this.select_button.Click += new System.EventHandler(this.select_button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "シート名";
            // 
            // sheetName_textBox
            // 
            this.sheetName_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sheetName_textBox.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.sheetName_textBox.Location = new System.Drawing.Point(0, 0);
            this.sheetName_textBox.Name = "sheetName_textBox";
            this.sheetName_textBox.Size = new System.Drawing.Size(466, 23);
            this.sheetName_textBox.TabIndex = 22;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.sheetName_textBox);
            this.panel1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel1.Location = new System.Drawing.Point(15, 131);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(466, 27);
            this.panel1.TabIndex = 23;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "png",
            "jpeg",
            "bmp",
            "gif",
            "emf",
            "tiff",
            "wmf"});
            this.comboBox1.Location = new System.Drawing.Point(183, 336);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(75, 20);
            this.comboBox1.TabIndex = 24;
            this.comboBox1.Text = "png";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(181, 317);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "画像保存形式";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 404);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Comment);
            this.Controls.Add(this.capture_button);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.checkBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(510, 442);
            this.MinimumSize = new System.Drawing.Size(510, 442);
            this.Name = "MainForm";
            this.Text = "EvidenceAppli (5課内β版)";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button capture_button;
        private System.Windows.Forms.TextBox Comment;
        private System.Windows.Forms.Button clear_button;
        private System.Windows.Forms.Label captuerTargetInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton douitu_radioButton;
        private System.Windows.Forms.RadioButton sheetgoto_radioButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rect_radioButton;
        private System.Windows.Forms.RadioButton form_radioButton;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button select_button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox sheetName_textBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
    }
}

