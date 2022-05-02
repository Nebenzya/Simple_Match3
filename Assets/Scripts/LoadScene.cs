using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
   public void LoadNewScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
