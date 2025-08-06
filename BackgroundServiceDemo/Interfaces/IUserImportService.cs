namespace BackgroundServiceDemo.Interfaces
{
    public interface IUserImportService
    {
        Task ImportAsync(CancellationToken cancellationToken);
    }
}
