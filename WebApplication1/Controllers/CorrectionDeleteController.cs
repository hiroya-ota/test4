using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebApplication1.Models;
using System.Data.Entity;

using System.Threading.Tasks;
using System.IO;
//using System.Data;

namespace WebApplication1.Controllers
{
    public class CorrectionDeleteController : Controller
    {
        //context as db
        private Sqlsvr db = new Sqlsvr();
        InputCheck ck = new InputCheck();
        ExceptionLog el = new ExceptionLog();

        // GET: CorrectionDelete
        public ActionResult InitialCoDe()
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

        // GET: CorrectionDelete
        [HttpPost]
        public ActionResult Delete(int id)
        {

            try
            {
                //削除対象を検索
                Flashcard flashcards = db.Flashcards.Find(id);
                return View(flashcards);
            }
            catch (Exception exception)
            {
                el.writeLod(exception.ToString());
                return View("MainController");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                //削除対象を取得・削除・保存
                Flashcard flashcard = db.Flashcards.Find(id);
                db.Flashcards.Remove(flashcard);
                db.SaveChanges();
                //InitialCoDe();
                var flashcards = db.Flashcards.ToList();

                ViewData["msg"] = "削除を完了しました。";
                return View("InitialCoDe", flashcards);
            }
            catch (Exception exception)
            {
                el.writeLod(exception.ToString());
                return View("MainController");
            }

        }


        [HttpPost]
        public ActionResult Edit(int id)
        {
            //指定したidの値を取得
            Flashcard flashcards = db.Flashcards.Find(id);
            return View(flashcards);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComp(Flashcard flashcard)
        {
            if (ModelState.IsValid)
            {
                //入力チェック
                string inpCk = ck.word(flashcard.Word, flashcard.Meaning, flashcard.Remarks);

                if (inpCk != "OK")
                {
                    ViewData["msg"] = inpCk;
                    Edit(flashcard.ID);
                    return View("Edit");
                }

                //重複登録チェック
                string sameCkUp = ck.sameCkUp(flashcard.ID, flashcard.Word);

                if (sameCkUp != "OK")
                {
                    ViewData["msg"] = sameCkUp;
                    Edit(flashcard.ID);
                    return View("Edit");
                }


                try
                {
                    //更新であることを明示
                    db.Entry(flashcard).State = EntityState.Modified;
                    db.SaveChanges();

                    InitialCoDe();
                    ViewData["msg"] = "修正を完了しました。";
                    return View("InitialCoDe");
                }
                catch (Exception exception)
                {
                    el.writeLod(exception.ToString());
                    return View("MainController");
                }

            }
            
            return View(flashcard);
        }


        // GET: btnSearch
        [HttpPost]
        public ActionResult BtnSearch(string word)
        {

            if (string.IsNullOrEmpty(word))
            {
                InitialCoDe();
                return View("InitialCoDe");
            }

            try
            {
                // デフォルトではすべてのデータを取得
                var flashcards = from a in db.Flashcards select a;
                flashcards = flashcards.Where(a => a.Word.Contains(word));

                return View("InitialCoDe", flashcards);
            }
            catch (Exception exception)
            {
                el.writeLod(exception.ToString());
                return View("MainController");
            }

        }

    }
}