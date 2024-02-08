using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Net.Security;
using System.Resources.Tools;
using DataBase;
using ExcelDataReader;

namespace P519_Import {
   public partial class FrmImport : Form {

      private static string strSeccion = "P519";
      private int intTotalRows = 0;
      private int intOkRows = 0;
      private int intErrorRows = 0;

      private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
      private readonly string strTitle = "Process Serial Numbers From Excel File";
      private static ClsConnection cn = new ClsConnection(strSeccion);

      public FrmImport() {
         InitializeComponent();
         Logger.Info("Import form started");
         cn.IniciaConnection(this);
      }

      private void FrmImport_FormClosed(object sender, FormClosedEventArgs e) {
         Logger.Info("Import form ended");
      }

      private void importMasterSNToolStripMenuItem_Click(object sender, EventArgs e) {
         this.Text = strTitle;
         intTotalRows = 0;
         intOkRows = 0;
         intErrorRows = 0;
         tspbProgress.Maximum = 0;
         tspbProgress.Value = 0;
         muestraContadores();
         dgvImport.DataSource = null;
         DataTable? dt = TraeArchivo(this, "Process MASTER Serial Number");
         if (dt != null) {
            dgvImport.DataSource = dt;
            if (!ValidaMasterSN(ref dt)) {
               dgvImport.DataSource = dt;
               //               ColorErrorsDgv(ref dgvImport);
            } else {
               dgvImport.DataSource = dt;
               SaveMasterSN(dt);
            }
            this.Text += " (MASTER) ";
         }
         muestraContadores();
      }

      private void importToteSNToolStripMenuItem_Click(object sender, EventArgs e) {
         this.Text = strTitle;
         dgvImport.DataSource = TraeArchivo(this, "Process TOTE Serial Number");
         if (dgvImport.DataSource != null) {
            ValidaToteSN();
            this.Text += " (TOTE) ";
         }
      }

      private void closeToolStripMenuItem_Click(object sender, EventArgs e) {
         this.Close();
      }

      private void muestraContadores() {
         tsslTotalRows.Text = $"Total Rows Readed: {intTotalRows}";
         tsslRowsOk.Text = $"Rows OK: {intOkRows}";
         tsslErrors.Text = $"Errors: {intErrorRows}";
      }


      private static string PideArchivo(IWin32Window owner, string strWitch) {
         OpenFileDialog ofd = new OpenFileDialog();
         ofd.Title = strWitch;
         ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
         if (ofd.ShowDialog(owner) == DialogResult.OK) {
            return ofd.FileName;
         }
         return "";
      }

      private static DataTable LeeArchivo(string filePath) {
         System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
         using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
            using (var reader = ExcelReaderFactory.CreateReader(stream)) {

               var result = reader.AsDataSet(
                  new ExcelDataSetConfiguration() {
                     ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration() {
                        UseHeaderRow = true
                     }
                  }
               );
               DataTable table = result.Tables[0];
               return table;
            }
         }
      }

