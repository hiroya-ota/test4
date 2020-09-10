using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.Entity;

namespace WebApplication1.Models
{
    public class Search
    {
    }

    public class Flashcard
    {
        public int ID { get; set; }
        public string Word { get; set; }
        public string Meaning { get; set; }
        public string Remarks { get; set; }
    }


    //public class Sqlsvr : DbContext
    //{
    //    public DbSet<Flashcard> Flashcards { get; set; }
    //}
}