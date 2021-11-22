/* Sourcefile:      scr_Enemy.cs
 * Author:          Sam Pollock
 * Student Number:  101279608
 * Last Modified:   October 24th, 2021
 * Description:     Alien enemy type for gameplay
 * Last edit:       Added sound effects
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Enemy : MonoBehaviour
{

    public int health;
    public float shotCooldown = 2;
    public GameObject enemyShotPrefab;
    public int scoreValue = 50;

    private Rigidbody2D rb;
    private float timeUntilNextShot;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(100, -600));
    }

    void Update()
    {
        Movement();
        AutomaticShooting();

        // Enemy dies if health depleted.
        if (health <= 0)
        {
            scr_SoundEffectsManager.SFXManager.PlaySoundEffect(7);
            GameObject.Find("Player").GetComponent<scr_Player>().AddScore(scoreValue);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Move random direction when current speed is low enough
    /// </summary>
    void Movement()
    {
        if (rb.velocity.magnitude <= 0.1f)
        {
            float randomHorizontal = Random.Range(-500f, 500f);
            float randomVertical = Random.Range(-500f, 500f);

            rb.AddForce(new Vector2(randomHorizontal, randomVertical));

        }

    }

    /// <summary>
    /// Shoot when shot timer is ready.
    /// </summary>
    void AutomaticShooting()
    {
        if (Time.time > timeUntilNextShot)
        {
            Shoot();
        }
    }

    /// <summary>
    /// Create shots
    /// </summary>
    void Shoot()
    {
        GameObject shot = Instantiate(enemyShotPrefab, transform.position, Quaternion.identity);
        shot.transform.localScale = new Vector3(2, -2, 1);
        timeUntilNextShot = Time.time + shotCooldown;
    }

    private void FixedUpdate()
    {
        // Slow enemy speed at a fixed rate. 
        rb.velocity *= 0.99f;

    }

    /// <summary>
    /// Check for collision with Bounceshot to play sound effect
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<scr_BounceshotRB>() != null)
        {
            //health--;
            scr_SoundEffectsManager.SFXManager.PlaySoundEffect(2);
        }

    }
}
