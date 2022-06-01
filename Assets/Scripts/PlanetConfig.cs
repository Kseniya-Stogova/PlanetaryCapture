using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetaryCapture
{
    [CreateAssetMenu(fileName = "Planet", menuName = "PlanetaryCapture/Planet", order = 1)]
    public class PlanetConfig : ScriptableObject
    {
        [SerializeField] private Percent _percent;
        [SerializeField] private Team _startTeam = Team.Neutral;
        [SerializeField] private float _speedGeneration = 1; //���������� �������� � �������
        [SerializeField] private float _captureSpeed = 3; //������� ������ ��������� ������ �������, ����� ��������� 1%

        public Team StartTeam => _startTeam;
        public float SpeedGeneration => _speedGeneration;
        public Percent Percent => _percent;
    }
}
