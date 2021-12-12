using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyZ.Models
{
    public class Questions
    {
        public int Qid;
        public string QuestionText;
        public  int EduId;
        public  int PageNo;
        public List<string> subqnslist;




        public Questions()
        {


        }

        public Questions(int Qid, string QuestionText, int EduId, int PageNo)
        {
            this.Qid = Qid;
            this.QuestionText = QuestionText;
            this.EduId = EduId;
            this.PageNo = PageNo;

        }


    }
}