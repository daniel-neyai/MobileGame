using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LS_PlayerMotor : MonoBehaviour {

    private const float LANE_DISTANCE = 3.5f;
    private const float TURN_SPEED = 0.05f;

    public static LS_GameManager LSGMinstance;

    //$
    private int CRASH_BEFORE_DEATH = 0; // 2 
    private int CRASHES_UNTIL_DEATH = 15; // 3

    //
    public bool gameBegin = false;

    public GameObject PlayerDeathEffect;
    public GameObject PlayerObject;

    //Movement
    private CharacterController controller;
    private float jumpForce = 10.0f;
    private float gravity = 40.0f;
    private float verticalVelocity;

    [HideInInspector]
    public int desiredLane = 1; // 0 = Left, 1 = Middle, 2 = Right

    // Speed Modifier
    private float originalSpeed = 14.0f;
    private float speed;
    private float speedIncreaseLastTick;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;

    private void Start()
    {
        speed = originalSpeed;

        controller = GetComponent<CharacterController>();
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        LSGMinstance.PlayerEnhancements = false;
    }

    private void Update()
    {
        if (!gameBegin)
            return;
        calculateAll();
        IncreaseSpeed();
    }

    private void IncreaseSpeed()
    {
        if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            speed += speedIncreaseAmount;
            LS_GameManager.instance.UpdateModifier(speed - originalSpeed);
        }
    }

    private void calculateAll()
    {
            if (LS_MobileInput.Instance.SwipeLeft)
                MoveLane(false);
            if (LS_MobileInput.Instance.SwipeRight)
                MoveLane(true);

        // Calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
            targetPosition += Vector3.left * LANE_DISTANCE;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * LANE_DISTANCE;

        // Calculate our move delta
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;

        bool isGrounded = IsGrounded();

        // Calculate Y
        if(isGrounded) // if Grounded
        {
            verticalVelocity = -0.1f;

            if (LS_MobileInput.Instance.SwipeUp)
            {
                //Jump
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);

            //Fast Falling mechanic
            if (LS_MobileInput.Instance.SwipeDown)
            {
                verticalVelocity = -jumpForce;
            }
        }

        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        // Move the ball
        controller.Move(moveVector * Time.deltaTime);

        // Rotate the ball to where he is going
        Vector3 dir = controller.velocity;
        if (dir != controller.velocity)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
        }
    }

    private void MoveLane(bool goingRight)
    {
            desiredLane += goingRight ? 1 : -1;
            desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }

    private bool IsGrounded()
    {
        Ray groundRay = new Ray(new Vector3(controller.bounds.center.x,(controller.bounds.center.y - controller.bounds.extents.y) + 0.2f,controller.bounds.center.z),Vector3.down);
        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.cyan, 1.0f);

        return (Physics.Raycast(groundRay, 0.2f + 0.1f));
    }

    public void GameBegin()
    {
        gameBegin = true;
    }
    /*
     * WORK
     * IN
     * PROGRESS
     *
    private void InspectCrash()
    {
        if (CRASH_BEFORE_DEATH != CRASHES_UNTIL_DEATH)
        {
            CRASH_BEFORE_DEATH++;
            Debug.Log("LS_PlayerMotor: Checking if CRASH_BEFORE_DEATH which is = " + CRASH_BEFORE_DEATH + "Is equal to " + CRASHES_UNTIL_DEATH);
        }
        else
            CrashCondition();
    }*/
    
    private void CrashCondition()
    {
        gameBegin = false;
        LS_GameManager.instance.OnDeath();

        FindObjectOfType<LS_AudioManager>().Play("PlayerDeath");
            Instantiate(PlayerDeathEffect, gameObject.transform.position, Quaternion.identity);
        LS_GameManager.instance.IsDead = true;
            Destroy(gameObject);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {

            /* Adding things that will kill your character
                    case "ThatThingsTag"
                    //DeathCondition
                     Break;
            */

            case "Obstacle":
                CrashCondition();
                break;
        }
    }
}
