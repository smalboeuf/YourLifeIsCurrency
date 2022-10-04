using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Rigidbody2D Rb2d;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] Material _defaultSpriteMaterial;
    [SerializeField] Material _flashSpriteMaterial;

    [SerializeField] private float _onHitFlashLength;
    private float _flashTimer = 0;
    private bool _isFlashing = false;

    public bool CanMove = true;

    protected virtual void Start()
    {
        Rb2d = GetComponent<Rigidbody2D>();
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
        Vector3 direction = (source.transform.position - gameObject.transform.position).normalized;
        Rb2d.AddForce(-direction * knockbackPower);
        StartCoroutine(KnockbackTimer(Rb2d, knockbackDuration));
        yield return 0;
    }

    public void OnHitFlash()
    {
        print("flash");
        _flashTimer = _onHitFlashLength;
        _spriteRenderer.material = _flashSpriteMaterial;
        _isFlashing = true;
    }

    private IEnumerator KnockbackTimer(Rigidbody2D rb, float knockbackDuration)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockbackDuration);
            rb.velocity = Vector2.zero;
        }
    }
}
