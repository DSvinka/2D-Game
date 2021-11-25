using System.Collections.Generic;
using Code.Configs;
using Code.Interfaces.Controllers;
using Code.Managers;
using Code.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers
{
    internal sealed class CoinController: IController, ICleanup, IStart
    {
        private SpriteAnimatorController _spriteAnimatorController;
        private SpriteAnimatorConfig _spriteAnimatorConfig;
        private Dictionary<int, CoinView> _coinViews;

        public CoinController(SpriteAnimatorController spriteAnimatorController, SpriteAnimatorConfig spriteAnimatorConfig)
        {
            _spriteAnimatorController = spriteAnimatorController;
            _spriteAnimatorConfig = spriteAnimatorConfig;

            _coinViews = new Dictionary<int, CoinView>();
        }

        public void Setup(SceneViews sceneViews)
        {
            var coinViews = sceneViews.CoinViews;
            foreach (var coin in coinViews)
            {
                _coinViews.Add(coin.gameObject.GetInstanceID(), coin);
            }
        }
        public void ReSetup(SceneViews sceneViews)
        {
            Cleanup();
            Setup(sceneViews);
            Start();
        }
        
        public void Start()
        {
            foreach (var coinView in _coinViews)
            {
                _spriteAnimatorController.StartAnimation(coinView.Value.GetComponent<SpriteRenderer>(), _spriteAnimatorConfig, AnimState.Run);
                coinView.Value.OnEnter += OnCoinPickup;
            }
        }
        
        public void Cleanup()
        {
            foreach (var coinView in _coinViews)
                DestroyCoin(coinView.Value, true);

            _coinViews.Clear();
        }

        private void OnCoinPickup(GameObject gameObject, int coinID)
        {
            var coinView = _coinViews[coinID];
            if (gameObject.TryGetComponent(out PlayerView playerView))
            {
                playerView.Pickup(coinView.gameObject, PickupManager.Coin);
                DestroyCoin(coinView);
            }
        }

        private void DestroyCoin(CoinView coinView, bool destroy = false)
        {
            if (coinView == null || coinView.gameObject == null) 
                return;
            
            var gameObject = coinView.gameObject;
            
            _spriteAnimatorController.StopAnimation(coinView.GetComponent<SpriteRenderer>());
                
            coinView.OnEnter -= OnCoinPickup;
                
            if (destroy)
            {
                Object.Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
                _coinViews.Remove(gameObject.GetInstanceID());
            }
        }
    }
}