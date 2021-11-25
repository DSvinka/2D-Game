using UnityEngine;
using static Code.Utils.DataUtils;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "ConfigStore", menuName = "Configs/Store", order = 0)]
    internal sealed class ConfigStore : ScriptableObject
    {
        [SerializeField] [AssetPath.Attribute(typeof(PlayerConfig))] private string _playerCfgPath;
        [SerializeField] [AssetPath.Attribute(typeof(HudConfig))] private string _hudCfgPath;
        [SerializeField] [AssetPath.Attribute(typeof(BulletConfig))] private string _bulletCfgPath;
        [SerializeField] [AssetPath.Attribute(typeof(SpriteAnimatorConfig))] private string _coinAnimCfgPath;
        [SerializeField] [AssetPath.Attribute(typeof(LevelConfig))] private string _levelCfgPath;

        private HudConfig _hudCfg;
        private PlayerConfig _playerCfg;
        private BulletConfig _bulletCfg;
        private SpriteAnimatorConfig _coinAnimCfg;
        private LevelConfig _levelCfg;
        
        public PlayerConfig PlayerCfg => GetData(_playerCfgPath, _playerCfg);
        public HudConfig HudCfg => GetData(_hudCfgPath, _hudCfg);
        public BulletConfig BulletCfg => GetData(_bulletCfgPath, _bulletCfg);
        public SpriteAnimatorConfig CoinAnimCfg => GetData(_coinAnimCfgPath, _coinAnimCfg);
        public LevelConfig LevelCfg => GetData(_levelCfgPath, _levelCfg);
    }
}