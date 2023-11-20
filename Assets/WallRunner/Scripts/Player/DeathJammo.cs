using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallRunner
{
    

public class DeathJammo : MonoBehaviour
{
    

   
    void Update()
    {
        // Rotates player on the death screen
        gameObject.transform.Rotate(Vector3.up, 1f);
    }
}
}
