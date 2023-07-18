using Scripts.Infrastructure.Services.ContainerService;
using Zenject;

namespace Scripts.Infrastructure
{
    public class SceneBootstrapper : MonoInstaller
    {
        [Inject] public ContainerService ContainerService;
        
        public override void InstallBindings()
        {
            ContainerService.Container = Container;
        }
    }
}