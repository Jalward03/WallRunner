using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallRunner
{
    

public class EmptyWallsMover : MonoBehaviour
{

    [SerializeField] private Score score;

    private float startSpeed = 0.01f;

    void Update()
    {
        if(Time.timeScale == 0)
        {
            startSpeed = 0;
        }
        // Moves the first part of the world with no obstacles
        gameObject.transform.position -= new Vector3(0, 0, startSpeed + Time.deltaTime * score.GetScore() * 0.025f);

        
        // Destroying part of world that gets behind player
        if(gameObject.transform.position.z < -27)
        {
            Destroy(gameObject);
        }
    }
}
}