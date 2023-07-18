using Scripts.Data;
using Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI.Screens
{
    public abstract class BaseScreen : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        protected IPersistentProgressService ProgressService;
        protected PlayerProgress Progress => ProgressService.Progress;

        [Inject]
        public void Construct(IPersistentProgressService progressService)
        {
            ProgressService = progressService;
        }

        private void Awake() => OnAwake();

        protected virtual void OnAwake()
        {
            _closeButton.onClick.AddListener(() => Destroy(gameObject));
        }

        private void Start()
        {
            Initialize();
            Subscribe();
        }

        private void OnDestroy()
        {
            Cleanup();
        }

        protected virtual void Initialize(){}
        protected virtual void Subscribe(){}
        protected virtual void Cleanup(){}
    }
}