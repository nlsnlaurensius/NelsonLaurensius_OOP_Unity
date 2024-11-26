using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed;        
    [SerializeField] float rotateSpeed; 
    Vector2 newPosition;                

    void Start()
    {
        ChangePosition();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            bool playerHasWeapon = player.GetComponentInChildren<Weapon>() != null;

            gameObject.GetComponent<SpriteRenderer>().enabled = playerHasWeapon;
            gameObject.GetComponent<Collider2D>().enabled = playerHasWeapon;

            if (playerHasWeapon)
            {
                if (Vector2.Distance(transform.position, newPosition) < 0.5f)
                {
                    ChangePosition();
                }

                transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

                transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
    }

    void ChangePosition()
    {
        newPosition = new Vector2(Random.Range(-10f, 10f), Random.Range(-5f, 5f));
    }
}
