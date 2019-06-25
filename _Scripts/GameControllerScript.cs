using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public GameObject hazard;

    public Vector3 spawnValues;

    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float endLoop;
    public int waveWait;

    public Text scoreText;
    public int scoreMultiplier;
    private int score;
    
    void Start()
    {
        score = 0;
        UpdateScore();
        StartCoroutine(spawnWaves());
    }

    IEnumerator spawnWaves ()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {

            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 SpawnPosition =
                    new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);

                Quaternion SpawnRotation =
                    Quaternion.identity;

                Instantiate(hazard, SpawnPosition, SpawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            
            /*
            if (endLoop == 3)
            {
                break;
            }*/
            yield return new WaitForSeconds(waveWait);
        }
    }

    public void scoreDefector()
    {
        score += scoreMultiplier;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
