using System.Threading.Channels;

namespace Copart.BLL.Services.BackgroundJob
{
    public class BackgroundJobQueue : IBackgroundJobQueue
    {
        private readonly Channel<Func<CancellationToken, Task>> _queue;

        public BackgroundJobQueue()
        {
            _queue = Channel.CreateUnbounded<Func<CancellationToken, Task>>();
        }

        public void EnqueueJob(Func<CancellationToken, Task> job)
        {
            if (!_queue.Writer.TryWrite(job))
            {
                throw new Exception("Canot add job to the queue");
            }
        }

        public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}
