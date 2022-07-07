using UnityEngine;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    [SerializeField] private GameObject _winImage;
    [SerializeField] private GameObject _loseImage;

    public void GameOver(bool win, int levelNumber)
    {
        int score = ServiceLocator.GetService<Score>().score;

        gameObject.SetActive(true);

        if (win)
        {
            _winImage.SetActive(true);

            SaveLoad.Load();
            SaveLoad.openLevels[levelNumber + 1] = true;
            SaveLoad.stars[levelNumber] = score;
            SaveLoad.Save();
        }
        else
        {
            _loseImage.SetActive(true);
        }
    }
}
