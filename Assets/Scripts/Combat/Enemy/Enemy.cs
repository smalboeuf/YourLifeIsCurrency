using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy, IDie
{
    [SerializeField] private GameObject _target;
    List<Vector3> _pathVectorList = new List<Vector3>();
    private int _currentPathIndex = 0;


    private float _speed = 4f;

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
        SetTargetPosition();

        // Movement
        if (_pathVectorList != null)
        {
            Vector3 targetPosition = _pathVectorList[_currentPathIndex];

            if (Vector2.Distance(transform.position, targetPosition) > 0.5f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                transform.position = transform.position + moveDir * _speed * Time.deltaTime;
            }
            else
            {
                _currentPathIndex++;
                if (_currentPathIndex >= _pathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }
    }

    private void SetTargetPosition()
    {
        // Pathfinding
        _currentPathIndex = 0;
        _pathVectorList = Pathfinding.Instance.FindPath(transform.position, _target.transform.position);

        if (_pathVectorList != null && _pathVectorList.Count > 0)
        {
            _pathVectorList.RemoveAt(0);
        }
    }

    public void Die()
    {
        print("Enemy died");
        Destroy(gameObject);
    }

    private void StopMoving()
    {
        _pathVectorList = null;
    }
}
