using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using BTL_LTDD.ViewModel;

namespace BTL_LTDD.Data
{
   public class DayDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public DayDatabase(String path)
        {
            _database = new SQLiteAsyncConnection(path);
            _database.CreateTableAsync<Day>().Wait();
        }
        public Task<List<Day>> GetAdultDaysAsync() => _database.Table<Day>().Where(i => i.Type.Contains("adult")).ToListAsync();

        public Task<List<Day>> GetChildrenDaysAsync() => _database.Table<Day>().Where(i => i.Type.Contains("children")).ToListAsync();

        public Task<List<Day>> GetDayAsync(Day day) => _database.Table<Day>().Where(i => (i.Name == day.Name) && (i.Type == day.Type) ).ToListAsync();

        public Task<int> SaveDayAsync(Day day)
        {
            if (day.ID != 0)
                return _database.UpdateAsync(day);
            else
                return _database.InsertAsync(day);
        }

        public Task<int> DeleteDayAsync(Day day) => _database.DeleteAsync(day);

        public Task<int> DeleteAllDayAsync()=> _database.DeleteAllAsync<Day>();



    }
}
