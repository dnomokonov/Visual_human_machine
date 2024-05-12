using Dapper;
using Homework_LogicalApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_LogicalApp.Helpers
{
    public class ComponentRepository
    {
        private static string FormConnStringFromPath(string path) => $"Data Source={path}";

        public static List<ConnectableModel> GetAll(string path)
        {
            var connectionString = FormConnStringFromPath(path);
            using (IDbConnection db = new SQLiteConnection(connectionString))
            {
                return db.Query<ConnectableModel>("SELECT * FROM components").ToList();
            }
        }

        public static bool InsertOne(string path, ConnectableModel model)
        {
            var connectionString = FormConnStringFromPath(path);
            using (IDbConnection db = new SQLiteConnection(connectionString))
            {
                db.Open();
                if (!CheckIfTableExists(db, "components"))
                {
                    CreateComponentsTable(db);
                }
                string sql = @"INSERT INTO components (Id, Name, PosX, PosY, InputId, OutputId) VALUES (@Id, @Name, @PosX, @PosY, @InputId, @OutputId)";
                int rowsAffected = db.Execute(sql, model);
                return rowsAffected > 0;
            }
        }

        private static bool CheckIfTableExists(IDbConnection db, string tableName)
        {
            string sql = "SELECT name FROM sqlite_master WHERE type='table' AND name=@tableName";
            var result = db.Query<string>(sql, new { tableName }).FirstOrDefault();
            return result != null;
        }

        private static void CreateComponentsTable(IDbConnection db)
        {
            string sql = @"
                CREATE TABLE components (
                    Id INTEGER PRIMARY KEY,
                    Name TEXT NOT NULL,
                    PosX REAL NOT NULL,
                    PosY REAL NOT NULL,
                    InputId INTEGER,
                    OutputId INTEGER,
                    FOREIGN KEY (InputId) REFERENCES components (Id),
                    FOREIGN KEY (OutputId) REFERENCES components (Id)
                );";
            db.Execute(sql);
        }
    }
}
