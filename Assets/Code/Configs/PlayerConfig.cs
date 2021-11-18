using UnityEngine;
using static Code.Utils.DataUtils;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "PlayerCfg", menuName = "Configs/Player Cfg", order = 1)]
    internal sealed class PlayerConfig : ScriptableObject
    {
        [SerializeField] [AssetPath.Attribute(typeof(GameObject))] 
        private string _playerPrefabPath;
        [SerializeField] [AssetPath.Attribute(typeof(GameObject))] 
        private string _cameraPrefabPath;
        [SerializeField] [AssetPath.Attribute(typeof(SpriteAnimatorConfig))]
        private string _spriteAnimatorCfgPath; 
        
        [Header("Игрок")]
        [SerializeField] private float _maxHealth = 20f;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _jumpTreshold = 0.01f;
        [SerializeField] private float _movementTreshold = 0.01f;
        [SerializeField] private float _fallingVelocity = 2.5f;
        [SerializeField] private float _speedWalk = 5f;

        [Header("Камера")] 
        [SerializeField] private Vector2 _cameraOffset;
        [SerializeField] private float _cameraSpeed = 300f;
        [SerializeField] private float _cameraDistance = -10f;

        private GameObject _playerPrefab;
        private GameObject _cameraPrefab;
        private SpriteAnimatorConfig _spriteAnimatorCfg;

        public GameObject PlayerPrefab => GetData(_playerPrefabPath, _playerPrefab);
        public GameObject CameraPrefab => GetData(_cameraPrefabPath, _cameraPrefab);
        public SpriteAnimatorConfig SpriteAnimatorCfg => GetData(_spriteAnimatorCfgPath, _spriteAnimatorCfg);
        
        public float MaxHealth => _maxHealth;
        public float SpeedWalk => _speedWalk;
        public float JumpForce => _jumpForce;
        public float JumpTreshold => _jumpTreshold;
        public float MovementTreshold => _movementTreshold;
        public float FallingVelocity => _fallingVelocity;

        public Vector2 CameraOffset => _cameraOffset;
        public float CameraDistance => _cameraDistance;
        public float CameraSpeed => _cameraSpeed;
    }
}