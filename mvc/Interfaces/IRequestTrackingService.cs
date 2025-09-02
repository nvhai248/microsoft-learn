namespace mvc.Interfaces;

public interface IRequestTrackingService
{
    Guid RequestId { get; }
    DateTime RequestStartTime { get; }
}