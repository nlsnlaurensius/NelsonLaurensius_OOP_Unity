    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class LevelManager : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] AudioSource audioSource;   
        public Animator Animator => animator;

        void Awake()
        {   
            animator.enabled = false;
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        IEnumerator LoadSceneAsync(string sceneName)
        {
            animator.enabled = true;
            
            animator.SetTrigger("EndTransition");
            
            if (audioSource.clip != null)
            {
                audioSource.Play(); 
            }
            
            yield return new WaitForSeconds(0.2f);  

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
