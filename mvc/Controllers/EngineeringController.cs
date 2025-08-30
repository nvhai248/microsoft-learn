using Microsoft.AspNetCore.Mvc;

namespace mvc.Controllers;

public class EngineeringController : Controller
{
    public ActionResult Index()
    {
        return View();
    }

    public ActionResult CurrentProjects()
    {
        var EnDept = new Models.EngineeringDepartment();
        return View(EnDept);
    }
}


