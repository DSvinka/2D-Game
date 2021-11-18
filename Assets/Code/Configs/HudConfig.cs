using UnityEngine;
using static Code.Utils.DataUtils;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "HudCfg", menuName = "Configs/Hud Cfg", order = 5)]
    internal sealed class HudConfig : ScriptableObject
    {
        [SerializeField] [AssetPath.Attribute(typeof(GameObject))] private string _prefabPath;

        private GameObject _prefab;

        public GameObject Prefab => GetData(_prefabPath, _prefab);
    }
}