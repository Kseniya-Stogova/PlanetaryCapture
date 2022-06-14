using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlanetaryCapture
{
    public class Planet : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TMP_Text _blueText;
        [SerializeField] private TMP_Text _redText;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private SpriteRenderer _selected;

        [SerializeField] private PlanetConfig _planetConfig;

        public Team Team { get; private set; }
        private MaskController _percent;

        private float _blueCount;
        private float _redCount;

        public event Action<Planet> choose; 

        private void Awake()
        {
            _percent = GetComponentInChildren<MaskController>();
        }

        private void Start()
        {
            Team = _planetConfig.StartTeam;
            StartCoroutine(GenerateSpaceship());
        }

        public int TakeBlueSpaceship(float percent)
        {
            int spaceship = (int) (_blueCount * percent);
            _blueCount -= spaceship;
            SetCountTeams();

            return spaceship;
        }

        public int TakeRedSpaceship(float percent)
        {
            int spaceship = (int)(_redCount * percent);
            _redCount -= spaceship;
            SetCountTeams();

            return spaceship;
        }

        public float TakeRedPercentCapture()
        {
            if (_redCount + _blueCount == 0) return 0;

            float percent = _redCount / (_redCount + _blueCount);

            return percent;
        }

        public void UpCount(Team team, int count)
        {
            switch (team)
            {
                case Team.Red:
                    _redCount += count;
                    break;
                case Team.Blue:
                    _blueCount += count;
                    break;
            }

            SetCountTeams();
        }

        public void UpdateCapture(float attackSpeed)
        {
            _redCount -= _blueCount * attackSpeed;
            if ((int)_redCount == 0 || _redCount < 0) _redCount = 0;

            _blueCount -= _redCount * attackSpeed;
            if ((int)_blueCount == 0 || _blueCount < 0) _blueCount = 0;

            SetCountTeams();
        }

        private void SetCountTeams()
        {
            int blueCount = (int) _blueCount;
            int redCount = (int) _redCount;

            if (blueCount == 0 && redCount == 0)
            {
                _text.enabled = false;
                _redText.gameObject.SetActive(false);
                _blueText.gameObject.SetActive(false);
            }
            else if (blueCount == 0 || redCount == 0)
            {
                _text.enabled = true;
                _redText.gameObject.SetActive(false);
                _blueText.gameObject.SetActive(false);

                _text.text = blueCount == 0 ? redCount.ToString() : blueCount.ToString();
                _text.color = blueCount == 0 ? Color.red : Color.blue;
            }
            else
            {
                _redText.text = redCount.ToString();
                _blueText.text = blueCount.ToString();

                _redText.gameObject.SetActive(true);
                _blueText.gameObject.SetActive(true);
                _text.enabled = false;
            }

            float percent = 0;

            if (blueCount + redCount != 0)
            {
                percent = (float) redCount / (blueCount + redCount);

                if (percent == 1) Team = Team.Red;
                else if (blueCount / (blueCount + redCount) == 1)
                {
                    Team = Team.Blue;
                }
                else Team = Team.Neutral;
            }

            _percent.SetPercent(percent);
        }

        public void Choose(bool on)
        {
            _selected.gameObject.SetActive(on);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            choose?.Invoke(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Choose(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_selected.color == Color.green) return;
            Choose(false);
        }

        public void Select(bool on)
        {
            _selected.color = on ? Color.green : Color.white;
            Choose(on);
        }

        IEnumerator GenerateSpaceship()
        {
            while (true)
            {
                if (Team != Team.Neutral) UpCount(Team, 1);
                yield return new WaitForSeconds(_planetConfig.SpeedGeneration);
            }
        }
    }
}
