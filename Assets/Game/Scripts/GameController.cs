using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetaryCapture
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private LevelConfig _levelConfig;

        private float _spaceshipAttackSpeed;

        private List<Planet> _planets;

        private Planet _firstPlanet;
        private Planet _secondPlanet;

        private void Start()
        {
            Begin();
        }

        public void Begin()
        {
            _spaceshipAttackSpeed = _levelConfig.AttackSpeed;
            _planets = FindObjectsOfType<Planet>().ToList();

            foreach (var planet in _planets)
            {
                planet.choose += PlanetSelect;
            }

            _planets[0].UpCount(Team.Red, 20);
            _planets[0].UpCount(Team.Blue, 60);

            StartCoroutine(UpdateCapture());
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

        private void PlanetSelect(Planet planet)
        {
            if (_firstPlanet == null)
            {
                planet.Select(true);
                _firstPlanet = planet;
            }
            else if (_secondPlanet == null)
            {
                _firstPlanet.Select(false);
                if (planet == _firstPlanet)
                {
                    return;
                }
                _secondPlanet = planet;
                CreateSpaceShip();
            }
            else
            {
                _firstPlanet = planet;
                _secondPlanet = null;
            }
        }

        private void CreateSpaceShip()
        {
            float percentToSend = ServiceLocator.GetService<Slider>().value;
            int countToSend = _firstPlanet.TakeBlueSpaceship(percentToSend);
            if (countToSend == 0) return;

            Spaceship spaceship = SpaceshipPool.SharedInstance.GetSpaceship();
            spaceship.transform.position = _firstPlanet.transform.position;
            spaceship.Initialize(_secondPlanet, Team.Blue, countToSend);
            spaceship.arrived += SpaceshipArrived;

            _firstPlanet.Select(false);
            _firstPlanet = null;
            _secondPlanet.Select(false);
            _secondPlanet = null;
        }

        private void SpaceshipArrived(Planet planet, Spaceship spaceship)
        {
            planet.UpCount(spaceship.team, spaceship.count);
            spaceship.arrived -= SpaceshipArrived;
        }
    }
}
