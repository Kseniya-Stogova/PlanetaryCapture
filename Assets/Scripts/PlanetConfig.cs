using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetaryCapture
{
    public class PlanetConfig : ScriptableObject
    {
        [SerializeField] private Team _startTeam = Team.Neutral;
        [SerializeField] private float _speedGeneration;


        public Team StartTeam => _startTeam;
        public float SpeedGeneration => _speedGeneration;
    }
}
