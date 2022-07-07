using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Toggle _sound;
    [SerializeField] private Toggle _vibro;

    public bool SoundOn()
    {
        return _sound.isOn;
    }

    public bool VibroOn()
    {
        return _vibro.isOn;
    }
}
