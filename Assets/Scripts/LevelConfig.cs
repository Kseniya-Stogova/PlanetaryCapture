using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "PlanetaryCapture/Level", order = 1)]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private float _attackSpeed = 0.01f;

    public float AttackSpeed => _attackSpeed;
}
