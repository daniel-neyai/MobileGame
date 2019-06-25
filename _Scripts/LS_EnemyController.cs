using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LS_EnemyController : MonoBehaviour {

    public int health;
    public float speed = 3f;
    public float rotationSpeed = 100f;

    [TextArea]
    public string Info = "::ADVANCED USERS ONLY:: Changing these without proper knowledge of how to use will break the script!";

    public int plus = 1;
    public int minus = 3;
    public int rotPlus = 1;
    public int rotMinus = 4;
    public int randPlus = 1;
    public int randMinus = 2;
    public int rotLorPlus = 1;
    public int rotLorMinus = 4;
    public int walkWaitPlus = 1;
    public int walkWaitMinus = 4;
    public int walkTimePlus = 1;
    public int walkTimeMinus = 5;
    public int rand;

    private Animator anim;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    public bool playerDetected = false;

    public float detectionRange = 3.5f;
    public Vector3 distanceVector;
    public float distanceSquared;

    public GameObject EnemyGO;

    public Transform playerDetectionArea;
    public LayerMask whatIsPlayer;
    public GameObject EnemyTest;
    public GameObject m_Player;

    private LS_EnemyController LSEC;
    public Transform targetTransform;
    public float distanceFromPlayer = 1.5f;
    public float speedS = 3.5f;

    public void DetectPlayer()
    {

            Vector3 displacementFromTarget = targetTransform.position - transform.position;
            Vector3 directionToTarget = displacementFromTarget.normalized;
            Vector3 velocity = directionToTarget * speedS;

            float distanceToTarget = displacementFromTarget.magnitude;

            if (distanceToTarget > distanceFromPlayer)
            {
                transform.Translate(velocity * Time.deltaTime);
            }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(EnemyTest);
        }
    }
    /*
        void Start()
        {
            distanceVector = m_Player.transform.position - EnemyGO.transform.position;
            Debug.Log("Distance: " + distanceSquared);
            rand = Random.Range(randPlus, randMinus);
            anim = GetComponent<Animator>();
            EAI = GetComponent<EnemyAi>();
            LSPD = GetComponent<LS_PlayerDetected>();
        }

        void Update()
        {
            distanceSquared = Vector3.Distance(m_Player.transform.position, EnemyGO.transform.position);

            //CALLS:
            CheckMovement();
            CheckDetectionArea();
            checkDistance();
        }

        public void checkDistance()
        {
            if (distanceSquared > 0 && distanceSquared < detectionRange)
            {
                LSPD.TriggerDetected = true;
            } else
            {
                LSPD.TriggerDetected = false;
            }
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(EnemyTest);
            }
        }

        void CheckMovement()
        {
            if(isWandering == false && !playerDetected)
            {
                StartCoroutine(WanderFunction());
            }

            if(isRotatingRight == true && !playerDetected)
            {
                // FOR 3D PURPOSES!
                //transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);

                transform.position += transform.right * speed * Time.deltaTime;
            }

            if (isRotatingLeft == true && !playerDetected)
            {
                // FOR 3D PURPOSES!
                //transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed);
                transform.position -= transform.right * speed * Time.deltaTime;
            }

            if(isWalking == true && rand == 1 && !playerDetected)
            {
                transform.position += transform.right * speed * Time.deltaTime;
            } else if (isWalking == true && rand == 2 && !playerDetected)
            {
                transform.position -= transform.right * speed * Time.deltaTime;
            }
        }

        void CheckDetectionArea()
        {
         //   Collider2D[] playersToDetect = Physics2D.OverlapCircleAll(playerDetectionArea.position, detectionRange, whatIsPlayer);

                if (!LSPD.TriggerDetected)
                {
                    playerDetected = false;
                }

                else if (LSPD.TriggerDetected)
                {
                    EAI.DetectPlayer();
                    playerDetected = true;
                }
        }

        IEnumerator WanderFunction()
        {
            int rotationTime = Random.Range(plus, minus);
            int rotateWait = Random.Range(rotPlus, rotMinus);
            int rotateLorR = Random.Range(rotLorPlus, rotLorMinus);
            int walkWait = Random.Range(walkWaitPlus, walkWaitMinus);
            int walkTime = Random.Range(walkTimePlus, walkTimeMinus);
            int rand = Random.Range(randPlus, randMinus);

            isWandering = true;

            yield return new WaitForSeconds(walkWait);
            isWalking = true;
            yield return new WaitForSeconds(walkTime);
            isWalking = false;
            yield return new WaitForSeconds(rotateWait);

            if(rotateLorR == 1)
            {
                isRotatingRight = true;
                yield return new WaitForSeconds(rotationTime);
                isRotatingRight = false;
            }

            if (rotateLorR == 2)
            {
                isRotatingLeft = true;
                yield return new WaitForSeconds(rotationTime);
                isRotatingLeft = false;
            }
            isWandering = false;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerDetectionArea.position, detectionRange);
        }*/
}
