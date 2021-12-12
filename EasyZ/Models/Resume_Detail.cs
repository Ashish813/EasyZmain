using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyZ.Models
{
    public class Resume_Detail
    {
        public int resumeid { get; set; }
        public int namelenmax { get; set; }
        public int introlenmin { get; set; }
        public int introlenmax { get; set; }
        public int interncountmax { get; set; }

        public int projectcountmax { get; set; }
        public int projectDescmaxlen { get; set; }
        public int certicountmax { get; set; }
        public int langcountmax { get; set; }
        public int Hobbcountmax { get; set; }
        public int skillcountmax { get; set; }
        public int expblockmax { get; set; }

        public int expresplenmin { get; set;}

        public int expresplenmax { get; set; }
        public string snapsrc { get; set; }
        public string paidstatus { get; set; }
        public string functionname { get; set; }


    }
}