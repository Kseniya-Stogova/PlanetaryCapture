using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetaryCapture
{
    public class Percent : MonoBehaviour
    {
        [SerializeField] private Image _redPercent;
        [SerializeField] private Image _bluePercent;

        [SerializeField] private Text _text;
        [SerializeField] private Text _redText;
        [SerializeField] private Text _blueText;

        private void Awake()
        {
            transform.SetParent(FindObjectOfType<Canvas>().transform);
        }

        public void SetCountTeams(int blueCount, int redCount)
        {
            if (blueCount == 0 && redCount == 0)
            {
                _text.enabled = false;
                _redText.gameObject.SetActive(false);
                _blueText.gameObject.SetActive(false);
            } else if (blueCount == 0 || redCount == 0)
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

            if (blueCount + redCount == 0)
            {
                _bluePercent.fillAmount = 0;
                _redPercent.fillAmount = 0;
            }
            else
            {
                _bluePercent.fillAmount = (float)blueCount / (blueCount + redCount);
                _redPercent.fillAmount = (float)redCount / (blueCount + redCount);
            }
        }

        public void SetPosition(Transform target)
        {
            transform.SetParent(FindObjectOfType<Canvas>().transform);
            Vector3 position = Camera.main.WorldToScreenPoint(target.position);
            transform.position = new Vector3(position.x, position.y, transform.position.z);
            
        }
    }
}
