using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    private GameObject _target;
    List<Vector3> _pathVectorList = new List<Vector3>();

    private float _speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            Attack();
        }
    }

    public void Attack()
    {
        // Pathfinding
        _pathVectorList = Pathfinding.Instance.FindPath(transform.position, _target.transform.position);

        if (_pathVectorList != null && _pathVectorList.Count > 0)
        {
            _pathVectorList.RemoveAt(0);
        }

        // Movement
        if (_pathVectorList != null)
        {
            Vector3 targetPosition = _target.transform.position;

            if (Vector2.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                transform.position = transform.position + moveDir * _speed * Time.deltaTime;
            }
            else
            {
                // Do not move
            }
        }
    }
}
