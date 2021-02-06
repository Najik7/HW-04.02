using System;
using System.Data.SqlClient;
using System.Data;

namespace Alif_HW_04._02
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = @"Data source=HOME-PC\SQLEXPRESS; initial catalog=Alif04.02; Integrated Security=True";
   
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                Console.WriteLine("connected");
            }


            Console.WriteLine("Выберите операцию: \n" +
                "Если хотите Добавить в таблицу нажмите 1;\n" +
                "Если хотите Удалить из таблицы нажмите 2;\n" +
                "Если хотите Выбрать Все нажмите 3;\n" +
                "Если вы хотите Выбрать Один по Id нажмите 4;\n" +
                "Если хотите Обновить нажмите 5.\n" +
                "");

            int choice = int.Parse(Console.ReadLine());
            SqlCommand command = connection.CreateCommand();


            if (choice == 1)
            {
                Console.Write("LastName = "); string lastName = Console.ReadLine();
                Console.Write("FirstName = "); string firstName = Console.ReadLine();
                Console.Write("MiddleName = "); string middleName = Console.ReadLine();
                Console.Write("BirthDate = "); string birthDate = Console.ReadLine();
                string SqlInsert = $"Insert into Person ([LastName], [FirstName], [MiddleName], [BirthDate]) values ('{lastName}', '{firstName}', '{middleName}', '{birthDate}')";
                using (SqlConnection connection1 = new SqlConnection(connectionString))
                {
                    connection1.Open();
                    SqlCommand command1 = new SqlCommand(SqlInsert, connection1);
                    int number = command1.ExecuteNonQuery();
                    Console.WriteLine($"Добывлено объектов: {number}");

                }
            }
            else if (choice == 2)
            {
                Console.Write("Введите ID сотрудника, которое вы хотите удалить: ");
                int Id = int.Parse(Console.ReadLine());

                command.CommandText = $"Delete Person where ID = {Id}";
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Seccessfully deleted the person under ID = " + Id);
                }


            }
            else if (choice == 3)
            {
                command.CommandText = "Select * from Person";
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["ID"]}\n" +
                        $"LastName: {reader["LastName"]};\n" +
                        $"FirstName: {reader["FirstName"]};\n" +
                        $"MiddleName: {reader["MiddleName"]};\n" +
                        $"BirthDate: {reader["BirthDate"]}.\n\n");
                }
            }
            else if (choice == 4)
            {
                Console.Write("Введите ID: ");
                int Id = int.Parse(Console.ReadLine());
                command.CommandText = $"Select* from Person where ID = {Id}";

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["ID"]}\n" +
                            $"LastName: {reader["LastName"]};\n" +
                            $"FirstName: {reader["FirstName"]};\n" +
                            $"MiddleName: {reader["MiddleName"]};\n" +
                            $"BirthDate: {reader["BirthDate"]}.\n\n");
                    }
                }
                else
                {
                    throw new Exception("Выбранное вами Id пустое!");
                }
               
            }
            else if (choice == 5)
            {
                Console.Write("Введите ID сотрудник для Обновления информации: ");
                int Id1 = int.Parse(Console.ReadLine());


                Console.Write("Введите обновленную информацию о LastName: ");
                string newLastName = Console.ReadLine();

                Console.Write("Введитe обновленную информацию о FirstName: ");
                string newFirstName = Console.ReadLine();

                Console.Write("Введитe обновленную информацию о MiddleName: ");
                string newMiddleName = Console.ReadLine();

                Console.Write("Введитe обновленную информацию о BirthDate: ");
                string newBirthDate = Console.ReadLine();

                command.CommandText = command.CommandText = "update person set " + "LastName = " + $"'{newLastName}'," + "FirstName = " + $"'{newFirstName}'," + "MiddleName = " + $"'{newMiddleName}'," + "BirthDate = " + $"'{newBirthDate} '" + "where Id = " + $"'{Id1}'";


                int result = command.ExecuteNonQuery();
                if(result > 0)
                {
                    Console.WriteLine("Seccessfully uploaded information!");
                }
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["ID"]}\n" +
                            $"LastName: {reader["LastName"]};\n" +
                            $"FirstName: {reader["FirstName"]};\n" +
                            $"MiddleName: {reader["MiddleName"]};\n" +
                            $"BirthDate: {reader["BirthDate"]}.\n\n");
                }

            }
            else
            {
                Console.WriteLine("Введена цифра не из списка!!!");
            }

           

            

            

        }
    }
}
