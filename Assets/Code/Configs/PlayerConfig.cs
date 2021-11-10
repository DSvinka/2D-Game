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
        
        [SerializeField] private float _maxHealth = 20f;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _jumpTreshold = 0.01f;
        [SerializeField] private float _speedWalk = 5f;

        private GameObject _prefab;
        private SpriteAnimatorConfig _spriteAnimatorCfg;

        public GameObject Prefab => GetData(_prefabPath, _prefab);
        public SpriteAnimatorConfig SpriteAnimatorCfg => GetData(_spriteAnimatorCfgPath, _spriteAnimatorCfg);
        public float MaxHealth => _maxHealth;
        public float SpeedWalk => _speedWalk;
        public float JumpForce => _jumpForce;
        public float JumpTreshold => _jumpTreshold;
    }
}