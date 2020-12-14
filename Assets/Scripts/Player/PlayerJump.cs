using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerJump : MonoBehaviour
{
    CharacterController _characterController;
    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private bool isJumping=false;
    [SerializeField] public bool playerGrounded;
    public bool canDoubleJump = false;
    [SerializeField] bool isDoubleJumping=false;
    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }
    //private void FixedUpdate()
    //{
    //    _characterController.SimpleMove(Vector3.forward * 0);
    //    playerGrounded = _characterController.isGrounded;
    //}
    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if (isJumping == false)
            {
                isJumping = true;
                if (_animator != null)
                    _animator.SetTrigger("jump");
                StartCoroutine(JumpEvent());
            }
            else
            {
                //double jump
                if (canDoubleJump == false || isDoubleJumping) return;
                isDoubleJumping = true;
                StopAllCoroutines();
                StartCoroutine(JumpEvent());

            }
        }
//        playerGrounded = _characterController.isGrounded;
    }
    private IEnumerator JumpEvent()
    {
        float timeInAir = 0.0f;
        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir) * jumpMultiplier * Time.deltaTime;

            _characterController.Move(Vector3.up * jumpForce);

            timeInAir += Time.deltaTime;

            playerGrounded = _characterController.isGrounded;
            isJumping = !_characterController.isGrounded;

            yield return null;
        } while (_characterController.collisionFlags != CollisionFlags.Above && isJumping == true);
        isJumping = false;
        isDoubleJumping = false;
    }
}
