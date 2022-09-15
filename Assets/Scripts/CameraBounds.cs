using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothing;
    [SerializeField] private Vector2 _maxPosition;
    [SerializeField] private Vector2 _minPosition;

    private void LateUpdate()
    {
        if (transform.position != _target.position)
        {
            Vector3 targetPosition = new Vector3(_target.position.x, _target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, _minPosition.x, _maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, _minPosition.y, _maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothing);
        }
    }
}
