using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jolly : MonoBehaviour
{
    private Animator _animator = null;
    private Rigidbody2D _jollyRigid = null;
    private SpriteRenderer _renderer = null;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _jollyRigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
        if(Input.GetKey(KeyCode.D))
        {
            _renderer.flipX = false;

            _animator.SetTrigger("Walk");
            _animator.SetBool("Walking", true);
            _jollyRigid.velocity = new Vector2(800, 0) * Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            _animator.SetTrigger("Walk");
            _animator.SetBool("Walking", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _renderer.flipX = true;
            _animator.SetTrigger("Walk");
            _animator.SetBool("Walking", true);
            _jollyRigid.velocity = new Vector2(-800, 0) * Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            _animator.SetTrigger("Walk");
            _animator.SetBool("Walking", false);
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            _animator.SetTrigger("Jump");
            _jollyRigid.AddForce(transform.up * 200);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
