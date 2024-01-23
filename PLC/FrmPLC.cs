using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLC {
   public partial class FrmPLC : Form {


      public SerialPort? sp = null;
      private bool cambio = false;
      private string mensaje = "";

      public FrmPLC(ref SerialPort spPLC) {
         InitializeComponent();
         CheckForIllegalCrossThreadCalls = false;
         sp = spPLC;
         llenaCom(tsddbCom, spPLC.PortName);
      }

      private void FrmPLC_Load(object sender, EventArgs e) {
         MuestraConfig();
      }

      private void tsddbCom_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
         if (sp == null) return;
         if (sp.IsOpen) sp.Close();
         sp.PortName = e.ClickedItem!.Text;
         try {
            sp.Open();
            tsddbCom.Text = e.ClickedItem.Text;
            MuestraConfig();
         } catch (Exception ex) {
            MessageBox.Show(ex.Message);
         }
      }

      private void MuestraConfig() {
         tsddbCom.Text = "COM?";
         if (sp == null) return;
         if (sp.IsOpen) tsddbCom.Text = sp.PortName;
         tsslParametros.Text = "";
         tsslParametros.Text = tsslParametros.Text + " Dato:";
         tsslParametros.Text = tsslParametros.Text + sp.DataBits.ToString();
         tsslParametros.Text = tsslParametros.Text + " Paridad:";
         tsslParametros.Text = tsslParametros.Text + sp.Parity.ToString();
         tsslParametros.Text = tsslParametros.Text + " BitsParo:";
         tsslParametros.Text = tsslParametros.Text + sp.StopBits.ToString();
         tsslParametros.Text = tsslParametros.Text + " Sincro:";
         tsslParametros.Text = tsslParametros.Text + sp.Handshake.ToString();
         tsslParametros.Text = tsslParametros.Text + " Velocidad:";
         tsslParametros.Text = tsslParametros.Text + sp.BaudRate.ToString();
      }

      private void llenaCom(ToolStripDropDownButton tsddb, string strCom) {
         tsddb.DropDownItems.Clear();
         ToolStripItem tsi = tsddb.DropDownItems.Add("COM?");
         tsddb.Text = "COM?";
         foreach (string sp in SerialPort.GetPortNames()) {
            tsi = tsddb.DropDownItems.Add(sp);
            if (sp == strCom) tsddb.Text = strCom;
         }
      }

      public string Muestra {
         get { return txtMensajes.Text; }
         set {
            mensaje = "\r\n" + value;
            cambio = true;
         }
      }

      private void timer1_Tick(object sender, EventArgs e) {
         if (cambio) {
            cambio = false;
            txtMensajes.AppendText(mensaje);
         }
      }

      private void btnEnviar_Click(object sender, EventArgs e) {
         if (sp == null) return;
         string Dato = txtEnviar.Text;
         txtMensajes.Text += "\r\nEnvío: " + Dato;
         sp.Write(Dato);
         sp.DiscardOutBuffer();
      }

   }
}
