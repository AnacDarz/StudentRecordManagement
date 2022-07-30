using Microsoft.AspNetCore.Mvc;
using StudentRecordManagementApp.Models;
using StudentRecordManagementApp.Services;

namespace StudentRecordManagementApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IStudentService _studentServices;
        public StudentController(IConfiguration configuration, IStudentService studentServices)
        {
            _configuration = configuration;
            _studentServices = studentServices;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult StudentsList()
        {
            AllModels model = new AllModels();

            model.StudentsList = _studentServices.GetStudentList().ToList();

            return View(model);
        }
        [HttpPost]
        public IActionResult InsertUpdateStudentRecord(Student formData)
        {
            AllModels model = new AllModels();

            formData.CreateBy = 1;
            formData.CreateOn = DateTime.Now;
            
            string url = Request.Headers["Referer"].ToString();

            string result = String.Empty;
            if (formData.StudentID>0)
            {
                 result = _studentServices.UpdateStudentRecord(formData);
            }
            else
            {
                 result = _studentServices.InsertStudent(formData);
            }

            
            if (result== "Saved Successfully")
            {
                TempData["SuccessMsg"] = "Saved Successfully";
                return Redirect(url);
            }
            else
            {
                TempData["ErrorMsg"] = result;
                return Redirect(url);
            }
        }
        [HttpPost]
        public JsonResult DeleteStudentRecord(int StudentID)
        {
            string url = Request.Headers["Referer"].ToString();

            string result = _studentServices.DeleteStudentRecord(StudentID);
            if (result == "Deleted Successfully")
            {
                return Json(new { message = "Deleted Successfully" });
            }
            else
            {
                return Json(new { message = "An Error occured" });
            }
        }
    }
}
