using DataBase;
using Microsoft.VisualBasic.ApplicationServices;
using PLC;
using System.IO;
using System.Linq.Expressions;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.Design.AxImporter;

namespace P519_G2798 {
   public partial class FrmG2798 : Form {
      private readonly string appSeccion = "P519 EOL";

      private ClsConnection _cn;

      private bool conPLC = false;
      private ClsPLC? PLCPKYK = null;

      public FrmG2798() {
         InitializeComponent();
         _cn = new ClsConnection(appSeccion);
         _cn.IniciaConnection(this);

         conPLC = ClsConfig.GetBool(appSeccion, "WithPLC", "No");
      }

      private void FrmG2798_Load(object sender, EventArgs e) {

         tsddbCom.Visible = conPLC;
         if (conPLC) {
            string strCom = ClsConfig.Get(appSeccion, "SerialPort", "Com1");
            try {
               PLCPKYK = new ClsPLC(EntradaPLC, strCom, 9600);
            } catch {
               MessageBox.Show(this, $"Can not connect to serial port ({strCom})", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (PLCPKYK != null)
               PLCPKYK.llenaCom(tsddbCom, strCom);
            tsddbCom.Text = strCom;
            tsddbCom.BackColor = (PLCPKYK!.IsOpen ? Color.LightGreen : Color.Red);
         } else {
         }
      }

      private bool Autorizacion() {
         ClsUser usr = new ClsUser(ref _cn);
         FrmLogin frm = new FrmLogin(ref usr);

         frm.Text = "Authorization is required";
         if (frm.ShowDialog() != DialogResult.OK) Application.Exit();
         frm.Dispose();

         if (usr.rol.Trim() == "") return false;
         if (usr.rol.Substring(0, 2).ToUpper() == "AD") return true;
         if (usr.rol.Substring(0, 2).ToUpper() == "SU") return true;
         return false;
      }

      private void tsddbCom_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
         if (PLCPKYK == null) return;
         ToolStripDropDownButton cb = (ToolStripDropDownButton)sender;

         if (e.ClickedItem!.Text == "COM?") {
            PLCPKYK.MuestraComunicacion(this);
         } else {
            if (Autorizacion()) {
               string strCom = PLCPKYK.PortName;
               if (!PLCPKYK.ComConectar(e.ClickedItem.Text!)) {
                  PLCPKYK.ComConectar(strCom);
               }
            } else {
               msgProceso("Authorization denied");
            }
         }
         cb.Text = PLCPKYK.PortName;
         ClsConfig.Save(appSeccion, "SerialPort", PLCPKYK.PortName);
         cb.BackColor = (PLCPKYK.IsOpen ? Color.LightGreen : Color.Red);
      }

      private bool msgProceso(string strMensaje, bool boolError = true) {
         tsslMensaje.Text = strMensaje;
         return !boolError;
      }

      private void EntradaPLC(string strDato) {
         MessageBox.Show(this, strDato, "Data receive", MessageBoxButtons.OK);
      }

   }
}
