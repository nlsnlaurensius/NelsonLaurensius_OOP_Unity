using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public LevelManager LevelManager { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        LevelManager = GetComponentInChildren<LevelManager>();

        DontDestroyOnLoad(gameObject);
        GameObject camera = GameObject.Find("Camera");
        if (camera != null)
        {
            DontDestroyOnLoad(camera);
        }

        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            DontDestroyOnLoad(player);
        }

        GameObject sceneTransition = GameObject.Find("SceneTransition");
        if (sceneTransition != null)
        {
            DontDestroyOnLoad(sceneTransition);
        }
    }
    public void ResetPlayerPosition()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = Vector3.zero; 
        }
    }
}
