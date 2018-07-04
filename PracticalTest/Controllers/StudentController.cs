using PracticalTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticalTest.Controllers
{
    public class StudentController : Controller
    {
        StudentEntities en = new StudentEntities();

        public List<Stu> GetStudentList()
        {
            List<Student> students = en.Students.ToList();
            
            List<Stu> slist = new List<Stu>();
            for (int i = 0; i < students.Count; i++)
            {
                Stu student = new Stu();
                student.ID = students[i].ID;
                student.NRIC = students[i].NRIC;
                student.StudentName = students[i].Name;
                student.Gender = students[i].Gender == "Female" ? "F" : "M";
                student.Age = DateTime.Now.Year - students[i].Birthday.Year;
                student.Number = 0;
                if (Convert.ToBoolean(students[i].English) == true)
                {
                    student.Number += 1;
                }
                if (Convert.ToBoolean(students[i].Math) == true)
                {
                    student.Number += 1;
                }
                if (Convert.ToBoolean(students[i].Science == true))
                {
                    student.Number += 1;
                }
                slist.Add(student);
            }
            return slist;
        }

        public int UpdateStudent(Stu s,int type)
        {
            int result = 0;
            Student student = new Student();
            if (type == 1)
            {
                student = en.Students.Where(x => x.ID == s.ID).FirstOrDefault();
            }
            student.NRIC = s.NRIC;
            student.Name = s.StudentName;
            student.Birthday = s.Birthday;
            student.AvailableDate = s.AvailableDate;
            student.Gender = s.Gender;
            student.English = s.English;
            student.Math = s.Math;
            student.Science = s.Science;
            using (StudentEntities context = new StudentEntities())
            {
                if (type == 0)
                {
                    context.Students.Add(student);
                }
                else
                {
                    context.Entry(student).State = System.Data.Entity.EntityState.Modified;
                }
                context.SaveChanges();
            }
            return result;
        }

        public Student GetStudent(int id)
        {
            
            using (StudentEntities context = new StudentEntities())
            {
                return context.Students.Where(x => x.ID == id).FirstOrDefault();
            }
            
           
        }

        // GET: Student
        public ActionResult Index()
        {
            try
            {
                return View(GetStudentList());
            }
            catch (Exception)
            {
                return View(new List<Stu>());
            }            
        }

        public ActionResult Search(string option,string search)
        {
            try
            {
                List<Stu> students = GetStudentList();
                if (option == "NRIC")
                {
                    students = students.Where(x => x.NRIC.ToString().ToLower().Contains(search.Trim().ToString().ToLower())).ToList();
                }
                else
                {
                    students = students.Where(x => x.StudentName.ToString().ToLower().Contains(search.Trim().ToString().ToLower())).ToList();
                }
                return View("Index", students);
            }
            catch (Exception)
            {
                return View("Index",new List<Stu>());
            }
          
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            try
            {
                Student s = GetStudent(id);
                Stu stu = new Stu();
                stu.NRIC = s.NRIC;
                stu.StudentName = s.Name;
                stu.Birthday = s.Birthday;
                stu.AvailableDate = s.AvailableDate;
                stu.Gender = s.Gender;
                stu.English = s.English;
                stu.Math = s.Math;
                stu.Science = s.Science;
                return View(stu);
            }
            catch (Exception)
            {
                return View("Index", new List<Stu>());
            }
            
        }

        [HttpPost]
        public ActionResult Create(Stu s)
        {
            try
            {
                UpdateStudent(s, 0);
                return View("Index", GetStudentList());
            }
            catch (Exception)
            {
                return View("Create");
            }
        }

        [HttpPost]
        public ActionResult Edit(Stu s)
        {
            try
            {
                UpdateStudent(s, 1);
                return View("Index", GetStudentList());
            }
            catch (Exception)
            {
                return View("Index",new List<Stu>());
            }
          
        }
    }
}