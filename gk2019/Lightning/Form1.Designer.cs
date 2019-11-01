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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.objectColorGroupbox = new System.Windows.Forms.GroupBox();
            this.contColorRadio = new System.Windows.Forms.RadioButton();
            this.textureColorRadio = new System.Windows.Forms.RadioButton();
            this.normalVectorGroupbox = new System.Windows.Forms.GroupBox();
            this.normalMapRadio = new System.Windows.Forms.RadioButton();
            this.constNormalRadio = new System.Windows.Forms.RadioButton();
            this.fillColorGroupbox = new System.Windows.Forms.GroupBox();
            this.interpolatedFillColorRadio = new System.Windows.Forms.RadioButton();
            this.preciseFillColorRadio = new System.Windows.Forms.RadioButton();
            this.hybridFillColorRadio = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.objectColorGroupbox.SuspendLayout();
            this.normalVectorGroupbox.SuspendLayout();
            this.fillColorGroupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.Controls.Add(this.objectColorGroupbox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.normalVectorGroupbox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.fillColorGroupbox, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // objectColorGroupbox
            // 
            this.objectColorGroupbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectColorGroupbox.Controls.Add(this.textureColorRadio);
            this.objectColorGroupbox.Controls.Add(this.contColorRadio);
            this.objectColorGroupbox.Location = new System.Drawing.Point(503, 3);
            this.objectColorGroupbox.Name = "objectColorGroupbox";
            this.objectColorGroupbox.Size = new System.Drawing.Size(294, 67);
            this.objectColorGroupbox.TabIndex = 0;
            this.objectColorGroupbox.TabStop = false;
            this.objectColorGroupbox.Text = "Object color";
            // 
            // contColorRadio
            // 
            this.contColorRadio.AutoSize = true;
            this.contColorRadio.Location = new System.Drawing.Point(7, 20);
            this.contColorRadio.Name = "contColorRadio";
            this.contColorRadio.Size = new System.Drawing.Size(52, 17);
            this.contColorRadio.TabIndex = 0;
            this.contColorRadio.TabStop = true;
            this.contColorRadio.Text = "Const";
            this.contColorRadio.UseVisualStyleBackColor = true;
            // 
            // textureColorRadio
            // 
            this.textureColorRadio.AutoSize = true;
            this.textureColorRadio.Location = new System.Drawing.Point(7, 44);
            this.textureColorRadio.Name = "textureColorRadio";
            this.textureColorRadio.Size = new System.Drawing.Size(61, 17);
            this.textureColorRadio.TabIndex = 1;
            this.textureColorRadio.TabStop = true;
            this.textureColorRadio.Text = "Texture";
            this.textureColorRadio.UseVisualStyleBackColor = true;
            // 
            // normalVectorGroupbox
            // 
            this.normalVectorGroupbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.normalVectorGroupbox.Controls.Add(this.normalMapRadio);
            this.normalVectorGroupbox.Controls.Add(this.constNormalRadio);
            this.normalVectorGroupbox.Location = new System.Drawing.Point(503, 76);
            this.normalVectorGroupbox.Name = "normalVectorGroupbox";
            this.normalVectorGroupbox.Size = new System.Drawing.Size(294, 67);
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
            this.normalMapRadio.TabStop = true;
            this.normalMapRadio.Text = "Normal Map";
            this.normalMapRadio.UseVisualStyleBackColor = true;
            // 
            // constNormalRadio
            // 
            this.constNormalRadio.AutoSize = true;
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
            this.fillColorGroupbox.Location = new System.Drawing.Point(503, 149);
            this.fillColorGroupbox.Name = "fillColorGroupbox";
            this.fillColorGroupbox.Size = new System.Drawing.Size(294, 92);
            this.fillColorGroupbox.TabIndex = 2;
            this.fillColorGroupbox.TabStop = false;
            this.fillColorGroupbox.Text = "Fill color";
            // 
            // interpolatedFillColorRadio
            // 
            this.interpolatedFillColorRadio.AutoSize = true;
            this.interpolatedFillColorRadio.Location = new System.Drawing.Point(7, 44);
            this.interpolatedFillColorRadio.Name = "interpolatedFillColorRadio";
            this.interpolatedFillColorRadio.Size = new System.Drawing.Size(81, 17);
            this.interpolatedFillColorRadio.TabIndex = 1;
            this.interpolatedFillColorRadio.TabStop = true;
            this.interpolatedFillColorRadio.Text = "Interpolated";
            this.interpolatedFillColorRadio.UseVisualStyleBackColor = true;
            // 
            // preciseFillColorRadio
            // 
            this.preciseFillColorRadio.AutoSize = true;
            this.preciseFillColorRadio.Location = new System.Drawing.Point(7, 20);
            this.preciseFillColorRadio.Name = "preciseFillColorRadio";
            this.preciseFillColorRadio.Size = new System.Drawing.Size(60, 17);
            this.preciseFillColorRadio.TabIndex = 0;
            this.preciseFillColorRadio.TabStop = true;
            this.preciseFillColorRadio.Text = "Precise";
            this.preciseFillColorRadio.UseVisualStyleBackColor = true;
            // 
            // hybridFillColorRadio
            // 
            this.hybridFillColorRadio.AutoSize = true;
            this.hybridFillColorRadio.Location = new System.Drawing.Point(7, 67);
            this.hybridFillColorRadio.Name = "hybridFillColorRadio";
            this.hybridFillColorRadio.Size = new System.Drawing.Size(55, 17);
            this.hybridFillColorRadio.TabIndex = 2;
            this.hybridFillColorRadio.TabStop = true;
            this.hybridFillColorRadio.Text = "Hybrid";
            this.hybridFillColorRadio.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.objectColorGroupbox.ResumeLayout(false);
            this.objectColorGroupbox.PerformLayout();
            this.normalVectorGroupbox.ResumeLayout(false);
            this.normalVectorGroupbox.PerformLayout();
            this.fillColorGroupbox.ResumeLayout(false);
            this.fillColorGroupbox.PerformLayout();
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
    }
}

