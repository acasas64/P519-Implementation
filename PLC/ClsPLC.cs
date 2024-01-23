using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLC {
   public class ClsPLC {

      FrmPLC? fPLC = null;
      SerialPort? spPLC = null;

      public ClsPLC(string strCom, int intVelocidad = 9600) {
         try {
            ComConectar(strCom, intVelocidad);
         } catch (Exception) {
            throw;
         }
      }

      public void ComConectar(string strCom, int intVelocidad = 9600) {
         if (spPLC == null)
            spPLC = new SerialPort();
         if (spPLC.IsOpen) spPLC.Close();
         spPLC.PortName = strCom;
         spPLC.BaudRate = intVelocidad;
         spPLC.DataBits = 8;
         spPLC.Parity = Parity.None;
         spPLC.StopBits = StopBits.One;
         spPLC.Handshake = Handshake.None;
         spPLC.DtrEnable = true;    // Data-terminal-ready
         spPLC.RtsEnable = true;    // Request-to-send
         try {
            spPLC.Open();
            spPLC.DataReceived += new SerialDataReceivedEventHandler(DataRecive);
         } catch (Exception) {
            throw;
         }
      }

      private static void DataRecive(object sender, SerialDataReceivedEventArgs e) {
         SerialPort sp = (SerialPort)sender;
         string message = sp.ReadLine();
      }

      private bool ocupado = false;
      public string RecibeDato() {
         while (ocupado) ;
         ocupado = true;
         string datos = spPLC!.ReadExisting();
         if (fPLC != null) fPLC.Muestra = "Recep: " + datos;
         ocupado = false;
         return datos;
      }

      public bool EnviaDato(string strMsg) {
         try {
            spPLC!.Write(strMsg);
            if (fPLC != null) fPLC.Muestra = "Envío: " + strMsg;
            return true;
         } catch (Exception e) {
            if (fPLC != null) fPLC.Muestra = "Error: " + e.Message;
            return false;
         }
      }

      public void llenaCom(ToolStripDropDownButton tsddb, string strCom) {
         tsddb.DropDownItems.Clear();

         ToolStripItem tsi = tsddb.DropDownItems.Add("COM?");
         tsddb.Text = "COM?";
         foreach (string sp in SerialPort.GetPortNames()) {
            tsi = tsddb.DropDownItems.Add(sp);
            if (sp == strCom) tsddb.Text = strCom;
         }
      }

      public static void llenaCom(ComboBox cbo, string strCom) {
         cbo.Items.Clear();

         foreach (string sp in SerialPort.GetPortNames()) {
            cbo.Items.Add(sp);
            if (sp == strCom) cbo.SelectedText = sp;
         }
      }

      public void Muestra(string strDato) {
         if (fPLC != null) fPLC.Muestra = "Recep: " + strDato;
      }

      public void MuestraComunicacion(IWin32Window frmPadre) {
         if (fPLC == null) {
            fPLC = new FrmPLC(ref spPLC!);
         }
         try {
            fPLC.Show(frmPadre);
         } catch (Exception) {
            fPLC = new FrmPLC(ref spPLC!);
            fPLC.Show(frmPadre);
         }
      }

   }
}
