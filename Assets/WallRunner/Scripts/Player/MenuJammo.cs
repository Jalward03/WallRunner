using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallRunner
{


    public class MenuJammo : MonoBehaviour
    {

        void Update()
        {
            // Handles player continuously running on the menu screen
            gameObject.transform.position += new Vector3(100f * Time.deltaTime, 0, 0);

            if(gameObject.transform.position.x > 600)
            {
                gameObject.transform.position = new Vector3(303, 224, -345);
            }
        }
    }
}
