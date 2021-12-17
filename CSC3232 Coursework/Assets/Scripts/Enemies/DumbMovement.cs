using UnityEngine;

public class DumbMovement : MonoBehaviour
{

    [SerializeField]
    private EnemyStatTracker statTracker;
    [SerializeField]
    private EnemyRaycast enemyRaycast;

    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private BoxCollider2D boxCollider;

    [SerializeField]
    private float[] walkTime;
    [SerializeField]
    private float[] idleTime;

    [SerializeField]
    private float idleSpeed;
    [SerializeField]
    private float idleAcceleration;

    [SerializeField]
    private float combatSpeed;
    [SerializeField]
    private float combatAcceleration;

    private bool playerVisible;
    private bool playerAttackable;

    private float actionTime;
    private float timer;
    private bool isMoving;
    private int direction;


    public bool GetIsMoving()
    {
        return isMoving;
    }

    public int GetDirection()
    {
        return direction;
    }

    // Start is called before the first frame update
    void Start()
    {
        //initialise rigidbody
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        circleCollider = GetComponent<CircleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        //setup variables for random movement.
        //enemy starts off in idle mode
        direction = 1;
        actionTime = GenerateRandomNumber(idleTime[0], idleTime[1]);
        timer = actionTime;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        //look for player
        playerVisible = enemyRaycast.GetPlayerVisisble();
        //check if in range for attack
        playerAttackable = enemyRaycast.GetPlayerAttackable();

        if (playerVisible)
        {
            if (statTracker.GetAttacking() || playerAttackable)
            {
                isMoving = false;
                timer = 1.5f;
            }
            else
            {
                isMoving = true;
            }
        }
        else if (statTracker.GetCurrentHealth() > 0 && !statTracker.GetAttacking())
        {
            //idle movement stuff

            timer -= Time.deltaTime;

            //if enemy is walking and timer is less than 0, stop walking, generate time to be idle for
            if (isMoving && timer < 0f)
            {
                //set ismoving to false
                isMoving = false;
                //generate the amount of time the enemy will idle for
                timer = GenerateRandomNumber(idleTime[0], idleTime[1]);
            }
            //if enemy is idle and timer is less than 0, flip orientation and start walking, generate time to be moving for
            else if (!isMoving && timer < 0f)
            {
                //flip direction
                direction *= -1;
                //set ismoving to true
                isMoving = true;
                //generate the amount of time the enemy will walk for
                timer = GenerateRandomNumber(walkTime[0], walkTime[1]);
            }
        }
    }

    private void FixedUpdate()
    {

        //if the enemy is dead
        if (statTracker.GetCurrentHealth() <= 0)
        {
            //stop it from moving
            rb.velocity = Vector2.right * 0;
            //disable colliders and stop enemy from falling through the ground
            boxCollider.enabled = false;
            circleCollider.enabled = false;
            rb.isKinematic = true;
            Destroy(gameObject, 2f);
        }

        //if the player is visible
        if (playerVisible)
        {
            if (statTracker.GetAttacking() || playerAttackable)
            {
                //stop moving
                rb.velocity = Vector2.right * 0;
            }
            else
            {
                //move towards player
                MoveEnemy(combatSpeed, combatAcceleration);
            }
        }
        else
        {
            //idle movement stuff
            if (statTracker.GetCurrentHealth() > 0 && isMoving && !statTracker.GetIsHit() && !statTracker.GetAttacking())
            {
                //move
                MoveEnemy(idleSpeed, idleAcceleration);
            }
            else if (Mathf.Abs(rb.velocity.x) > 0.01f)
            {
                //stop moving
                rb.velocity = Vector2.right * 0;
            }
        }

    }

    private void MoveEnemy(float speed, float acceleration)
    {
        //set the movementforce to the difference between top speed and current velocity
        float movementForce = speed - Mathf.Abs(rb.velocity.x);
        //increase the movement force due to acceleration
        movementForce = Mathf.Pow(movementForce, acceleration) * direction;
        //add the force
        rb.AddForce(Vector2.right * movementForce * Time.fixedDeltaTime);
    }

    //generate a random number between two values
    private float GenerateRandomNumber(float number1, float number2)
    {
        float randomNumber = 0;

        randomNumber = Random.Range(number1, number2);

        return randomNumber;
    }

    //gets the distance to the player
    public float GetDistanceToPlayer()
    {
        float distance = 0;
        distance = Vector2.Distance(rb.position, GameObject.Find("Player").GetComponent<PlayerMovement>().GetPosition());
        return distance;
    }

}
