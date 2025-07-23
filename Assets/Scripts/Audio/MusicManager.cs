using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip level1Music;
    public AudioClip gameOverMusic;
    public AudioClip victoryMusic;
    public AudioClip sadMusic;

    private AudioSource audioSource;
    private string currentScene;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.name;
        PlayMusicForScene(currentScene);
    }

    void PlayMusicForScene(string sceneName)
    {
        AudioClip clipToPlay = null;

        switch (sceneName)
        {
            case "Menù":
                clipToPlay = menuMusic;
                break;
            case "Level1" or "Level2":
                clipToPlay = level1Music;
                break;
            case "GameOver":
                clipToPlay = gameOverMusic;
                break;
            case "Victory":
                clipToPlay = victoryMusic;
                break;
            case "Esci":
                clipToPlay = sadMusic;
                break;
        }

        if (clipToPlay != null && audioSource.clip != clipToPlay)
        {
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }
    }
}
