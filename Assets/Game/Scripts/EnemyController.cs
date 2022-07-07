using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlanetaryCapture
{
    public class EnemyController : MonoBehaviour
    {
        private List<Planet> _planets;

        private void Awake()
        {
            _planets = FindObjectsOfType<Planet>(true).ToList();
        }

        private void Start()
        {
            StartCoroutine(EnemyCapture());
        }

        private void CheckPlanetCapture(Planet planetForCheck)
        {
            float percent = planetForCheck.TakeRedPercentCapture();

            if (percent >= 0.6f) return;
            
            
            foreach (var planet in _planets)
            {
                if (planet.TakeRedPercentCapture() == 1 && planetForCheck != planet)
                {
                    CreateRedSpaceship(0.5f, planet, planetForCheck);
                    return;
                }
            }
        }

        private void CreateRedSpaceship(float percentToSend, Planet firstPoint, Planet secondPoint)
        {
            int countToSend = firstPoint.TakeRedSpaceship(percentToSend);
            if (countToSend == 0) return;

            Spaceship spaceship = SpaceshipPool.SharedInstance.GetSpaceship();
            spaceship.transform.position = firstPoint.transform.position;
            spaceship.Initialize(secondPoint, Team.Red, countToSend);
            spaceship.ChangeColor(Team.Red);
            spaceship.arrived += SpaceshipArrived;
        }

        private void SpaceshipArrived(Planet planet, Spaceship spaceship)
        {
            planet.UpCount(spaceship.team, spaceship.count);
            spaceship.arrived -= SpaceshipArrived;
        }

        IEnumerator EnemyCapture()
        {
            while (true)
            {
                foreach (var planet in _planets)
                {
                    CheckPlanetCapture(planet);
                }
                yield return new WaitForSeconds(5);
            }
        }
    }
}
