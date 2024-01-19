namespace P519_Import {
   internal static class Program {
      /// <summary>
      ///  The main entry point for the application.
      /// </summary>

      private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

      [STAThread]
      static void Main() {
         Logger.Info("Import application started");
         // To customize application configuration such as set high DPI settings or default font,
         // see https://aka.ms/applicationconfiguration.
         ApplicationConfiguration.Initialize();
         Application.Run(new FrmImport());
         Logger.Info("Import application ended");
         NLog.LogManager.Shutdown();
      }

      static void Application_ThreadException(object sender, ThreadExceptionEventArgs e) {
         Logger.Fatal(e.Exception, "Excepcion en aplicacion");
         NLog.LogManager.Shutdown();
      }

      static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
         Logger.Fatal((Exception)(e.ExceptionObject), "Excepcion en domain");
         NLog.LogManager.Shutdown();
      }

   }
}