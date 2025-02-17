using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    private ILevelStrategy levelStrategy;
    private bool isLevelCompleted = false;

    public static Level Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            InitializeLevelStrategy();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        InitializeLevelStrategy();
    }

    public void InitializeLevelStrategy()
    {
        if (NarrationManager.Instance != null)
        {
            NarrationManager.Instance.PlayLevelStartNarration();
        }

        isLevelCompleted = false;
        levelStrategy = null;

        LevelType levelType = GetLevelType(); ;
        if (levelType == LevelType.None)
        {
            return;
        }

        // Identifica qual estratégia usar com base no nível atual
        levelStrategy = LevelFactory.CreateLevelStrategy(
            levelType,
            FindObjectsByType<DropSlot>(FindObjectsSortMode.None).ToList(),
            FindObjectsByType<DragObject>(FindObjectsSortMode.None).ToList(),
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISpriteMatching>().ToList()
        );

        levelStrategy.InitializeLevel();
    }

    private void Update()
    {
        if (levelStrategy == null)
        {
            return;
        }

        if (!isLevelCompleted && levelStrategy.CheckWinCondition())
        {
            isLevelCompleted = true;
            GameEvents.LevelEnd();
        }
    }

    private LevelType GetLevelType()
    {
        // Aqui você pode definir a lógica para determinar o tipo do nível
        if (FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISpriteMatching>().Any())
        {
            return LevelType.SpriteMatching;
        }
        else if (FindAnyObjectByType<DragObject>() != null && FindAnyObjectByType<DropSlot>() != null)
        {
            return LevelType.DragAndDrop;
        }
        else
        {
            return LevelType.None;
        }
    }

    private void OnDestroy()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}