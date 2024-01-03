using ExcelDataReader;
using System.Data;
using System.Security.Cryptography.Xml;

namespace P519_Import {
   public partial class FrmImport : Form {

      private readonly string strTitle = "Process Serial Numbers From Excel File";

      public FrmImport() {
         InitializeComponent();
      }

      private void closeToolStripMenuItem_Click(object sender, EventArgs e) {
         this.Close();
         Application.Exit();
      }

      private void importMasterSNToolStripMenuItem_Click(object sender, EventArgs e) {
         this.Text = strTitle;
         if (traeArchivo(this, dgvImport, "Process MASTER Serial Number")) {
            validaMasterSN();
            this.Text += " (MASTER) ";
         }
      }

      private void importToteSNToolStripMenuItem_Click(object sender, EventArgs e) {
         this.Text = strTitle;
         if (traeArchivo(this, dgvImport, "Process TOTE Serial Number")) {
            validaToteSN();
            this.Text += " (TOTE) ";
         }
      }

      private static string pideArchivo(IWin32Window owner, string strWitch) {
         OpenFileDialog ofd = new OpenFileDialog();
         ofd.Title = strWitch;
         ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
         if (ofd.ShowDialog(owner) == DialogResult.OK) {
            return ofd.FileName;
         }
         return "";
      }

      private static DataTable leeArchivo(string filePath) {
         System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
         using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read)) {
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

      private static bool traeArchivo(IWin32Window owner, DataGridView dgv, string strWitch) {
         dgv.DataSource = null;
         string strFileName = pideArchivo(owner, strWitch);
         if (strFileName == "") return false;

         DataTable dt = leeArchivo(strFileName);
         if (dt == null) return false;

         dgv.DataSource = dt;

         return true;
      }

      private static void validaMasterSN() {

      }

      private static void validaToteSN() {

      }

   }
}
