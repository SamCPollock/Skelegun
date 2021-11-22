/* Sourcefile:      scr_HollowCircle.cs
 * Author:          Sam Pollock
 * Student Number:  101279608
 * Last Modified:   October 24th, 2021
 * Description:     Circle used to indicate power shot charging
 * Last edit:       Created script
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_HollowCircle : MonoBehaviour
{
    void Start()
    {
        // Automatically destroy self after a set amount of time.
        Invoke("DestroySelf", 2);
    }



    private void FixedUpdate()
    {
        // Shrink the Hollow Circle. Shrinking is done sort of oblong because it looks cool.
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 0.978f, gameObject.transform.localScale.y * 0.97f, 0);

    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
