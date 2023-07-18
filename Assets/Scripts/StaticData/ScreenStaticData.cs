using Scripts.UI.Screens;
using Scripts.UI.Services.Screens;
using UnityEngine;

namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "Screens", menuName = "StaticData/Screens")]
    public class ScreenStaticData : ScriptableObject
    {
        public SerializableDictionary<ScreenId, BaseScreen> Screens;
    }
}