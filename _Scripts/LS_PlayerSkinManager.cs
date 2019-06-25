using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LS_PlayerSkinManager : MonoBehaviour
{
    [SerializeField]
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
    [SerializeField]
    private Material Basketball, Finland, France, Golfball, Rainbow, Romania;

    private Material[] playerMaterials;
    private int playerInt;

    public void Awake()
    {
        // Setup default player material
        // TO DO: Save player's latest material and load it here.
        playerInt = PlayerPrefs.GetInt("playerInt");
    }
    private void Start()
    {
        InstantiateMaterial();
    }

    public void InitializeNumber(int integer)
    {
        playerInt = integer;
        PlayerPrefs.SetInt("playerInt", integer);
    }

    public void SaveNumber()
    {
        // Sets coin amount
        // If some how coin amount is float, changes it to int
        /*   float s = savedCoins;
           if (s % 1 == 0)
               s += 1;*/
        PlayerPrefs.SetInt("playerInt", (int)playerInt);
        //   PlayerPrefs.Save();
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
            case 6:
                player.GetComponent<Renderer>().material = Basketball;
                break;
            case 7:
                player.GetComponent<Renderer>().material = Finland;
                break;
            case 8:
                player.GetComponent<Renderer>().material = France;
                break;
            case 9:
                player.GetComponent<Renderer>().material = Romania;
                break;
            case 10:
                player.GetComponent<Renderer>().material = Golfball;
                break;
            case 11:
                player.GetComponent<Renderer>().material = Rainbow;
                break;
            default:
                player.GetComponent<Renderer>().material = orange;
                break;
        }
    }
}
