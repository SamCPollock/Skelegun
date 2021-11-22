/* Sourcefile:      scr_PowerShot.cs
 * Author:          Sam Pollock
 * Student Number:  101279608
 * Last Modified:   October 24th, 2021
 * Description:     Powerful shot from the player to destroy enemies
 * Last edit:       Added sound effect
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PowerShot : MonoBehaviour
{
    public float speed;
    public int damage;



    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
    }

    /// <summary>
    /// Checks for collisions with Enemies and rockets, to destroy them.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<scr_Enemy>() != null)
        {
            collision.gameObject.GetComponent<scr_Enemy>().health -= damage;
            Explode();
        }
        if (collision.gameObject.GetComponent<scr_Rocket>() != null)
        {
            Explode();
        }
        
    }

    /// <summary>
    ///  Destroys self.
    /// </summary>
    public void Explode()
    {
        Destroy(gameObject);
    }
}

