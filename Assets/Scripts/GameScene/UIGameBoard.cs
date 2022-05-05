using UnityEngine;
using UnityEngine.UI;

public class UIGameBoard : MonoBehaviour
{
    private UIGameBoard() { } // To save the "Singleton"
    public static UIGameBoard instance;
    private void Awake()
    {
        instance = this;
        stepText.text = $"Steps: {_step}";
    }

    public GameObject gameOverPanel;

    #region score
    public Text scoreText;
    private int _score;
    private readonly int _scoreRate = 50;
    public int Score
    {
        get => _score;
        set
        {
            // At the entrance we get the number of matched tiles and multiply by the scoreRate
            _score += value * _scoreRate;
            if (_step == maxStep)
            {
                _score = 0;
            }
            scoreText.text = $"Score: {_score}";
        }
    }
    #endregion // region

    #region steps
    public Text stepText;
    private static int maxStep = 20;
    private static int _step = maxStep;
    
    public void Step()
    {
        _step--;
        if (_step <= 0)
        {
            GameOver();
        }
        stepText.text = $"Steps: {_step}";
    }
    #endregion // steps

    public void Restart()
    {
        _score = 0;
        _step = maxStep;
    }
    private void GameOver()
    {
        System.Threading.Thread.Sleep(500);
        gameOverPanel.SetActive(true);
    }

}
