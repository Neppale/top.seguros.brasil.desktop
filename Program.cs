using Top_Seguros_Brasil_Desktop.src.Components;
using Top_Seguros_Brasil_Desktop.src.Screens.BaseForm;
using Top_Seguros_Brasil_Desktop.src.Screens.Management_Stage;

namespace Top_Seguros_Brasil_Desktop
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.   aaa
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());
        }
    }
}