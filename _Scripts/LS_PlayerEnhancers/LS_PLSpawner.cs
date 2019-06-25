using UnityEngine;

public class LS_PLSpawner : MonoBehaviour {

    public int maxEnhancements = 5; // Maximum amount of coins allowed to spawn. ("Example: Spawns 5 coins on top of a longblock");
    public float chanceToSpawn = 0.5f;
    public bool forceSpawnAll = false; // While (true), spawns all the coins. While (false) spawns the coins by chance.

    private float offsetX = 0.0f;
    private float offsetY = 1.0f;

    private GameObject[] playerEnhancements;

    private void Awake()
    {
        playerEnhancements = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            playerEnhancements[i] = transform.GetChild(i).gameObject;

        OnDisable();
    }

    private void OnEnable()
    {
        if (Random.Range(offsetX, offsetY) > chanceToSpawn)
            return;

        if(forceSpawnAll)
        {
            for (int i = 0; i < maxEnhancements; i++)
            {
                playerEnhancements[i].SetActive(true);
            }
        }
        else
        {
            int r = Random.Range(0, maxEnhancements);
            for (int i = 0; i < r; i++)
            {
                playerEnhancements[i].SetActive(true);
            }
        }

    }

    private void OnDisable()
    {

        // Disables coins
        foreach (GameObject go in playerEnhancements)
            go.SetActive(false);
    }
}
