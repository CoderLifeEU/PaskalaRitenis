using PaskalaRitenis.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace PaskalaRitenis.Controllers
{
    public class RegistrController : Controller
    {
        //
        // GET: /Registr/

        public ActionResult StudentRegistr()
        {
            return View();
        }

        public ActionResult TeacherRegistr()
        {
            var model = new TeacherRegModel();
            model.School = RawSchoolList();

            return View(model);
        }

        [HttpPost]
        public ActionResult TeacherRegistr(TeacherRegModel model)
        {
            var schoolList = new List<SelectListItem>();           
            var schoolType = model.StudyType.Where(s => s.Value == model.SelectedStudyTypeId.ToString()).Select(t => t.Text).FirstOrDefault();

            var school = string.Empty;

            if (model.SelectedStudyTypeId == 0)
            {
                school = model.SpecialSchool;
            }
            else if (model.SelectedStudyTypeId == 1)
            {
                schoolList = RawSchoolList();
                school = schoolList.Where(s => s.Value == model.SelectedSchoolId.ToString()).Select(t => t.Text).FirstOrDefault();
            }
            else
            {
                schoolList = RawOtherList();
                school = schoolList.Where(s => s.Value == model.SelectedSchoolId.ToString()).Select(t => t.Text).FirstOrDefault();
            }

            var placeReq = model.PlaceRequired.Where(s => s.Value == model.PlaceRequiredId.ToString()).Select(t => t.Text).FirstOrDefault();

            using(SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DataSourceConnectionString"].ConnectionString))
            {
                con.Open();
                string command = @"INSERT INTO Registration (RegType, RegCode, Vards, Uzvards, Pilseta, Telefons, Email, SkolasTips, SkolasNosaukums,Kopnite) 
                                     VALUES ('" + model.RegType.ToString() + "', 'TTEST01','" + model.Name + "', '" + model.Surname + "', '" + model.City + "', '" + model.Phone + "', '" + model.Email + "', '" + schoolType + "', '" + school + "', '"+ placeReq +"')";

                using(SqlCommand query = new SqlCommand(command, con))
                {
                    query.ExecuteNonQuery();

                }
            }

            return RedirectToAction("Index", "Home");;
        }

        [HttpGet]
        public ActionResult GetSchoolList(int val)
        {
            List<SelectListItem> schoolNames = new List<SelectListItem>();

            if (val == 1)
            {
                schoolNames = RawSchoolList();
            }
            else if (val == 2)
            {
                schoolNames = RawOtherList();
            }

            return Json(schoolNames, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetSchoolClassList(int val)
        {
            List<SelectListItem> schoolClass = new List<SelectListItem>();

            if (val == 1)
            {
                schoolClass.Add(new SelectListItem { Value = "1", Text = "6. klase" });
                schoolClass.Add(new SelectListItem { Value = "2", Text = "7. klase" });
                schoolClass.Add(new SelectListItem { Value = "3", Text = "8. klase" });
                schoolClass.Add(new SelectListItem { Value = "4", Text = "9. klase" });
                schoolClass.Add(new SelectListItem { Value = "5", Text = "10. klase" });
                schoolClass.Add(new SelectListItem { Value = "6", Text = "11. klase" });
                schoolClass.Add(new SelectListItem { Value = "7", Text = "12. klase" });
            }
            else if (val == 2)
            {
                schoolClass.Add(new SelectListItem { Value = "1", Text = "1.kurss" });
                schoolClass.Add(new SelectListItem { Value = "2", Text = "2.kurss" });
                schoolClass.Add(new SelectListItem { Value = "3", Text = "3.kurss" });
                schoolClass.Add(new SelectListItem { Value = "4", Text = "4.kurss" });
            }

            return Json(schoolClass, JsonRequestBehavior.AllowGet);
        }

        private void SetSchoolList(List<SelectListItem> list)
        {
            list = RawSchoolList();
        }

        private List<SelectListItem> RawSchoolList()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DataSourceConnectionString"].ConnectionString);

            var sql = "SELECT * FROM [DU_IK_PaskalaRitenisNew].[dbo].[Skolas]";

            con.Open();
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new SelectListItem
                        {
                            Value = dr["Id"].ToString(),
                            Text = dr["Iestades_nosaukums"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        private List<SelectListItem> RawOtherList()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DataSourceConnectionString"].ConnectionString);

            var sql = "SELECT * FROM [DU_IK_PaskalaRitenisNew].[dbo].[Profes_Skolas]";

            con.Open();
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new SelectListItem
                        {
                            Value = dr["Id"].ToString(),
                            Text = dr["Iestades_nosaukums"].ToString()
                        });
                    }
                }
            }

            return list;
        }
    }
}
