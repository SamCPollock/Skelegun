/* Sourcefile:      scr_ScrollingBG.cs
 * Author:          Sam Pollock
 * Student Number:  101279608
 * Last Modified:   October 24th, 2021
 * Description:     Moves background vertically
 * Last edit:       Set up repositioning when reaching botttom of screen.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ScrollingBG : MonoBehaviour
{

    public float scrollSpeed; 

    /// <summary>
    /// Updating the position according to speed. Moves the assets to above the screen after it leaves the screen.
    /// </summary>
    private void FixedUpdate()
    {
        gameObject.transform.Translate(new Vector3(0, -scrollSpeed, 0f));

        if (transform.position.y <= -7.6f)
        {
            transform.position = new Vector3(transform.position.x, 7.6f, 1f);
        }
    }
}
