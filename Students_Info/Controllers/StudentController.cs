using Microsoft.AspNetCore.Mvc;
using Students_Info.DataContext;
using Students_Info.Models;

namespace Students_Info.Controllers
{
    // use command add-migration
    // update-database 
    public class StudentController : Controller
    {
        public readonly CollectionContext _ctx;
        public StudentController(CollectionContext ctx)
        {
            _ctx = ctx; 
        }    
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InsertStudent(Student std)
        {
            _ctx.students.Add(std);
            _ctx.SaveChanges();

            return RedirectToAction("ShowAllStudents", "Student");
        }

        [HttpGet]
        public IActionResult EditStudent(int id) 
        {
            Student newStd= new Student();
            List<Student> stdList = new List<Student>();
            stdList = _ctx.students.ToList();
            foreach(Student s in stdList) 
            {
                if(s.StdId== id)
                {
                   newStd=s;
                }
                
            }
            
            return View(newStd);
        }

        [HttpPost]
        public IActionResult EditStudent(Student newStd) 
        {
            Student Std = new Student();
            List<Student> stdList = new List<Student>();
            stdList = _ctx.students.ToList();
            foreach (Student s in stdList)
            {
                if (s.StdId == newStd.StdId)
                {
                    s.StdId=newStd.StdId;
                    s.StdName=newStd.StdName;
                    s.FatherName=newStd.FatherName;
                    s.MotherName=newStd.MotherName;
                    s.StdAge = newStd.StdAge;
                    s.HomeAddress=newStd.HomeAddress;                 
                    s.RegistrationDate=newStd.RegistrationDate;
                    Std = s;

                }
               
            }
            _ctx.students.Update(Std);
            _ctx.SaveChanges();
            return RedirectToAction("ShowAllStudents", "Student");
        }

        public IActionResult DeleteStudent(int id)
        {
                     
            Student student = _ctx.students.FirstOrDefault(u=>u.StdId == id);   

            if (student != null)
            {
                student.IsDeleted = true; // Set the IsDeleted property to true
                _ctx.SaveChanges();
            }


            return RedirectToAction("ShowAllStudents", "Student");
        }



        public IActionResult ShowAllStudents(string registrationDateFilter, string studentNameFilter, string homeCityFilter)
        {
            ViewBag.RegistrationDateFilter = registrationDateFilter;
            ViewBag.StudentNameFilter = studentNameFilter;
            ViewBag.HomeCityFilter = homeCityFilter;

            List<Student> filteredStudents = _ctx.students
                .Where(s => !s.IsDeleted)
                .ToList(); // Load data into memory using ToList()

            if (!string.IsNullOrEmpty(registrationDateFilter))
            {
                DateTime parsedDate = DateTime.Parse(registrationDateFilter);
                filteredStudents = filteredStudents
                    .Where(s => s.RegistrationDate.Date == parsedDate.Date)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(studentNameFilter))
            {
                filteredStudents = filteredStudents
                    .Where(s => s.StdName.Contains(studentNameFilter, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(homeCityFilter))
            {
                filteredStudents = filteredStudents
                    .Where(s => s.HomeAddress.Contains(homeCityFilter, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View(filteredStudents);
        }


    }
}
