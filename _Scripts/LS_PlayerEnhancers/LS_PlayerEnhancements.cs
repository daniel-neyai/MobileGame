using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LS_PlayerEnhancements : MonoBehaviour {

    public static LS_PlayerEnhancements instance;

    private int randomSelect;
    private float TimePer = 15f;

    public float PEModifier;

    private void Awake()
    {
        randomSelect = Random.Range(1, 5);
        Debug.Log(":: LS_PlayerEnhancements :: randomSelect has Selected the Number " + randomSelect);
    }

    private void Start()
    {
        AssembleModifier();
    }

    public void AssembleModifier()
    {
        PEModifier = randomSelect;

        if (PEModifier == 1)
            PEModifier += 9;
        else if (PEModifier == 2)
            PEModifier += 18;
        else if (PEModifier == 3)
            PEModifier += 27;
        else if (PEModifier == 4)
            PEModifier += 36;
        else if (PEModifier == 5)
            PEModifier += 45;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && randomSelect == 1)
        {
            LS_GameManager.instance.PlayerEnhancements = true;
            //  LS_GameManager.instance.playerEnhancementFIFTY();
            StartCoroutine(FindObjectOfType<LS_GameManager>().PlayerEnhancementTEN());
            Debug.Log(":: LS_PlayerEnhancements :: 1 SELECTED");
        }
        else if (other.tag == "Player" && randomSelect == 2)
        {
            LS_GameManager.instance.PlayerEnhancements = true;
            //  LS_GameManager.instance.playerEnhancementTWENTY();
            StartCoroutine(FindObjectOfType<LS_GameManager>().playerEnhancementTWENTY());
            Debug.Log(":: LS_PlayerEnhancements :: 2 SELECTED");
        }
        else if (other.tag == "Player" && randomSelect == 3)
        {
            LS_GameManager.instance.PlayerEnhancements = true;
            //   LS_GameManager.instance.playerEnhancementTHIRTY();
            StartCoroutine(FindObjectOfType<LS_GameManager>().playerEnhancementTHIRTY());
            Debug.Log(":: LS_PlayerEnhancements :: 3 SELECTED");
        }
        else if (other.tag == "Player" && randomSelect == 4)
        {
            LS_GameManager.instance.PlayerEnhancements = true;
            //   LS_GameManager.instance.playerEnhancementFOURTY();
            StartCoroutine(FindObjectOfType<LS_GameManager>().playerEnhancementFOURTY());
            Debug.Log(":: LS_PlayerEnhancements :: 4 SELECTED");
        }
        else if (other.tag == "Player" && randomSelect == 5)
        {
            LS_GameManager.instance.PlayerEnhancements = true;
            //   LS_GameManager.instance.playerEnhancementFIFTY();
            StartCoroutine(FindObjectOfType<LS_GameManager>().playerEnhancementFIFTY());
            Debug.Log(":: LS_PlayerEnhancements :: 5 SELECTED");
        }
    }

}
