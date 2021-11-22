/* Sourcefile:      scr_ScoreDispla.cs
 * Author:          Sam Pollock
 * Student Number:  101279608
 * Last Modified:   October 24th, 2021
 * Description:     Sets the text for score
 * Last edit:       Created script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class scr_ScoreDisplay : MonoBehaviour
{
    public string playerPrefKey;

    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt(playerPrefKey).ToString();
    }


}
