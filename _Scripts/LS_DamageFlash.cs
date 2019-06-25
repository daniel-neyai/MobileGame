using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LS_DamageFlash : MonoBehaviour {

    public float timeAmount = 0.1f;

    public Color defaultColor = Color.white;
    public Color flashingColor = Color.red;

    private bool isFlashing = false;
    public bool Flash = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            isFlashing = true;
        }
        else
        {
            isFlashing = false;
        }

        if (isFlashing)
        {
            Flash = true;

            if (Flash)
            {
                StartCoroutine(FlashObject(timeAmount, defaultColor, flashingColor));
            }
        }
        else
        {
            Flash = false;
        }
    }

    // Deprecated
    public void FlashSprite(float flashTime, Color defaultColor, Color flashingColor)
    {

        if (flashingColor == null)
            FlashObject(1f, Color.red, Color.black);

        GetComponent<SpriteRenderer>().color = flashingColor;
        for (int i = 0; i < flashTime; i++)
        {
            if (i >= flashTime)
            {
                GetComponent<SpriteRenderer>().color = defaultColor;
            }
        }
    }

    public IEnumerator FlashObject(float flashTime, Color defaultColor, Color flashingColor)
    {
        GetComponent<Renderer>().material.color = flashingColor;
        yield return new WaitForSeconds(timeAmount);
        GetComponent<Renderer>().material.color = defaultColor;
        //   return flashTime;
    }
}
