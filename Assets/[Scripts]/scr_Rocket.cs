/* Sourcefile:      scr_Rocket.cs
 * Author:          Sam Pollock
 * Student Number:  101279608
 * Last Modified:   October 24th, 2021
 * Description:     Bonus points rocket for gameplay
 * Last edit:       Set up patrolling behaviour.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Rocket : MonoBehaviour
{

    public float movementSpeed;
    public int remainingLaps;
    public int scoreValue;

    private float currentSpeed;

    void Start()
    {
        currentSpeed = movementSpeed;
    }


    void Update()
    {
        MoveRocket();
    }

    /// <summary>
    /// Moves rocket according to speed, flips it when it reaches the ends of the screen. 
    /// </summary>
    void MoveRocket()
    {
        transform.Translate(currentSpeed * Time.deltaTime, 0f, 0f);

        if (transform.position.x > 4)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(4, transform.position.y, transform.position.z);
            currentSpeed = -movementSpeed;
            remainingLaps--;
        }

        if (transform.position.x < -4)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(-4, transform.position.y, transform.position.z);
            currentSpeed = movementSpeed;
            remainingLaps--;
        }

        // Destroys the rocket if it has crossed the screen a certain number of times
        if (remainingLaps <= 0)
        {
            Destroy(gameObject);
        }

    }

    /// <summary>
    /// Checks for collisions with Power Shot to add player scores
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<scr_PowerShot>() != null)
        {
            GameObject.Find("Player").GetComponent<scr_Player>().AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
