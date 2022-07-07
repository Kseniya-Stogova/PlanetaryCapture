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
        [SerializeField] private int _levelNumber;

        private float _spaceshipAttackSpeed;

        private List<Planet> _planets;

        private Planet _firstPlanet;
        private Planet _secondPlanet;

        public int levelNumber => _levelNumber;

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

                CheckFinish();

                yield return new WaitForSeconds(1);
            }
        }

        private void PlanetSelect(Planet planet)
        {
            if (ServiceLocator.GetService<Settings>().SoundOn())
            {
                ServiceLocator.GetService<AudioSource>().Play();
            }

            if (ServiceLocator.GetService<Settings>().VibroOn()) Handheld.Vibrate();

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
                CreateBlueSpaceship();
            }
            else
            {
                _firstPlanet = planet;
                _secondPlanet = null;
            }
        }

        private void CreateBlueSpaceship()
        {
            float percentToSend = ServiceLocator.GetService<Slider>().value;
            int countToSend = _firstPlanet.TakeBlueSpaceship(percentToSend);
            if (countToSend == 0) return;

            Spaceship spaceship = SpaceshipPool.SharedInstance.GetSpaceship();
            spaceship.transform.position = _firstPlanet.transform.position;
            spaceship.Initialize(_secondPlanet, Team.Blue, countToSend);
            spaceship.ChangeColor(Team.Blue);
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

        public void CheckFinish()
        {
            if (_planets.TrueForAll(p => p.TakeRedPercentCapture() == 1))
            {
                PlanetDestroy();
                StopAllCoroutines();
                ServiceLocator.GetService<End>().GameOver(false, _levelNumber);
            }

            if (_planets.TrueForAll(p => p.TakeBluePercentCapture() == 1))
            {
                PlanetDestroy();
                StopAllCoroutines();
                ServiceLocator.GetService<End>().GameOver(true, _levelNumber);
            }
        }

        private void PlanetDestroy()
        {
            foreach (var planet in _planets)
            {
                planet.gameObject.SetActive(false);
            }
        }
    }
}
