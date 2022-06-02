using System;
using TMPro;
using UnityEngine;

namespace PlanetaryCapture
{
    public class Spaceship : MonoBehaviour
    {
        [SerializeField] private SpaceshipConfig _spaceshipConfig;
        [SerializeField] private TMP_Text _text;
        [HideInInspector] public Transform target;

        public float speed { get; private set; }
        public Team team;
        public int count;

        public event Action arrived;

        private void Awake()
        {
            speed = _spaceshipConfig.Speed;
        }

        public void Initialize(Transform planet, Team team, int count)
        {
            target = planet.transform;
            this.team = team;
            this.count = count;
            _text.text = count.ToString();
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (target == null) return;
            transform.up = target.position - transform.position;
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject == target.gameObject)
            {
                arrived?.Invoke();
                target = null;
                gameObject.SetActive(false);
            }
        }
    }
}
