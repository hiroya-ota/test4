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

    
    public class RegistrationController : Controller
    {
        private Sqlsvr db = new Sqlsvr();
        ExceptionLog el = new ExceptionLog();
        InputCheck ck = new InputCheck();

        // GET: RegistrationDefault
        public ActionResult InitialRegistration()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InitialRegistration(Flashcard flashcard)
        {

            //POSTされた時
            //値を受けっとってdbに保存します
            if (ModelState.IsValid)
            {

                //入力チェック
                string inpCk = ck.word(flashcard.Word, flashcard.Meaning, flashcard.Remarks);

                if (inpCk != "OK")
                {
                    ViewData["word"] = flashcard.Word;
                    ViewData["meaning"] = flashcard.Meaning;
                    ViewData["remarks"] = flashcard.Remarks;
                    ViewData["msg"] = inpCk;
                    return View("InitialRegistration");
                }

                //重複登録チェック
                string sameCk = ck.sameCk(flashcard.Word);

                if (sameCk != "OK")
                {
                    ViewData["word"] = flashcard.Word;
                    ViewData["meaning"] = flashcard.Meaning;
                    ViewData["remarks"] = flashcard.Remarks;
                    ViewData["msg"] = sameCk;
                    return View("InitialRegistration");
                }

                try
                {
                    db.Flashcards.Add(flashcard);
                    db.SaveChanges();

                    ViewData["msg"] = "登録を完了しました。";
                    return View();
                }
                catch (Exception exception)
                {
                    el.writeLod(exception.ToString());
                    return View("MainController");
                }


            }

            //バリデーションに問題があったら元のページに返す
            return View(flashcard);
        }


    }
}