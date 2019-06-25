using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LS_DestroyAfterLifetime : MonoBehaviour {

    private void Start()
    {
        Destruct(8.5f);
    }

    void Destruct(float lifetime)
    {
        Destroy(gameObject, lifetime);
    }

}
