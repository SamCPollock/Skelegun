using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Sourcefile:      scr_DestroyIfOffscreen.cs
 * Author:          Sam Pollock
 * Student Number:  101279608
 * Last Modified:   October 24th, 2021
 * Description:     Checks position and destroys gameobject if it leaves screen top or bottom.
 * Last edit:       Created script.
 */
public class scr_DestroyIfOffscreen : MonoBehaviour
{
    private float screenHeight = 5.0f;


    void Update()
    {
        if (gameObject.transform.position.y > screenHeight || gameObject.transform.position.y < -screenHeight)
        {
            Destroy(gameObject);
        }
    }
}
