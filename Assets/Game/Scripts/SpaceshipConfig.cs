using UnityEngine;

namespace PlanetaryCapture
{
    [CreateAssetMenu(fileName = "Spaceship", menuName = "PlanetaryCapture/Spaceship", order = 1)]
    public class SpaceshipConfig : ScriptableObject
    {
        [SerializeField] private float _speed = 3;

        public float Speed => _speed;
    }
}
