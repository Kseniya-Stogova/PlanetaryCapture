using UnityEngine;

namespace PlanetaryCapture
{
    public class Planet : MonoBehaviour
    {
        [SerializeField] private PlanetConfig _planetConfig;

        public Team Team { get; private set; }

        private int _blueCount;
        private int _redCount;

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
    }
}
