using System.Threading.Tasks;
using Scripts.Infrastructure.Services;

namespace Scripts.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        Task WarmUp();
        void CreateUIRoot();
    }
}