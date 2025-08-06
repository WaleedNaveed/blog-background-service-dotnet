using BackgroundServiceDemo.Helper;
using BackgroundServiceDemo.Interfaces;
using ClosedXML.Excel;
using Microsoft.Data.Sqlite;

namespace BackgroundServiceDemo.Services
{
    public class UserImportService : IUserImportService
    {
        private const string DbPath = Constants.DatabaseName;
        private const string ExcelPath = "Uploads/MOCK_DATA.xlsx";

        public async Task ImportAsync(CancellationToken cancellationToken)
        {
            using var workbook = new XLWorkbook(ExcelPath);
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // skip header

            using var connection = new SqliteConnection($"Data Source={DbPath}");
            connection.Open();

            foreach (var row in rows)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                var cmd = connection.CreateCommand();
                cmd.CommandText = @"
                INSERT INTO Users (FirstName, LastName, Email, Gender, IpAddress)
                VALUES ($firstName, $lastName, $email, $gender, $ip)";
                cmd.Parameters.AddWithValue("$firstName", row.Cell(2).GetValue<string>());
                cmd.Parameters.AddWithValue("$lastName", row.Cell(3).GetValue<string>());
                cmd.Parameters.AddWithValue("$email", row.Cell(4).GetValue<string>());
                cmd.Parameters.AddWithValue("$gender", row.Cell(5).GetValue<string>());
                cmd.Parameters.AddWithValue("$ip", row.Cell(6).GetValue<string>());

                cmd.ExecuteNonQuery();

                await Task.Delay(200, cancellationToken); // simulate long processing
            }
        }
    }
}
