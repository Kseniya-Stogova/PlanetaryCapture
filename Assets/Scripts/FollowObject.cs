using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    void Update()
    {
        Vector3 position = target.position + offset;
        position = Camera.main.WorldToScreenPoint(position);
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
