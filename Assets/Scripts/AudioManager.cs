using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource audioSource;

    [SerializeField] private AudioClip correctActionAudio;
    [SerializeField] private AudioClip victoryAudio;

    public static AudioManager Instance { get; private set; }
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
        GameEvents.OnCorrectAction += PlayCorrectActionAudio;
        GameEvents.OnLevelEnd += PlayVictoryAudio;
    }

    private void OnDisable()
    {
        GameEvents.OnCorrectAction -= PlayCorrectActionAudio;
        GameEvents.OnLevelEnd -= PlayVictoryAudio;
    }
    
    public void PlayCorrectActionAudio()
    {
        audioSource.clip = correctActionAudio;
        audioSource.volume = 0.3f;
        audioSource.pitch = Random.Range(0.96f, 0.98f);
        audioSource.Play();
    }

    public void PlayVictoryAudio()
    {
        StartCoroutine(PlayVictoryAudioWithDelay());
    }

    private IEnumerator PlayVictoryAudioWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.clip = victoryAudio;
        audioSource.volume = 0.8f;
        audioSource.Play();
    }

}