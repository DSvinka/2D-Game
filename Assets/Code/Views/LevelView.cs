using UnityEngine;
using UnityEngine.Tilemaps;

namespace Code.Views
{
    internal sealed class LevelView : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;

        public Tilemap Tilemap => _tilemap;
    }
}