/* Sourcefile:      scr_BounceShotRB.cs
 * Author:          Sam Pollock
 * Student Number:  101279608
 * Last Modified:   October 24th, 2021
 * Description:     Bouncing Battery Balls for Gameplay.
 * Last edit:       Added battery effects. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_BounceshotRB : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    private scr_Player playerScript;

    public float batteryPerBounce;


    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<scr_Player>();
        batteryPerBounce = playerScript.batteryPerBounce;

    }

    /// <summary>
    /// Sets initial force of Bounceshots.
    /// </summary>
    /// <param name="rightForce"></param>
    /// <param name="upForce"></param>
    public void Launch(float rightForce, float upForce)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(rightForce, upForce));
    }

    /// <summary>
    /// Checks for collisions against walls and Enemy. 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<scr_Enemy>() != null)
        {
            playerScript.shotBattery += batteryPerBounce;
        }
        else if (collision.gameObject.tag == "Walls")
        {
            scr_SoundEffectsManager.SFXManager.PlaySoundEffect(3);

        }
    }

}
