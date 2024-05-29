namespace Labb_1_SQL_ADO__
{
    using System;
    using System.Data.SqlClient;
    internal class Program
    {
        static void Main(string[] args)
        {

            string connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Skola;Integrated Security=True;Encrypt=False";

            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                Console.WriteLine("Skriv in siffran på valet du vill göra.\n 1. Hämta alla elever \n 2. Hämta alla elever i en viss klass \n 3. Lägga till ny personal \n 4. Hämta personal \n 5. Hämta alla betyg som satts den senaste månaden \n 6. Snittbetyg per kurs \n 7. Lägga till nya elever");
                var choice = Console.ReadLine();
                string query = "";

                switch (choice)
                {
                    case "1":
                        query = "SELECT * FROM Elever";
                        break;
                    case "2":
                        Console.WriteLine("Skriv in ID på kursen du vill hämta ifrån");
                        int CourseId = int.Parse(Console.ReadLine());
                        query = $"SELECT s.ID, s.Firtsname, s.Lastname, g.Grade, c.Class FROM Elever s INNER JOIN Betyg g ON s.GradeID = g.ID INNER JOIN Klasser c ON s.ClassID = c.ID WHERE c.ID = {CourseId}";
                        break;
                    case "3":
                        Console.WriteLine("Skriv in förnamn:");
                        string TFirstname = Console.ReadLine();
                        Console.WriteLine("Skriv in efternamn:");
                        string TLastname = Console.ReadLine();
                        Console.WriteLine("Skriv in kategori:");
                        string TCategory = Console.ReadLine();
                        query = $"INSERT INTO Personal (Firstname, Lastname, Category)\r\nVALUES ('{TFirstname}', '{TLastname}', '{TCategory}')";
                        Console.WriteLine($"Personalen har blivit tillagd {TFirstname} {TLastname} {TCategory}");
                        break;
                    case "4":
                        query = "SELECT * FROM Personal";
                        break;
                    case "5":
                        query = "SELECT s.ID, s.Firtsname, s.Lastname, s.GradeID, s.GradeDate FROM Elever s INNER JOIN Betyg g ON s.GradeID = g.ID WHERE YEAR(s.GradeDate) = YEAR(GETDATE()) AND MONTH(s.GradeDate) = MONTH(GETDATE());";
                        break;
                    case "6":
                        Console.WriteLine("Skriv in ID på kursen du vill välja:");
                        int CourseID = int.Parse(Console.ReadLine());
                        Console.Clear();
                        Console.WriteLine("AverageGradeID\n1 = IG\n2 = G\n3 = IG\n\n");
                        query = $"SELECT c.Class, AVG(s.GradeID) AS AverageGradeId FROM Elever s INNER JOIN Klasser c ON s.ClassID = c.ID WHERE c.ID = {CourseID} GROUP BY c.Class;";

                        break;
                    case "7":
                        Console.WriteLine(DateTime.Today);
                        Console.WriteLine("Skriv in förnamn:");
                        string SFirstname = Console.ReadLine();
                        Console.WriteLine("Skriv in efternamn:");
                        string SLastname = Console.ReadLine();
                        Console.WriteLine("Klass Id:");
                        string ClassID = Console.ReadLine();
                        Console.WriteLine("Betyg Id 1=IG 2=G 3=VG:");
                        string GradeID = Console.ReadLine();
                        query = $"INSERT INTO Elever (Firtsname, Lastname, ClassID, GradeID, GradeDate)\r\nVALUES ('{SFirstname}', '{SLastname}', {ClassID}, {GradeID}, '{DateTime.Today}')";
                        Console.WriteLine($"Eleven har blivit tillagd {SFirstname} {SLastname} Klass: {ClassID} Betyg: {GradeID}");

                        break;
                }

                
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader.GetName(i) + "\t     ");
                        }
                        Console.WriteLine();

                    while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write(reader[i].ToString() + "\t     ");
                            }
                            Console.WriteLine();
                        }
                    }
                

                
            }
        }
    }
}
