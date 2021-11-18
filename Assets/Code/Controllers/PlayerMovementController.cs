using Code.Configs;
using Code.Controllers.Initializations;
using Code.Input.Inputs;
using Code.Interfaces.Controllers;
using Code.Interfaces.Input;
using Code.Managers;
using Code.Models;
using Code.Utils;
using Code.Utils.Modules;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class PlayerMovementController: IController, IUpdate, ICleanup, IStart
    {
        private PlayerInitialization _playerInitialization;
        private SpriteAnimatorController _spriteAnimatorController;
        private PlayerConfig _config;
        
        private ContactPooler _contactPooler;
        private PlayerModel _player;

        private float _xVelocity;

        public PlayerMovementController(PlayerInitialization playerInitialization, SpriteAnimatorController spriteAnimatorController, PlayerConfig playerConfig)
        {
            _playerInitialization = playerInitialization;
            _spriteAnimatorController = spriteAnimatorController;
            _config = playerConfig;
            
            Setup();
            
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

        public void ReSetup()
        {
            Cleanup();
            Setup();
            Start();
        }
        
        private void Setup()
        {
            
        }
        
        public void Start()
        {
            _player = _playerInitialization.GetPlayer();
            _contactPooler = new ContactPooler(_player.Collider);

            _axisXInputProxy.AxisOnChange += OnAxisXInput;
            _jumpInputProxy.KeyOnDown += OnJumpInput;
        }

        public void Cleanup()
        {
            _axisXInputProxy.AxisOnChange -= OnAxisXInput;
            _jumpInputProxy.KeyOnDown -= OnJumpInput;
            
            _spriteAnimatorController.StopAnimation(_player.SpriteRenderer);
        }
        
        public void Update(float deltaTime) 
        {
            _contactPooler.Update(deltaTime);
            
            MoveTowards();
            Jump();
            Animate();
        }

        private void MoveTowards()
        {
            _xVelocity = _config.SpeedWalk * _axisXInput;
            _player.Rigidbody.velocity = _player.Rigidbody.velocity.Change(x: _xVelocity);
        }

        private void Jump()
        {
            if (_jumpInput && _player.IsGrounded) 
            {
                _player.Rigidbody.velocity = _player.Rigidbody.velocity.Change(y: _config.JumpForce);
            }
            
            if (Mathf.Abs(_player.Rigidbody.velocity.y) <= _config.JumpTreshold)
            {
                _player.IsGrounded = true;
            }

            if (Mathf.Abs(_player.Rigidbody.velocity.y) >= _config.FallingVelocity)
            {
                _player.IsGrounded = false;
            }
        }

        private void Animate()
        {
            if (_xVelocity < 0 && !_player.LeftTurn)
            {
                _player.Transform.localScale = _player.LeftScale;
                _player.LeftTurn = true;
            }
            else if (_xVelocity > 0 && _player.LeftTurn)
            {
                _player.Transform.localScale = _player.RightScale;
                _player.LeftTurn = false;
            }

            AnimState animState;
            if (!_player.IsGrounded)
                animState = AnimState.Jump;
            else if ((_xVelocity < -_config.MovementTreshold && !_contactPooler.HasLeftContact) || (_xVelocity > _config.MovementTreshold && !_contactPooler.HasRightContact))
                animState = AnimState.Run;
            else
                animState = AnimState.Idle;
                
            _spriteAnimatorController.StartAnimation(_player.SpriteRenderer, _config.SpriteAnimatorCfg, animState);
        }
    }
}