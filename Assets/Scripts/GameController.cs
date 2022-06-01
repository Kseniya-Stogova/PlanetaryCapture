using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlanetaryCapture
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private LevelConfig _levelConfig;

        private float _spaceshipAttackSpeed;

        private List<Planet> _planets;

        private void Start()
        {
            Begin();
        }

        public void Begin()
        {
            _planets = FindObjectsOfType<Planet>().ToList();
            _spaceshipAttackSpeed = _levelConfig.AttackSpeed;

            _planets[0].UpCount(Team.Red, 60);
            _planets[0].UpCount(Team.Blue, 20);

            StartCoroutine(UpdateCapture());
        }

        private void Update()
        {
        }

        IEnumerator UpdateCapture()
        {
            while (true)
            {
                foreach (var planet in _planets)
                {
                    planet.UpdateCapture(_spaceshipAttackSpeed);
                }
                yield return new WaitForSeconds(1);
            }
        }
    }
}
