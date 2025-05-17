namespace Copart.BLL.Services.BackgroundJob
{
    public interface IBackgroundJobQueue
    {
        void EnqueueJob(Func<CancellationToken, Task> job);
        public Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
    }
}
