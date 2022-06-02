using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskController : MonoBehaviour
{
    private float _maxScaleY;

    private void Awake()
    {
        _maxScaleY = transform.localScale.y;
    }

    public void SetPercent(float percent)
    {
        float scaleY = _maxScaleY * percent;
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }
}