namespace PLC {
   partial class FrmPLC {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
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
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         components = new System.ComponentModel.Container();
         statusStrip1 = new StatusStrip();
         tsddbCom = new ToolStripDropDownButton();
         tsslParametros = new ToolStripStatusLabel();
         txtEnviar = new TextBox();
         txtMensajes = new TextBox();
         btnEnviar = new Button();
         timer1 = new System.Windows.Forms.Timer(components);
         statusStrip1.SuspendLayout();
         SuspendLayout();
         // 
         // statusStrip1
         // 
         statusStrip1.Items.AddRange(new ToolStripItem[] { tsddbCom, tsslParametros });
         statusStrip1.Location = new Point(0, 401);
         statusStrip1.Name = "statusStrip1";
         statusStrip1.Padding = new Padding(1, 0, 16, 0);
         statusStrip1.Size = new Size(590, 22);
         statusStrip1.TabIndex = 2;
         statusStrip1.Text = "statusStrip1";
         // 
         // tsddbCom
         // 
         tsddbCom.DisplayStyle = ToolStripItemDisplayStyle.Text;
         tsddbCom.ImageTransparentColor = Color.Magenta;
         tsddbCom.Name = "tsddbCom";
         tsddbCom.Size = new Size(51, 20);
         tsddbCom.Text = "Com?";
         tsddbCom.DropDownItemClicked += tsddbCom_DropDownItemClicked;
         // 
         // tsslParametros
         // 
         tsslParametros.Name = "tsslParametros";
         tsslParametros.Size = new Size(118, 17);
         tsslParametros.Text = "toolStripStatusLabel1";
         // 
         // txtEnviar
         // 
         txtEnviar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
         txtEnviar.Location = new Point(14, 372);
         txtEnviar.Margin = new Padding(4, 3, 4, 3);
         txtEnviar.Name = "txtEnviar";
         txtEnviar.Size = new Size(243, 23);
         txtEnviar.TabIndex = 3;
         // 
         // txtMensajes
         // 
         txtMensajes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
         txtMensajes.Location = new Point(15, 14);
         txtMensajes.Margin = new Padding(4, 3, 4, 3);
         txtMensajes.Multiline = true;
         txtMensajes.Name = "txtMensajes";
         txtMensajes.ScrollBars = ScrollBars.Vertical;
         txtMensajes.Size = new Size(560, 350);
         txtMensajes.TabIndex = 4;
         // 
         // btnEnviar
         // 
         btnEnviar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
         btnEnviar.Location = new Point(265, 372);
         btnEnviar.Margin = new Padding(4, 3, 4, 3);
         btnEnviar.Name = "btnEnviar";
         btnEnviar.Size = new Size(92, 23);
         btnEnviar.TabIndex = 5;
         btnEnviar.Text = "Enviar";
         btnEnviar.UseVisualStyleBackColor = true;
         btnEnviar.Click += btnEnviar_Click;
         // 
         // timer1
         // 
         timer1.Enabled = true;
         timer1.Interval = 15;
         timer1.Tick += timer1_Tick;
         // 
         // FrmPLC
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(590, 423);
         Controls.Add(btnEnviar);
         Controls.Add(txtMensajes);
         Controls.Add(txtEnviar);
         Controls.Add(statusStrip1);
         Margin = new Padding(4, 3, 4, 3);
         MinimizeBox = false;
         Name = "FrmPLC";
         Text = "Comunicación con PLC via RS232";
         Load += FrmPLC_Load;
         statusStrip1.ResumeLayout(false);
         statusStrip1.PerformLayout();
         ResumeLayout(false);
         PerformLayout();
      }

      #endregion

      private System.Windows.Forms.StatusStrip statusStrip1;
      private System.Windows.Forms.ToolStripDropDownButton tsddbCom;
      private System.Windows.Forms.ToolStripStatusLabel tsslParametros;
      private System.Windows.Forms.TextBox txtEnviar;
      private System.Windows.Forms.TextBox txtMensajes;
      private System.Windows.Forms.Button btnEnviar;
      private System.Windows.Forms.Timer timer1;

   }
}