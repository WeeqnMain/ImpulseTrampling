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

    private bool isGrounded;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundLayer;

    public Action<int> OnDamageReceive;

    [Header("SoundEffects")]
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource landSound;
    [SerializeField] private AudioSource moveSound;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        inputDirection = GetInputDirection();
     
        bool isGroundedInPreviousFrame = isGrounded;
        isGrounded = Physics.Raycast(groundCheckPoint.position, -transform.up, groundCheckDistance, groundLayer);

        if (!isGroundedInPreviousFrame && isGrounded)
        {
            landSound.Play();
        }

        if (isGrounded)
        {
            LookAtDirection();

            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
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

    private void Jump()
    {
        rigidbody.AddForce(jumpForce * transform.up);
        jumpSound.Play();
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
        OnDamageReceive?.Invoke(0);
        Destroy(gameObject);
    }

    public void OnEnemyDestroy()
    {
        Jump();
    }

    private Vector3 GetInputDirection() => new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
}
