using System.Data;
using System.Data.OleDb;
using DataBase;
using ExcelDataReader;

namespace P519_Import {
   public partial class FrmImport : Form {

      private static string strSeccion = "P519";

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
         DataTable? dt = TraeArchivo(this, "Process MASTER Serial Number");
         if (dt != null) {
            ValidaMasterSN(ref dt);
            this.Text += " (MASTER) ";
         }
         dgvImport.DataSource = dt;
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

         if (!ValidaColumnasMasterSN(ref dt)) return false;
         if (!ValidaTablasPallet(ref tblPart, ref tblPallet)) return false;

         bool boolRes = true;
         long lngPrevSN = -1;
         long lngSP1 = 0; ;
         long lngSP2 = 0; ;
         string strTmp = "";

         foreach (DataRow r in dt.Rows) {
            Logger.Info($"Serial {r["PalletId"]}, Grammer No. 1 {r["Grammer No 1"].ToString()}, Grammer No. 2 {r["Grammer No 2"].ToString()}, Quantity {r["Quantity"]}");

            // Valida Numero Serial
            strTmp = ValidaMasterSN(r["PalletId"].ToString(), tblPallet!, ref lngPrevSN).ToString();
            if (strTmp != "0") boolRes = false;
            r["Validate"] = strTmp;

            // Valida Numero Grammer1
            strTmp = ValidaGrammerNo(r["Grammer No 1"].ToString(), tblPart!, ref lngSP1).ToString();
            if (strTmp != "0") boolRes = false;
            r["Validate"] += $",{strTmp}";

            // Valida Numero Grammer2
            strTmp = ValidaGrammerNo(r["Grammer No 2"].ToString(), tblPart!, ref lngSP2).ToString();
            if (strTmp != "0") boolRes = false;
            r["Validate"] += $",{strTmp}";

            // Valida cantidad numerico
            strTmp = ValidaCantidad(r["Quantity"].ToString(), lngSP1, lngSP2).ToString();
            if (strTmp != "0") boolRes = false;
            r["Validate"] += $",{strTmp}";
         }
         return boolRes;
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

   }
}
