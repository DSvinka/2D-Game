using Code.Views;
using UnityEngine;
using static Code.Utils.DataUtils;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "ConfigStore", menuName = "Configs/Store", order = 1)]
    internal sealed class ConfigStore : ScriptableObject
    {
        [SerializeField] [AssetPath.Attribute(typeof(SpriteAnimatorConfig))] private string _playerAnimatorCfgPath;
        [SerializeField] [AssetPath.Attribute(typeof(PlayerConfig))] private string _playerCfgPath;
        
        private SpriteAnimatorConfig _playerAnimationCfg;
        private PlayerConfig _playerCfg;

        public SpriteAnimatorConfig PlayerAnimationCfg => GetData(_playerAnimatorCfgPath, _playerAnimationCfg);
        public PlayerConfig PlayerCfg => GetData(_playerCfgPath, _playerCfg);
    }
}