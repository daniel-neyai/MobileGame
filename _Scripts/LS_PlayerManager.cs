using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LS_PlayerManager : MonoBehaviour {
    //Optimized for multiplayer(Removed for now)
    // [SyncVar]
    [SerializeField]
    private Animator anim;
    public GameObject player;
    [SerializeField]
    private Material orange;
    [SerializeField]
    private Material red;
    [SerializeField]
    private Material blue;
    [SerializeField]
    private Material green;
    [SerializeField]
    private Material pink;

    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField]
    private int maxHealth = 100;

 //   [SyncVar]
    private int currentHealth;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;
    private Material[] playerMaterials;
    private int playerInt;

    public void Awake()
    {
        // Setup default player material
        // TO DO: Save player's latest material and load it here.
        playerInt = 1;
    }

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }
        SetDefaults();
    }

    public void InitializeNumber(int integer)
    {
        playerInt = integer;
    }

    public void InstantiateMaterial()
    {
        switch (playerInt)
        {
            case 1:
                player.GetComponent<Renderer>().material = orange;
                break;
            case 2:
                player.GetComponent<Renderer>().material = red;
                break;
            case 3:
                player.GetComponent<Renderer>().material = blue;
                break;
            case 4:
                player.GetComponent<Renderer>().material = green;
                break;
            case 5:
                player.GetComponent<Renderer>().material = pink;
                break;
            default:
                player.GetComponent<Renderer>().material = orange;
                break;
        }
    }

 //   [ClientRpc]
    public void RpcTakeDamage (int _amount)
    {
        if (isDead)
            return;

        currentHealth -= _amount;

        Debug.Log("LS_PlayerManager: " + transform.name + " now has" + currentHealth + " health.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        anim.SetTrigger("Dead");
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = false;

        Debug.Log("LS_PlayerManager: " + transform.name + " is DEAD!");

        // Needs if statement to check for hearts left.
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(LS_GameManager.instance.respawnTime);

        SetDefaults();
        Transform _spawnpoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnpoint.position;
        transform.rotation = _spawnpoint.rotation;

        Debug.Log("LS_PlayerManager: " + transform.name + " Respawned.");
    }

    public void SetDefaults()
    {
        isDead = false;

        currentHealth = maxHealth;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = true;
    }


}
