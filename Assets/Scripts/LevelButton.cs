using UnityEngine;

public class LevelButton : MonoBehaviour
{

    LevelManager levelManager;

    private void Awake() {
        levelManager = LevelManager.Instance;
    }

    public void PassLevel()
    {
        levelManager.NextLevel();
    }

    public void RestartGame()
    {
        levelManager.RestartGame();
    }

    public void QuitGame()
    {
        levelManager.QuitGame();
    }
}
