using mvc.Interfaces;

namespace mvc.Services;
public class AppConfigService : IAppConfigService
{
    private readonly string _appName;

    public AppConfigService()
    {
        _appName = "My MVC";
    }

    public string GetAppName() => _appName;
}
