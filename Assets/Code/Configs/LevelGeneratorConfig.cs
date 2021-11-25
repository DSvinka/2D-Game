using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Code.Utils.DataUtils;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "LevelGenCfg", menuName = "Configs/Level Gen Cfg", order = 1)]
    internal sealed class LevelGeneratorConfig : ScriptableObject
    {
        [SerializeField] [AssetPath.Attribute(typeof(GameObject))] 
        private string _prefabPath;

        [SerializeField] private Tiles _tiles;
        [SerializeField] private int _mapWidth;
        [SerializeField] private int _mapHeight;
        [SerializeField] private bool _borders;

        [SerializeField] [Range(0, 100)] private int _fillPercent;
        [SerializeField] [Range(0, 100)] private int _smoothFactor;

        private GameObject _prefab;

        public GameObject Prefab => GetData(_prefabPath, _prefab);
        public Tiles Tiles => _tiles;
        public int MapWidth => _mapWidth;
        public int MapHeight => _mapHeight;
        public bool Borders => _borders;
        
        public int FillPercent => _fillPercent;
        public int SmoothFactor => _smoothFactor;
    }

    [Serializable]
    internal struct Tiles
    {
        public Tile WinterTile;
        public Tile GrassTile;
        public Tile DirtTile;
    }
}