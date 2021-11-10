using Code.Configs;
using Code.Controllers.Initializations;
using Code.Input.Inputs;
using Code.Interfaces.Controllers;
using Code.Interfaces.Input;
using Code.Models;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class PlayerMovementController: IController, IUpdate, ICleanup, IStart
    {
        private readonly PlayerInitialization _playerInitialization;
        private readonly SpriteAnimatorController _spriteAnimatorController;
        private readonly PlayerConfig _config;
        private PlayerModel _player;

        private float _movementInput;
        private bool _jumpInput;

        public PlayerMovementController(PlayerInitialization playerInitialization, SpriteAnimatorController spriteAnimatorController, PlayerConfig playerConfig)
        {
            _playerInitialization = playerInitialization;
            _spriteAnimatorController = spriteAnimatorController;
            _config = playerConfig;
           
            _movementInputProxy = AxisInput.Horizontal;
            _jumpInputProxy = KeysInput.Jump;
        }

        #region Input Proxy

        private IUserAxisProxy _movementInputProxy;
        private IUserKeyDownProxy _jumpInputProxy;

        private void OnMovementHorizontalInput(float value) => _movementInput = value;
        private void OnJumpInput(bool value) => _jumpInput = value;

        #endregion

        public void Start()
        {
            _player = _playerInitialization.GetPlayer();

            _movementInputProxy.AxisOnChange += OnMovementHorizontalInput;
            _jumpInputProxy.KeyOnDown += OnJumpInput;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Cleanup()
        {
            _movementInputProxy.AxisOnChange -= OnMovementHorizontalInput;
            _jumpInputProxy.KeyOnDown -= OnJumpInput;
            
            _spriteAnimatorController.StopAnimation(_player.SpriteRenderer);
        }
        
        public void Update(float deltaTime) 
        {
            MoveTowards();
            Jump();
            Animate();
        }

        private void MoveTowards()
        {
            _player.Rigidbody.velocity = new Vector2(_movementInput * _config.SpeedWalk, _player.Rigidbody.velocity.y);
        }

        private void Jump()
        {
            if (_jumpInput && _player.IsGrounded) 
            {
                _player.Rigidbody.velocity = new Vector2(_player.Rigidbody.velocity.x, _config.JumpForce);
                _player.IsGrounded = false;
            }
            
            if (_player.Rigidbody.velocity.y == 0)
            {
                _player.IsGrounded = true;
            }
        }

        private void Animate()
        {
            if (_movementInput < 0 && !_player.LeftTurn)
            {
                _player.Transform.localScale = _player.LeftScale;
                _player.LeftTurn = true;
            }
            else if (_movementInput > 0 && _player.LeftTurn)
            {
                _player.Transform.localScale = _player.RightScale;
                _player.LeftTurn = false;
            }
            
            AnimState animState;

            if (!_player.IsGrounded)
                animState = AnimState.Jump;
            else if (_player.Rigidbody.velocity.x > 0 || _player.Rigidbody.velocity.x < 0)
                animState = AnimState.Run;
            else
                animState = AnimState.Idle;
                
            _spriteAnimatorController.StartAnimation(_player.SpriteRenderer, _config.SpriteAnimatorCfg, animState);
        }
    }
}