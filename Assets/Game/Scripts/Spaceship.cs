using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetaryCapture
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Spaceship : MonoBehaviour
    {
        [SerializeField] private Sprite _blueSprite;
        [SerializeField] private Sprite _redSprite;
        [SerializeField] private SpaceshipConfig _spaceshipConfig;
        [SerializeField] private TMP_Text _text;
        [HideInInspector] public Planet target;

        public float speed { get; private set; }
        public Team team;
        public int count;

        public event Action<Planet, Spaceship> arrived;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            speed = _spaceshipConfig.Speed;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(Planet planet, Team team, int count)
        {
            target = planet;
            this.team = team;
            this.count = count;
            _text.text = count.ToString();
            gameObject.SetActive(true);
            transform.up = target.transform.position - transform.position;
        }

        private void Update()
        {
            if (target == null) return;

            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
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

        public void ChangeColor(Team team)
        {
            _spriteRenderer.sprite = team == Team.Red ? _redSprite : _blueSprite;
        }
    }
}