      private static DataTable? TraeArchivo(IWin32Window owner, string strWitch) {
         try {
            string strFileName = PideArchivo(owner, strWitch);
            if (strFileName == "") return null;

            DataTable dt = LeeArchivo(strFileName);
            if (dt == null) return null;

            return dt;

         } catch (Exception e) {
            Logger.Error(e, "Something went wrong when reading the file");
            MessageBox.Show(owner, "Could not read the file, please make sure that the file is not open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
         }
      }

      private bool ValidaMasterSN(ref DataTable dt) {
         ClsTable? tblPallet = null;
         ClsPart? tblPart = null;

         tspbProgress.Maximum = dt.Rows.Count;

         if (!ValidaColumnasMasterSN(ref dt)) return false;
         if (!ValidaTablasPallet(ref tblPart, ref tblPallet)) return false;

         long lngPrevSN = -1;
         long lngSP1 = 0; ;
         long lngSP2 = 0; ;
         foreach (DataRow r in dt.Rows) {
            Logger.Info($"Serial {r["PalletId"]}, Grammer No. 1 {r["Grammer No 1"].ToString()}, Grammer No. 2 {r["Grammer No 2"].ToString()}, Quantity {r["Quantity"]}");
            int intErrorsTmp = intErrorRows;
            // Valida Numero Serial
            string strTmp = ValidaMasterSN(r["PalletId"].ToString(), tblPallet!, ref lngPrevSN).ToString();
            if (strTmp != "0") intErrorRows++;
            r["Validate"] = strTmp;

            // Valida Numero Grammer1
            strTmp = ValidaGrammerNo(r["Grammer No 1"].ToString(), tblPart!, ref lngSP1).ToString();
            if (strTmp != "0") intErrorRows++;
            r["Validate"] += $",{strTmp}";

            // Valida Numero Grammer2
            strTmp = ValidaGrammerNo(r["Grammer No 2"].ToString(), tblPart!, ref lngSP2).ToString();
            if (strTmp != "0") intErrorRows++;
            r["Validate"] += $",{strTmp}";

            // Valida cantidad numerico
            strTmp = ValidaCantidad(r["Quantity"].ToString(), lngSP1, lngSP2).ToString();
            if (strTmp != "0") intErrorRows++;
            r["Validate"] += $",{strTmp}";
            if (intErrorsTmp == intErrorRows) intOkRows++;
            tspbProgress.Value =  ++intTotalRows;
         }
         return intErrorRows <= 0;
      }

      private static void ValidaToteSN() {

      }

      private bool ValidaColumnasMasterSN(ref DataTable dt) {
         if (!dt.Columns.Contains("PalletId")) {
            MessageBox.Show(this, "Column 'PalletId' not found en Excel file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
         }
         if (!dt.Columns.Contains("Grammer No 1")) {
            MessageBox.Show(this, "Column 'Grammer No 1' not found en Excel file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
         }
         if (!dt.Columns.Contains("Grammer No 2")) {
            MessageBox.Show(this, "Column 'Grammer No 2' not found en Excel file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
         }
         if (!dt.Columns.Contains("Quantity")) {
            MessageBox.Show(this, "Column 'Quantity' not found en Excel file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
         }
         dt.Columns.Add("Validate", typeof(string));
         dt.DefaultView.Sort = "PalletId ASC";
         dt = dt.DefaultView.ToTable();
         return true;
      }

      private bool ValidaTablasPallet(ref ClsPart? tblPart, ref ClsTable? tblPallet) {
         try {
            tblPallet = new ClsTable(ref cn, "Pallet");
            tblPart = new ClsPart(ref cn);
         } catch (OleDbException e) {
            Logger.Error(e, "Error could not open table");
            MessageBox.Show(this, $"Error could not open table\n{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
         } catch (Exception e) {
            Logger.Error(e, "Error could not open table");
            MessageBox.Show(this, $"Error could not open table\n{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
         }
         return true;
      }

      private long ValidaMasterSN(string? strPalletId, ClsTable tblPallet, ref long lngPrevSN) {
         long lngSN = -1;
         if (!long.TryParse(strPalletId, out lngSN)) {
            Logger.Info($"   Serial {strPalletId} NO se puede convertir a numero");
            return 1;
         }
         if (lngPrevSN == lngSN) {
            Logger.Info($"   Serial Duplicado en mismo archivo");
            return 2;
         }
         lngPrevSN = lngSN;
         if (tblPallet.Existe(lngSN.ToString())) {
            Logger.Info($"   Serial Duplicado en base de datos");
            return 3;
         }
         return 0;   // Todo bien con el serial
      }

      private long ValidaGrammerNo(string? strGrammerNo, ClsPart tblPart, ref long lngSP) {
         lngSP = 0; ;
         if (strGrammerNo == null) {
            Logger.Info($"   Grammer No. no esta definido");
            return 1;   // GrammerNo no esta definido
         }
         if (!tblPart!.Trae(strGrammerNo)) {
            Logger.Info($"   Grammer No. {strGrammerNo} No se encontro en la base de datos");
            return 2;   // GrammerNo no existe
         }
         lngSP = tblPart.sp();
         return 0;   // GrammerNo Ok
      }

      private long ValidaCantidad(string? strSP, long lngSP1, long lngSP2) {
         long lngSP = 0;
         if (!long.TryParse(strSP, out lngSP)) {
            Logger.Info($"   Serial {strSP} NO se puede convertir a numero");
            return 1;   // No es un valor numerico
         }
         if (lngSP != lngSP1 + lngSP2) {
            Logger.Info($"   La cantidad no coincide con la registrada en la base de datos");
            return 2;   // El standar pack no concide con los de numero de partes
         }
         return 0;   // Sp OK
      }

      private void ColorErrorsDgv(object sender, DataGridViewCellFormattingEventArgs e) {
         DataGridView dgv = (DataGridView)sender;
         if (!dgv.Columns.Contains("Validate")) return;
         foreach (DataGridViewRow r in dgv.Rows) {
            string strRes = r.Cells["Validate"].Value.ToString()!;
            string[] arrRes = strRes.Split(',');
            if (arrRes.Length < 4) return;
            if (!strRes.Equals("0,0,0,0")) {
               if (arrRes[0] != "0") r.Cells["PalletId"].Style.BackColor = Color.Red;
               if (arrRes[1] != "0") r.Cells["Grammer No 1"].Style.BackColor = Color.Red;
               if (arrRes[2] != "0") r.Cells["Grammer No 2"].Style.BackColor = Color.Red;
               if (arrRes[3] != "0") r.Cells["Quantity"].Style.BackColor = Color.Red;
            }
         }
         dgv.Refresh();
      }

      private bool SaveMasterSN(DataTable dt) {
         try {
            ClsTable tblPallet = new ClsTable(ref cn, "Pallet");
            foreach (DataRow dtr in dt.Rows) {
               tblPallet["PalletId"] = dtr["PalletId"];
               tblPallet["GrammerNo1"] = dtr["Grammer No 1"];
               tblPallet["GrammerNo2"] = dtr["Grammer No 2"];
               tblPallet["Quantity"] = dtr["Quantity"];
               tblPallet.Insertar();
            }
            return true;
         } catch (OleDbException e) {
            Logger.Error(e, "Error could not open table Pallet");
            MessageBox.Show(this, $"Error could not open table\n{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
         } catch (Exception e) {
            Logger.Error(e, "Error could not open table Pallet");
            MessageBox.Show(this, $"Error could not open table\n{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
         return false;
      }

      private void dgvImport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {

         DataGridView dgv = (DataGridView)sender;
         if (!dgv.Columns.Contains("Validate")) return;
         string strColumn = dgv.Columns[e.ColumnIndex].Name;

         DataGridViewRow r = dgv.Rows[e.RowIndex];

         string strRes = dgv.Rows[e.RowIndex].Cells["Validate"].Value.ToString()!;
         string[] arrRes = strRes.Split(',');
         if (arrRes.Length < 4) return;
         if (!strRes.Equals("0,0,0,0")) {
            if (arrRes[0] != "0" && strColumn == "PalletId") r.Cells["PalletId"].Style.BackColor = Color.Red;
            if (arrRes[1] != "0" && strColumn == "Grammer No 1") r.Cells["Grammer No 1"].Style.BackColor = Color.Red;
            if (arrRes[2] != "0" && strColumn == "Grammer No 2") r.Cells["Grammer No 2"].Style.BackColor = Color.Red;
            if (arrRes[3] != "0" && strColumn == "Quantity") r.Cells["Quantity"].Style.BackColor = Color.Red;
         }

      }
   }

}
