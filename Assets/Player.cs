using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float runSpeed = 5.0f;

    [SerializeField]
    private float jumpSpeed = 5.0f;

    [SerializeField]
    private float climbSpeed = 5.0f;

    [SerializeField]
    private LayerMask groundLayer = 0;

    [SerializeField]
    private LayerMask ladderLayer = 0;

    [SerializeField]
    private Vector2 deathKick = new Vector2(25f, 25f);

    private bool isAlive = true;

    private Vector2 playerInput = Vector2.zero;
    private Rigidbody2D rb = null;
    private Animator animator = null;
    private CapsuleCollider2D capsuleCollider2D = null;
    private BoxCollider2D feet = null;

    private InputActions Actions = null;

    private void Awake()
    {
    }

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
        feet = GetComponent<BoxCollider2D>();
    }

    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (feet && feet.IsTouchingLayers(groundLayer) && !feet.IsTouchingLayers(ladderLayer))
        {
            animator.SetTrigger("Jumping");
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            rb.velocity += jumpVelocityToAdd;
        }
    }

    private void Move(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        playerInput = obj.ReadValue<Vector2>();
    }

    void Update()
    {
        if(!isAlive) { return; }

        Run();
        ClimbLadder();
        FlipSprite();
        Die();
    }

    private void Run()
    {
        Vector2 playerVelocity = new Vector2(playerInput.x, rb.velocity.y);
        rb.velocity = new Vector2(playerVelocity.x * runSpeed, playerVelocity.y);

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void ClimbLadder()
    {
        if(!feet.IsTouchingLayers(ladderLayer))
        {
            rb.gravityScale = 1.0f;
            animator.SetBool("Climbing", false);
            return;
        }

        rb.gravityScale = 0;
        bool hasVerticalInput = Mathf.Abs(playerInput.y) > Mathf.Epsilon;

        if(hasVerticalInput)
        {
            float controlThrow = playerInput.y;
            Vector2 climbVelocity = new Vector2(rb.velocity.x, controlThrow * climbSpeed);
            rb.velocity = climbVelocity;
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }

        animator.SetBool("Climbing", hasVerticalInput);
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void Die()
    {
        if(capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")) || feet.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            Debug.Log("Dead");
            animator.SetTrigger("Dying");
            rb.velocity = deathKick;
            isAlive = false;

            Debug.Log("Player" + GameSession.Instance.GetInstanceID());
            GameSession.Instance.ProcessPlayerDeath();
        }
    }
}
