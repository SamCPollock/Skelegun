/* Sourcefile:      scr_Player.cs
 * Author:          Sam Pollock
 * Student Number:  101279608
 * Last Modified:   October 24th, 2021
 * Description:     Player ship for gameplay.
 * Last edit:       Added touch controls
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class scr_Player : MonoBehaviour
{

    [Header("Movement")]
    public float movementSpeed;
    public float chargingMovementSpeed;
    public float normalMovementSpeed;
    public float shotAngle = 30.0f;

    [Header("Powershots")]
    public float powerShotChargeTime;

    [Header("BatteryShots")]
    public bool isAutoShooting;
    public float shotBattery;
    public float shotBatteryMax;
    public float shotCost;
    public float batteryRechargeRate;
    public float batteryPerBounce;

    [Header("Scoring")]
    public int lives = 3;
    public int score = 0;

    [Header("Prefabs")]
    public GameObject bounceShotRBPrefab;
    public GameObject bounceShotPrefab;
    public GameObject powerShotPrefab;
    public GameObject hollowCirclePrefab;
    public GameObject scoreText;
    public GameObject batterySliderRef;


    private float horBounds = 2f;
    private float verBounds = 4.6f;
    private Vector3 shotOffsetLeft = new Vector3(-0.3f, 0f, 0f);
    private Vector3 shotOffsetRight = new Vector3(0.3f, 0f, 0f);
    private TextMeshProUGUI scoreTMP;
    private Slider batterySliderUI;

    private Vector3 moveTarget;


    void Start()
    {
        moveTarget = gameObject.transform.position;
        scoreTMP = scoreText.GetComponent<TextMeshProUGUI>();
        batterySliderUI = batterySliderRef.GetComponent<Slider>();
    }

    void Update()
    {
        TouchMovement();
        KeyboardMovement();
        CheckBounds();
        GenerateBattery();

    }

    /// <summary>
    /// Keyboard movement used for testing on desktop
    /// </summary>
    void KeyboardMovement()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        transform.position += new Vector3(hInput * movementSpeed * Time.deltaTime, vInput * movementSpeed * Time.deltaTime, 0f);


        if (Input.GetKeyDown("space"))
        {
            StartCharging();
        }

        if (Input.GetKeyUp("space"))
        {
            ReleaseCharging();
        }

    }

    /// <summary>
    /// Touch movement for use on mobile. Moves towards touch position.
    /// </summary>
    void TouchMovement()
    {
        if (Input.touchCount > 0)
        {
            moveTarget = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

            float xDistance = gameObject.transform.position.x - moveTarget.x;
            float yDistance = gameObject.transform.position.y - moveTarget.y;


            float step = movementSpeed * Time.deltaTime;

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, moveTarget, step);
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

            // Launching bullets automatically according to current battery
            if (isAutoShooting && shotBattery >= shotCost)
            {
                LaunchBatteryShots();
            }

        }

  



        #region Virtual Joystick
        ////Virtual Joystick -- RETIRED DUE TO JANKINESS, preserved for reference.
        //float horizontalInput = 0;
        //float verticalInput = 0;

        //if (Input.touchCount > 0)
        //{
        //    if (Input.GetTouch(0).phase == TouchPhase.Began)
        //    {
        //        touchStartPos = Input.touches[0].position;
        //        Debug.Log("TouchSTARTPOS: " + touchStartPos);
        //    }

        //    if (Input.GetTouch(0).phase == TouchPhase.Moved)
        //    {
        //        touchMovePos = Input.touches[0].position;
        //        Debug.Log("TouchMOVEPOS: " + touchMovePos);

        //            horizontalInput = (touchMovePos.x - touchStartPos.x ) * 0.01f;
        //            Debug.Log(touchMovePos.x + " - " + touchStartPos.x + " = " + (touchMovePos.x - touchStartPos.x));
        //        if (horizontalInput > 10)
        //            horizontalInput = 10;
        //        else if (horizontalInput < -10)
        //            horizontalInput = -10;
        //        transform.position += new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, 0f);

        //    }

        //}
        #endregion




    }

    /// <summary>
    /// Checks bounds against screen size to ensure player stays on screen.
    /// </summary>
    void CheckBounds()
    {
        // Check horizontal bounds
        if (transform.position.x > horBounds)
        {
            transform.position = new Vector3(horBounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -horBounds)
        {
            transform.position = new Vector3(-horBounds, transform.position.y, transform.position.z);
        }

        // Check vertical bounds
        if (transform.position.y > verBounds)
        {
            transform.position = new Vector3(transform.position.x, verBounds, transform.position.z);
        }
        else if (transform.position.y < -verBounds)
        {
            transform.position = new Vector3(transform.position.x, -verBounds, transform.position.z);
        }
    }

    /// <summary>
    /// Checking for collision against BounceShot and enemy projectiles
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Bounce shot caught
        if (collision.gameObject.GetComponent<scr_BounceshotRB>() != null)
        {
            scr_SoundEffectsManager.SFXManager.PlaySoundEffect(0);
            Destroy(collision.gameObject);
            PowerShot();
        }
        // Hit by enemy projectile 
        if (collision.gameObject.GetComponent<scr_EnemyShot>() != null)
        {
            Destroy(collision.gameObject);
            LoseLife();
        }
    }

    /// <summary>
    /// Slows player while charging a shot (Used in Keyboard controls)
    /// </summary>
    void StartCharging()
    {
        movementSpeed = chargingMovementSpeed;
    }

    /// <summary>
    /// Launches shots when releasing button. (Used in keyboard controls)
    /// </summary>
    void ReleaseCharging()
    {

        movementSpeed = normalMovementSpeed;

        if (shotBattery >= shotCost)
        {
            //Instantiate(bounceShotPrefab, gameObject.transform.position + shotOffsetLeft, Quaternion.Euler(0f, 0f, shotAngle));
            //Instantiate(bounceShotPrefab, gameObject.transform.position + shotOffsetRight, Quaternion.Euler(0f, 0f, -shotAngle));
            LaunchBatteryShots();
        }
    }

    /// <summary>
    /// Instantiate shots and set their speed.
    /// </summary>
    void LaunchBatteryShots()
    {
        scr_SoundEffectsManager.SFXManager.PlaySoundEffect(1);
        GameObject rightShot = Instantiate(bounceShotRBPrefab, gameObject.transform.position + shotOffsetLeft, Quaternion.identity) as GameObject;
        GameObject leftShot = Instantiate(bounceShotRBPrefab, gameObject.transform.position + shotOffsetRight, Quaternion.identity) as GameObject;

        leftShot.GetComponent<scr_BounceshotRB>().Launch(200, 200);
        rightShot.GetComponent<scr_BounceshotRB>().Launch(-200, 200);
        shotBattery -= shotCost;
    }

    /// <summary>
    /// Create a shrinking circle to indicate charge time, then instantiate a power shot at end of charge time.
    /// </summary>
    void PowerShot()
    {
        scr_SoundEffectsManager.SFXManager.PlaySoundEffect(5);
        GameObject hollowCircle = Instantiate(hollowCirclePrefab, gameObject.transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity) as GameObject;
        hollowCircle.transform.SetParent(gameObject.transform);
        Invoke("LaunchPowerShot", powerShotChargeTime);
    }

    /// <summary>
    /// Create a power shot
    /// </summary>
    void LaunchPowerShot()
    {
        GameObject powerShot = Instantiate(powerShotPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
    }

    /// <summary>
    /// Automatic passive battery generation
    /// </summary>
    void GenerateBattery()
    {
        if (shotBattery < shotBatteryMax)
        {
            shotBattery += batteryRechargeRate * Time.deltaTime;
        }

        if (shotBattery > shotBatteryMax)
        {
            shotBattery = shotBatteryMax;
        }

        // update the battery UI element 
        batterySliderUI.value = shotBattery;
        
    }

    /// <summary>
    /// Increase score. Called by enemies.
    /// </summary>
    /// <param name="scoreToAdd"></param>
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreTMP.text = score.ToString();
    }

    /// <summary>
    /// Losing life. Called when hit by projectiles. Plays sound, updates UI, and moves to game over if out of lives. 
    /// </summary>
    public void LoseLife()
    {

        lives--;
        scr_SoundEffectsManager.SFXManager.PlaySoundEffect(6);



        Debug.Log("LIVES REMAINING: " + lives);
        if (lives == 2)
        {
            Destroy(GameObject.Find("Lives3"));
        }
        if (lives == 1)
        {
            Destroy(GameObject.Find("Lives2"));
        }
        if (lives <= 0)
        {
            Destroy(GameObject.Find("Lives1"));
            GameOver();

        }

    }

    /// <summary>
    /// Updates scores and loads Game over screen
    /// </summary>
    void GameOver()
    {
        if (!PlayerPrefs.HasKey("Highscore"))
        {
            PlayerPrefs.SetInt("Highscsore", 0);
        }


        PlayerPrefs.SetInt("Score", score);

        if (PlayerPrefs.GetInt("Highscore") < score)
        {
            PlayerPrefs.SetInt("Highscore", score);
            Debug.Log("NEW HIGH SCORE OF: " + PlayerPrefs.GetInt("Highscore"));
        }
        SceneManager.LoadScene(3);
    }

}
