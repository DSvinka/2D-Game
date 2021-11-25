using System.Globalization;
using Code.Configs;
using Code.Controllers.Initializations;
using Code.Interfaces.Controllers;
using Code.Views;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class LevelGeneratorController: Controller
    {
        private MarchingSquaresController _marchingSquaresController;
        private LevelInitialization _levelInitialization;
        
        private LevelView _levelView;
        private LevelGeneratorConfig _levelGeneratorConfig;

        private TileType[,] _map;
        
        private int _countWall = 4;

        public LevelGeneratorController(LevelInitialization levelInitialization, LevelGeneratorConfig levelGeneratorConfig, MarchingSquaresController marchingSquaresController)
        {
            _levelGeneratorConfig = levelGeneratorConfig;
            _levelInitialization = levelInitialization;
            _marchingSquaresController = marchingSquaresController;

            _map = new TileType[_levelGeneratorConfig.MapWidth, _levelGeneratorConfig.MapHeight];
        }
        
        public override void ReSetup(SceneViews sceneViews)
        {
            Cleanup();
            Initialization();
        }
        
        public override void Initialization()
        {
            _levelView = _levelInitialization.GetLevel();
            
            RandomFillMap();

            for (var i = 0; i < _levelGeneratorConfig.SmoothFactor; i++)
                SmoothMap();
  
            _marchingSquaresController.GenerateGrid(_map, 1);
            _marchingSquaresController.DrawTilesOnMap(_levelView.Tilemap, _levelGeneratorConfig.Tiles);

        }

        public override void Cleanup()
        {
            if (_levelView != null && _levelView.gameObject != null)
                Object.Destroy(_levelView.gameObject);
        }

        private void RandomFillMap()
        {
            var seed = Time.time.ToString(CultureInfo.InvariantCulture);
            var random = new System.Random(seed.GetHashCode());
            
            for (var x = 0; x < _levelGeneratorConfig.MapWidth; x++)
            {
                for (var y = 0; y < _levelGeneratorConfig.MapHeight; y++)
                {
                    if (x == 0 || y == _levelGeneratorConfig.MapWidth - 1 || y == 0 || y == _levelGeneratorConfig.MapHeight - 1)
                    {
                        if (_levelGeneratorConfig.Borders)
                        {
                            _map[x, y] = TileType.Dirt;
                        }
                    }
                    else
                    {
                        _map[x, y] = (random.Next(0, 100) < _levelGeneratorConfig.FillPercent) ? TileType.Dirt : TileType.Air;
                    }
                }
            }
        }

        private void SmoothMap()
        {
            for (var x = 0; x < _levelGeneratorConfig.MapWidth; x++)
            {
                for (var y = 0; y < _levelGeneratorConfig.MapHeight; y++)
                {
                    var neighbourWall = GetWallCount(x, y);

                    if (neighbourWall > _countWall)
                    {
                        if (y+1 < _levelGeneratorConfig.MapHeight && _map[x, y+1] == TileType.Air)
                            _map[x, y] = TileType.Grass;
                        else if (y+1 >= _levelGeneratorConfig.MapHeight)
                            _map[x, y] = TileType.Winter;
                        else
                            _map[x, y] = TileType.Dirt;
                    }
                    else if (neighbourWall < _countWall)
                    {
                        _map[x, y] = TileType.Air;
                    }
                }
            }
        }

        private int GetWallCount(int x, int y)
        {
            var wallCount = 0;

            for (var gridX = x-1; gridX <= x+1; gridX++)
            {
                for (var gridY = y-1; gridY <= y+1; gridY++)
                {
                    if (gridX >= 0 && gridX < _levelGeneratorConfig.MapWidth && gridY >= 0 && gridY < _levelGeneratorConfig.MapHeight)
                    {
                        if (gridX != x || gridY != y)
                        {
                            wallCount += _map[gridX, gridY] == TileType.Air ? 0 : 1;
                        }
                    }
                    else
                    {
                        wallCount++;
                    }
                }
            }

            return wallCount;
        }
    }

    public enum TileType
    {
        Air = 0,
        Dirt = 1,
        Grass = 2,
        Winter = 3,
    }
}