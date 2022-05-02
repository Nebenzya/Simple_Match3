using UnityEngine;

public enum Level
{
    None,
    Easy,
    Middle,
    Hard
}

public class GameLevel : MonoBehaviour
{
    public static GameLevel intence;
    private void Awake()
    {
        intence = this;
    }
    private Level currentLevel = Level.Middle;
    public Level CurrentLevel { get => currentLevel; }

    public void SetEasy() => currentLevel = Level.Easy;
    public void SetMiddle() => currentLevel = Level.Middle;
    public void SetHard() => currentLevel = Level.Hard;
}
