using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float runSpeed = 5.0f;

    private bool isAlive = true;

    private Vector2 playerInput = Vector2.zero;
    private Rigidbody2D rb = null;
    private Animator animator = null;

    private InputActions Actions = null;
    void Start()
    {
        Actions = new InputActions();
        Actions.Player.Move.started += Move_started;
        Actions.Player.Move.canceled += Move_started;
        Actions.Player.Move.performed += Move_started;

        Actions.Enable();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Move_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
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
        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
