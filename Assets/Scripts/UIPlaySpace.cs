using UnityEngine;
using UnityEngine.UI;

public class UIPlaySpace : MonoBehaviour
{
    public static UIPlaySpace instance;
    private void Awake() => instance = this;

    public Text scoreText;

    private int score = 0;
    public int Score
    {
        get => score;
        set
        {
            score += value;
            scoreText.text = $"Score: {score}";
        }
    }
}
