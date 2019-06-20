using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace BTL_LTDD.ViewModel
{
   public class Exercise
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public string NameExercise { get; set; }
        public string Type { get; set; }
        public string Time { get; set; }
        public string ImageExercise { get; set; }
    }
}
