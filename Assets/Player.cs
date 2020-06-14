using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float runSpeed = 5.0f;

    [SerializeField]
    private float jumpSpeed = 5.0f;

    [SerializeField]
    private LayerMask groundLayer = 0;

    private bool isAlive = true;

    private Vector2 playerInput = Vector2.zero;
    private Rigidbody2D rb = null;
    private Animator animator = null;
    private Collider2D capsuleCollider2D = null;

    private InputActions Actions = null;
    void Start()
    {
        Actions = new InputActions();
        Actions.Player.Move.started += Move;
        Actions.Player.Move.canceled += Move;
        Actions.Player.Move.performed += Move;

        Actions.Player.Jump.started += Jump; 
        //Actions.Player.Jump.canceled+= Jump;
        //Actions.Player.Jump.performed += Jump;

        Actions.Enable();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (capsuleCollider2D.IsTouchingLayers(groundLayer))
        {
            Debug.Log("Jump");
            animator.SetTrigger("Jumping");
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            rb.velocity += jumpVelocityToAdd;
        }
    }

    private void Move(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Move");
        playerInput = obj.ReadValue<Vector2>();
    }

    void Update()
    {
        Run();
    }

    private void Run()
    {
        Vector2 playerVelocity = new Vector2(playerInput.x, rb.velocity.y);
        rb.velocity = new Vector2(playerVelocity.x * runSpeed, playerVelocity.y);
        FlipSprite();

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void Jump()
    {

    }
}
