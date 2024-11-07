using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    public Animator Animator => animator;


    void Awake(){   
        animator.enabled = false;
        
    }
    
    IEnumerator LoadSceneAsync(string sceneName)
    {
        animator.enabled = true;
        
            animator.SetTrigger("EndTransition");
            yield return new WaitForSeconds(1);  
            Debug.Log("StartTransition ke trigger");
        

         SceneManager.LoadSceneAsync(sceneName);

        if (sceneName == "Main")
        {
            GameManager.Instance.ResetPlayerPosition();
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}
