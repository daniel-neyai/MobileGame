using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class LS_GameManager : MonoBehaviour {

    public static LS_GameManager instance;

    private const int COIN_SCORE_AMOUNT = 5;

    /* TO-DO:
     * When enabled, coins will dissapear after about 2-3 seconds after being picked up
     * reducing lag as they normally dissapear with segments through LS_LevelManager
     * reducing lag as they normally dissapear with segments through LS_LevelManager
     */
    public const bool OptimizeGame = false;

    //  public LS_MatchSettings matchSettings;

    public GameObject[] CoinsLeft;

    public float respawnTime = 5;
    private float TimePer = 15f;

    public bool IsDead { set; get; }
    public bool PlayerEnhancements { set; get; }
    private bool isGameStarted = false;
    public bool disMusic = false;
    private LS_PlayerMotor motor;
    

    public GameObject hazard;

    public Vector3 spawnValues;

    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float endLoop;
    public int waveWait;


    //UI and the UI Fields
    public Animator gameCanvas, menuAnim, coinAnim, ctpAnim;
    public Text scoreText, coinText, modifierText, hiScoreText, hiScoreText2, savedCoinsText;
    private float score, coinScore, modifierScore, coinModifierScore, hiScore, savedCoins, storedCoins;
    private float oldModifierScore;
    private int lastScore;

    // Death menu
    public Animator deathMenuAnim;
    public Text DeadScoreText, DeadCoinText;

    void Start()
    {
        //  score = 0;
        StartCoroutine(spawnWaves());
        if (!disMusic)
        {
            FindObjectOfType<LS_AudioManager>().Play("Background Music");
            FindObjectOfType<LS_AudioManager>().Stop("DeathMusic");
        }
    }

    private void Update()
    {
        // Calls
        // removeOldCoins(15);
        hiScoreText.text = hiScoreText.text = "Highscore: " + PlayerPrefs.GetInt("Hiscore").ToString("0");
        savedCoinsText.text = savedCoinsText.text = "Coins: " + PlayerPrefs.GetInt("storedCoins").ToString("0");
        if (LS_MobileInput.Instance.Tap && !isGameStarted)
        {
            motor.GameBegin();
            isGameStarted = true;
            disMusic = true;
            FindObjectOfType<LS_BackgroundSpawner>().IsScrolling = true;
            FindObjectOfType<LS_AudioManager>().Play("GameStartClick");
            FindObjectOfType<CameraFollow>().IsMoving = true;
            gameCanvas.SetTrigger("Show");
            menuAnim.SetTrigger("Hide");
            ctpAnim.SetTrigger("Hide");
        }
        if (motor.gameBegin && !IsDead)
        {
            // Bump the score up
            score += (Time.deltaTime * modifierScore);
            scoreText.text = "Score: " + score.ToString("0s");
            lastScore = (int)score;
            if(lastScore != (int)score)
            {
                lastScore = (int)score;
                Debug.Log("LS_GameManager: Updating Score...");
                scoreText.text = "Score: " + score.ToString("0s");
            }
        }
    }
    /*
    public void removeOldCoins(float lifeTime)
    {

        if (isGameStarted)
        {
            foreach (GameObject go in CoinsLeft)
            {
                Destroy(go, lifeTime);
            }
        }
    }*/

    public IEnumerator PlayerEnhancementTEN()
    {
            oldModifierScore = modifierScore;
            modifierScore += 10;
            coinModifierScore += 10;
            modifierText.text = "x" + modifierScore.ToString();
            yield return new WaitForSecondsRealtime(15);
            modifierText.text = "x" + modifierScore.ToString();
            PlayerEnhancements = false;
    }

    public IEnumerator playerEnhancementTWENTY()
    {
            oldModifierScore = modifierScore;
            modifierScore += 20;
            coinModifierScore += 20;
            modifierText.text = "x" + modifierScore.ToString();
            yield return new WaitForSecondsRealtime(15);
            modifierText.text = "x" + modifierScore.ToString();
            PlayerEnhancements = false;
    }

    public IEnumerator playerEnhancementTHIRTY()
    {
            oldModifierScore = modifierScore;
            modifierScore += 30;
            coinModifierScore += 30;
            modifierText.text = "x" + modifierScore.ToString();
            yield return new WaitForSecondsRealtime(15);
            modifierText.text = "x" + modifierScore.ToString();
            PlayerEnhancements = false;
    }

    public IEnumerator playerEnhancementFOURTY()
    {
            oldModifierScore = modifierScore;
            modifierScore += 40;
            coinModifierScore += 40;
            modifierText.text = "x" + modifierScore.ToString();
            yield return new WaitForSecondsRealtime(15);
            modifierText.text = "x" + modifierScore.ToString();
            PlayerEnhancements = false;
    }

    public IEnumerator playerEnhancementFIFTY()
    {
            oldModifierScore = modifierScore;
            modifierScore += 50;
            coinModifierScore += 50;
            modifierText.text = "x" + modifierScore.ToString();
            yield return new WaitForSecondsRealtime(15);
            modifierText.text = "x" + modifierScore.ToString();
            PlayerEnhancements = false;
    }

    public void coinPickup()
    {
        coinAnim.SetTrigger("Initiate");
        FindObjectOfType<LS_AudioManager>().Play("CoinPickup");
        coinScore++;

        // Adds coins to score value
        scoreText.text = scoreText.text = "Coins: " + score.ToString("0");
        // Adds coins to coin value
        coinText.text = coinText.text = "Coins: " + savedCoins.ToString("0");
        
        // Calculate how much should be added to coin score
        if (PlayerEnhancements)
        {
            savedCoins += COIN_SCORE_AMOUNT + coinModifierScore + coinScore;

            /*
            // Assemble modifier bonus
            if(LS_PlayerEnhancements.instance.PEModifier == 10)
            {
                // Add modifier bonus to coin score
                savedCoins += COIN_SCORE_AMOUNT + coinScore + 10;
            }
            else if (LS_PlayerEnhancements.instance.PEModifier == 20)
            {
                // Add modifier bonus to coin score
                savedCoins += COIN_SCORE_AMOUNT + coinScore + 20;
            }
            else if (LS_PlayerEnhancements.instance.PEModifier == 30)
            {
                // Add modifier bonus to coin score
                savedCoins += COIN_SCORE_AMOUNT + coinScore + 30;
            }
            else if (LS_PlayerEnhancements.instance.PEModifier == 40)
            {
                // Add modifier bonus to coin score
                savedCoins += COIN_SCORE_AMOUNT + coinScore + 40;
            }
            else if (LS_PlayerEnhancements.instance.PEModifier == 50)
            {
                // Add modifier bonus to coin score
                savedCoins += COIN_SCORE_AMOUNT + coinScore + 50;
            }

                // Add modifier bonus to coin score
            //    savedCoins += COIN_SCORE_AMOUNT + coinScore +  LS_PlayerEnhancements.instance.PEModifier;*/
        }
        else
        {
            savedCoins += COIN_SCORE_AMOUNT + coinScore;
        }


        // Adds coins to coin value
        savedCoins += COIN_SCORE_AMOUNT + coinScore;
        coinText.text = coinText.text = "Coins: " + savedCoins.ToString("0");

        score += COIN_SCORE_AMOUNT;
        scoreText.text = scoreText.text = "Score: " + score.ToString("0s");
    }

    public void UpdateModifier(float modifierAmount)
    {
        if (PlayerEnhancements)
        {
            // $$ OBSOLETE
            // I don't even know what this line does to be honest. It works so let's leave it at that.
            LS_PlayerEnhancements.instance.PEModifier = LS_PlayerEnhancements.instance.PEModifier + 1.0f + modifierAmount + modifierScore;
            // $$
            modifierScore = LS_PlayerEnhancements.instance.PEModifier;
         //   coinModifierScore = LS_PlayerEnhancements.instance.PEModifier;
            modifierText.text = "x" + modifierScore.ToString("0.0");
        }
        else
        {
            modifierScore = 1.0f + modifierAmount;
            coinModifierScore = 1.0f;
            modifierText.text = "x" + modifierScore.ToString("0.0");
        }
    }

    public void OnPlayButton()
    {
        //Obsolete
        //    UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        FindObjectOfType<LS_AudioManager>().Play("SceneChanger");
        FindObjectOfType<LS_LevelChanger>().FadeToLevel(0);
    }

    public void OnShopButton()
    {
        disMusic = true;
        FindObjectOfType<LS_AudioManager>().Stop("Background Music");
        FindObjectOfType<LS_AudioManager>().Play("DeathMusic");
        FindObjectOfType<LS_AudioManager>().Play("SceneChanger");
        FindObjectOfType<LS_LevelChanger>().FadeToLevel(1);
    }

    public void OnDeath()
    {
        IsDead = true;
        gameCanvas.SetTrigger("Hide");
        FindObjectOfType<LS_BackgroundSpawner>().IsScrolling = false;
        FindObjectOfType<LS_AudioManager>().Play("DeathMusic");
        FindObjectOfType<LS_AudioManager>().Stop("Background Music");

        //Check if this run was a highscore
        CheckForHighscore();
        // Save amount of coins collected this run
        SaveCoinAmount();

        DeadScoreText.text = scoreText.text = "Highscore: " + PlayerPrefs.GetInt("Hiscore").ToString("0");
        hiScoreText2.text = hiScoreText2.text = "Highscore: " + PlayerPrefs.GetInt("Hiscore").ToString("0");
        savedCoinsText.text = savedCoinsText.text = "Coins: " + PlayerPrefs.GetInt("storedCoins").ToString("0");
        DeadCoinText.text = coinText.text = "Score: " + score.ToString("0");

        deathMenuAnim.SetTrigger("Dead");
    }

    public void CheckForHighscore()
    {
        // Check if this is a highscore
        if (score > PlayerPrefs.GetInt("Hiscore"))
        {
            /*
            float s = score;
            if (s % 1 == 0)
                s += 1;*/
            PlayerPrefs.SetInt("Hiscore", (int)score);
         //   PlayerPrefs.Save();
        }
    }
    
    public void SaveCoinAmount()
    {
        // Sets coin amount
        // If some how coin amount is float, changes it to int
         /*   float s = savedCoins;
            if (s % 1 == 0)
                s += 1;*/
            PlayerPrefs.SetInt("storedCoins", PlayerPrefs.GetInt("storedCoins", 0) + (int)savedCoins);
         //   PlayerPrefs.Save();
    }

    void Awake()
    {
        modifierText.text = "x" + modifierScore.ToString("0.0");
        scoreText.text = scoreText.text = "Score: " + score.ToString("0");
        coinText.text = coinText.text = "Coins: " + coinScore.ToString("0s");

        if (instance != null)
        {
            Debug.Log("LS_GameManager: More than one 'Game Manager' in scene(s).");
        } else
        {
            instance = this;
        }

        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<LS_PlayerMotor>();
    }

    IEnumerator spawnWaves()
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


    #region Player tracking

    private const string PLAYER_ID_PREFIX = "Player: ID(";

    private static Dictionary<string, LS_PlayerManager> players = new Dictionary<string, LS_PlayerManager>();

    public static void RegisterPlayer(string _netID, LS_PlayerManager _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID + ")";
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }

    public static void UnRegisterPlayer(string _playerID)
    {
        players.Remove(_playerID);
    }

    public static LS_PlayerManager GetPlayer(string _playerID)
    {
        return players[_playerID];
    }

    /*
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical();

        foreach(string _playerID in players.Keys)
        {
            GUILayout.Label(_playerID + "  -  " + players[_playerID].transform.name);
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }*/
    
    #endregion
}
