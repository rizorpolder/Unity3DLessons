using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FPS
{
    
    public class PlayerMove:IMove
    {
       
        public float chJumpForce = 5f;
        public float runCycleOffset = 0.2f;
       
        private float _chSpeed = 2f;
        private float _chDefaultSpeed = 2f;
        private float _gravityFoce;
        private bool _isCrouching = false;
        private float _chCapsuleHeight;
        private float _chControllerHeight;

        private Transform _instance;
        private Animator _animator;
        private CharacterController _characterController;
        private Vector3 _charDirection;
        private CapsuleCollider _chCapsuleCollider;
        private Vector3 _chCapsuleCenter;
        private Vector3 _chControllerCenter;

        public PlayerMove(Transform instance)
        {
            _instance = instance;
            _characterController = instance.GetComponent<CharacterController>();
            _chCapsuleCollider = instance.GetComponent<CapsuleCollider>();
            _animator = instance.GetComponentInChildren<Animator>();
            _chCapsuleHeight = _chCapsuleCollider.height;
            _chCapsuleCenter = _chCapsuleCollider.center;
            _chControllerHeight = _characterController.height;
            _chControllerCenter = _characterController.center;
        }


        public void Move()
        {
            CharacterMove();
            CharacterCrouch(Input.GetKeyDown(KeyCode.C));
            CharacterGravity();
            UpdateAnimator(_charDirection);

        }


        private void CharacterMove()
        {
            if (_characterController.isGrounded)
            {
                Vector3 temp = _instance.forward * Input.GetAxis("Vertical") +
                               _instance.right * Input.GetAxis("Horizontal");
                _charDirection.x = temp.x * _chSpeed;
                _charDirection.z = temp.z * _chSpeed;
            }

            _charDirection.y = _gravityFoce;
            _characterController.Move(_charDirection * Time.deltaTime);
        }

        private void CharacterGravity()
        {
            if (!_characterController.isGrounded) _gravityFoce -= 30 * Time.deltaTime;
            else _gravityFoce = -1;
            if (Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded)
                _gravityFoce = chJumpForce;
        }

        private void CharacterCrouch(bool down)
        {

            if (_characterController.isGrounded && down)
            {
                if (!_isCrouching)
                {
                    _chCapsuleCollider.height = _chCapsuleCollider.height / 2f;
                    _chCapsuleCollider.center = _chCapsuleCollider.center /2f;
                    _characterController.center = _characterController.center /2f;
                    _characterController.height =_characterController.height/2;
                    _chSpeed /= 2;
                    _isCrouching = true;
                }
                else
                {
                    //Ray crouchRay = new Ray(_instance.position + Vector3.up * _chCapsuleCollider.radius * 0.5f,
                    //    Vector3.up);
                    //float crouchRayLength = _chCapsuleHeight - _chCapsuleCollider.radius * 0.5f;
                    //if (Physics.SphereCast(crouchRay, _chCapsuleCollider.radius * 0.5f, crouchRayLength,
                    //    Physics.AllLayers, QueryTriggerInteraction.Ignore))
                    //{
                    //    _isCrouching = true;
                    //    return;
                    //}
                    _characterController.height = _chControllerHeight;
                    _characterController.center = _chControllerCenter;
                    _chCapsuleCollider.height = _chCapsuleHeight;
                    _chCapsuleCollider.center = _chCapsuleCenter;
                    
                    
                    _chSpeed = _chDefaultSpeed;
                    _isCrouching = false;
                }
            }
        }

        private void UpdateAnimator(Vector3 move)
        {
            Debug.Log(move);
            _animator.SetFloat("Strafe",move.x, 0.1f, Time.deltaTime);
            _animator.SetFloat("Move",move.z,0.1f,Time.deltaTime);// соомнительные записи
            //_animator.SetFloat("Turn",_charDirection.x,0.1f,Time.deltaTime);    //
            _animator.SetBool("Crouch",_isCrouching);
            _animator.SetBool("OnGround", _characterController.isGrounded);
            if (!_characterController.isGrounded) _animator.SetFloat("Jump", _gravityFoce);// или _chJumpForce

            float runCycle = Mathf.Repeat(
                _animator.GetCurrentAnimatorStateInfo(0).normalizedTime + runCycleOffset, 1);
            float jumpLeg = (runCycle < 0.5f ? 1 : -1) * _charDirection.z;
            if (_characterController.isGrounded)
            {
                _animator.SetFloat("JumpLeg",jumpLeg);
            }
        }

    }
    
}