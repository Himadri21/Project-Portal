using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectProgress.Models;

namespace ProjectProgress.Controllers
{
    
    public class StudentsController : Controller
    {
        private ProjectProgressModel db = new ProjectProgressModel();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Department).Include(s => s.Mark).Include(s => s.Project).Include(s => s.Teacher);
            return View(students.ToList());
        }
        // GET : Students
        public ActionResult SelectIndex()
        {
            var students = db.Students.Include(s => s.Department).Include(s => s.Mark).Include(s => s.Project).Include(s => s.Teacher);
            return View(students.ToList());
        }
        // GET: Students/Details/5
        public ActionResult Details()
        {
            var id = User.Identity.Name;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Student> students = db.Students.Where(s => s.Teacher.TeacherName == id).ToList() ;
            if(students.Count()==0)
            {
                return RedirectToAction("Create", "Students");
            }
            return View(students);
        }

        // GET: Students/Select/5
        public ActionResult Select(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", student.DepartmentID);
            ViewBag.StudentUSN = new SelectList(db.Students, "StudentUSN", "StudentUSN", student.StudentUSN);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectTitle", student.ProjectID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "TeacherName", student.TeacherID);
            return View(student);
        }

        // POST: Students/Select/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Select([Bind(Include = "StudentUSN,StudentName,StudentPhoneNumber,Semester,StudentEmailId,DepartmentID,ProjectID,TeacherID")] Student student)
        {
            if (ModelState.IsValid)
            {
                var CurrentStudent = db.Students.FirstOrDefault(p => p.StudentUSN == student.StudentUSN);
                CurrentStudent.ProjectID = student.ProjectID;
                CurrentStudent.TeacherID = student.TeacherID;
                db.SaveChanges();
                return RedirectToAction("SelectIndex");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", student.DepartmentID);
            ViewBag.StudentUSN = new SelectList(db.Students, "StudentUSN", "StudentUSN", student.StudentUSN);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectTitle", student.ProjectID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "TeacherName", student.TeacherID);
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            ViewBag.StudentUSN = new SelectList(db.Marks, "StudentUSN", "StudentUSN");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentUSN,StudentName,StudentPhoneNumber,Semester,StudentEmailId,DepartmentID,ProjectID,TeacherID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", student.DepartmentID);
            ViewBag.StudentUSN = new SelectList(db.Marks, "StudentUSN", "StudentUSN", student.StudentUSN);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", student.DepartmentID);
            ViewBag.StudentUSN = new SelectList(db.Marks, "StudentUSN", "StudentUSN", student.StudentUSN);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectTitle", student.ProjectID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "TeacherName", student.TeacherID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentUSN,StudentName,StudentPhoneNumber,Semester,StudentEmailId,DepartmentID,ProjectID,TeacherID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", student.DepartmentID);
            ViewBag.StudentUSN = new SelectList(db.Marks, "StudentUSN", "StudentUSN", student.StudentUSN);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectTitle", student.ProjectID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "TeacherName", student.TeacherID);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
