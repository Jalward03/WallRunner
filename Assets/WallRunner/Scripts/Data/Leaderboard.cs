using System;
using System.Collections;
using System.Collections.Generic;using TMPro;
using UnityEngine;


namespace WallRunner
{


    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreField;
        [SerializeField] private int leaderBoardPosition;
        
        
        // Assigns the score to the leaderboard position
        private void Start()
        {
            scoreField.text = PlayerPrefs.GetFloat($"score{leaderBoardPosition}", 0).ToString("0");
        }

        // Clears leaderboard
        private void Update()
        {
            if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Tab))
            {
                PlayerPrefs.DeleteAll();
            }
        }
    }
}
