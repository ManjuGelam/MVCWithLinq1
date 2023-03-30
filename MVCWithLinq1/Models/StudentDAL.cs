using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWithLinq1.Models
{
    public class StudentDAL
    {
        MVCDBDataContext context = new MVCDBDataContext();
        public List<Student> GetStudents(bool? Status)
        {
            List<Student> students;
            try
            {
                if (Status != null)
                    students = (from s in context.Students where s.Status == Status select s).ToList();
                else
                    students = context.Students.ToList();
                return students;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Student GetStudent(int Sid, bool? Status)
        {
            Student student;
            try
            {
                if (Status == null)
                    student = (from s in context.Students where s.Sid == Sid select s).Single();
                else
                    student = (from s in context.Students where s.Sid == Sid && s.Status == Status select s).Single();
                return student;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddStudent(Student student)
        {
            try
            {
                context.Students.InsertOnSubmit(student);
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateStudent(Student newValues)
        {
            try
            {
                Student oldValues = context.Students.Single(S => S.Sid == newValues.Sid);
                oldValues.Name = newValues.Name;
                oldValues.Class = newValues.Class;
                oldValues.Fees = newValues.Fees;
                oldValues.Photo = newValues.Photo;
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteStudent(int Sid)
        {
            try
            {
                Student oldValues = context.Students.First(S => S.Sid == Sid);
                //dc.Students.DeleteOnSubmit(oldValues); //Permenant Deletion 
                oldValues.Status = false; //Updates the status with-out deleting the record
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}