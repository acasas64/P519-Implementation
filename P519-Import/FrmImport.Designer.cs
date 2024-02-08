namespace P519_Import {
   partial class FrmImport {
      /// <summary>
      ///  Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      ///  Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing) {
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      ///  Required method for Designer support - do not modify
      ///  the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         dgvImport = new DataGridView();
         menuStrip1 = new MenuStrip();
         fileToolStripMenuItem = new ToolStripMenuItem();
         importMasterSNToolStripMenuItem = new ToolStripMenuItem();
         importToteSNToolStripMenuItem = new ToolStripMenuItem();
         toolStripSeparator1 = new ToolStripSeparator();
         closeToolStripMenuItem = new ToolStripMenuItem();
         statusStrip1 = new StatusStrip();
         tsslTotalRows = new ToolStripStatusLabel();
         tsslRowsOk = new ToolStripStatusLabel();
         tsslErrors = new ToolStripStatusLabel();
         tsslDummy = new ToolStripStatusLabel();
         tspbProgress = new ToolStripProgressBar();
         ((System.ComponentModel.ISupportInitialize)dgvImport).BeginInit();
         menuStrip1.SuspendLayout();
         statusStrip1.SuspendLayout();
         SuspendLayout();
         // 
         // dgvImport
         // 
         dgvImport.AllowUserToAddRows = false;
         dgvImport.AllowUserToDeleteRows = false;
         dgvImport.AllowUserToOrderColumns = true;
         dgvImport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         dgvImport.Dock = DockStyle.Fill;
         dgvImport.Location = new Point(0, 29);
         dgvImport.Name = "dgvImport";
         dgvImport.ReadOnly = true;
         dgvImport.Size = new Size(872, 500);
         dgvImport.TabIndex = 0;
         dgvImport.CellFormatting += dgvImport_CellFormatting;
         // 
         // menuStrip1
         // 
         menuStrip1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
         menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
         menuStrip1.Location = new Point(0, 0);
         menuStrip1.Name = "menuStrip1";
         menuStrip1.Size = new Size(872, 29);
         menuStrip1.TabIndex = 1;
         menuStrip1.Text = "menuStrip1";
         // 
         // fileToolStripMenuItem
         // 
         fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { importMasterSNToolStripMenuItem, importToteSNToolStripMenuItem, toolStripSeparator1, closeToolStripMenuItem });
         fileToolStripMenuItem.Name = "fileToolStripMenuItem";
         fileToolStripMenuItem.Size = new Size(49, 25);
         fileToolStripMenuItem.Text = "File";
         // 
         // importMasterSNToolStripMenuItem
         // 
         importMasterSNToolStripMenuItem.Name = "importMasterSNToolStripMenuItem";
         importMasterSNToolStripMenuItem.Size = new Size(214, 26);
         importMasterSNToolStripMenuItem.Text = "Import Master SN";
         importMasterSNToolStripMenuItem.Click += importMasterSNToolStripMenuItem_Click;
         // 
         // importToteSNToolStripMenuItem
         // 
         importToteSNToolStripMenuItem.Name = "importToteSNToolStripMenuItem";
         importToteSNToolStripMenuItem.Size = new Size(214, 26);
         importToteSNToolStripMenuItem.Text = "Import Tote SN";
         importToteSNToolStripMenuItem.Click += importToteSNToolStripMenuItem_Click;
         // 
         // toolStripSeparator1
         // 
         toolStripSeparator1.Name = "toolStripSeparator1";
         toolStripSeparator1.Size = new Size(211, 6);
         // 
         // closeToolStripMenuItem
         // 
         closeToolStripMenuItem.Name = "closeToolStripMenuItem";
         closeToolStripMenuItem.Size = new Size(214, 26);
         closeToolStripMenuItem.Text = "Close";
         closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
         // 
         // statusStrip1
         // 
         statusStrip1.Items.AddRange(new ToolStripItem[] { tsslTotalRows, tsslRowsOk, tsslErrors, tsslDummy, tspbProgress });
         statusStrip1.Location = new Point(0, 507);
         statusStrip1.Name = "statusStrip1";
         statusStrip1.Size = new Size(872, 22);
         statusStrip1.TabIndex = 2;
         statusStrip1.Text = "statusStrip1";
         // 
         // tsslTotalRows
         // 
         tsslTotalRows.Name = "tsslTotalRows";
         tsslTotalRows.Size = new Size(117, 17);
         tsslTotalRows.Text = "Total Rows Readed: 0";
         // 
         // tsslRowsOk
         // 
         tsslRowsOk.Name = "tsslRowsOk";
         tsslRowsOk.Size = new Size(66, 17);
         tsslRowsOk.Text = "Rows OK: 0";
         // 
         // tsslErrors
         // 
         tsslErrors.Name = "tsslErrors";
         tsslErrors.Size = new Size(49, 17);
         tsslErrors.Text = "Errors: 0";
         // 
         // tsslDummy
         // 
         tsslDummy.Name = "tsslDummy";
         tsslDummy.Size = new Size(423, 17);
         tsslDummy.Spring = true;
         // 
         // tspbProgress
         // 
         tspbProgress.Alignment = ToolStripItemAlignment.Right;
         tspbProgress.Name = "tspbProgress";
         tspbProgress.Size = new Size(200, 16);
         // 
         // FrmImport
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(872, 529);
         Controls.Add(statusStrip1);
         Controls.Add(dgvImport);
         Controls.Add(menuStrip1);
         MainMenuStrip = menuStrip1;
         Name = "FrmImport";
         Text = "Serials Import From Excel";
         FormClosed += FrmImport_FormClosed;
         ((System.ComponentModel.ISupportInitialize)dgvImport).EndInit();
         menuStrip1.ResumeLayout(false);
         menuStrip1.PerformLayout();
         statusStrip1.ResumeLayout(false);
         statusStrip1.PerformLayout();
         ResumeLayout(false);
         PerformLayout();
      }

      #endregion

      private DataGridView dgvImport;
      private MenuStrip menuStrip1;
      private ToolStripMenuItem fileToolStripMenuItem;
      private ToolStripMenuItem importMasterSNToolStripMenuItem;
      private ToolStripMenuItem importToteSNToolStripMenuItem;
      private ToolStripSeparator toolStripSeparator1;
      private ToolStripMenuItem closeToolStripMenuItem;
      private StatusStrip statusStrip1;
      private ToolStripStatusLabel tsslTotalRows;
      private ToolStripStatusLabel tsslRowsOk;
      private ToolStripStatusLabel tsslErrors;
      private ToolStripProgressBar tspbProgress;
      private ToolStripStatusLabel tsslDummy;
   }
}
