using UnityEngine;

namespace PlanetaryCapture
{
    public class Planet : MonoBehaviour
    {
        //[SerializeField] private Percent _prefabPercent;
        [SerializeField] private PlanetConfig _planetConfig;

        public Team Team { get; private set; }
        private Percent percent;

        private float _blueCount;
        private float _redCount;

        private void Start()
        {
            Team = _planetConfig.StartTeam;
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
        }

        public void UpdateCapture(float attackSpeed)
        {
            if (percent == null)
            {
                percent = Instantiate(_planetConfig.Percent);
                percent.SetPosition(transform);
            }

            _redCount -= _blueCount * attackSpeed;
            if ((int)_redCount == 0 || _redCount < 0) _redCount = 0;

            _blueCount -= _redCount * attackSpeed;
            if ((int)_blueCount == 0 || _blueCount < 0) _blueCount = 0;

            percent.SetCountTeams((int) _blueCount, (int) _redCount);
        }
    }
}
