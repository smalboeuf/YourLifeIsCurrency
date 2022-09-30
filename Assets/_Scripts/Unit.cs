using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Rigidbody2D _rb2d;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] Material _defaultSpriteMaterial;
    [SerializeField] Material _flashSpriteMaterial;

    [SerializeField] private float _onHitFlashLength;
    private float _flashTimer = 0;
    private bool _isFlashing = false;

    protected virtual void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (_isFlashing && _flashTimer <= 0)
        {
            _spriteRenderer.material = _defaultSpriteMaterial;
            _isFlashing = false;
        }
        else if (_isFlashing)
        {
            _flashTimer -= Time.deltaTime;
        }
    }

    public IEnumerator ExecuteKnockback(float knockbackDuration, float knockbackPower, Transform source)
    {
        OnHitFlash();
        // float timer = 0;
        // // while (knockbackDuration > timer)
        // // {
        // timer -= Time.deltaTime;
        // Vector2 direction = (source.transform.position - target.transform.position).normalized;
        // target.GetComponent<Rigidbody2D>().AddForce(-direction * knockbackPower);
        // // }

        yield return 0;
    }

    public void OnHitFlash()
    {
        _flashTimer = _onHitFlashLength;
        _spriteRenderer.material = _flashSpriteMaterial;
        _isFlashing = true;
    }
}
