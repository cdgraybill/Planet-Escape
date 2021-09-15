using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float invokeDelay = 1f;
    [SerializeField] AudioClip levelComplete;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                PlaySuccessSequence();
                break;
            default:
                PlayCrashSequence();
                break;
        }
    }

    void PlayCrashSequence()
    {
        StopPlayerMovement();
        audioSource.PlayOneShot(crash);
        Invoke("ReloadLevel", invokeDelay);
    }

    void PlaySuccessSequence()
    {
        StopPlayerMovement();
        audioSource.PlayOneShot(levelComplete);
        Invoke("LoadNextLevel", invokeDelay);
    }

    void ReloadLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }

    void LoadNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentLevel + 1;

        if (nextLevel == SceneManager.sceneCountInBuildSettings)
        {
            nextLevel = 0;
        }

        StopPlayerMovement();
        SceneManager.LoadScene(nextLevel);
    }

    void StopPlayerMovement()
    {
        GetComponent<Movement>().enabled = false;
    }
}
