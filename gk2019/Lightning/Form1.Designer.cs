namespace Lightning
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
            this.lightGroupbox = new System.Windows.Forms.GroupBox();
            this.circulatingLightRadio = new System.Windows.Forms.RadioButton();
            this.lightColorLabel = new System.Windows.Forms.Label();
            this.lightColorPictureBox = new System.Windows.Forms.PictureBox();
            this.constLightRadio = new System.Windows.Forms.RadioButton();
            this.objectColorGroupbox = new System.Windows.Forms.GroupBox();
            this.objectColorPictureBox = new System.Windows.Forms.PictureBox();
            this.textureColorRadio = new System.Windows.Forms.RadioButton();
            this.constColorRadio = new System.Windows.Forms.RadioButton();
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
            this.coefficientsRandomRadio = new System.Windows.Forms.RadioButton();
            this.userDefinedCoefficientsRadio = new System.Windows.Forms.RadioButton();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.objectColorDialog = new System.Windows.Forms.ColorDialog();
            this.lightColorDialog = new System.Windows.Forms.ColorDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.lightGroupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lightColorPictureBox)).BeginInit();
            this.objectColorGroupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectColorPictureBox)).BeginInit();
            this.normalVectorGroupbox.SuspendLayout();
            this.fillColorGroupbox.SuspendLayout();
            this.coefficientsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ksSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 260F));
            this.tableLayoutPanel1.Controls.Add(this.lightGroupbox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.objectColorGroupbox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.normalVectorGroupbox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.fillColorGroupbox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.coefficientsGroupBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.canvas, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1184, 761);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lightGroupbox
            // 
            this.lightGroupbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lightGroupbox.Controls.Add(this.circulatingLightRadio);
            this.lightGroupbox.Controls.Add(this.lightColorLabel);
            this.lightGroupbox.Controls.Add(this.lightColorPictureBox);
            this.lightGroupbox.Controls.Add(this.constLightRadio);
            this.lightGroupbox.Location = new System.Drawing.Point(929, 450);
            this.lightGroupbox.Margin = new System.Windows.Forms.Padding(5);
            this.lightGroupbox.Name = "lightGroupbox";
            this.lightGroupbox.Size = new System.Drawing.Size(250, 93);
            this.lightGroupbox.TabIndex = 5;
            this.lightGroupbox.TabStop = false;
            this.lightGroupbox.Text = "Light";
            // 
            // circulatingLightRadio
            // 
            this.circulatingLightRadio.AutoSize = true;
            this.circulatingLightRadio.Location = new System.Drawing.Point(6, 70);
            this.circulatingLightRadio.Name = "circulatingLightRadio";
            this.circulatingLightRadio.Size = new System.Drawing.Size(74, 17);
            this.circulatingLightRadio.TabIndex = 4;
            this.circulatingLightRadio.Text = "Circulating";
            this.circulatingLightRadio.UseVisualStyleBackColor = true;
            // 
            // lightColorLabel
            // 
            this.lightColorLabel.AutoSize = true;
            this.lightColorLabel.Location = new System.Drawing.Point(6, 20);
            this.lightColorLabel.Name = "lightColorLabel";
            this.lightColorLabel.Size = new System.Drawing.Size(31, 13);
            this.lightColorLabel.TabIndex = 3;
            this.lightColorLabel.Text = "Color";
            // 
            // lightColorPictureBox
            // 
            this.lightColorPictureBox.BackColor = System.Drawing.Color.White;
            this.lightColorPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lightColorPictureBox.Location = new System.Drawing.Point(65, 16);
            this.lightColorPictureBox.Name = "lightColorPictureBox";
            this.lightColorPictureBox.Size = new System.Drawing.Size(52, 17);
            this.lightColorPictureBox.TabIndex = 2;
            this.lightColorPictureBox.TabStop = false;
            this.lightColorPictureBox.Click += new System.EventHandler(this.lightColorPicturebox_Click);
            // 
            // constLightRadio
            // 
            this.constLightRadio.AutoSize = true;
            this.constLightRadio.Checked = true;
            this.constLightRadio.Location = new System.Drawing.Point(7, 44);
            this.constLightRadio.Name = "constLightRadio";
            this.constLightRadio.Size = new System.Drawing.Size(52, 17);
            this.constLightRadio.TabIndex = 1;
            this.constLightRadio.TabStop = true;
            this.constLightRadio.Text = "Const";
            this.constLightRadio.UseVisualStyleBackColor = true;
            this.constLightRadio.CheckedChanged += new System.EventHandler(this.constLightRadio_CheckedChanged);
            // 
            // objectColorGroupbox
            // 
            this.objectColorGroupbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectColorGroupbox.Controls.Add(this.objectColorPictureBox);
            this.objectColorGroupbox.Controls.Add(this.textureColorRadio);
            this.objectColorGroupbox.Controls.Add(this.constColorRadio);
            this.objectColorGroupbox.Location = new System.Drawing.Point(929, 5);
            this.objectColorGroupbox.Margin = new System.Windows.Forms.Padding(5);
            this.objectColorGroupbox.Name = "objectColorGroupbox";
            this.objectColorGroupbox.Size = new System.Drawing.Size(250, 67);
            this.objectColorGroupbox.TabIndex = 0;
            this.objectColorGroupbox.TabStop = false;
            this.objectColorGroupbox.Text = "Object color";
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
            this.objectColorPictureBox.Click += new System.EventHandler(this.objectColorPictureBox_Click);
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
            // constColorRadio
            // 
            this.constColorRadio.AutoSize = true;
            this.constColorRadio.Checked = true;
            this.constColorRadio.Location = new System.Drawing.Point(7, 20);
            this.constColorRadio.Name = "constColorRadio";
            this.constColorRadio.Size = new System.Drawing.Size(52, 17);
            this.constColorRadio.TabIndex = 0;
            this.constColorRadio.TabStop = true;
            this.constColorRadio.Text = "Const";
            this.constColorRadio.UseVisualStyleBackColor = true;
            this.constColorRadio.CheckedChanged += new System.EventHandler(this.constColorRadio_CheckedChanged);
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
            this.constNormalRadio.CheckedChanged += new System.EventHandler(this.constNormalRadio_CheckedChanged);
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
            this.hybridFillColorRadio.CheckedChanged += new System.EventHandler(this.hybridFillColorRadio_CheckedChanged);
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
            this.interpolatedFillColorRadio.CheckedChanged += new System.EventHandler(this.interpolatedFillColorRadio_CheckedChanged);
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
            this.preciseFillColorRadio.CheckedChanged += new System.EventHandler(this.preciseFillColorRadio_CheckedChanged);
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
            this.coefficientsGroupBox.Controls.Add(this.coefficientsRandomRadio);
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
            this.mTextbox.Location = new System.Drawing.Point(177, 132);
            this.mTextbox.Name = "mTextbox";
            this.mTextbox.ReadOnly = true;
            this.mTextbox.Size = new System.Drawing.Size(63, 20);
            this.mTextbox.TabIndex = 9;
            this.mTextbox.Text = "1";
            // 
            // ksTextbox
            // 
            this.ksTextbox.Location = new System.Drawing.Point(177, 100);
            this.ksTextbox.Name = "ksTextbox";
            this.ksTextbox.ReadOnly = true;
            this.ksTextbox.Size = new System.Drawing.Size(63, 20);
            this.ksTextbox.TabIndex = 8;
            this.ksTextbox.Text = "0";
            // 
            // kdTextbox
            // 
            this.kdTextbox.Location = new System.Drawing.Point(177, 67);
            this.kdTextbox.Name = "kdTextbox";
            this.kdTextbox.ReadOnly = true;
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
            this.mSlider.Maximum = 100;
            this.mSlider.Minimum = 1;
            this.mSlider.Name = "mSlider";
            this.mSlider.Size = new System.Drawing.Size(138, 45);
            this.mSlider.TabIndex = 5;
            this.mSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.mSlider.Value = 1;
            this.mSlider.ValueChanged += new System.EventHandler(this.mSlider_ValueChanged);
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
            this.ksSlider.ValueChanged += new System.EventHandler(this.ksSlider_ValueChanged);
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
            this.kdSlider.ValueChanged += new System.EventHandler(this.kdSlider_ValueChanged);
            // 
            // coefficientsRandomRadio
            // 
            this.coefficientsRandomRadio.AutoSize = true;
            this.coefficientsRandomRadio.Location = new System.Drawing.Point(7, 21);
            this.coefficientsRandomRadio.Name = "coefficientsRandomRadio";
            this.coefficientsRandomRadio.Size = new System.Drawing.Size(65, 17);
            this.coefficientsRandomRadio.TabIndex = 1;
            this.coefficientsRandomRadio.Text = "Random";
            this.coefficientsRandomRadio.UseVisualStyleBackColor = true;
            this.coefficientsRandomRadio.CheckedChanged += new System.EventHandler(this.coefficientsRandomRadio_CheckedChanged);
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
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.White;
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(10, 10);
            this.canvas.Margin = new System.Windows.Forms.Padding(10);
            this.canvas.Name = "canvas";
            this.tableLayoutPanel1.SetRowSpan(this.canvas, 5);
            this.canvas.Size = new System.Drawing.Size(904, 741);
            this.canvas.TabIndex = 4;
            this.canvas.TabStop = false;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            // 
            // objectColorDialog
            // 
            this.objectColorDialog.Color = System.Drawing.Color.White;
            // 
            // lightColorDialog
            // 
            this.lightColorDialog.Color = System.Drawing.Color.White;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(820, 490);
            this.Name = "Form1";
            this.Text = "Lightning";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.lightGroupbox.ResumeLayout(false);
            this.lightGroupbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lightColorPictureBox)).EndInit();
            this.objectColorGroupbox.ResumeLayout(false);
            this.objectColorGroupbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectColorPictureBox)).EndInit();
            this.normalVectorGroupbox.ResumeLayout(false);
            this.normalVectorGroupbox.PerformLayout();
            this.fillColorGroupbox.ResumeLayout(false);
            this.fillColorGroupbox.PerformLayout();
            this.coefficientsGroupBox.ResumeLayout(false);
            this.coefficientsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ksSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox objectColorGroupbox;
        private System.Windows.Forms.RadioButton textureColorRadio;
        private System.Windows.Forms.RadioButton constColorRadio;
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
        private System.Windows.Forms.RadioButton coefficientsRandomRadio;
        private System.Windows.Forms.RadioButton userDefinedCoefficientsRadio;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.PictureBox objectColorPictureBox;
        private System.Windows.Forms.ColorDialog objectColorDialog;
        private System.Windows.Forms.GroupBox lightGroupbox;
        private System.Windows.Forms.RadioButton circulatingLightRadio;
        private System.Windows.Forms.Label lightColorLabel;
        private System.Windows.Forms.PictureBox lightColorPictureBox;
        private System.Windows.Forms.RadioButton constLightRadio;
        private System.Windows.Forms.ColorDialog lightColorDialog;
    }
}

