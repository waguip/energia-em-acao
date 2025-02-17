using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class NarrationManager : MonoBehaviour
{
    [SerializeField] private AudioSource narrationAudioSource;
    [SerializeField] private List<LevelNarrationData> levelNarrations;

    public static NarrationManager Instance { get; private set; }
    public bool IsNarrationPlaying { get; private set; } = false;
    private bool hasPlayedCorrectActionNarration = false;
    private Coroutine levelCompleteCoroutine;

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
        GameEvents.OnCorrectAction += PlayCorrectActionNarration;
        GameEvents.OnIncorrectAction += PlayIncorrectActionNarration;
    }

    private void OnDisable()
    {
        GameEvents.OnCorrectAction -= PlayCorrectActionNarration;
        GameEvents.OnIncorrectAction -= PlayIncorrectActionNarration;
    }

    public void PlayLevelStartNarration()
    {
        hasPlayedCorrectActionNarration = false;
        PlayNarration(GetLevelNarrationForScene()?.levelStartNarration);
    }

    public void PlayCorrectActionNarration()
    {
        if (hasPlayedCorrectActionNarration) return;
        PlayNarration(GetLevelNarrationForScene()?.correctActionNarration);
        hasPlayedCorrectActionNarration = true;

    }

    public void PlayIncorrectActionNarration()
    {
        PlayNarration(GetLevelNarrationForScene()?.incorrectActionNarration);
    }

    public void PlayLevelCompleteNarration()
    {
        levelCompleteCoroutine = StartCoroutine(PlayNarrationAndWait(GetLevelNarrationForScene().levelCompleteNarration));
    }

    public bool hasLevelCompleteNarration()
    {
        return GetLevelNarrationForScene()?.levelCompleteNarration != null;
    }

    private IEnumerator PlayNarrationAndWait(AudioClip narrationClip)
    {
        if (narrationClip == null)
        {
            yield break;
        }

        IsNarrationPlaying = true;
        PlayNarration(narrationClip);
        yield return new WaitForSeconds(narrationClip.length);
        IsNarrationPlaying = false;
    }

    private LevelNarrationData GetLevelNarrationForScene()
    {
        int sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        return levelNarrations.Find(narration => narration.sceneIndex == sceneIndex);
    }

    private void PlayNarration(AudioClip narrationClip)
    {
        if (narrationClip != null)
        {
            narrationAudioSource.clip = narrationClip;
            narrationAudioSource.Play();
        }
    }

    public void StopNarration()
    {
        if (narrationAudioSource.isPlaying)
        {
            narrationAudioSource.Stop();
        }

        if (levelCompleteCoroutine != null)
        {
            StopCoroutine(levelCompleteCoroutine);
            levelCompleteCoroutine = null;
        }

        IsNarrationPlaying = false;
    }
}