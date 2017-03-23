using NPOI.HSSF.Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PaskalaRitenis.CustomManagers;
using PaskalaRitenis.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaskalaRitenis.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RegistrationController : Controller
        {

        private IUserRepository _repository;

        public RegistrationController()
            : this(new UserRepository())
        {
        }

        public RegistrationController(UserRepository repository)
        {
            _repository = repository;
        }
    
        //
        // GET: /Registration/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetStudents(DataTablesParams model)
        {
            DataTablesContext<Registration> result = new DataTablesContext<Registration>();

            model.Search = Request.QueryString["Search[value]"].ToString();

            result.data = _repository.GetRegistrations(model.Start, model.Length,"Student", model.Search);
            result.recordsTotal = _repository.CountRegistrations("Student",model.Search);
            result.recordsFiltered = result.recordsTotal;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Teachers()
        {
            return View();
        }

        public JsonResult GetTeachers(DataTablesParams model)
        {
            DataTablesContext<Registration> result = new DataTablesContext<Registration>();

            model.Search = Request.QueryString["Search[value]"].ToString();

            result.data = _repository.GetRegistrations(model.Start, model.Length, "Teacher", model.Search);
            result.recordsTotal = _repository.CountRegistrations("Teacher", model.Search);
            result.recordsFiltered = result.recordsTotal;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public void ExportStudentList()
        {
            var data = _repository.GetRegistrations(0, 99999, "Student", "");

            List<string> headers = new List<string>()
            {
                "Token",
                "Vards",
                "Uzvards",
                "Telefons",
                "Email",
                "Pilseta",
                "Skolas tips",
                "Skolas nosaukums",
                "IrIzveletaSkola",
                "Klase",
                "Kopnite",
                "KopnitesTips",
                "Pieprasījuma laiks"
            };


            string filename = "ExportedStudentList.xls";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();

            XlsManager manager = new XlsManager();

            HSSFWorkbook hssfworkbook = manager.CreateRegistrationXLS(headers, data);
            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);

            Response.BinaryWrite(file.GetBuffer());
            Response.End();

        }



        public void ExportTeacherList()
        {
            var data = _repository.GetRegistrations(0, 99999, "Teacher", "");

            List<string> headers = new List<string>()
            {
                "Token",
                "Vards",
                "Uzvards",
                "Telefons",
                "Email",
                "Pilseta",
                "Skolas tips",
                "Skolas nosaukums",
                "IrIzveletaSkola",
                "Klase",
                "Kopnite",
                "KopnitesTips",
                "Pieprasījuma laiks"
            };


            string filename = "ExportedTeacherList.xls";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();

            XlsManager manager = new XlsManager();

            HSSFWorkbook hssfworkbook = manager.CreateRegistrationXLS(headers, data);
            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);

            Response.BinaryWrite(file.GetBuffer());
            Response.End();
        }

        
    }
}
