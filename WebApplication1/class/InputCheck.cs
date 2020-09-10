using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WebApplication1.Models;
using System.Data.Entity;

using System.Threading.Tasks;
using System.IO;


namespace WebApplication1
{

    class InputCheck
    {

        private Sqlsvr db = new Sqlsvr();
        ExceptionLog el = new ExceptionLog();


        //空・null判定、文字数判定
        public string word(string word, string meaning, string remarks)
        {
            //null、空判定
            if (string.IsNullOrEmpty(word))
            {
                return "単語を入力してください。";
            }
            else if (string.IsNullOrEmpty(meaning))
            {
                return "意味を入力してください。";
            }

            //文字列の長さ判定
            if (word.Length > 50)
            {
                return "単語は50文字以内で入力してください。";
            }
            else if (meaning.Length > 250)
            {
                return "意味は250文字以内で入力してください。";
            }
            else if (remarks != null)
            {
                if (remarks.Length > 250)
                {
                    return "備考は250文字以内で入力してください。";
                }
                
            }

            return "OK";
        }

        //重複登録チェック (登録)
        public string sameCk(string word)
        {

                try
                {
                    var flashcards = db.Flashcards.ToList();

                for (int count = 0; count < flashcards.Count(); count++)
                {
                    if (flashcards[count].Word == word)
                    {
                        return "既に登録済みの単語です。";

                    }
                }
           　　 }
                catch (Exception exception)
                {
                    el.writeLod(exception.ToString());
                }
            return "OK";
        }

        //重複登録チェック (修正)
        public string sameCkUp(int id, string word)
        {

            try
            {
                var flashcards = db.Flashcards.ToList();
                for (int count = 0; count < flashcards.Count(); count++)
                {
                    if (flashcards[count].Word == word && flashcards[count].ID != id)
                    {
                        return "既に登録済みの単語です。";
                    }
                }
            }
            catch (Exception exception)
            {
                el.writeLod(exception.ToString());
            }
            return "OK";

        }


    }
    
}