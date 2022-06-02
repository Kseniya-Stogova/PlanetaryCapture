using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Vector3 _rotation;

    void Start()
    {
        _rotation = transform.eulerAngles;
    }

    void Update()
    {
        transform.eulerAngles = _rotation;
    }
}
