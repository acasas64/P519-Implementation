using ExcelDataReader;
using System.Data;

namespace P519_Import {
   public partial class FrmImport : Form {
      public FrmImport() {
         InitializeComponent();
      }

      private void FrmImport_Load(object sender, EventArgs e) {
         string strFileName = AbreArchivo(this);
         if (strFileName == "") return;

         DataTable dt = LeeArchivo(strFileName);
         if (dt == null) return; 

         dgvImport.DataSource = dt;
      }

      private static string AbreArchivo(IWin32Window owner) {
         OpenFileDialog ofd = new OpenFileDialog();
         ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
         if (ofd.ShowDialog(owner) == DialogResult.OK) {
            return ofd.FileName;
         }
         return "";
      }

      private static DataTable LeeArchivo(string filePath) {
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

   }
}
