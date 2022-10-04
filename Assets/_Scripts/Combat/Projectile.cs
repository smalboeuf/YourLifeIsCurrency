using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private float _timeBeforeDestroyed = 5;

    private Vector3 _shootingDirection;
    private float _speed;
    private int _damage;

    public delegate void OnHitEnemy();
    private OnHitEnemy OnHitEnemyEvents = delegate { };

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyAfterTime());
    }

    private void Update()
    {
        transform.position += _shootingDirection.normalized * _speed * Time.deltaTime;
    }

    public void SetProjectile(Vector3 shootingDirection, float speed, int damage)
    {
        _shootingDirection = shootingDirection;
        _speed = speed;
        _damage = damage;
    }

    public void SetOnHitEnemyEvent(OnHitEnemy onHitEnemy)
    {
        OnHitEnemyEvents += onHitEnemy;
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(_timeBeforeDestroyed);
        DisableProjectile();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            OnHitEnemyEvents.Invoke();
            DisableProjectile();
            collision.GetComponent<Health>().TakeDamage(_damage);
            collision.GetComponent<Unit>().OnHitFlash();
        }

        if (collision.tag == "Projectile Bounds")
        {
            DisableProjectile();
        }
    }

    private void DisableProjectile()
    {
        if (OnHitEnemyEvents != null)
        {
            OnHitEnemyEvents -= Globals.PlayerController.OnHitEnemy;
        }

        gameObject.SetActive(false);
    }
}
