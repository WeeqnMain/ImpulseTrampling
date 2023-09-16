using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float jumpForce;

    private Rigidbody rigidbody;
    [SerializeField] private float extraGravityForce;

    private Vector3 inputDirection;
    private MobileInput mobileInput;

    private bool isGrounded;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundLayer;

    public Action OnDamageReceive;

    private bool isInputFromMobile;

    public void Init(MobileInput mobileInput = null)
    {
        isInputFromMobile = mobileInput != null;
        if (isInputFromMobile)
        {
            this.mobileInput = mobileInput;
            mobileInput.JumpButtonPressed += OnJumpButtonPressed;
        }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        inputDirection = isInputFromMobile ? mobileInput.direction : GetInputDirection();
     
        bool isGroundedInPreviousFrame = isGrounded;
        isGrounded = Physics.Raycast(groundCheckPoint.position, -transform.up, groundCheckDistance, groundLayer);

        if (isGrounded)
        {
            if (!isGroundedInPreviousFrame)
                AudioManager.instance.PlayEffect("PlayerLand");

            LookAtDirection();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump(checkGround: true);
        }
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            Move();
        }
        else
        {
            ConstantMove();
            rigidbody.AddForce(-transform.up * extraGravityForce);
        }
    }

    private void Jump(bool checkGround)
    {
        if (checkGround)
            if (!isGrounded) return;

        rigidbody.AddForce(jumpForce * transform.up);
        AudioManager.instance.PlayEffect("PlayerJump");
    }

    private void Move()
    {
        rigidbody.MovePosition(transform.position + transform.forward * inputDirection.magnitude * moveSpeed * Time.deltaTime);
    }

    private void ConstantMove()
    {
        rigidbody.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
    }

    private void LookAtDirection()
    {
        if (inputDirection == Vector3.zero) return;

        var lookRotation = Quaternion.LookRotation(inputDirection.ConvertToIsometric(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
    }

    public void RecieveDamage()
    {
        OnDamageReceive?.Invoke();
        Destroy(gameObject);
    }

    public void OnEnemyDestroy()
    {
        Jump(checkGround: false);
    }

    private void OnJumpButtonPressed()
    {
        Jump(checkGround: true);
    }

    private Vector3 GetInputDirection() => new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
}
