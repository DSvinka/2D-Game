using Code.Configs;
using Code.Controllers.Initializations;
using Code.Input.Inputs;
using Code.Interfaces.Controllers;
using Code.Interfaces.Input;
using Code.Models;
using Code.Utils;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class PlayerMovementControllerNotPhysic: IController, IUpdate, IStart, ICleanup
    {
        private float _movingTreshold = 0.01f;

        private bool _isMoving;

        private float _jumpTreshold = 1f;
        private float _gravity = -9.8f;
        private float _groundLevel = 0.5f;
        private float _yVelocity;

        private PlayerModel _player;
        private PlayerConfig _playerConfig;
        private PlayerInitialization _playerInitialization;
        private SpriteAnimatorController _spriteAnimatorController;

        public PlayerMovementControllerNotPhysic(PlayerInitialization playerInitialization, PlayerConfig playerConfig, SpriteAnimatorController animatorController)
        {
            _playerInitialization = playerInitialization;
            _spriteAnimatorController = animatorController;
            _playerConfig = playerConfig;
            
            _axisXInputProxy = AxisInput.Horizontal;
            _jumpInputProxy = KeysInput.Jump;
        }
        
        #region Input
        
        private float _axisXInput;
        private bool _jumpInput;

        private IUserAxisProxy _axisXInputProxy;
        private IUserKeyDownProxy _jumpInputProxy;

        private void OnAxisXInput(float value) => _axisXInput = value;
        private void OnJumpInput(bool value) => _jumpInput = value;

        #endregion
        
        public void Start()
        {
            _player = _playerInitialization.GetPlayer();
            _spriteAnimatorController.StartAnimation(_player.SpriteRenderer, _playerConfig.SpriteAnimatorCfg, AnimState.Idle);

            _axisXInputProxy.AxisOnChange += OnAxisXInput;
            _jumpInputProxy.KeyOnDown += OnJumpInput;
        }

        public void Cleanup()
        {
            _axisXInputProxy.AxisOnChange -= OnAxisXInput;
            _jumpInputProxy.KeyOnDown -= OnJumpInput;
            
            _spriteAnimatorController.StopAnimation(_player.SpriteRenderer);
        }

        private void MoveTowards(float deltaTime)
        {
            _player.Transform.position += Vector3.right * (deltaTime * _axisXInput * _playerConfig.SpeedWalk * (_axisXInput < 0 ? -1 : 1));
            _player.Transform.localScale = (_axisXInput < 0 ? _player.LeftScale : _player.RightScale);
        }
        private bool IsGrounded()
        {
            return _player.Transform.position.y <= _groundLevel && _yVelocity <= 0;
        }
        
        public void Update(float deltaTime)
        {
            _isMoving = Mathf.Abs(_axisXInput) > _movingTreshold;

            if (_isMoving)
            {
                MoveTowards(deltaTime);
            }

            if (IsGrounded())
            {
                _spriteAnimatorController.StartAnimation(_player.SpriteRenderer, _playerConfig.SpriteAnimatorCfg, _isMoving ? AnimState.Run : AnimState.Idle);

                if (_jumpInput && _yVelocity <= 0)
                {
                    _yVelocity = _playerConfig.JumpForce;
                }
                else if (_yVelocity < 0)
                {
                    _yVelocity = float.Epsilon;
                    _player.Transform.position = _player.Transform.position.Change(y: _groundLevel);
                }
            }
            else
            {
                if (Mathf.Abs(_yVelocity) > _jumpTreshold)
                {
                    _spriteAnimatorController.StartAnimation(_player.SpriteRenderer, _playerConfig.SpriteAnimatorCfg, AnimState.Jump);
                }

                _yVelocity += _gravity * deltaTime;
                _player.Transform.position += Vector3.up * (deltaTime * _yVelocity);
            }
        }
    }
}