namespace P519_G2798 {
   partial class FrmG2798 {
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmG2798));
         statusStrip1 = new StatusStrip();
         tsddbCom = new ToolStripDropDownButton();
         tsslUsuario = new ToolStripStatusLabel();
         tsslMensaje = new ToolStripStatusLabel();
         statusStrip1.SuspendLayout();
         SuspendLayout();
         // 
         // statusStrip1
         // 
         statusStrip1.Items.AddRange(new ToolStripItem[] { tsddbCom, tsslUsuario, tsslMensaje });
         statusStrip1.Location = new Point(0, 428);
         statusStrip1.Name = "statusStrip1";
         statusStrip1.Size = new Size(800, 22);
         statusStrip1.TabIndex = 0;
         statusStrip1.Text = "statusStrip1";
         // 
         // tsddbCom
         // 
         tsddbCom.DisplayStyle = ToolStripItemDisplayStyle.Text;
         tsddbCom.Image = (Image)resources.GetObject("tsddbCom.Image");
         tsddbCom.ImageTransparentColor = Color.Magenta;
         tsddbCom.Name = "tsddbCom";
         tsddbCom.Size = new Size(53, 20);
         tsddbCom.Text = "COM?";
         tsddbCom.ToolTipText = "Select a serial port";
         tsddbCom.DropDownItemClicked += tsddbCom_DropDownItemClicked;
         // 
         // tsslUsuario
         // 
         tsslUsuario.Name = "tsslUsuario";
         tsslUsuario.Size = new Size(30, 17);
         tsslUsuario.Text = "User";
         // 
         // tsslMensaje
         // 
         tsslMensaje.Name = "tsslMensaje";
         tsslMensaje.Size = new Size(671, 17);
         tsslMensaje.Spring = true;
         tsslMensaje.Text = "Message";
         // 
         // FrmG2798
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(800, 450);
         Controls.Add(statusStrip1);
         Name = "FrmG2798";
         Text = "Station G2798";
         Load += FrmG2798_Load;
         statusStrip1.ResumeLayout(false);
         statusStrip1.PerformLayout();
         ResumeLayout(false);
         PerformLayout();
      }

      #endregion

      private StatusStrip statusStrip1;
      private ToolStripDropDownButton tsddbCom;
      private ToolStripStatusLabel tsslUsuario;
      private ToolStripStatusLabel tsslMensaje;
   }
}
