using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Score : MonoBehaviour
{
    private Text _scoreText;

    public int score { get; private set; }

    private void Awake()
    {
        _scoreText = GetComponent<Text>();
        _scoreText.text = score.ToString();
    }

    public void UpScore(int number)
    {
        score += number;
        _scoreText.text = score.ToString();
    }
}
