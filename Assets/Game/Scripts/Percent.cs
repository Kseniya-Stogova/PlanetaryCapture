using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetaryCapture
{
    [RequireComponent(typeof(Text))]
    public class Percent : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        public void ChangeText(Slider slider)
        {
            int percent = (int) (slider.value * 100);
            _text.text = percent + "%";
        }
    }
}
