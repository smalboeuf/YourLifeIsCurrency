using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit, IEnemy, IDie
{
    [SerializeField] private GameObject _prefab;
    private Health _health;
    [SerializeField] int _meleeDamage = 4;

    public int SpawnCost;

    // Pathfinding
    [SerializeField] private GameObject _target;
    List<Vector3> _pathVectorList = new List<Vector3>();
    private int _currentPathIndex = 0;

    [SerializeField] private float _speed = 4f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _health = GetComponent<Health>();
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (_target != null)
        {
            Attack();
        }
    }

    public void Attack()
    {
        SetTargetPosition();
        TargetPlayerMovement();
    }

    public GameObject GetPrefab()
    {
        return _prefab;
    }

    private void SetTargetPosition()
    {
        if (_target == null)
            print(_target);

        // Pathfinding
        _currentPathIndex = 0;
        if (_target == null)
            return;

        _pathVectorList = Pathfinding.Instance.FindPath(transform.position, _target.transform.position);

        if (_pathVectorList != null && _pathVectorList.Count > 0)
        {
            _pathVectorList.RemoveAt(0);
        }
    }

    private void TargetPlayerMovement()
    {
        if (_pathVectorList != null)
        {
            if (_currentPathIndex > _pathVectorList.Count - 1)
                return;

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

    public void Die()
    {
        print("died called");
        Globals.EnemySpawner.EnemyDies();
        Destroy(gameObject);
    }

    private void StopMoving()
    {
        _pathVectorList = null;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            GameObject playerGameObject = other.gameObject;
            playerGameObject.GetComponent<PlayerController>().TakeDamage(_meleeDamage);
            StartCoroutine(playerGameObject.GetComponent<Unit>().ExecuteKnockback(0.25f, 0.1f, this.transform));
        }
    }
}
