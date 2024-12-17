using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Задание 2
/*namespace VegetablesAndFruits
{
    class Program
    {
        static void Main(string[] args)
        {
            // Строка подключения к базе данных
            string connectionString = "Data Source=localhost;Initial Catalog=VegetablesAndFruits;Integrated Security=True";

            // Создание подключения
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Открытие подключения
                    connection.Open();
                    Console.WriteLine("Подключение к базе данных успешно установлено.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка подключения к базе данных: " + ex.Message);
                }
                finally
                {
                    // Закрытие подключения
                    connection.Close();
                }
            }
        }
    }
}*/
namespace DZ1
{
    class Program
    {
        private static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=VegetablesAndFruits;" ;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Подключиться к базе данных");
                Console.WriteLine("2. Вывести всю информацию из таблицы");
                Console.WriteLine("3. Вывести все названия овощей и фруктов");
                Console.WriteLine("4. Вывести все цвета");
                Console.WriteLine("5. Показать максимальную калорийность");
                Console.WriteLine("6. Показать минимальную калорийность");
                Console.WriteLine("7. Показать среднюю калорийность");
                Console.WriteLine("8. Показать количество овощей");
                Console.WriteLine("9. Показать количество фруктов");
                Console.WriteLine("10. Показать количество овощей и фруктов заданного цвета");
                Console.WriteLine("11. Показать овощи и фрукты с калорийностью ниже указанной");
                Console.WriteLine("12. Показать овощи и фрукты с калорийностью выше указанной");
                Console.WriteLine("13. Показать овощи и фрукты с калорийностью в указанном диапазоне");
                Console.WriteLine("14. Показать все овощи и фрукты, у которых цвет желтый или красный");
                Console.WriteLine("0. Выйти");
                Console.Write("Введите номер действия: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ConnectToDatabase();
                        break;
                    case "2":
                        ShowAllProducts();
                        break;
                    case "3":
                        ShowAllNames();
                        break;
                    case "4":
                        ShowAllColors();
                        break;
                    case "5":
                        ShowMaxCaloricContent();
                        break;
                    case "6":
                        ShowMinCaloricContent();
                        break;
                    case "7":
                        ShowAverageCaloricContent();
                        break;
                    case "8":
                        ShowCountOfVegetables();
                        break;
                    case "9":
                        ShowCountOfFruits();
                        break;
                    case "10":
                        ShowCountByColor();
                        break;
                    case "11":
                        ShowProductsBelowCaloricContent();
                        break;
                    case "12":
                        ShowProductsAboveCaloricContent();
                        break;
                    case "13":
                        ShowProductsInCaloricRange();
                        break;
                    case "14":
                        ShowProductsByColor();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте еще раз.");
                        break;
                }
            }
        }

        private static void ConnectToDatabase()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Подключение успешно установлено.");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Ошибка подключения: " + ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        Console.WriteLine("Подключение закрыто.");
                    }
                }
            }
        }

        private static void ShowAllProducts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Products", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["Id"]}, Название: {reader["Name"]}, Тип: {reader["Type"]}, Цвет: {reader["Color"]}, Калорийность: {reader["CaloricContent"]}");
                }
                reader.Close();
            }
        }

        private static void ShowAllNames()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT Name FROM Products", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Название: {reader["Name"]}");
                }
                reader.Close();
            }
        }

        private static void ShowAllColors()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT DISTINCT Color FROM Products", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Цвет: {reader["Color"]}");
                }
                reader.Close();
            }
        }

        private static void ShowMaxCaloricContent()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT MAX(CaloricContent) AS MaxCaloric FROM Products", connection);
                var result = command.ExecuteScalar();
                Console.WriteLine($"Максимальная калорийность: {result}");
            }
        }

        private static void ShowMinCaloricContent()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT MIN(CaloricContent) AS MinCaloric FROM Products", connection);
                var result = command.ExecuteScalar();
                Console.WriteLine($"Минимальная калорийность: {result}");
            }
        }

        private static void ShowAverageCaloricContent()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT AVG(CaloricContent) AS AvgCaloric FROM Products", connection);
                var result = command.ExecuteScalar();
                Console.WriteLine($"Средняя калорийность: {result}");
            }
        }

        private static void ShowCountOfVegetables()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Products WHERE Type = 'овощ'", connection);
                var count = command.ExecuteScalar();
                Console.WriteLine($"Количество овощей: {count}");
            }
        }

        private static void ShowCountOfFruits()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Products WHERE Type = 'фрукт'", connection);
                var count = command.ExecuteScalar();
                Console.WriteLine($"Количество фруктов: {count}");
            }
        }

        private static void ShowCountByColor()
        {
            Console.Write("Введите цвет: ");
            string color = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Products WHERE Color = @Color", connection);
                command.Parameters.AddWithValue("@Color", color);
                var count = command.ExecuteScalar();
                Console.WriteLine($"Количество овощей и фруктов цвета '{color}': {count}");
            }
        }

        private static void ShowProductsBelowCaloricContent()
        {
            Console.Write("Введите калорийность: ");
            int caloricContent = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Products WHERE CaloricContent < @CaloricContent", connection);
                command.Parameters.AddWithValue("@CaloricContent", caloricContent);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Название: {reader["Name"]}, Калорийность: {reader["CaloricContent"]}");
                }
                reader.Close();
            }
        }

        private static void ShowProductsAboveCaloricContent()
        {
            Console.Write("Введите калорийность: ");
            int caloricContent = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Products WHERE CaloricContent > @CaloricContent", connection);
                command.Parameters.AddWithValue("@CaloricContent", caloricContent);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Название: {reader["Name"]}, Калорийность: {reader["CaloricContent"]}");
                }
                reader.Close();
            }
        }

        private static void ShowProductsInCaloricRange()
        {
            Console.Write("Введите минимальную калорийность: ");
            int minCaloric = int.Parse(Console.ReadLine());
            Console.Write("Введите максимальную калорийность: ");
            int maxCaloric = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Products WHERE CaloricContent BETWEEN @MinCaloric AND @MaxCaloric", connection);
                command.Parameters.AddWithValue("@MinCaloric", minCaloric);
                command.Parameters.AddWithValue("@MaxCaloric", maxCaloric);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Название: {reader["Name"]}, Калорийность: {reader["CaloricContent"]}");
                }
                reader.Close();
            }
        }

        private static void ShowProductsByColor()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Products WHERE Color IN ('желтый', 'красный')", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Название: {reader["Name"]}, Цвет: {reader["Color"]}, Калорийность: {reader["CaloricContent"]}");
                }
                reader.Close();
            }
        }
    }
}

