using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace WallRunner
{


    public class PauseMenu : MonoBehaviour
    {

        [SerializeField] private GameObject menu;
        
        // Hides the pause menu
        private void Start()
        {
            menu.SetActive(false);
        }
        
        // Pauses on escape
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                OnPause();
            }
        }

        /// <summary>Popup menu becomes active and time freezes</summary>
        public void OnPause()
        {
            menu.SetActive(true);
            Time.timeScale = 0;
        }

        /// <summary>Resumes time and hides the pause menu</summary>
        public void OnResume()
        {
            Time.timeScale = 1;
            menu.SetActive(false);
            
        }

        /// <summary>Resumes time and loads the menu scene</summary>
        public void OnMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Menu");
        }
    }
}
