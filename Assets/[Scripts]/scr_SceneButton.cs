/*
/* Sourcefile:      scr_SceneButton.cs
 * Author:          Sam Pollock
 * Student Number:  101279608
 * Last Modified:   October 20th, 2021
 * Description:     Handles scene changing on button press
 * Last edit:       Created script
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_SceneButton : MonoBehaviour
{
    /// <summary>
    /// Loads up a scene according to an index passed to the function.
    /// </summary>
    /// <param name="targetSceneIndex"></param>
    public void OnSceneChangeButtonPressed(int targetSceneIndex)
    {
        SceneManager.LoadScene(targetSceneIndex);
    }
}
