using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyZ.Models
{
    public class ResumeQns
    {
        public int resumeid;
        public int Rtypeid;
        public int Qid;
        public string QuestionText;
        public int EduId;
        public string EduName;


        public ResumeQns()
        {

        }
        public ResumeQns(int resumeid,int Rtypeid,int Qid,string QuestionText,int EduId,string EduName) {
            this.resumeid = resumeid;
            this.Rtypeid = Rtypeid;
            this.Qid = Qid;
            this.QuestionText = QuestionText;
            this.EduId = EduId;
            this.EduName = EduName;
        
        }

    }


    public class Rinfo
    {
        public int resumeid { get; set; }
        public string rsnap { get; set; }

        public string functionname { get; set; }



    }




}