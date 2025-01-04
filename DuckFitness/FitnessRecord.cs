using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using System.IO;

namespace DuckFitness
{
    public class FitnessRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Activity { get; set; } // "Running" or "Weight Training"
        public double? Distance { get; set; } // Distance for running
        public string? WeightTrainingType { get; set; } // Type of weight training
    }
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<FitnessRecord>().Wait();
        }

        public Task<List<FitnessRecord>> GetRecordsAsync()
        {
            return _database.Table<FitnessRecord>().ToListAsync();
        }

        public Task<int> SaveRecordAsync(FitnessRecord record)
        {
            if (record.Id != 0)
            {
                return _database.UpdateAsync(record);
            }
            else
            {
                return _database.InsertAsync(record);
            }
        }

        public Task<int> DeleteRecordAsync(FitnessRecord record)
        {
            return _database.DeleteAsync(record);
        }
    }
}

