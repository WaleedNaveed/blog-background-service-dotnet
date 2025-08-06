using BackgroundServiceDemo.Helper;
using BackgroundServiceDemo.Interfaces;

namespace BackgroundServiceDemo.Services.Background
{
    public class UserImportBGS : BackgroundService, IUserImportTrigger
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IBGSStatusService _bGSStatusService;
        private bool _shouldRun;

        public UserImportBGS(IServiceProvider serviceProvider, IBGSStatusService bGSStatusService)
        {
            _serviceProvider = serviceProvider;
            _bGSStatusService = bGSStatusService;
        }

        public void TriggerImport()
        {
            _shouldRun = true;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_shouldRun)
                {
                    using var scope = _serviceProvider.CreateScope();
                    var userImportService = scope.ServiceProvider.GetRequiredService<IUserImportService>();

                    try
                    {
                        _bGSStatusService.BGSStatus = BGSStatusEnum.InProgress;
                        await userImportService.ImportAsync(stoppingToken);
                        _bGSStatusService.BGSStatus = BGSStatusEnum.Completed;
                    }
                    catch (Exception)
                    {
                        _bGSStatusService.BGSStatus = BGSStatusEnum.Failed;
                    }

                    _shouldRun = false;
                }

                await Task.Delay(1000, stoppingToken); // poll every second
            }
        }
    }
}
