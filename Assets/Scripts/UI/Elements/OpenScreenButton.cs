using Scripts.UI.Services.Screens;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI.Elements
{
    [RequireComponent(typeof(Button))]
    public class OpenScreenButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private ScreenId _screenId;
        private IScreenService _screenService;

        [Inject]
        public void Construct(IScreenService screenService)
        {
            _screenService = screenService;
        }
        
        private void Awake()
        {
            _button.onClick.AddListener(Open);
        }

        private void Open()
        {
            _screenService.Open(_screenId);
        }
    }
}