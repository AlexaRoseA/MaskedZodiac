using UnityEngine;
using System.Collections;

/// <summary>
/// Creates wandering behaviour for a CharacterController.
/// </summary>
/// 
public class NPCAIWalk : MonoBehaviour
{
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private float characterVelocity = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;

    public bool talking = false;
    private Animator animate;


    void Start()
    {
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
        animate = gameObject.GetComponent<Animator>();
        animate.SetBool("Walking", true);
    }

    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), transform.position.y).normalized;
        movementPerSecond = movementDirection * characterVelocity;
    }

    void Update()
    {
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            int ran = Random.Range(0, 10);
            Debug.Log(ran);

            if(talking) {
                animate.SetBool("Walking", false);
                StopMovement();
            } else if (ran > 5) {
                calcuateNewMovementVector();
                animate.SetBool("Walking", true);
            } else
            {
                animate.SetBool("Walking", false);
                StopMovement();
            }

        }

        // Flip the Character:
        Vector3 characterScale = transform.localScale;
        if (movementDirection.x < 0)
        {
            characterScale.x = -0.2f;
        }
        if (movementDirection.x > 0)
        {
            characterScale.x = 0.2f;
        }
        transform.localScale = characterScale;
        //move enemy: 
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
         transform.position.y);
    }

    void StopMovement()
    {
        movementDirection = new Vector2(0f, transform.position.y).normalized;
        movementPerSecond = Vector2.zero;
    }
}