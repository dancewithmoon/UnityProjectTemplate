using System;
using Scripts.UI.Services.Factory;

namespace Scripts.UI.Services.Screens
{
    public class ScreenService : IScreenService
    {
        private readonly IUIFactory _uiFactory;

        public ScreenService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(ScreenId screenId)
        {
            switch (screenId)
            {
                case ScreenId.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(screenId), screenId, null);
            }
        }
    }
}