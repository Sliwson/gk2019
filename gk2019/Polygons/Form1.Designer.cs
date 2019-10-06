namespace Polygons
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
            this.components = new System.ComponentModel.Container();
            this.canvasPanel = new System.Windows.Forms.TableLayoutPanel();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.rightPanel = new System.Windows.Forms.TableLayoutPanel();
            this.hierarchyGroupbox = new System.Windows.Forms.GroupBox();
            this.hierarchy = new System.Windows.Forms.TreeView();
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.canvasPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.rightPanel.SuspendLayout();
            this.hierarchyGroupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // canvasPanel
            // 
            this.canvasPanel.ColumnCount = 2;
            this.canvasPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.36905F));
            this.canvasPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.63095F));
            this.canvasPanel.Controls.Add(this.canvas, 0, 0);
            this.canvasPanel.Controls.Add(this.rightPanel, 1, 0);
            this.canvasPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasPanel.Location = new System.Drawing.Point(0, 0);
            this.canvasPanel.Name = "canvasPanel";
            this.canvasPanel.RowCount = 1;
            this.canvasPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.canvasPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.canvasPanel.Size = new System.Drawing.Size(1008, 725);
            this.canvasPanel.TabIndex = 0;
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.White;
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(10, 10);
            this.canvas.Margin = new System.Windows.Forms.Padding(10);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(649, 705);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            // 
            // rightPanel
            // 
            this.rightPanel.ColumnCount = 1;
            this.rightPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.rightPanel.Controls.Add(this.hierarchyGroupbox, 0, 0);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(672, 3);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.RowCount = 2;
            this.rightPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.rightPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.rightPanel.Size = new System.Drawing.Size(333, 719);
            this.rightPanel.TabIndex = 1;
            // 
            // hierarchyGroupbox
            // 
            this.hierarchyGroupbox.Controls.Add(this.hierarchy);
            this.hierarchyGroupbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hierarchyGroupbox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.hierarchyGroupbox.Location = new System.Drawing.Point(10, 10);
            this.hierarchyGroupbox.Margin = new System.Windows.Forms.Padding(10);
            this.hierarchyGroupbox.Name = "hierarchyGroupbox";
            this.hierarchyGroupbox.Size = new System.Drawing.Size(313, 339);
            this.hierarchyGroupbox.TabIndex = 0;
            this.hierarchyGroupbox.TabStop = false;
            this.hierarchyGroupbox.Text = "Hierarchy";
            // 
            // hierarchy
            // 
            this.hierarchy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hierarchy.Location = new System.Drawing.Point(6, 19);
            this.hierarchy.Name = "hierarchy";
            this.hierarchy.Size = new System.Drawing.Size(301, 330);
            this.hierarchy.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1008, 725);
            this.Controls.Add(this.canvasPanel);
            this.Menu = this.mainMenu;
            this.Name = "Form1";
            this.Text = "Polygons Editor";
            this.canvasPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.rightPanel.ResumeLayout(false);
            this.hierarchyGroupbox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel canvasPanel;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.TableLayoutPanel rightPanel;
        private System.Windows.Forms.GroupBox hierarchyGroupbox;
        private System.Windows.Forms.TreeView hierarchy;
        private System.Windows.Forms.MainMenu mainMenu;
    }
}

