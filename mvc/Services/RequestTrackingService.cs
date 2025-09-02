using mvc.Interfaces;

namespace mvc.Services;

public class RequestTrackingService : IRequestTrackingService
{
    public Guid RequestId { get; }
    public DateTime RequestStartTime { get; }

    public RequestTrackingService()
    {
        RequestId = Guid.NewGuid();
        RequestStartTime = DateTime.UtcNow;
    }
}