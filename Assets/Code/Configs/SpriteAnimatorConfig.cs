using System;
using System.Collections.Generic;
using Code.Managers;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "SpriteAnimatorCfg", menuName = "Configs/Animation Cfg", order = 2)]
    internal class SpriteAnimatorConfig : ScriptableObject
    {
        [Serializable]
        public sealed class SpriteSequence
        {
            public AnimState Track;
            public List<Sprite> Sprites = new List<Sprite>();
            public float AnimationSpeed = 10;
            public bool Loop = true;
        }

        public List<SpriteSequence> Sequences = new List<SpriteSequence>();
    }
}
