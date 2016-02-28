using AdminConsole.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminConsole.Controllers
{
    public class RegistrationController : Controller
    {
        //
        // GET: /Registration/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StudentList()
        {
            var model = GetRegistrationList("Student");
            return PartialView(model);
        }

        public ActionResult TeacherList()
        {
            var model = GetRegistrationList("Teacher");
            return PartialView(model);
        }

        public ActionResult ExportStudentList()
        {
            var registrList = GetRegistrationList("Teacher");

            var sw = new StreamWriter(new MemoryStream());

            sw.WriteLine("\"Token\",\"Vards\",\"Uzvards\",\"Telefons\",\"Email\",\"Pilseta\",\"Skolas tips\",\"Skolas nosaukums\",\"IrIzveletaSkola\",\"Klase\",\"Skolotajs\",\"Kopnite\",\"KopnitesTips\"");

            foreach (var line in registrList)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\"",
                    line.PersonCode,
                    line.Name,
                    line.Surname,
                    line.Phone,
                    line.Email,
                    line.City,
                    line.StudyType,
                    line.School,
                    line.IsSelected,
                    line.SelectedKlass,
                    line.Advicer,
                    line.PlaceRequired,
                    line.PlaceRequiredType));
            }

            sw.Flush();
            sw.BaseStream.Seek(0, SeekOrigin.Begin);

            return File(sw.BaseStream, "text/csv", "ExportedStudentList.csv");
        }


        public ActionResult ExportTeacherList()
        {
            var registrList = GetRegistrationList("Teacher");

            var sw = new StreamWriter(new MemoryStream());

            sw.WriteLine("\"Vards\",\"Uzvards\",\"Telefons\",\"Email\",\"Pilseta\",\"Skolas tips\",\"Skolas nosaukums\",\"IrIzveletaSkola\",\"Kopnite\",\"KopnitesTips\"");

            foreach (var line in registrList)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\"",
                    line.Name,
                    line.Surname,
                    line.Phone,
                    line.Email,
                    line.City,
                    line.StudyType,
                    line.School,
                    line.IsSelected,
                    line.PlaceRequired,
                    line.PlaceRequiredType));
            }

            sw.Flush();
            sw.BaseStream.Seek(0, SeekOrigin.Begin);

            return File(sw.BaseStream, "text/csv", "ExportedTeacherList.csv");   
        }

        private List<RegistrationModel> GetRegistrationList(string regType)
        {

            var regList = new List<RegistrationModel>();

            var con = ConfigurationManager.ConnectionStrings["DataSourceConnectionString"].ToString();

            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "Select * from Registration where RegType=@regType";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                oCmd.Parameters.AddWithValue("@regType", regType);
                myConnection.Open();
                using (SqlDataReader reader = oCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var record = new RegistrationModel();

                        record.Id = long.Parse(reader["Id"].ToString());
                        record.PersonCode = reader["RegCode"].ToString();
                        record.Name = reader["Vards"].ToString();
                        record.Surname = reader["Uzvards"].ToString();
                        record.Phone = reader["Telefons"].ToString();
                        record.Email = reader["Email"].ToString();
                        record.City = reader["Pilseta"].ToString();
                        record.StudyType = reader["SkolasTips"].ToString();
                        record.School = reader["SkolasNosaukums"].ToString();
                        record.IsSelected = reader["IrIzveletaSkola"].ToString();
                        record.SelectedKlass = reader["SkolasKlase"].ToString();
                        record.Advicer = reader["Skolotajs"].ToString();
                        record.PlaceRequired = reader["Kopnite"].ToString();
                        record.PlaceRequiredType = reader["KopnitesTips"].ToString();

                        regList.Add(record);
                    }

                    myConnection.Close();
                }
            }

            return regList;
        }
    }
}
