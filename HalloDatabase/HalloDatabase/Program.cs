// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;

Console.WriteLine("Hello, World!");

var conString = "Server=(localdb)\\mssqllocaldb;Database=Northwnd;Trusted_Connection=True;";

using var con = new SqlConnection(conString);
con.Open();

var cmd = con.CreateCommand();
cmd.CommandText = "SELECT COUNT(*) FROM Employees";
var empCount = cmd.ExecuteScalar();

Console.WriteLine($"{empCount} Employees in DB");

cmd = con.CreateCommand();
cmd.CommandText = "SELECT * FROM Employees";
var reader = cmd.ExecuteReader();
while (reader.Read())
{
    int id = reader.GetInt32(reader.GetOrdinal("EmployeeId"));
    string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
    string lastName = reader.GetString(reader.GetOrdinal("LastName"));
    var bDate = reader.GetDateTime(reader.GetOrdinal("BirthDate"));
    Console.WriteLine($"{id} {firstName} {lastName} {bDate:d}");
}
