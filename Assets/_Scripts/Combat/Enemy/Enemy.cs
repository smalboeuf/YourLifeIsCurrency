using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit, IEnemy, IDie
{
    private Animator _animator;
    [SerializeField] private GameObject _prefab;
    private Health _health;
    [SerializeField] int _meleeDamage = 4;

    public int SpawnCost;

    // Pathfinding
    [SerializeField] private GameObject _target;
    List<Vector3> _pathVectorList = new List<Vector3>();
    private int _currentPathIndex = 0;

    [SerializeField] private float _speed = 4f;

    [SerializeField] private float _attackRange = 3;
    private IEnemyAttack _enemyAttack;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _health = GetComponent<Health>();
        _target = GameObject.FindGameObjectWithTag("Player");
        _enemyAttack = GetComponent<IEnemyAttack>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (_target != null && CanMove)
        {
            Attack();
        }
        else
        {
            StopMoving();
        }
    }

    public void Attack()
    {
        if (CanMove)
        {
            SetTargetPosition();
            TargetPlayerMovement();
        }

        if (_enemyAttack != null && Vector2.Distance(gameObject.transform.position, _target.gameObject.transform.position) <= _attackRange)
        {
            _enemyAttack.EnemyAttack();
        }
    }

    public GameObject GetPrefab()
    {
        return _prefab;
    }

    private void SetTargetPosition()
    {
        // Pathfinding
        if (_target == null)
            return;

        _currentPathIndex = 0;
        _pathVectorList = Pathfinding.Instance.FindPath(transform.position, _target.transform.position);

        if (_pathVectorList != null && _pathVectorList.Count > 0)
        {
            _pathVectorList.RemoveAt(0);
        }
    }

    private void TargetPlayerMovement()
    {
        if (_pathVectorList != null && CanMove)
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
        else
        {
            StopMoving();
            transform.position = transform.position;
        }
    }

    public void Die()
    {
        Globals.EnemySpawner.EnemyDies();
        // Handle Random chance of pickup dropping
        GameObject pickup = Globals.GameManager.RandomPickupDrop();
        if (pickup != null)
        {
            Instantiate(pickup, gameObject.transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    public Animator GetAnimator()
    {
        return _animator;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
