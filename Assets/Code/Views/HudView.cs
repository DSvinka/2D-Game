using TMPro;
using UnityEngine;

namespace Code.Views
{
    internal sealed class HudView: MonoBehaviour
    {
        [SerializeField] private TMP_Text _health;
        [SerializeField] private TMP_Text _coins;

        public TMP_Text Health => _health;
        public TMP_Text Coins => _coins;
    }
}