using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyZ.Models
{
    public class Eduoptions
    {

     public   int eduid;
     public  string eduname;



        
        public Eduoptions(int eduid, string eduname) {

            this.eduid = eduid;
            this.eduname = eduname;
        
        
        }
    }
}