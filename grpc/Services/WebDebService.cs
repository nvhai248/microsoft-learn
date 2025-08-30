using Grpc.Core;
using grpc;

namespace grpc.Services;

public class WebDevService : WebDev.WebDevBase
{
    private readonly ILogger<WebDevService> _logger;
    public WebDevService(ILogger<WebDevService> logger)
    {
        _logger = logger;
    }

    public override Task<ProjectConfirmation> CreateProject(Project request, ServerCallContext context)
    {
        return Task.FromResult(new ProjectConfirmation
        {
            Message = "New Project created: " + request.Name
        });
    }
}
