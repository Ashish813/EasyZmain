using EasyZ.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyZ.Controllers
{
    public class ResumeProcessController : Controller
    {

        DB_cmd obj_DBcmd=new DB_cmd();
        public ActionResult Index()
        {
            return View();
        }
         
        public ActionResult ResumeProcess()
        {

            DataSet ds= obj_DBcmd.returnDataset("getAllQuestions_and_byresid");
            List<Eduoptions> edulist = new List<Eduoptions>();
            List<Questions> Qnslist = new List<Questions>();
            List<ResumeQns> ResumeQnslist = new List<ResumeQns>();




            int max = ds.Tables[0].Rows.Count > ds.Tables[1].Rows.Count ? (ds.Tables[0].Rows.Count > ds.Tables[2].Rows.Count ? ds.Tables[0].Rows.Count : ds.Tables[2].Rows.Count) : ds.Tables[1].Rows.Count > ds.Tables[2].Rows.Count ? (ds.Tables[1].Rows.Count > ds.Tables[0].Rows.Count ? ds.Tables[1].Rows.Count : ds.Tables[0].Rows.Count) : ds.Tables[2].Rows.Count > ds.Tables[1].Rows.Count ? ds.Tables[2].Rows.Count : ds.Tables[1].Rows.Count;

            for (int i = 0; i < max; i++) {
                if (i < ds.Tables[0].Rows.Count)
                {
                    edulist.Add(new Eduoptions((int)ds.Tables[0].Rows[i]["EduId"], ds.Tables[0].Rows[i]["EduName"].ToString()));
                } 

                if (i < ds.Tables[1].Rows.Count)
                {

                    Qnslist.Add(new Questions() 

                    {
                        Qid = Convert.ToInt32(ds.Tables[1].Rows[i]["Qid"]),
                        QuestionText = Convert.ToString(ds.Tables[1].Rows[i]["QuestionText"]),
                        EduId = Convert.ToInt32(ds.Tables[1].Rows[i]["EduId"]),
                        PageNo = Convert.ToInt32(ds.Tables[1].Rows[i]["PageNo"])

                    });
                }


                if (i < ds.Tables[2].Rows.Count)
                {
                    ResumeQnslist.Add(new ResumeQns { 
                        resumeid=Convert.ToInt32(ds.Tables[2].Rows[i]["resumeid"]),
                        Rtypeid= Convert.ToInt32(ds.Tables[2].Rows[i]["Rtypeid"]),
                        Qid= Convert.ToInt32(ds.Tables[2].Rows[i]["Qid"]),
                        QuestionText= Convert.ToString(ds.Tables[2].Rows[i]["QuestionText"]),
                        EduId= Convert.ToInt32(Convert.IsDBNull(ds.Tables[2].Rows[i]["EduId"])?0: ds.Tables[2].Rows[i]["EduId"]),
                        EduName= Convert.ToString(Convert.IsDBNull(ds.Tables[2].Rows[i]["EduName"])?"": ds.Tables[2].Rows[i]["EduName"])


                    });

                }

                }

     //     int a=  Qnslist.Count;

            ViewBag.eduoption = edulist;
            ViewBag.qns = Qnslist;
            ViewBag.p_res_data = ResumeQnslist;

            return View();
        }


        public ActionResult ResumeProcess2(UserDetail user) {
            TempData["educationid"] = user.Highestlev;
            TempData["user"] = user;
            List<Rinfo> SnapRList = new List<Rinfo>();


            obj_DBcmd.addParameter("@maxed_id", user.Highestlev.ToString());
            obj_DBcmd.addParameter("@expid_restype", user.ExpStatus.ToString());
            DataSet ds = obj_DBcmd.returnDataset("ResumebyExp_type_HighED");

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                SnapRList.Add(new Rinfo()
                {
                    resumeid = Convert.ToInt32(row["resumeid"]),
                    rsnap = row["snapsrc"].ToString()
                });

            }


            ViewBag.SnapRList = SnapRList;
            return View(user);
        }

        public ActionResult ResumeProcess3(UserDetail user)
        {

            string id = user.Rselectid;
            user = (UserDetail)TempData["user"];
            user.Rselectid = id;
            TempData["user"] = user;
            //Ashish
            //kumar jha
            // MASTER

            // MAsterrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr main folder

            //change gain

            //ASasadadas
            //dfdsfsdfasdasfdas
            
            List<Questions> Qbyrid_list = new List<Questions>();

            obj_DBcmd.addParameter("@rid", user.Rselectid.ToString());
            DataSet ds = obj_DBcmd.returnDataset("getqns_byrid");

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                List<string> subqnsl = new List<string>();

                foreach (DataRow subqnsrow in ds.Tables[1].Rows)
                {
                    if (Convert.ToInt32(row["Qid"]) == Convert.ToInt32(subqnsrow["Qid"]))
                    {
                        subqnsl.Add(Convert.ToString(subqnsrow["Subqnstext"]));
                    }

                }               

                Qbyrid_list.Add(new Questions()
                {
                    Qid = Convert.ToInt32(row["Qid"]),
                    QuestionText = Convert.ToString(row["QuestionText"]),
                    EduId = Convert.ToInt32(Convert.IsDBNull(row["EduId"]) ? 0 : row["EduId"]),
                    subqnslist = subqnsl

                });


            }



            // string aa = TempData["educationid"].ToString();  //working but already get it from view 
            // DB_cmd obbb = new DB_cmd();

            obj_DBcmd = new DB_cmd();// Need some explanation.......
            obj_DBcmd.addParameter("@resumeid", user.Rselectid.ToString());

            ds = obj_DBcmd.returnDataset("getresumedetail");
            Resume_Detail Rd_obj = new Resume_Detail();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Rd_obj = new Resume_Detail()
                {
                    resumeid = Convert.ToInt32(row["resumeid"]),
                    namelenmax = Convert.ToInt32(row["namelenmax"]),
                    introlenmin = Convert.ToInt32(row["introlenmin"]),
                    introlenmax = Convert.ToInt32(row["introlenmax"]),
                    interncountmax = Convert.ToInt32(row["interncountmax"]),
                    projectcountmax = Convert.ToInt32(row["projectcountmax"]),       
                    projectDescmaxlen = Convert.ToInt32(row["projectDescmaxlen"]),
                    certicountmax = Convert.ToInt32(row["certicountmax"]),
                    langcountmax = Convert.ToInt32(row["langcountmax"]),
                    Hobbcountmax = Convert.ToInt32(row["Hobbcountmax"]),
                    skillcountmax = Convert.ToInt32(row["skillcountmax"]),
                    expblockmax = Convert.ToInt32(Convert.ToString(row["expblockmax"]) == "" ? 0 : row["expblockmax"]),
                    expresplenmin = Convert.ToInt32(Convert.ToString(row["expresplenmin"]) == "" ? 0 : row["expresplenmin"]),
                    expresplenmax = Convert.ToInt32(Convert.ToString(row["expresplenmax"]) == "" ? 0 : row["expresplenmax"]),
                    snapsrc = Convert.ToString(row["snapsrc"]),
                    paidstatus = Convert.ToString(row["paidstatus"]),
                    functionname = Convert.ToString(row["functionname"])
                };
            }





            ViewBag.Highed_id = Convert.ToInt32(user.Highestlev);
            ViewBag.Resume_Detail = Rd_obj;
            ViewBag.Qbyrid_list = Qbyrid_list;

            TempData["Qbyrid_list"] = Qbyrid_list;
            TempData["Rd_obj"] = Rd_obj;


            return View();  
        }


        public ActionResult ResumeProcess4(UserDetail user)
        {

            UserDetail prevobj = (UserDetail)TempData["user"];
            TempData.Keep("user");
            user.ExpStatus = prevobj.ExpStatus;
            user.Highestlev = prevobj.Highestlev;
            user.Rselectid = prevobj.Rselectid;

            user.city = prevobj.city;
            user.email = prevobj.email;
            user.name = prevobj.name;
            user.number = prevobj.number;

            List<Questions> Qbyrid_list = (List<Questions>)TempData["Qbyrid_list"];
            Resume_Detail Rd_obj= (Resume_Detail)TempData["Rd_obj"];
            TempData.Keep("Qbyrid_list");
            TempData.Keep("Rd_obj");
            TempData["user"] = user;
            ViewBag.Resume_Detail = Rd_obj;
            ViewBag.Qbyrid_list = Qbyrid_list;

            return View(user);
        }




        public ActionResult DownloadResume(UserDetail user)
        {     

            
            resume1();
            return View();
        }     







        public ActionResult SecondPage(int expid,int eduid)
        {
            TempData["educationid"] = eduid;
            List<Rinfo> SnapRList = new List<Rinfo>();


            obj_DBcmd.addParameter("@maxed_id", eduid.ToString());
            obj_DBcmd.addParameter("@expid_restype",expid.ToString());
            DataSet ds = obj_DBcmd.returnDataset("ResumebyExp_type_HighED");

            foreach(DataRow row in ds.Tables[0].Rows)
            {
                SnapRList.Add(new Rinfo()
                {
                    resumeid = Convert.ToInt32(row["resumeid"]),
                    rsnap=row["snapsrc"].ToString()
                });

            }
            
            return PartialView("SecondPageP",SnapRList);
        }




        //int expid, int Maxeduid,int resumeid
        public ActionResult ThirdPage(int expid, int Maxeduid, int resumeid)
        {
           

            List<Questions> Qbyrid_list = new List<Questions>();

            obj_DBcmd.addParameter("@rid", resumeid.ToString());
            DataSet ds = obj_DBcmd.returnDataset("getqns_byrid");

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                List<string> subqnsl = new List<string>();

                foreach (DataRow subqnsrow in ds.Tables[1].Rows)
                {
                    if (Convert.ToInt32(row["Qid"]) == Convert.ToInt32(subqnsrow["Qid"]))
                    {
                        subqnsl.Add(Convert.ToString(subqnsrow["Subqnstext"]));
                    }

                }

                Qbyrid_list.Add(new Questions()
                {
                    Qid = Convert.ToInt32(row["Qid"]),
                    QuestionText = Convert.ToString(row["QuestionText"]),
                    EduId = Convert.ToInt32(Convert.IsDBNull(row["EduId"]) ? 0 : row["EduId"]),
                    subqnslist = subqnsl

                });


            }


            // string aa = TempData["educationid"].ToString();  //working but already get it from view 
            // DB_cmd obbb = new DB_cmd();

            obj_DBcmd = new DB_cmd();// Need some explanation.......
            obj_DBcmd.addParameter("@resumeid", resumeid.ToString());
        
            ds= obj_DBcmd.returnDataset("getresumedetail");
            Resume_Detail Rd_obj=new Resume_Detail();
            foreach (DataRow row in ds.Tables[0].Rows) {
                Rd_obj = new Resume_Detail()
                {
                    resumeid = Convert.ToInt32(row["resumeid"]),
                    namelenmax = Convert.ToInt32(row["namelenmax"]),
                    introlenmin = Convert.ToInt32(row["introlenmin"]),
                    introlenmax = Convert.ToInt32(row["introlenmax"]),
                    interncountmax = Convert.ToInt32(row["interncountmax"]),
                    projectcountmax = Convert.ToInt32(row["projectcountmax"]),
                    projectDescmaxlen = Convert.ToInt32(row["projectDescmaxlen"]),
                    certicountmax = Convert.ToInt32(row["certicountmax"]),
                    langcountmax = Convert.ToInt32(row["langcountmax"]),
                    Hobbcountmax = Convert.ToInt32(row["Hobbcountmax"]),
                    skillcountmax = Convert.ToInt32(row["skillcountmax"]),
                    expblockmax = Convert.ToInt32(Convert.ToString(row["expblockmax"])=="" ? 0 : row["expblockmax"]),
                    expresplenmin = Convert.ToInt32(Convert.ToString(row["expresplenmin"])=="" ? 0 : row["expresplenmin"]),
                    expresplenmax = Convert.ToInt32(Convert.ToString(row["expresplenmax"]) == ""? 0 : row["expresplenmax"]),
                    snapsrc = Convert.ToString(row["snapsrc"]),
                    paidstatus = Convert.ToString(row["paidstatus"]),
                    functionname = Convert.ToString(row["functionname"])
                };
            }




                ViewBag.Highed_id = Maxeduid;
                ViewBag.Resume_Detail = Rd_obj;
            

            return PartialView("ThirdPageP",Qbyrid_list);
        }


       


        public void resume1()
        {
            try
            {

                #region Pdf Generation

                #region create to pdfDoc.open
                 string folderPath = "D:\\PDF\\";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                //File Name
                int fileCount = Directory.GetFiles("D:\\PDF").Length;
                string strFileName = "DescriptionForm" + (fileCount + 1) + ".pdf";

                FileStream stream = new FileStream(folderPath + strFileName, FileMode.Create);

                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                #endregion 
                #region line and shapes 
                PdfContentByte cb = writer.DirectContent;
                cb.SetColorFill(new BaseColor(232, 223, 223));
                cb.SetColorStroke(BaseColor.WHITE);
                cb.Rectangle(0, 0, pdfDoc.PageSize.Width / 3, pdfDoc.PageSize.Height);


                cb.FillStroke();

                #endregion

                int nametier = 0;
                int introtier = 0;
                string firstname = "Ashish ";
                string secondname = "kumar jha";  // 9 when its in  lower case
                //29 length max
                string fullname = firstname + secondname;
                ArrayList certlist = new ArrayList() { "Android", "Web Dev", "Machine learning", "Machine learning" };
                ArrayList langlist = new ArrayList() { "English", "Hindi", "Methali", "Methali" };
                ArrayList hobblist = new ArrayList() { "Cricket", "Reading", "Football", "Football" };
                int intshipcount = 3;
                int projcount = 3;
                int lancount = langlist.Count;
                int hobbcount = hobblist.Count;
                int certicount = certlist.Count;
                int[] lastsection = new int[] { lancount, hobbcount, certicount };



                float inilines = 10;
                float fontcust = 10;

                
                string datai = "1. Digital Marketing Intern, 2017 – 2018 American Heart Association.";
                string datap = "1. Digital Marketing Intern,American Heart Association(Dallas, Texas), 2017 – 2018," +
                   "Professional arts and entertainment reporter with experience working in newsrooms of all sizes.Focused and dedicated to all stories with the ability to produce high-quality news content at high volumes and on strict deadlines.Currently seeking a role as an arts and entertainment reporter at a professional newsroom." +
                   "";
                string oneline = "1. Digital Marketing Intern,American Heart Association(Dallas, Texas), 2017 –";
                int totallen = 3 * datap.Length;
                int onel = 77;
                int finallines = totallen / onel;
                float increaseinperl = ((finallines - inilines) / inilines) * 100;
                increaseinperl = increaseinperl <= 0 ? increaseinperl == 0 ? increaseinperl + increaseinperl * 0.05f : increaseinperl - increaseinperl * 0.3f : increaseinperl - increaseinperl * 0.3f; //setting font is too fluctuating
                fontcust = fontcust * (100 - increaseinperl) / 100.0f;
                #region headerr
                if (fullname.Length < 16)
                {
                    Font ffont = FontFactory.GetFont("BOD_PSTC", 46, iTextSharp.text.Font.BOLD, new BaseColor(227, 155, 11));
                    Font sfont = FontFactory.GetFont("BOD_PSTC", 40, iTextSharp.text.Font.BOLD, BaseColor.RED);
                    Chunk firstn = new Chunk("\n\n\n" + firstname, ffont);
                    Chunk secondn = new Chunk(secondname, sfont);
                    Phrase fs = new Phrase(firstn);
                    fs.Add(secondn);
                    // Font verdana = FontFactory.GetFont("Verdana", 24, iTextSharp.text.Font.BOLDITALIC, new CMYKColor(0, 0, 0, 95));
                    Paragraph headboxp = new Paragraph();
                    headboxp.Add(fs);
                    headboxp.IndentationLeft = (float)(pdfDoc.PageSize.Width / 2.8);
                    //headboxp.IndentationRight =
                    //headboxp.SpacingBefore= 200;
                    //headboxp.SpacingAfter = 200;
                    pdfDoc.Add(headboxp);
                    nametier = 1;

                }
                else
                {
                    Font firstfont = FontFactory.GetFont("Verdana", 46, iTextSharp.text.Font.BOLDITALIC, new BaseColor(227, 155, 11));

                    Paragraph firstp = new Paragraph(firstname, firstfont);
                    //firstp.SpacingAfter = 0f;
                    if (fullname.Length < 23)
                    {
                        Font secondfont = FontFactory.GetFont("abc", 40, iTextSharp.text.Font.BOLDITALIC, new BaseColor(99, 68, 5));
                        Paragraph secondp = new Paragraph(secondname, secondfont);
                        firstp.IndentationLeft = (float)(pdfDoc.PageSize.Width / 2.8);
                        secondp.IndentationLeft = (float)(pdfDoc.PageSize.Width / 2.5);
                        secondp.SpacingBefore = -20f;
                        pdfDoc.Add(firstp);
                        pdfDoc.Add(secondp);
                        nametier = 2;
                    }
                    else
                    {

                        string[] mL = secondname.Split(' ');
                        string middlename = mL[0];
                        string lastname = mL[1];
                        Font secondfont = FontFactory.GetFont("abc", 30, iTextSharp.text.Font.BOLDITALIC, new BaseColor(99, 68, 5));
                        Paragraph middlep = new Paragraph(middlename + " " + lastname, secondfont);
                        middlep.SpacingBefore = -20f;
                        Font lastfont = FontFactory.GetFont("abc", 30, iTextSharp.text.Font.BOLDITALIC, new BaseColor(129, 111, 232));
                        // Paragraph lastp = new Paragraph(lastname,lastfont);
                        firstp.IndentationLeft = (float)(pdfDoc.PageSize.Width / 2.8);
                        middlep.IndentationLeft = (float)(pdfDoc.PageSize.Width / 2.8);
                        //  lastp.IndentationLeft = (float)(pdfDoc.PageSize.Width / 2.3);
                        // lastp.SpacingBefore = -20f;
                        pdfDoc.Add(firstp);
                        pdfDoc.Add(middlep);
                        //  pdfDoc.Add(lastp);
                        nametier = 3;
                    }
                }

                #endregion

                #region intro
                Paragraph linep = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2.0F, 100.0F, new BaseColor(79, 22, 35), Element.ALIGN_RIGHT, 1)));
                linep.IndentationLeft = (float)(pdfDoc.PageSize.Width / 2.8);
                pdfDoc.Add(linep);

                string introstring = "Professional arts and entertainment reporter with experience working in newsrooms of all sizes. Focused and dedicated to all stories with the ability to produce high-quality news content at high volumes and on strict deadlines.Currently seeking a role as an arts and entertainment reporter a.";
                // Font introfont = FontFactory.GetFont("Helvetica Italic", 10, new BaseColor(0,0,0));
                introstring = introstring + introstring.Length;
                Font Intf = FontFactory.GetFont("BOD_PSTC", fontcust
                    , iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));
                Font uIntf = FontFactory.GetFont("BOD_PSTC", 10
                    , iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));
                Paragraph intro = new Paragraph(introstring, uIntf);
                intro.Alignment = Element.ALIGN_JUSTIFIED;
                intro.IndentationLeft = (float)(pdfDoc.PageSize.Width / 2.8);
                //intro length must be under 295
                // and must be above 88
                introtier = introstring.Length > 174 ? introstring.Length > 260 ? 4 : 3 : 2;
                pdfDoc.Add(intro);

                Font edufont = FontFactory.GetFont("Helvetica Italic", 12, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));
                Paragraph edu = new Paragraph("Education", edufont);
                edu.SpacingBefore = 20f;
                edu.IndentationLeft = (float)(pdfDoc.PageSize.Width / 2.37);

                pdfDoc.Add(edu);

                #endregion
                
                #region Image
                string imageURL = "https://localhost:44330/../Images/students-cap.png";
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);

                //Resize image depend upon your need
                jpg.ScaleToFit(55f, 45f);
                //Give space before image
                //jpg.SpacingBefore = 0f;
                //Give some space after the image
                jpg.SpacingAfter = 1f;
                //y->570 for three tier name
                //y->610 for two tier name
                //y->655 for one tier name
                float yax = nametier == 1 ? 655 : nametier == 2 ? 610 : 625f;
                jpg.SetAbsolutePosition((float)(pdfDoc.PageSize.Width / 2.8) + 10, yax - 16 + (4 - introtier) * 15);
                pdfDoc.Add(jpg);
                #endregion

                #region Education
                PdfPTable pdfTable4 = new PdfPTable(3);
                Font headf = FontFactory.GetFont("BOD_PSTC", 18, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));
                Font cellf = FontFactory.GetFont("BOD_PSTC", 14, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));

                pdfTable4.SpacingBefore = 10f;
                pdfTable4.DefaultCell.Padding = 5;
                pdfTable4.WidthPercentage = 61.70f;

                pdfTable4.DefaultCell.BorderWidth = 0f;
                pdfTable4.HorizontalAlignment = Element.ALIGN_RIGHT;

                pdfTable4.AddCell(new Paragraph("10th", uIntf));
                pdfTable4.AddCell(new Phrase("12th", uIntf));

                pdfTable4.AddCell(new Phrase("UG/Diploma", uIntf));
                pdfTable4.AddCell(new Phrase("79.80", uIntf));
                pdfTable4.AddCell(new Phrase("71.20", uIntf));
                pdfTable4.AddCell(new Phrase("73.42", uIntf));
                pdfTable4.AddCell(new Phrase("2014", uIntf));
                pdfTable4.AddCell(new Phrase("2016", uIntf));
                pdfTable4.AddCell(new Phrase("2021", uIntf));
                pdfTable4.AddCell(new Phrase("CBSE", uIntf));
                pdfTable4.AddCell(new Phrase("CBSE", uIntf));
                pdfTable4.AddCell(new Phrase("GGSIPU", uIntf));


                //  pdfTable4.AddCell(new Phrase("Demo Vendor"));

                // pdfTable4.AddCell(new Phrase("Kolkata"));

                pdfDoc.Add(pdfTable4);
                #endregion

                #region Internships               
                Paragraph paragraph = new Paragraph();

                paragraph.SpacingBefore = 10;
                paragraph.SpacingAfter = 5;
                paragraph.IndentationLeft = (float)(pdfDoc.PageSize.Width / 2.7);
                string text = "Internships";
                paragraph.Font = headf;
                paragraph.Add(text);
                pdfDoc.Add(paragraph);


                var bb = pdfDoc.PageSize.Height * 0.6;

                for (int i = 0; i < intshipcount; i++)
                {  //(Dallas, Texas)
                    Paragraph count = new Paragraph();
                    count.Alignment = Element.ALIGN_JUSTIFIED;
                    count.SpacingBefore = 5;
                    count.SpacingAfter = 3;
                    count.IndentationLeft = (float)(pdfDoc.PageSize.Width / 2.6);
                    string Ihead = "" + (i + 1) + ". Digital Marketing Intern, 2017 – 2018 American Heart Association,Digital Marketing Intern, 2017 – 2018 American Heart Association.";
                    count.Font = uIntf;
                    count.Add(Ihead);
                    pdfDoc.Add(count);
                }


                #endregion

                #region Projects
                //[Project 1 name], [Company name], [Date of project 1]
                //[Project 1 description and your role in it]
                Font Pfont = FontFactory.GetFont("BOD_PSTC", 9, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));
                Paragraph Ptitle = new Paragraph();
                Ptitle.SpacingBefore = 10;
                Ptitle.SpacingAfter = 5;
                Ptitle.IndentationLeft = (float)(pdfDoc.PageSize.Width / 2.7);
                string ptext = "Projects";
                Ptitle.Font = headf;
                Ptitle.Add(ptext);
                pdfDoc.Add(Ptitle);

                for (int i = 0; i < projcount; i++)
                {
                    Paragraph count = new Paragraph();
                    count.Alignment = Element.ALIGN_JUSTIFIED;
                    count.SpacingBefore = 5;
                    count.SpacingAfter = 3;
                    count.IndentationLeft = (float)(pdfDoc.PageSize.Width / 2.6);
                    //max desc 300
                    // 
                    string desc = "Professional arts and entertainment reporter with experience working in newsrooms of all sizes.Focused and dedicated to all stories with the ability to produce high-quality news content at high volumes and on strict deadlines.Currently seeking a role as an arts and entertainment reporter at a professional newsroom.";
                    string Ihead = "" + (i + 1) + ". Digital Marketing Intern,American Heart Association(Dallas, Texas), 2017 – 2018," + desc + desc.Length;
                    count.Font = Intf;
                    count.Add(Ihead);
                    pdfDoc.Add(count);
                }




                #endregion

                #region Certifications and Hobbies/lastsection 
                PdfPTable tablelast = new PdfPTable(3);


                tablelast.SpacingBefore = 10f;
                tablelast.DefaultCell.Padding = 5;
                tablelast.WidthPercentage = 61.70f;

                tablelast.DefaultCell.BorderWidth = 0f;
                tablelast.HorizontalAlignment = Element.ALIGN_RIGHT;

                tablelast.AddCell(new Paragraph("Certifications", cellf));
                tablelast.AddCell(new Phrase("Hobbies", cellf));
                tablelast.AddCell(new Phrase("Language", cellf));

                for (int i = 0; i < lastsection.Max(); i++)
                {
                    tablelast.AddCell(new Phrase(" " + (i < certlist.Count ? "\u2022 " + certlist[i] : " ") + "", uIntf));
                    tablelast.AddCell(new Phrase(" " + (i < hobblist.Count ? "\u2022 " + hobblist[i] : " ") + "", uIntf));
                    tablelast.AddCell(new Phrase(" " + (i < langlist.Count ? "\u2022 " + langlist[i] : " ") + "", uIntf));

                }
                //tablelast.AddCell(new Phrase(" \u2022 Android", uIntf));
                //tablelast.AddCell(new Phrase(" \u2022 Cricket", uIntf));
                //tablelast.AddCell(new Phrase(" \u2022 English", uIntf));

                //tablelast.AddCell(new Phrase(" \u2022 Web Dev", uIntf));
                //tablelast.AddCell(new Phrase(" \u2022 Reading", uIntf));
                //tablelast.AddCell(new Phrase(" \u2022 Hindi", uIntf));

                //tablelast.AddCell(new Phrase(" \u2022 Machine learning", uIntf));
                //tablelast.AddCell(new Phrase(" ", uIntf));
                //tablelast.AddCell(new Phrase(" ", uIntf));


                //  pdfTable4.AddCell(new Phrase("Demo Vendor"));

                // pdfTable4.AddCell(new Phrase("Kolkata"));

                pdfDoc.Add(tablelast);

                #endregion

                #region side lines
                //bottom right
                cb.MoveTo((pdfDoc.PageSize.Width / 10.0) * 9, pdfDoc.PageSize.Height / 100.0);

                cb.SetColorStroke(BaseColor.BLACK);
                cb.LineTo((pdfDoc.PageSize.Width / 10.0) * 9.858, (pdfDoc.PageSize.Height / 100.0));

                cb.LineTo((pdfDoc.PageSize.Width / 10.0) * 9.858, (pdfDoc.PageSize.Height / 100.0) + (pdfDoc.PageSize.Height / 100.0) * 6.066);
                cb.Stroke();

                //upper right
                cb.MoveTo((pdfDoc.PageSize.Width / 10.0) * 9, (pdfDoc.PageSize.Height / 100.0) * 99);
                cb.LineTo((pdfDoc.PageSize.Width / 10.0) * 9.858, (pdfDoc.PageSize.Height / 100.0) * 99);
                cb.LineTo((pdfDoc.PageSize.Width / 10.0) * 9.858, pdfDoc.PageSize.Height - ((pdfDoc.PageSize.Height / 100.0) + (pdfDoc.PageSize.Height / 100.0) * 6.066));
                cb.Stroke();

                //bottom left
                cb.MoveTo((pdfDoc.PageSize.Width / 10.0) * 1, pdfDoc.PageSize.Height / 100.0);
                cb.LineTo(((pdfDoc.PageSize.Width / 10.0) * 1) - (pdfDoc.PageSize.Width / 10.0) * 0.858, pdfDoc.PageSize.Height / 100.0);
                cb.LineTo(((pdfDoc.PageSize.Width / 10.0) * 1) - (pdfDoc.PageSize.Width / 10.0) * 0.858, (pdfDoc.PageSize.Height / 100.0) + (pdfDoc.PageSize.Height / 100.0) * 6.066);
                cb.Stroke();

                //upper left
                cb.MoveTo(((pdfDoc.PageSize.Width / 10.0) * 1) - (pdfDoc.PageSize.Width / 10.0) * 0.858, pdfDoc.PageSize.Height - ((pdfDoc.PageSize.Height / 100.0) + (pdfDoc.PageSize.Height / 100.0) * 6.066));
                cb.LineTo(((pdfDoc.PageSize.Width / 10.0) * 1) - (pdfDoc.PageSize.Width / 10.0) * 0.858, (pdfDoc.PageSize.Height / 100.0) * 99);
                cb.LineTo((pdfDoc.PageSize.Width / 10.0) * 1, (pdfDoc.PageSize.Height / 100.0) * 99);
                cb.Stroke();


                #endregion

                #region profile pic

                string ppicURL = "https://localhost:44330/../Images/rocky-mountains-3717220.jpg";
                iTextSharp.text.Image pjpg = iTextSharp.text.Image.GetInstance(ppicURL);
                //below for making image in circle shape
                float width = 150f;
                float height = 150f;

                PdfContentByte content = writer.DirectContent;
                PdfTemplate temp = content.CreateTemplate(width, height);
                temp.Ellipse(0, 0, width, height);
                temp.Clip();
                temp.NewPath();
                temp.AddImage(pjpg, width, 0, 0, height, 0, 0);
                Image clipped = Image.GetInstance(temp);


                clipped.SetAbsolutePosition((pdfDoc.PageSize.Width / 3) * 0.119f, pdfDoc.PageSize.Height * 0.78f);
                cb.AddImage(clipped);

                //below ellipse for border
                cb.Ellipse((pdfDoc.PageSize.Width / 3) * 0.119f - 5f, pdfDoc.PageSize.Height * 0.78f - 5f, (pdfDoc.PageSize.Width / 3) * 0.119f + width + 5f, pdfDoc.PageSize.Height * 0.78f + height + 5f);

                cb.Stroke();


                #endregion

                #region INFO
                ppicURL = "https://localhost:44330/../Images/icon-g33af9e750_1280.png";
                pjpg = iTextSharp.text.Image.GetInstance(ppicURL);
                //below for making image in circle shape
                pjpg.ScaleToFit(25f, 25f);
                //Give space before image
                //jpg.SpacingBefore = 0f;
                //Give some space after the image
                pjpg.SetAbsolutePosition((pdfDoc.PageSize.Width / 3) * 0.119f, pdfDoc.PageSize.Height * 0.72f);
                cb.AddImage(pjpg);
                cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL).BaseFont, 15);
                cb.BeginText();
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, " : 9650101727", (pdfDoc.PageSize.Width / 3) * 0.119f + 80f, pdfDoc.PageSize.Height * 0.72f + 11f, 0);
                cb.EndText();


                ppicURL = "https://localhost:44330/../Images/address-gd183953d9_1280.png";
                pjpg = iTextSharp.text.Image.GetInstance(ppicURL);
                //below for making image in circle shape
                pjpg.ScaleToFit(25f, 25f);
                //Give space before image
                //jpg.SpacingBefore = 0f;
                //Give some space after the image
                pjpg.SetAbsolutePosition((pdfDoc.PageSize.Width / 3) * 0.119f, pdfDoc.PageSize.Height * 0.66f);
                cb.AddImage(pjpg);
                cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL).BaseFont, 15);
                cb.BeginText();
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, " : New Delhi", (pdfDoc.PageSize.Width / 3) * 0.119f + 80f, pdfDoc.PageSize.Height * 0.66f + 11f, 0);
                cb.EndText();

                ppicURL = "https://localhost:44330/../Images/calender-gaaf9e7094_1280.png";
                pjpg = iTextSharp.text.Image.GetInstance(ppicURL);
                //below for making image in circle shape
                pjpg.ScaleToFit(25f, 25f);
                //Give space before image
                //jpg.SpacingBefore = 0f;
                //Give some space after the image
                pjpg.SetAbsolutePosition((pdfDoc.PageSize.Width / 3) * 0.119f, pdfDoc.PageSize.Height * 0.60f);
                cb.AddImage(pjpg);
                cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL).BaseFont, 15);
                cb.BeginText();
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, " : 12/01/1999", (pdfDoc.PageSize.Width / 3) * 0.119f + 80f, pdfDoc.PageSize.Height * 0.60f + 10.4f, 0);
                cb.EndText();


                ppicURL = "https://localhost:44330/../Images/letter-g624f18f6a_1280.png";
                pjpg = iTextSharp.text.Image.GetInstance(ppicURL);
                //below for making image in circle shape
                pjpg.ScaleToFit(25f, 25f);
                //Give space before image
                //jpg.SpacingBefore = 0f;
                //Give some space after the image
                pjpg.SetAbsolutePosition((pdfDoc.PageSize.Width / 3) * 0.119f, pdfDoc.PageSize.Height * 0.54f);
                cb.AddImage(pjpg);
                cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL).BaseFont, 15);
                cb.BeginText();
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, " : Email", (pdfDoc.PageSize.Width / 3) * 0.119f + 70f, pdfDoc.PageSize.Height * 0.54f + 4f, 0);
                cb.EndText();
                cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL).BaseFont, 13);
                cb.BeginText();
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "ashishkr.jha1999@gmail.com", (pdfDoc.PageSize.Width / 3) * 0.119f + 70f, pdfDoc.PageSize.Height * 0.51f, 0);
                cb.EndText();
                #endregion

                #region Skills
                cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, iTextSharp.text.Font.NORMAL).BaseFont, 22);
                cb.BeginText();
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Skills", (pdfDoc.PageSize.Width / 10.0f) * 0.142f, pdfDoc.PageSize.Height * 0.46f, 0);
                cb.EndText();


                List<string> skill_list = new List<string> { "abcdefghijkl", "awerds","lkjhgf","iopeds",
                "lopsdf","qwertyuio","asdfghd","zxcvbnmkl","lkjhgfdsapoiuytr","uiosdfhjs","ytrewuid","qwer","wertyuiop"
                ,"wert","qwerd","wertyuio","qwer","qwertyuiopasd","wwwwww","qwertyui","wer","qw","wW"
                };

                int skilltier = 0;
                int index = 0;
                List<string[]> combination = new List<string[]>();
                string[] onetier = new string[3];

                string lastval = "";
                int lastvaltier = 0;
                foreach (var skill in skill_list)
                {

                    skilltier = skill.Length > 6 ? skill.Length > 12 ? 3 : 2 : 1;
                    switch (skilltier)
                    {
                        case 1:
                            if (lastvaltier == 0 || lastvaltier == 3 || index == 0)
                            {

                                onetier[index] = skill;
                                index++;
                                if (skill_list[skill_list.Count - 1] == skill)
                                {
                                    combination.Add(onetier);
                                    index = 0;
                                }
                            }
                            else
                            {
                                if (lastvaltier == 2)
                                {
                                    onetier[index] = skill;
                                    combination.Add(onetier);
                                    onetier = new string[3];
                                    index = 0;

                                }
                                else if (lastvaltier == 1 && index == 2)
                                {
                                    onetier[index] = skill;
                                    combination.Add(onetier);
                                    onetier = new string[3];
                                    index = 0;
                                }
                                else
                                {
                                    onetier[index] = skill;
                                    index++;
                                }
                            }
                            break;
                        case 2:
                            if (lastvaltier == 0 || lastvaltier == 3 || index == 0)
                            {
                                onetier[index] = skill;
                                index++;
                            }
                            else
                            {

                                if (lastvaltier == 2)
                                {
                                    if (lastval.Length + skill.Length < 19)
                                    {
                                        onetier[index] = skill;
                                        combination.Add(onetier);
                                        onetier = new string[3];
                                        index = 0;
                                    }
                                    else
                                    {
                                        combination.Add(onetier);
                                        onetier = new string[3];

                                        index = 0;
                                        onetier[index] = skill;
                                        index++;
                                    }

                                }
                                else if (lastvaltier == 1 && index == 1)
                                {
                                    onetier[index] = skill;
                                    combination.Add(onetier);
                                    onetier = new string[3];
                                    index = 0;
                                }
                                else
                                {
                                    combination.Add(onetier);
                                    onetier = new string[3];
                                    index = 0;

                                    onetier[index] = skill;
                                    index++;
                                    if (skill_list[skill_list.Count - 1] == skill)
                                    {
                                        combination.Add(onetier);
                                        index = 0;
                                    }


                                }
                            }


                            break;
                        case 3:
                            if (lastvaltier != 0 && index != 0)
                            {
                                if (lastval.Length + skill.Length < 19)
                                {
                                    onetier[index] = skill;
                                    combination.Add(onetier);
                                    onetier = new string[3];
                                    index = 0;
                                    lastval = skill;
                                    lastvaltier = skilltier;
                                    continue;
                                }
                                combination.Add(onetier);
                                onetier = new string[3];
                            }

                            onetier[index] = skill;
                            combination.Add(onetier);
                            onetier = new string[3];
                            index = 0;
                            break;
                        default:
                            break;
                    }

                    lastval = skill;
                    lastvaltier = skilltier;
                }





                var hght = pdfDoc.PageSize.Height * 0.42f;

                foreach (var skillrarr in combination)
                {
                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL).BaseFont, 14);
                    // cb.SetColorFill(new BaseColor());
                    cb.BeginText();
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "" + (skillrarr[0] != null ? " \u2022 " + skillrarr[0] : "") + "" + (skillrarr[1] != null ? " \u2022 " + skillrarr[1] : "") + "" + (skillrarr[2] != null ? " \u2022 " + skillrarr[2] : "") + "", 20, hght, 0);
                    cb.EndText();
                    hght -= 28;
                }

                #endregion





                //   cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA, 10,iTextSharp.text.Font.NORMAL).BaseFont, 10);
                //// cb.SetFontAndSize(FontFactory.GetFont("BOD_PSTC", 10, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0)).BaseFont,20);
                //  cb.BeginText();
                //  cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "INVOICE", 60, 596, 0);
                //  cb.EndText();


                //  pdfDoc.NewPage();



                pdfDoc.Close();
                stream.Close();

                #endregion

                #region Display PDF

                System.Diagnostics.Process.Start(Path.Combine(folderPath, strFileName));
                #endregion

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        
    }
}