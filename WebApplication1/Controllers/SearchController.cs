using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebApplication1.Models;
using System.Data.Entity;

using System.Threading.Tasks;
using System.IO;

namespace WebApplication1.Controllers
{

     public class SearchController : Controller
     {
            //context as db
            private Sqlsvr db = new Sqlsvr();
            ExceptionLog el = new ExceptionLog();


        public ActionResult InitialSearch()
            {

            try
            {
                var flashcards = db.Flashcards.ToList();
                return View(flashcards);
            }
            catch (Exception exception)
            {
                el.writeLod(exception.ToString());
                return View("MainController");
            }
        
            }

          // GET: btnSearch
          [HttpPost]
           public ActionResult BtnSearch(string word)
           {
            
           if (string.IsNullOrEmpty(word))
           {
                InitialSearch();
                return View("InitialSearch");
           }

            try
           {
                // デフォルトではすべてのデータを取得
                var flashcards = from a in db.Flashcards select a;
                flashcards = flashcards.Where(a => a.Word.Contains(word));

                return View("InitialSearch", flashcards);
           }
           catch (Exception exception)
           {
                el.writeLod(exception.ToString());
                return View("MainController");
            }

           }


     }
}