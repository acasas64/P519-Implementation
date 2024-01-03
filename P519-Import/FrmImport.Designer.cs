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
         ((System.ComponentModel.ISupportInitialize)dgvImport).BeginInit();
         SuspendLayout();
         // 
         // dgvImport
         // 
         dgvImport.AllowUserToAddRows = false;
         dgvImport.AllowUserToDeleteRows = false;
         dgvImport.AllowUserToOrderColumns = true;
         dgvImport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         dgvImport.Dock = DockStyle.Fill;
         dgvImport.Location = new Point(0, 0);
         dgvImport.Name = "dgvImport";
         dgvImport.ReadOnly = true;
         dgvImport.Size = new Size(872, 529);
         dgvImport.TabIndex = 0;
         // 
         // FrmImport
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(872, 529);
         Controls.Add(dgvImport);
         Name = "FrmImport";
         Text = "Serials Import From Excel";
         Load += FrmImport_Load;
         ((System.ComponentModel.ISupportInitialize)dgvImport).EndInit();
         ResumeLayout(false);
      }

      #endregion

      private DataGridView dgvImport;
   }
}
