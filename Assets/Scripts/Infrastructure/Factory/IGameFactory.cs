using System.Collections.Generic;
using System.Threading.Tasks;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.PersistentProgress;

namespace Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        Task WarmUp();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void CleanUp();
    }
}