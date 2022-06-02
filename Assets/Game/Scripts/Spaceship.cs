using System;
using TMPro;
using UnityEngine;

namespace PlanetaryCapture
{
    public class Spaceship : MonoBehaviour
    {
        [SerializeField] private SpaceshipConfig _spaceshipConfig;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _planetRadius = 8f;
        [SerializeField] AnimationCurve _flybyCurve;
        [HideInInspector] public Planet target;

        private bool _barrier;

        public float speed { get; private set; }
        public Team team;
        public int count;

        private Vector3 _offset;

        public event Action<Planet, Spaceship> arrived;

        private Vector3 _beginPosition;

        private void Awake()
        {
            speed = _spaceshipConfig.Speed;
        }

        public void Initialize(Planet planet, Team team, int count)
        {
            target = planet;
            this.team = team;
            this.count = count;
            _text.text = count.ToString();
            gameObject.SetActive(true);
            _beginPosition = transform.position;
            transform.up = target.transform.position - transform.position;
            CheckForBarrier();
        }

        private void Update()
        {
            if (target == null) return;

            Vector3 offset = Vector3.zero;
            if (_barrier) 
            {
                offset = transform.right * _planetRadius * _flybyCurve.Evaluate(Vector3.Distance(_beginPosition, transform.position + _offset) / Vector3.Distance(_beginPosition, target.transform.position)) * Time.deltaTime;
                _offset += offset;
            }
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step) + offset;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject == target.gameObject)
            {
                arrived?.Invoke(target, this);
                target = null;
                gameObject.SetActive(false);
            }
        }

        private void CheckForBarrier()
        {
            /*Spaceship spaceship = SpaceshipPool.SharedInstance.GetSpaceship();
            spaceship.transform.position = transform.position + target.transform.position.normalized * _planetRadius;
            spaceship.gameObject.SetActive(true);*/

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, Vector3.Distance(transform.position, target.transform.position));

            _barrier = !(hit.collider == null || hit.collider == target.GetComponent<Collider2D>() || hit.collider == GetComponent<Collider2D>());
        }
    }
}
