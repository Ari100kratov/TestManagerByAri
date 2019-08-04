using System;
using System.ServiceModel;
using System.Data.SqlClient;
using DataAccessLayer;

namespace TMWcfService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var host = new ServiceHost(typeof(Service)))
                {
                    host.Open();
                    Console.WriteLine("Служба WCF запущена...");

                    var conn = new SqlConnection(Properties.Settings.Default.ConnectionString);
                    conn.Open();
                    conn.Close();
                    Console.WriteLine("Подключение к базе данных произошло успешно!");

                    DataManager.ConnectionString = Properties.Settings.Default.ConnectionString;
                    Console.WriteLine("Для остановки работы службы нажмите любую клавишу...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
