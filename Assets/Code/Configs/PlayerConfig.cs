using UnityEngine;
using static Code.Utils.DataUtils;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "PlayerCfg", menuName = "Configs/Player Cfg", order = 0)]
    internal sealed class PlayerConfig : ScriptableObject
    {
        [SerializeField] [AssetPath.Attribute(typeof(GameObject))] 
        private string _prefabPath;
        [SerializeField] [AssetPath.Attribute(typeof(SpriteAnimatorConfig))]
        private string _spriteAnimatorCfgPath; 
        
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _speedWalk;

        private GameObject _prefab;
        private SpriteAnimatorConfig _spriteAnimatorCfg;

        public GameObject Prefab => GetData(_prefabPath, _prefab);
        public SpriteAnimatorConfig SpriteAnimatorCfg => GetData(_spriteAnimatorCfgPath, _spriteAnimatorCfg);
        public float MaxHealth => _maxHealth;
        public float SpeedWalk => _speedWalk;
        public float JumpForce => _jumpForce;
    }
}