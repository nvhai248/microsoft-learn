namespace mvc.Models;

public class EngineeringDepartment
{
    public string[] CurrentSoftwareProjects { get; set; }

    public EngineeringDepartment()
    {
        // retrieve data from DB or web service
        CurrentSoftwareProjects = ["MVC", "Razor", "Blazor"];
    }
}
