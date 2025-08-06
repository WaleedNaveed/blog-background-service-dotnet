using BackgroundServiceDemo.Helper;

namespace BackgroundServiceDemo.Interfaces
{
    public interface IBGSStatusService
    {
        BGSStatusEnum BGSStatus { get; set; }
    }
}
