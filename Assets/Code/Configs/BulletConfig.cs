using static Code.Utils.DataUtils;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "BulletCfg", menuName = "Configs/Bullet Cfg", order = 5)]
    internal sealed class BulletConfig : ScriptableObject
    {
        [SerializeField] [AssetPath.Attribute(typeof(GameObject))] private string _prefabPath;
        [SerializeField] private float _damage;

        private GameObject _prefab;

        public GameObject Prefab => GetData(_prefabPath, _prefab);
        public float Damage => _damage;
    }
}