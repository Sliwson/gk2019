﻿namespace Lightning
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.objectColorGroupbox = new System.Windows.Forms.GroupBox();
            this.textureColorRadio = new System.Windows.Forms.RadioButton();
            this.contColorRadio = new System.Windows.Forms.RadioButton();
            this.normalVectorGroupbox = new System.Windows.Forms.GroupBox();
            this.normalMapRadio = new System.Windows.Forms.RadioButton();
            this.constNormalRadio = new System.Windows.Forms.RadioButton();
            this.fillColorGroupbox = new System.Windows.Forms.GroupBox();
            this.hybridFillColorRadio = new System.Windows.Forms.RadioButton();
            this.interpolatedFillColorRadio = new System.Windows.Forms.RadioButton();
            this.preciseFillColorRadio = new System.Windows.Forms.RadioButton();
            this.coefficientsGroupBox = new System.Windows.Forms.GroupBox();
            this.mTextbox = new System.Windows.Forms.TextBox();
            this.ksTextbox = new System.Windows.Forms.TextBox();
            this.kdTextbox = new System.Windows.Forms.TextBox();
            this.mLabel = new System.Windows.Forms.Label();
            this.mSlider = new System.Windows.Forms.TrackBar();
            this.ksSlider = new System.Windows.Forms.TrackBar();
            this.ksLabel = new System.Windows.Forms.Label();
            this.kdLabel = new System.Windows.Forms.Label();
            this.kdSlider = new System.Windows.Forms.TrackBar();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.userDefinedCoefficientsRadio = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.objectColorDialog = new System.Windows.Forms.ColorDialog();
            this.objectColorPictureBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.objectColorGroupbox.SuspendLayout();
            this.normalVectorGroupbox.SuspendLayout();
            this.fillColorGroupbox.SuspendLayout();
            this.coefficientsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ksSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectColorPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 260F));
            this.tableLayoutPanel1.Controls.Add(this.objectColorGroupbox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.normalVectorGroupbox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.fillColorGroupbox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.coefficientsGroupBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1184, 661);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // objectColorGroupbox
            // 
            this.objectColorGroupbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectColorGroupbox.Controls.Add(this.objectColorPictureBox);
            this.objectColorGroupbox.Controls.Add(this.textureColorRadio);
            this.objectColorGroupbox.Controls.Add(this.contColorRadio);
            this.objectColorGroupbox.Location = new System.Drawing.Point(929, 5);
            this.objectColorGroupbox.Margin = new System.Windows.Forms.Padding(5);
            this.objectColorGroupbox.Name = "objectColorGroupbox";
            this.objectColorGroupbox.Size = new System.Drawing.Size(250, 67);
            this.objectColorGroupbox.TabIndex = 0;
            this.objectColorGroupbox.TabStop = false;
            this.objectColorGroupbox.Text = "Object color";
            // 
            // textureColorRadio
            // 
            this.textureColorRadio.AutoSize = true;
            this.textureColorRadio.Location = new System.Drawing.Point(7, 44);
            this.textureColorRadio.Name = "textureColorRadio";
            this.textureColorRadio.Size = new System.Drawing.Size(61, 17);
            this.textureColorRadio.TabIndex = 1;
            this.textureColorRadio.Text = "Texture";
            this.textureColorRadio.UseVisualStyleBackColor = true;
            // 
            // contColorRadio
            // 
            this.contColorRadio.AutoSize = true;
            this.contColorRadio.Checked = true;
            this.contColorRadio.Location = new System.Drawing.Point(7, 20);
            this.contColorRadio.Name = "contColorRadio";
            this.contColorRadio.Size = new System.Drawing.Size(52, 17);
            this.contColorRadio.TabIndex = 0;
            this.contColorRadio.TabStop = true;
            this.contColorRadio.Text = "Const";
            this.contColorRadio.UseVisualStyleBackColor = true;
            // 
            // normalVectorGroupbox
            // 
            this.normalVectorGroupbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.normalVectorGroupbox.Controls.Add(this.normalMapRadio);
            this.normalVectorGroupbox.Controls.Add(this.constNormalRadio);
            this.normalVectorGroupbox.Location = new System.Drawing.Point(929, 82);
            this.normalVectorGroupbox.Margin = new System.Windows.Forms.Padding(5);
            this.normalVectorGroupbox.Name = "normalVectorGroupbox";
            this.normalVectorGroupbox.Size = new System.Drawing.Size(250, 67);
            this.normalVectorGroupbox.TabIndex = 1;
            this.normalVectorGroupbox.TabStop = false;
            this.normalVectorGroupbox.Text = "Normal vectors";
            // 
            // normalMapRadio
            // 
            this.normalMapRadio.AutoSize = true;
            this.normalMapRadio.Location = new System.Drawing.Point(7, 44);
            this.normalMapRadio.Name = "normalMapRadio";
            this.normalMapRadio.Size = new System.Drawing.Size(82, 17);
            this.normalMapRadio.TabIndex = 1;
            this.normalMapRadio.Text = "Normal Map";
            this.normalMapRadio.UseVisualStyleBackColor = true;
            // 
            // constNormalRadio
            // 
            this.constNormalRadio.AutoSize = true;
            this.constNormalRadio.Checked = true;
            this.constNormalRadio.Location = new System.Drawing.Point(7, 20);
            this.constNormalRadio.Name = "constNormalRadio";
            this.constNormalRadio.Size = new System.Drawing.Size(91, 17);
            this.constNormalRadio.TabIndex = 0;
            this.constNormalRadio.TabStop = true;
            this.constNormalRadio.Text = "Const [0, 0, 1]";
            this.constNormalRadio.UseVisualStyleBackColor = true;
            // 
            // fillColorGroupbox
            // 
            this.fillColorGroupbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fillColorGroupbox.Controls.Add(this.hybridFillColorRadio);
            this.fillColorGroupbox.Controls.Add(this.interpolatedFillColorRadio);
            this.fillColorGroupbox.Controls.Add(this.preciseFillColorRadio);
            this.fillColorGroupbox.Location = new System.Drawing.Point(929, 159);
            this.fillColorGroupbox.Margin = new System.Windows.Forms.Padding(5);
            this.fillColorGroupbox.Name = "fillColorGroupbox";
            this.fillColorGroupbox.Size = new System.Drawing.Size(250, 92);
            this.fillColorGroupbox.TabIndex = 2;
            this.fillColorGroupbox.TabStop = false;
            this.fillColorGroupbox.Text = "Fill color";
            // 
            // hybridFillColorRadio
            // 
            this.hybridFillColorRadio.AutoSize = true;
            this.hybridFillColorRadio.Location = new System.Drawing.Point(7, 67);
            this.hybridFillColorRadio.Name = "hybridFillColorRadio";
            this.hybridFillColorRadio.Size = new System.Drawing.Size(55, 17);
            this.hybridFillColorRadio.TabIndex = 2;
            this.hybridFillColorRadio.Text = "Hybrid";
            this.hybridFillColorRadio.UseVisualStyleBackColor = true;
            // 
            // interpolatedFillColorRadio
            // 
            this.interpolatedFillColorRadio.AutoSize = true;
            this.interpolatedFillColorRadio.Location = new System.Drawing.Point(7, 44);
            this.interpolatedFillColorRadio.Name = "interpolatedFillColorRadio";
            this.interpolatedFillColorRadio.Size = new System.Drawing.Size(81, 17);
            this.interpolatedFillColorRadio.TabIndex = 1;
            this.interpolatedFillColorRadio.Text = "Interpolated";
            this.interpolatedFillColorRadio.UseVisualStyleBackColor = true;
            // 
            // preciseFillColorRadio
            // 
            this.preciseFillColorRadio.AutoSize = true;
            this.preciseFillColorRadio.Checked = true;
            this.preciseFillColorRadio.Location = new System.Drawing.Point(7, 20);
            this.preciseFillColorRadio.Name = "preciseFillColorRadio";
            this.preciseFillColorRadio.Size = new System.Drawing.Size(60, 17);
            this.preciseFillColorRadio.TabIndex = 0;
            this.preciseFillColorRadio.TabStop = true;
            this.preciseFillColorRadio.Text = "Precise";
            this.preciseFillColorRadio.UseVisualStyleBackColor = true;
            // 
            // coefficientsGroupBox
            // 
            this.coefficientsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.coefficientsGroupBox.Controls.Add(this.mTextbox);
            this.coefficientsGroupBox.Controls.Add(this.ksTextbox);
            this.coefficientsGroupBox.Controls.Add(this.kdTextbox);
            this.coefficientsGroupBox.Controls.Add(this.mLabel);
            this.coefficientsGroupBox.Controls.Add(this.mSlider);
            this.coefficientsGroupBox.Controls.Add(this.ksSlider);
            this.coefficientsGroupBox.Controls.Add(this.ksLabel);
            this.coefficientsGroupBox.Controls.Add(this.kdLabel);
            this.coefficientsGroupBox.Controls.Add(this.kdSlider);
            this.coefficientsGroupBox.Controls.Add(this.radioButton2);
            this.coefficientsGroupBox.Controls.Add(this.userDefinedCoefficientsRadio);
            this.coefficientsGroupBox.Location = new System.Drawing.Point(929, 261);
            this.coefficientsGroupBox.Margin = new System.Windows.Forms.Padding(5);
            this.coefficientsGroupBox.Name = "coefficientsGroupBox";
            this.coefficientsGroupBox.Size = new System.Drawing.Size(250, 179);
            this.coefficientsGroupBox.TabIndex = 3;
            this.coefficientsGroupBox.TabStop = false;
            this.coefficientsGroupBox.Text = "Coefficients";
            // 
            // mTextbox
            // 
            this.mTextbox.Enabled = false;
            this.mTextbox.Location = new System.Drawing.Point(177, 132);
            this.mTextbox.Name = "mTextbox";
            this.mTextbox.Size = new System.Drawing.Size(63, 20);
            this.mTextbox.TabIndex = 9;
            this.mTextbox.Text = "1";
            // 
            // ksTextbox
            // 
            this.ksTextbox.Enabled = false;
            this.ksTextbox.Location = new System.Drawing.Point(177, 100);
            this.ksTextbox.Name = "ksTextbox";
            this.ksTextbox.Size = new System.Drawing.Size(63, 20);
            this.ksTextbox.TabIndex = 8;
            this.ksTextbox.Text = "0";
            // 
            // kdTextbox
            // 
            this.kdTextbox.Enabled = false;
            this.kdTextbox.Location = new System.Drawing.Point(177, 67);
            this.kdTextbox.Name = "kdTextbox";
            this.kdTextbox.Size = new System.Drawing.Size(63, 20);
            this.kdTextbox.TabIndex = 7;
            this.kdTextbox.Text = "0";
            // 
            // mLabel
            // 
            this.mLabel.AutoSize = true;
            this.mLabel.Location = new System.Drawing.Point(7, 132);
            this.mLabel.Name = "mLabel";
            this.mLabel.Size = new System.Drawing.Size(15, 13);
            this.mLabel.TabIndex = 6;
            this.mLabel.Text = "m";
            // 
            // mSlider
            // 
            this.mSlider.Location = new System.Drawing.Point(32, 132);
            this.mSlider.Maximum = 14;
            this.mSlider.Minimum = 1;
            this.mSlider.Name = "mSlider";
            this.mSlider.Size = new System.Drawing.Size(138, 45);
            this.mSlider.TabIndex = 5;
            this.mSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.mSlider.Value = 1;
            // 
            // ksSlider
            // 
            this.ksSlider.Location = new System.Drawing.Point(31, 100);
            this.ksSlider.Maximum = 100;
            this.ksSlider.Minimum = -100;
            this.ksSlider.Name = "ksSlider";
            this.ksSlider.Size = new System.Drawing.Size(138, 45);
            this.ksSlider.TabIndex = 4;
            this.ksSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // ksLabel
            // 
            this.ksLabel.AutoSize = true;
            this.ksLabel.Location = new System.Drawing.Point(7, 99);
            this.ksLabel.Name = "ksLabel";
            this.ksLabel.Size = new System.Drawing.Size(18, 13);
            this.ksLabel.TabIndex = 3;
            this.ksLabel.Text = "ks";
            // 
            // kdLabel
            // 
            this.kdLabel.AutoSize = true;
            this.kdLabel.Location = new System.Drawing.Point(7, 67);
            this.kdLabel.Name = "kdLabel";
            this.kdLabel.Size = new System.Drawing.Size(19, 13);
            this.kdLabel.TabIndex = 2;
            this.kdLabel.Text = "kd";
            // 
            // kdSlider
            // 
            this.kdSlider.Location = new System.Drawing.Point(32, 67);
            this.kdSlider.Maximum = 100;
            this.kdSlider.Minimum = -100;
            this.kdSlider.Name = "kdSlider";
            this.kdSlider.Size = new System.Drawing.Size(138, 45);
            this.kdSlider.TabIndex = 1;
            this.kdSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(7, 21);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(65, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Random";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // userDefinedCoefficientsRadio
            // 
            this.userDefinedCoefficientsRadio.AutoSize = true;
            this.userDefinedCoefficientsRadio.Checked = true;
            this.userDefinedCoefficientsRadio.Location = new System.Drawing.Point(7, 44);
            this.userDefinedCoefficientsRadio.Name = "userDefinedCoefficientsRadio";
            this.userDefinedCoefficientsRadio.Size = new System.Drawing.Size(85, 17);
            this.userDefinedCoefficientsRadio.TabIndex = 0;
            this.userDefinedCoefficientsRadio.TabStop = true;
            this.userDefinedCoefficientsRadio.Text = "User defined";
            this.userDefinedCoefficientsRadio.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(10, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(10);
            this.pictureBox1.Name = "pictureBox1";
            this.tableLayoutPanel1.SetRowSpan(this.pictureBox1, 4);
            this.pictureBox1.Size = new System.Drawing.Size(904, 641);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // objectColorPictureBox
            // 
            this.objectColorPictureBox.BackColor = System.Drawing.Color.White;
            this.objectColorPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.objectColorPictureBox.Location = new System.Drawing.Point(65, 20);
            this.objectColorPictureBox.Name = "objectColorPictureBox";
            this.objectColorPictureBox.Size = new System.Drawing.Size(52, 17);
            this.objectColorPictureBox.TabIndex = 2;
            this.objectColorPictureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(820, 490);
            this.Name = "Form1";
            this.Text = "Lightning";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.objectColorGroupbox.ResumeLayout(false);
            this.objectColorGroupbox.PerformLayout();
            this.normalVectorGroupbox.ResumeLayout(false);
            this.normalVectorGroupbox.PerformLayout();
            this.fillColorGroupbox.ResumeLayout(false);
            this.fillColorGroupbox.PerformLayout();
            this.coefficientsGroupBox.ResumeLayout(false);
            this.coefficientsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ksSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectColorPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox objectColorGroupbox;
        private System.Windows.Forms.RadioButton textureColorRadio;
        private System.Windows.Forms.RadioButton contColorRadio;
        private System.Windows.Forms.GroupBox normalVectorGroupbox;
        private System.Windows.Forms.RadioButton normalMapRadio;
        private System.Windows.Forms.RadioButton constNormalRadio;
        private System.Windows.Forms.GroupBox fillColorGroupbox;
        private System.Windows.Forms.RadioButton hybridFillColorRadio;
        private System.Windows.Forms.RadioButton interpolatedFillColorRadio;
        private System.Windows.Forms.RadioButton preciseFillColorRadio;
        private System.Windows.Forms.GroupBox coefficientsGroupBox;
        private System.Windows.Forms.TextBox mTextbox;
        private System.Windows.Forms.TextBox ksTextbox;
        private System.Windows.Forms.TextBox kdTextbox;
        private System.Windows.Forms.Label mLabel;
        private System.Windows.Forms.TrackBar mSlider;
        private System.Windows.Forms.TrackBar ksSlider;
        private System.Windows.Forms.Label ksLabel;
        private System.Windows.Forms.Label kdLabel;
        private System.Windows.Forms.TrackBar kdSlider;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton userDefinedCoefficientsRadio;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox objectColorPictureBox;
        private System.Windows.Forms.ColorDialog objectColorDialog;
    }
}

