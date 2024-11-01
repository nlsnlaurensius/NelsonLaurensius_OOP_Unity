using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed = new Vector2(10f, 10f);
    [SerializeField] Vector2 timeToFullSpeed = new Vector2(0.5f, 0.5f);
    [SerializeField] Vector2 timeToStop = new Vector2(0.5f, 0.5f);
    [SerializeField] Vector2 stopClamp = new Vector2(0.1f, 0.1f); 
 [SerializeField] float frictionCoefficient = 5f; 

    public Vector2 moveDirection;
    Vector2 moveVelocity;

    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if(moveDirection.magnitude == 0)
        {
            Initialize();
        } 
        else
        {
            moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        }
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
    }

    private void Initialize()
    {
        moveDirection = Vector2.zero;
        moveVelocity = Vector2.zero;
    }

    public void Move()
{
    Vector2 currentVelocity = rb.velocity;

    if (moveDirection.magnitude == 0)
    {
        Vector2 frictionForce = -currentVelocity.normalized * frictionCoefficient;
        
        currentVelocity += frictionForce * Time.deltaTime;

        currentVelocity.x = Mathf.Clamp(currentVelocity.x, -stopClamp.x, stopClamp.x);
        currentVelocity.y = Mathf.Clamp(currentVelocity.y, -stopClamp.y, stopClamp.y);

        if (Mathf.Abs(currentVelocity.x) <= stopClamp.x)
        {
            currentVelocity.x = 0;
        }
        if (Mathf.Abs(currentVelocity.y) <= stopClamp.y)
        {
            currentVelocity.y = 0;
        }
    }
    else
    {
        currentVelocity.x = Mathf.Lerp(currentVelocity.x, moveDirection.x * maxSpeed.x, timeToFullSpeed.x * Time.deltaTime);
        currentVelocity.y = Mathf.Lerp(currentVelocity.y, moveDirection.y * maxSpeed.y, timeToFullSpeed.y * Time.deltaTime);
    }

    rb.velocity = currentVelocity;
}


    public Vector2 GetFriction()
    {
        if (moveDirection.magnitude == 0)
        {
            return stopFriction;
        }
        else
        {
            return moveFriction;
        }
    }

    public void MoveBound()
    {
        // nantian isinya
    }

    public bool IsMoving()
    {
        if(rb.velocity.magnitude > 0)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
