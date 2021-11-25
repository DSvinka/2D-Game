using System.Globalization;
using Code.Configs;
using Code.Controllers.Initializations;
using Code.Interfaces.Controllers;
using Code.Views;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class LevelGeneratorController: IController, IStart, ICleanup
    {
        private MarchingSquaresController _marchingSquaresController;
        private LevelInitialization _levelInitialization;
        
        private LevelView _levelView;
        private LevelConfig _levelConfig;

        private int[,] _map;
        
        private int _countWall = 4;

        public LevelGeneratorController(LevelInitialization levelInitialization, LevelConfig levelConfig, MarchingSquaresController marchingSquaresController)
        {
            _levelConfig = levelConfig;
            _levelInitialization = levelInitialization;
            _marchingSquaresController = marchingSquaresController;

            _map = new int[_levelConfig.MapWidth, _levelConfig.MapHeight];
        }

        public void Setup(SceneViews sceneViews) { }
        public void ReSetup(SceneViews sceneViews)
        {
            Cleanup();
            Start();
        }
        
        public void Start()
        {
            _levelView = _levelInitialization.GetLevel();
            
            RandomFillMap();

            for (var i = 0; i < _levelConfig.SmoothFactor; i++)
                SmoothMap();
  
            _marchingSquaresController.GenerateGrid(_map, 1);
            _marchingSquaresController.DrawTilesOnMap(_levelView.Tilemap, _levelConfig.GroundTile);

        }

        public void Cleanup()
        {
            if (_levelView != null && _levelView.gameObject != null)
                Object.Destroy(_levelView.gameObject);
        }

        private void RandomFillMap()
        {
            var seed = Time.time.ToString(CultureInfo.InvariantCulture);
            var random = new System.Random(seed.GetHashCode());
            
            for (var x = 0; x < _levelConfig.MapWidth; x++)
            {
                for (var y = 0; y < _levelConfig.MapHeight; y++)
                {
                    if (x == 0 || y == _levelConfig.MapWidth - 1 || y == 0 || y == _levelConfig.MapHeight - 1)
                    {
                        if (_levelConfig.Borders)
                        {
                            _map[x, y] = 1;
                        }
                    }
                    else
                    {
                        _map[x, y] = (random.Next(0, 100) < _levelConfig.FillPercent) ? 1 : 0;
                    }
                }
            }
        }

        private void SmoothMap()
        {
            for (var x = 0; x < _levelConfig.MapWidth; x++)
            {
                for (var y = 0; y < _levelConfig.MapHeight; y++)
                {
                    var neighbourWall = GetWallCount(x, y);

                    if (neighbourWall > _countWall)
                    {
                        _map[x, y] = 1;
                    }
                    else if (neighbourWall < _countWall)
                    {
                        _map[x, y] = 0;
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
                    if (gridX >= 0 && gridX < _levelConfig.MapWidth && gridY >= 0 && gridY < _levelConfig.MapHeight)
                    {
                        if (gridX != x || gridY != y)
                        {
                            wallCount += _map[gridX, gridY];
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
}