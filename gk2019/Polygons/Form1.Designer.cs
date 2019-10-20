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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.relationCreator = new System.Windows.Forms.GroupBox();
            this.selectedEdge1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.selectedEdge2 = new System.Windows.Forms.TextBox();
            this.addEqualButton = new System.Windows.Forms.Button();
            this.addParallelButton = new System.Windows.Forms.Button();
            this.remove1 = new System.Windows.Forms.Button();
            this.remove2 = new System.Windows.Forms.Button();
            this.errorLabel = new System.Windows.Forms.Label();
            this.canvasPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.rightPanel.SuspendLayout();
            this.hierarchyGroupbox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.relationCreator.SuspendLayout();
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
            this.canvasPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 725F));
            this.canvasPanel.Size = new System.Drawing.Size(1008, 725);
            this.canvasPanel.TabIndex = 0;
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.White;
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(10, 10);
            this.canvas.Margin = new System.Windows.Forms.Padding(10, 10, 10, 30);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(649, 685);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            // 
            // rightPanel
            // 
            this.rightPanel.ColumnCount = 1;
            this.rightPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.rightPanel.Controls.Add(this.hierarchyGroupbox, 0, 0);
            this.rightPanel.Controls.Add(this.relationCreator, 0, 1);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(672, 3);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.RowCount = 3;
            this.rightPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.3467F));
            this.rightPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.6533F));
            this.rightPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
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
            this.hierarchyGroupbox.Size = new System.Drawing.Size(313, 478);
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
            this.hierarchy.Size = new System.Drawing.Size(301, 453);
            this.hierarchy.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 703);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // relationCreator
            // 
            this.relationCreator.Controls.Add(this.errorLabel);
            this.relationCreator.Controls.Add(this.remove2);
            this.relationCreator.Controls.Add(this.remove1);
            this.relationCreator.Controls.Add(this.addParallelButton);
            this.relationCreator.Controls.Add(this.addEqualButton);
            this.relationCreator.Controls.Add(this.selectedEdge2);
            this.relationCreator.Controls.Add(this.label2);
            this.relationCreator.Controls.Add(this.label1);
            this.relationCreator.Controls.Add(this.selectedEdge1);
            this.relationCreator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relationCreator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.relationCreator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.relationCreator.Location = new System.Drawing.Point(10, 508);
            this.relationCreator.Margin = new System.Windows.Forms.Padding(10);
            this.relationCreator.Name = "relationCreator";
            this.relationCreator.Size = new System.Drawing.Size(313, 180);
            this.relationCreator.TabIndex = 1;
            this.relationCreator.TabStop = false;
            this.relationCreator.Text = "Relation Creator";
            // 
            // selectedEdge1
            // 
            this.selectedEdge1.Enabled = false;
            this.selectedEdge1.Location = new System.Drawing.Point(93, 24);
            this.selectedEdge1.Name = "selectedEdge1";
            this.selectedEdge1.Size = new System.Drawing.Size(189, 20);
            this.selectedEdge1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "First Edge:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Second Edge:";
            // 
            // selectedEdge2
            // 
            this.selectedEdge2.Enabled = false;
            this.selectedEdge2.Location = new System.Drawing.Point(93, 58);
            this.selectedEdge2.Name = "selectedEdge2";
            this.selectedEdge2.Size = new System.Drawing.Size(189, 20);
            this.selectedEdge2.TabIndex = 3;
            // 
            // addEqualButton
            // 
            this.addEqualButton.Location = new System.Drawing.Point(11, 96);
            this.addEqualButton.Name = "addEqualButton";
            this.addEqualButton.Size = new System.Drawing.Size(142, 43);
            this.addEqualButton.TabIndex = 4;
            this.addEqualButton.Text = "Add equal length relation";
            this.addEqualButton.UseVisualStyleBackColor = true;
            // 
            // addParallelButton
            // 
            this.addParallelButton.Location = new System.Drawing.Point(171, 96);
            this.addParallelButton.Name = "addParallelButton";
            this.addParallelButton.Size = new System.Drawing.Size(142, 43);
            this.addParallelButton.TabIndex = 5;
            this.addParallelButton.Text = "Add perpendicular relation";
            this.addParallelButton.UseVisualStyleBackColor = true;
            // 
            // remove1
            // 
            this.remove1.BackgroundImage = global::Polygons.Properties.Resources.buttonX;
            this.remove1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.remove1.Location = new System.Drawing.Point(288, 22);
            this.remove1.Name = "remove1";
            this.remove1.Size = new System.Drawing.Size(23, 23);
            this.remove1.TabIndex = 6;
            this.remove1.UseVisualStyleBackColor = true;
            // 
            // remove2
            // 
            this.remove2.BackgroundImage = global::Polygons.Properties.Resources.buttonX;
            this.remove2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.remove2.Location = new System.Drawing.Point(288, 56);
            this.remove2.Name = "remove2";
            this.remove2.Size = new System.Drawing.Size(23, 23);
            this.remove2.TabIndex = 7;
            this.remove2.UseVisualStyleBackColor = true;
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorLabel.AutoSize = true;
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(13, 152);
            this.errorLabel.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(187, 13);
            this.errorLabel.TabIndex = 8;
            this.errorLabel.Text = "Error - label already in Relation Creator";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1008, 725);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.canvasPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Name = "Form1";
            this.Text = "Polygons Editor";
            this.canvasPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.rightPanel.ResumeLayout(false);
            this.hierarchyGroupbox.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.relationCreator.ResumeLayout(false);
            this.relationCreator.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel canvasPanel;
        private System.Windows.Forms.TableLayoutPanel rightPanel;
        private System.Windows.Forms.GroupBox hierarchyGroupbox;
        private System.Windows.Forms.TreeView hierarchy;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.GroupBox relationCreator;
        private System.Windows.Forms.TextBox selectedEdge1;
        private System.Windows.Forms.Button addParallelButton;
        private System.Windows.Forms.Button addEqualButton;
        private System.Windows.Forms.TextBox selectedEdge2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button remove1;
        private System.Windows.Forms.Button remove2;
        private System.Windows.Forms.Label errorLabel;
    }
}

