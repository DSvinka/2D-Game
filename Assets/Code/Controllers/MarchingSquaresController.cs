using System;
using Code.Configs;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Code.Controllers
{
    internal sealed class MarchingSquaresController
    {
        private SquareGrid _squareGrid;
        private Tilemap _tileMap;
        private Tiles _tiles;

        public void GenerateGrid(TileType[,] map, float squareSize)
        {
            _squareGrid = new SquareGrid(map, squareSize);
        }

        public void DrawTilesOnMap(Tilemap tileMap, Tiles tiles)
        {
            if (_squareGrid == null)
                return;

            _tileMap = tileMap;
            _tiles = tiles;

            for (var x = 0; x < _squareGrid.Squares.GetLength(0); x++)
            {
                for (var y = 0; y < _squareGrid.Squares.GetLength(1); y++)
                {
                    DrawTileInControlNode(_squareGrid.Squares[x, y].TopLeft.TileType, _squareGrid.Squares[x, y].TopLeft.Position);
                    DrawTileInControlNode(_squareGrid.Squares[x, y].TopRight.TileType, _squareGrid.Squares[x, y].TopRight.Position);
                    DrawTileInControlNode(_squareGrid.Squares[x, y].BottomRight.TileType, _squareGrid.Squares[x, y].BottomRight.Position);
                    DrawTileInControlNode(_squareGrid.Squares[x, y].BottomLeft.TileType, _squareGrid.Squares[x, y].BottomLeft.Position);
                }
            }
        }

        private void DrawTileInControlNode(TileType tileType, Vector3 position)
        {
            var positionTile = new Vector3Int((int) position.x, (int) position.y, 0);
            
            switch (tileType)
            {
                case TileType.Dirt:
                    _tileMap.SetTile(positionTile, _tiles.DirtTile);
                    break;
                case TileType.Grass:
                    _tileMap.SetTile(positionTile, _tiles.GrassTile);
                    break;
                case TileType.Winter:
                    _tileMap.SetTile(positionTile, _tiles.WinterTile);
                    break;
                case TileType.Air:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tileType), $"{tileType} не предусмотрен в этом коде!");
            }
        }
    }

    public class Node
    {
        public Vector3 Position;

        public Node(Vector3 position)
        {
            Position = position;
        }
    }
      
    public class ControlNode : Node
    {
        public TileType TileType;

        public ControlNode(Vector3 position, TileType tileType) : base(position)
        {
            TileType = tileType;
        }
    }

    public class Square
    {
        public ControlNode TopLeft, TopRight, BottomRight, BottomLeft;

        public Square(ControlNode topLeft, ControlNode topRight, ControlNode bottomRight, ControlNode bottomLeft)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomRight = bottomRight;
            BottomLeft = bottomLeft;
        }
    }

    public class SquareGrid
    {
        public Square[,] Squares;

        public SquareGrid(TileType[,] map, float squareSize)
        {
            var nodeCountX = map.GetLength(0);
            var nodeCountY = map.GetLength(1);
            
            var mapWidth = nodeCountX * squareSize;
            var mapHeight = nodeCountY * squareSize;

            var controlNodes = new ControlNode[nodeCountX, nodeCountY];

            for (var x = 0; x < nodeCountX; x++)
            {
                for (var y = 0; y < nodeCountY; y++)
                {
                    var position = new Vector3(-mapWidth/2 + x * squareSize + squareSize/2, -mapHeight/2 + y * squareSize + squareSize/2);
                    controlNodes[x,y] = new ControlNode(position, map[x,y]);
                }
            }

            Squares = new Square[nodeCountX - 1, nodeCountY - 1];

            for (var x = 0; x < nodeCountX - 1; x++)
            {
                for (var y = 0; y < nodeCountY - 1; y++)
                {
                    Squares[x,y] = new Square(
                        controlNodes[x, y+1], 
                        controlNodes[x+1, y+1],
                        controlNodes[x+1, y], 
                        controlNodes[x,y]
                    );
                }
            }
        }
    }
}