using BackgroundServiceDemo.Helper;
using BackgroundServiceDemo.Interfaces;

namespace BackgroundServiceDemo.Services.Background
{
    public class BGSStatusService : IBGSStatusService
    {
        public BGSStatusEnum BGSStatus { get; set; } = BGSStatusEnum.NotStarted;
    }
}
