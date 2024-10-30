using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using University.Models;
namespace University.Controllers
{
    public class StudentsController : Controller
    {
        private SimeDBEntities db = new SimeDBEntities();

        // GET: Students
        public ActionResult Index()
        {
            // Filtrar solo los estudiantes que no han sido eliminados lógicamente
            var students = db.Student.Where(s => !s.IsDeleted).ToList();
            return View(students);
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Buscar el estudiante y verificar si está eliminado
            Student student = db.Student.Find(id);
            if (student == null || student.IsDeleted)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstName,Email,EnrollmentDate,Phone")] Student student) // Sin StudentID
        {
            if (ModelState.IsValid)
            {
                student.IsDeleted = false; // Asegúrate de que esté activo al crear
                db.Student.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Buscar el estudiante y verificar si está eliminado
            Student student = db.Student.Find(id);
            if (student == null || student.IsDeleted)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,LastName,FirstName,Email,EnrollmentDate,Phone,IsDeleted")] Student student) // Incluye IsDeleted
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Buscar el estudiante y verificar si está eliminado
            Student student = db.Student.Find(id);
            if (student == null || student.IsDeleted)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Student.Find(id);
            if (student != null)
            {
                student.IsDeleted = true; // Marcar como eliminado
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
            }
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
