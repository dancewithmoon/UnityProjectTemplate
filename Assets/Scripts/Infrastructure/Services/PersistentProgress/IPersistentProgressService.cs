using Scripts.Data;

namespace Scripts.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }

    public interface IReadonlyProgressService : IService
    {
        IReadOnlyPlayerProgress ProgressReadonly { get; }
    }
}