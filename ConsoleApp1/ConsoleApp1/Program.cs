using System;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    internal class Program
    {
        static string connectionString = "Server=LAPTOP-HURDBM1G;Database=LibraryDB;Integrated Security=True";

        static void Main(string[] args)
        {
            DisplayMenu();
        }

        static void DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Display Book Inventory");
                Console.WriteLine("2. Add New Book");
                Console.WriteLine("3. Update Book Quantity");
                Console.WriteLine("4. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayBookInventory();
                        break;
                    case "2":
                        AddNewBook();
                        break;
                    case "3":
                        UpdateBookQuantity();
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void DisplayBookInventory()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Books", connection);
                DataSet booksDataSet = new DataSet();
                adapter.Fill(booksDataSet, "Books");

                DataTable booksTable = booksDataSet.Tables["Books"];

                foreach (DataRow row in booksTable.Rows)
                {
                    Console.WriteLine($"Title: {row["Title"]}, Author: {row["Author"]}, Genre: {row["Genre"]}, Quantity: {row["Quantity"]}");
                }
            }
        }

        static void AddNewBook()
        {
            Console.Write("Enter Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Author: ");
            string author = Console.ReadLine();

            Console.Write("Enter Genre: ");
            string genre = Console.ReadLine();

            Console.Write("Enter Quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Books (Title, Author, Genre, Quantity) VALUES (@Title, @Author, @Genre, @Quantity)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Author", author);
                command.Parameters.AddWithValue("@Genre", genre);
                command.Parameters.AddWithValue("@Quantity", quantity);

                connection.Open();
                command.ExecuteNonQuery();
            }

            Console.WriteLine("Book added successfully.");
        }

        static void UpdateBookQuantity()
        {
            Console.Write("Enter Title of the Book to Update: ");
            string title = Console.ReadLine();

            Console.Write("Enter New Quantity: ");
            int newQuantity = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Books SET Quantity = @Quantity WHERE Title = @Title";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Quantity", newQuantity);
                command.Parameters.AddWithValue("@Title", title);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Book quantity updated successfully.");
                }
                else
                {
                    Console.WriteLine("Book not found.");
                }
            }
        }
    }
}
