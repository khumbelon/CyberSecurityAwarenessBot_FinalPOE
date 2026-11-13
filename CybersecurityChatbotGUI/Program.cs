using System;// to use basic system functionalities
using System.Windows.Forms;// to use Windows Forms functionalities
using CybersecurityAwarenessBot.Data;
using MySql.Data.MySqlClient;// to use MySQL database functionalities

namespace CybersecurityChatbotGUI// namespace of the project
{
    static class Program// static class for the main entry point of the application
    {
        [STAThread]
        static void Main()// main entry point of the application
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);// set the default text rendering to be compatible with the current operating system
            
            // Explicitly create and run MainForm (NOT ChatbotForm)
            MainForm mainForm = new MainForm();
            Application.Run(mainForm);// run the MainForm as the main form of the application
        }
        private static void TestDatabaseConnection()
        {
            try
            {
                using (MySqlConnection connection =
                       new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();
                    MessageBox.Show("Database connection successful.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection failed: " + ex.Message);
            }
        }

    }
}
