/* Sourcefile:      scr_EnemyShot.cs
 * Author:          Sam Pollock
 * Student Number:  101279608
 * Last Modified:   October 24th, 2021
 * Description:     Enemy projectile falls from top to bottom.
 * Last edit:       Created script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_EnemyShot : MonoBehaviour
{

    public float fallSpeed; 

    /// <summary>
    /// Move the projectile according to fallspeed
    /// </summary>
    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - fallSpeed, transform.position.z);
    }
}
