using Code.Views;
using UnityEngine;
using static Code.Utils.DataUtils;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "ConfigStore", menuName = "Configs/Store", order = 1)]
    internal sealed class ConfigStore : ScriptableObject
    {
        [SerializeField] [AssetPath.Attribute(typeof(PlayerConfig))] private string _playerCfgPath;
        
        private PlayerConfig _playerCfg;
        
        public PlayerConfig PlayerCfg => GetData(_playerCfgPath, _playerCfg);
    }
}