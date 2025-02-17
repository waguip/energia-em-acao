using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [SerializeField] private GameObject victoryScreenPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnEnable()
    {
        GameEvents.OnLevelEnd += EndLevel;
    }

    private void OnDisable()
    {
        GameEvents.OnLevelEnd -= EndLevel;
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCoroutine());
    }

    private IEnumerator EndLevelCoroutine()
    {
        if (NarrationManager.Instance.hasLevelCompleteNarration())
        {
            NarrationManager.Instance.PlayLevelCompleteNarration();
            while (NarrationManager.Instance.IsNarrationPlaying)
            {
                yield return null;
            }
        }

        Instantiate(victoryScreenPrefab);
    }

    public void NextLevel()
    {
        NarrationManager.Instance.StopNarration();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
