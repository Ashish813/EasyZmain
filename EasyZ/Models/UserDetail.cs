using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyZ.Models
{
    public class UserDetail
    {
        public int id { get; set; }

        //[Required(ErrorMessage = "You must provide a Name")]
        [Display(Name = "What is Your Name:")]
        public string name { get; set; }

        //[Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "What is your Contact No:")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public long number { get; set; }

        //[Required(ErrorMessage = "You must Select a City")]
        [Display(Name = "In Which City do you live in:")]
        public string city { get; set; }

        //[Required(ErrorMessage = "You must Select a State")]
        //public string country { get; set; }

        //[Required(ErrorMessage = "You must Select a State")]
        //public string state { get; set; }

        //[Required(ErrorMessage = "You must provide an Email")]
        [Display(Name = "What is your Email:")]
        public string email { get; set; }

        public string ExpStatus { get; set; }


        public string Highestlev { get; set; }

        public string Rselectid { get; set; }


        public string intro { get; set; }

        public string tenthP { get; set; }
        
        public string tenthY { get; set; }

        public string tenthB { get; set; }

        public string twelthP { get; set; }

        public string twelthY { get; set; }

        public string twelthB { get; set; }

        public string GP { get; set; }


        public string GY { get; set; }

        public string GU { get; set; }

        public string PGP { get; set; }

        public string PGY { get; set; }
         
        public string PGU { get; set; }

        public string DescHighEdu { get; set;}

        public string InternshipCount { get; set;}

        public string ProjectCount { get; set;}



        public List<Internship> Interndata { get; set; }

        public List<Project> Projectdata { get; set; }

        
        public string[] skills { get; set; }

        public string[] certifications { get; set; }

        public string[] hobbies { get; set; }


        public string[] languages { get; set; }

    }


   





}