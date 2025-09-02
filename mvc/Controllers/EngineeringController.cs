using Microsoft.AspNetCore.Mvc;
using mvc.Interfaces;

namespace mvc.Controllers;

public class EngineeringController : Controller
{
    private readonly IRequestTrackingService _tracking1;
    private readonly IRequestTrackingService _tracking2;

    public EngineeringController(IRequestTrackingService tracking1, IRequestTrackingService tracking2)
    {
        _tracking1 = tracking1;
        _tracking2 = tracking2;
    }

    public ActionResult Index()
    {
        // Scoped → same instance within this request
        ViewBag.RequestId1 = _tracking1.RequestId;
        ViewBag.RequestId2 = _tracking2.RequestId;
        ViewBag.StartTime = _tracking1.RequestStartTime;
        return View();
    }

    public ActionResult CurrentProjects()
    {
        var EnDept = new Models.EngineeringDepartment();
        return View(EnDept);
    }
}


