using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MVCWithLinq1.Models;


namespace MVCWithLinq1.Controllers
{
    public class StudentController : Controller
    {
        StudentDAL obj = new StudentDAL();
        public ViewResult DisplayStudents()
        {
            return View(obj.GetStudents(true));
        }
        public ViewResult DisplayStudent(int Sid)
        {
            return View(obj.GetStudent(Sid, true));
        }
        [HttpGet]
        public ViewResult AddStudent()
        {
            return View(new Student());
        }
        [HttpPost]
        public RedirectToRouteResult AddStudent(Student student, HttpPostedFileBase selectedFile)
        {
            if (selectedFile != null)
            {
                //Checking whether the folder "Uploads" is exists or not and creating it if not exists
                string PhysicalPath = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(PhysicalPath))
                {
                    Directory.CreateDirectory(PhysicalPath);
                }
                selectedFile.SaveAs(PhysicalPath + selectedFile.FileName);
                student.Photo = selectedFile.FileName;
            }
            student.Status = true;
            obj.AddStudent(student);
            return RedirectToAction("DisplayStudents");
        }
        public ViewResult EditStudent(int Sid)
        {
            Student student = obj.GetStudent(Sid, true);
            TempData["Photo"] = student.Photo;
            return View(student);
        }
        public RedirectToRouteResult UpdateStudent(Student student, HttpPostedFileBase selectedFile)
        {
            if (selectedFile != null)
            {
                string PhysicalPath = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(PhysicalPath))
                {
                    Directory.CreateDirectory(PhysicalPath);
                }
                selectedFile.SaveAs(PhysicalPath + selectedFile.FileName);
                student.Photo = selectedFile.FileName;
            }
            else if (TempData["Photo"] != null)
            {
                student.Photo = TempData["Photo"].ToString();
            }
            obj.UpdateStudent(student);
            return RedirectToAction("DisplayStudents");
        }
        public RedirectToRouteResult DeleteStudent(int Sid)
        {
            obj.DeleteStudent(Sid);
            return RedirectToAction("DisplayStudents");
        }

    }
}