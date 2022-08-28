using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    private Rigidbody2D _rb2d;
    private Animator _animator;
    private bool _canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        // _animator = GetComponent<Animator>();
    }

    public void MoveCharacter(Vector3 direction)
    {
        if (_canMove)
        {
            if (direction != Vector3.zero)
            {
                _rb2d.MovePosition(transform.position + direction.normalized * _speed * Time.deltaTime);
                // _animator.SetFloat("moveX", direction.x);
                // _animator.SetFloat("moveY", direction.y);
                // _animator.SetBool("moving", true);
            }
            else
            {
                // _animator.SetBool("moving", false);
            }
        }
    }
}
