using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    Console.WriteLine("Введите строку подключения к базе данных");
                    //var userInput = Console.ReadLine();
                    //var sqlConn = new SqlConnection();

                    //if (userInput == "Default")
                        DataManager.ConnectionString = Properties.Settings.Default.ConnectionString;
                   // else
                     //   DataManager.ConnectionString = userInput;

                   // Console.WriteLine("Подключение к базе данных произошло успешно!");
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
