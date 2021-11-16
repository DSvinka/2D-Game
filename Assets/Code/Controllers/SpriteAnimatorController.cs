using System.Collections.Generic;
using Code.Configs;
using Code.Interfaces.Controllers;
using Code.Managers;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class SpriteAnimatorController : IController, ICleanup, IUpdate
    {
        private sealed class Animation : IUpdate
        {
            public AnimState Track;
            public List<Sprite> Sprites;
            public bool Loop;
            public float Speed = 10f;
            public float Counter;
            public bool Sleep;

            public void Update(float deltaTime)
            {
                if (Sleep) return;
                Counter += deltaTime * Speed;

                if (Loop)
                {
                    while (Counter > Sprites.Count)
                    {
                        Counter -= Sprites.Count;
                    }
                }
                else if (Counter > Sprites.Count)
                {
                    Counter = Sprites.Count;
                    Sleep = true;
                }
            }
        }
        
        private Dictionary<SpriteRenderer, Animation> _activeAnimation = new Dictionary<SpriteRenderer, Animation>();

        public void StartAnimation(SpriteRenderer spriteRenderer, SpriteAnimatorConfig config, AnimState track)
        {
            if (_activeAnimation.TryGetValue(spriteRenderer, out var animation))
            {
                animation.Sleep = false;

                if (animation.Track != track)
                {
                    var sequences = config.Sequences.Find(sequence => sequence.Track == track);
                    
                    animation.Track = track;
                    animation.Sprites = sequences.Sprites;
                    animation.Speed = sequences.AnimationSpeed;
                    animation.Loop = sequences.Loop;
                    animation.Counter = 0;
                }
            }
            else
            {
                var sequences = config.Sequences.Find(sequence => sequence.Track == track);
                
                _activeAnimation.Add(spriteRenderer, new Animation()
                {
                    Sleep = false,
                    Speed = sequences.AnimationSpeed,
                    Loop = sequences.Loop,
                    Track = track,
                    Sprites = sequences.Sprites,
                });
            }
        }

        public void StopAnimation(SpriteRenderer spriteRenderer)
        {
            if (_activeAnimation.ContainsKey(spriteRenderer))
            {
                _activeAnimation.Remove(spriteRenderer);
            }
        }

        public void Update(float deltaTime)
        {
            foreach (var animation in _activeAnimation)
            {
                var value = animation.Value;
                
                value.Update(deltaTime);
                if (value.Counter < value.Sprites.Count)
                {
                    animation.Key.sprite = value.Sprites[(int) value.Counter];
                }
            }
        }
        
        public void Cleanup()
        {
            _activeAnimation.Clear();
        }
    }
}