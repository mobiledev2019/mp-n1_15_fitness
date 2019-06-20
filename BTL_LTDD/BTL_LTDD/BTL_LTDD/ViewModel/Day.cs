using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace BTL_LTDD.ViewModel
{
    public class Day
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
    }
}
