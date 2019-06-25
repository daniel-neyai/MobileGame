using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LS_CoinPickup : MonoBehaviour {

    public GameObject Coin;
    public GameObject PlayerCoinPickupEffect;
    public Animator CoinAnimator;
    private float time = 0.5f;

    private void Awake()
    {
        CoinAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        CoinAnimator.SetTrigger("Spawn");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            LS_GameManager.instance.coinPickup();
            CoinAnimator.SetTrigger("CoinIsPickedUp");
            Instantiate(PlayerCoinPickupEffect, gameObject.transform.position, Quaternion.identity);
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

}
