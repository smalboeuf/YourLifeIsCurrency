using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{

    private Vector3 _movementInputs;
    private Vector3 _shootInputs;

    public Vector3 Inputs
    {
        get
        {
            return _movementInputs;
        }
    }

    public Vector3 ShootInputs
    {
        get
        {
            return _shootInputs;
        }

    }

    private void Start()
    {

    }

    void Update()
    {
        PlayerMovementInputs();
        HandleShootInputs();
    }

    private void PlayerMovementInputs()
    {
        _movementInputs = Vector3.zero;
        _movementInputs.x = Input.GetAxisRaw("Horizontal");
        _movementInputs.y = Input.GetAxisRaw("Vertical");
    }

    private void HandleShootInputs()
    {
        _shootInputs = Vector3.zero;

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _shootInputs.y = -1;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _shootInputs.y = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _shootInputs.x = -1;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _shootInputs.x = 1;
        }
    }
}
