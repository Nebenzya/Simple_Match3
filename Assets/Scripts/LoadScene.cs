using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private LoadScene() { }
    public static LoadScene instance;
    private void Awake()
    {
        instance = this;
    }

    public void LoadNewScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        UIGameBoard.instance.Restart();
    }
}
