using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using BTL_LTDD.ViewModel;
using System.Threading.Tasks;

namespace BTL_LTDD.Data
{
   public class ExerciseDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public ExerciseDatabase( string databasePath)
        {
            _database = new SQLiteAsyncConnection(databasePath);
            _database.CreateTableAsync<Exercise>().Wait();
        }

        public Task<List<Exercise>> GetExercisesAsync()
        {
            return _database.Table<Exercise>().ToListAsync();

        }

        public Task<List<Exercise>> GetAdultExercisesAsync()
        {
            return _database.Table<Exercise>().Where(i => i.Type.Contains("adult")).ToListAsync();

        }

        public Task<List<Exercise>> GetChildrenExercisesAsync()
        {
            return _database.Table<Exercise>().Where(i => i.Type.Contains("children")).ToListAsync();

        }

        public Task<List<Exercise>> GetExercisesByTypeAsync(Exercise exercise)
        {
            return _database.Table<Exercise>().Where(i => i.Type == exercise.Type).ToListAsync();
        }

        public Task<int> DeleteExercisesByTypeAsync(Exercise exercise)
        {
            return _database.Table<Exercise>().Where(i => i.Type == exercise.Type).DeleteAsync();
        }

        public Task<int> EqualExercise(Exercise exercise)
        {
            return _database.Table<Exercise>().Where(i => (i.NameExercise == exercise.NameExercise) &&(i.Time == exercise.Time) && (i.Type == exercise.Type)
            &&(i.ImageExercise == exercise.ImageExercise)).CountAsync();
        }

        public Task<int> SaveExerciseAsync(Exercise exercise)
        {
            if(exercise.ID != 0)
            {
               return _database.UpdateAsync(exercise);
            }
            else
            {
                return _database.InsertAsync(exercise);
            }
        }
        public Task<int> DeleteExerciseAsync(Exercise exercise)
        {
            return _database.DeleteAsync(exercise);
        }

        public Task<int> DeleteAllExerciseAsync() => _database.DeleteAllAsync<Exercise>();
    }
}
