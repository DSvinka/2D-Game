using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Configs
{
    public enum AnimState
    {
        Idle = 0,
        Run = 1,
        Jump = 2,
    }
    
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
