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
            this.tableLayoutPanel1.SuspendLayout();
            this.objectColorGroupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.Controls.Add(this.objectColorGroupbox, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox objectColorGroupbox;
        private System.Windows.Forms.RadioButton textureColorRadio;
        private System.Windows.Forms.RadioButton contColorRadio;
    }
}

