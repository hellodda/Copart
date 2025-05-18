using System.Threading.Channels;

namespace Copart.BLL.Services.BackgroundJob
{
    public class BackgroundJobQueue : IBackgroundJobQueue
    {
        private readonly List<Func<CancellationToken, Task>> _jobs = new();
        private readonly object _locker = new();
        private int _currentIndex = 0;

        public void EnqueueJob(Func<CancellationToken, Task> job)
        {
            if (job is null) throw new ArgumentNullException(nameof(job));

            lock (_locker)
            {
                _jobs.Add(job);
            }
        }

        public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            lock (_locker)
            {
                var job = _jobs[_currentIndex];
                _currentIndex = (_currentIndex + 1) % _jobs.Count;
                return job;
            }
        }
    }
}
