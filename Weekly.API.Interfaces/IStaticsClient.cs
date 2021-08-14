using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Weekly.Data;
using Weekly.Data.Dtos;

namespace Weekly.API.Client
{
    public interface IStaticsClient
    {
        string BaseUrl { get; set; }
        bool ReadResponseAsString { get; set; }

        System.Threading.Tasks.Task DeletePriorityAsync(Priority priority);
        System.Threading.Tasks.Task DeletePriorityAsync(Priority priority, CancellationToken cancellationToken);
        Task<ICollection<Priority>> GetPrioritiesAsync();
        Task<ICollection<Priority>> GetPrioritiesAsync(CancellationToken cancellationToken);
        System.Threading.Tasks.Task PutPriorityAsync(Priority priority);
        System.Threading.Tasks.Task PutPriorityAsync(Priority priority, CancellationToken cancellationToken);
    }
}