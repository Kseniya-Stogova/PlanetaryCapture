using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    [SerializeField] private List<Text> _scoreLevels;

    private List<Button> _levelsButtons;

    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);

        _levelsButtons = GetComponentsInChildren<Button>().ToList();

        foreach (var levelButton in _levelsButtons)
        {
            levelButton.interactable = false;
        }
    }

    private void Start()
    {
        SaveLoad.Load();

        _levelsButtons[0].interactable = true;

        for (int i = 1; i < _levelsButtons.Count; i++)
        {
            _levelsButtons[i].interactable = SaveLoad.openLevels[i];
        }

        for (int i = 0; i < _scoreLevels.Count; i++)
        {
            _scoreLevels[i].text = SaveLoad.stars[i].ToString();
        }
    }
}
