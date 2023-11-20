using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography;

using TMPro;

using UnityEngine;

using Random = UnityEngine.Random;

namespace WallRunner
{
	public class WorldSpawner : MonoBehaviour
	{
		[SerializeField] private GameObject[] obstacles;

		[SerializeField] private List<GameObject> used = new List<GameObject>();
		[SerializeField] private List<GameObject> unUsed = new List<GameObject>();
		[SerializeField] private Score score;

		private float startSpeed = 0.01f;


		private void Start()
		{
			// Spawns 15
			//
			//
			//
			//
			//
			//
			//
			//
			//
			//
			//
			//
			//
			//
			//
			// randomly selected object prefabs and added to a list
			for(int i = 0; i < 6; i++)
			{
				int random = Random.Range(0, obstacles.Length);

				unUsed.Add(Instantiate(obstacles[random], new Vector3(0, -50, 0), Quaternion.identity));
				unUsed.Add(Instantiate(obstacles[random], new Vector3(0, -50, 0), Quaternion.identity));
				unUsed.Add(Instantiate(obstacles[i], new Vector3(0, -50, 0), Quaternion.identity));
			}
			
			
			
			// Setting all the objects in the unused list to false 
			for(int i = 0; i < unUsed.Count; i++)
			{
				unUsed[i].SetActive(false);
			}


			// Spawns the first 4 obstacle prefabs to the game and removed from unused list and added to used list. Sets all the used objects to true
			for(int i = 0; i < 5; i++)
			{
				used.Add(unUsed[i]);
				unUsed.Remove(unUsed[i]);

				used[i].SetActive(true);
				used[i].transform.position = new Vector3(0, 0, (1 + i) * 28);
			}
		}

		
		private void Update()
		{
			// Moves all the used object prefabs and when it goes behind the player, it is put back into unused, and then moves a new one from unused into used, and spawns it in the back 
			for(int i = 0; i < used.Count; i++)
			{
				if(Time.timeScale == 0)
				{
					startSpeed = 0;
				}
				used[i].transform.position -= new Vector3(0, 0, startSpeed + Time.deltaTime * score.GetScore() * 0.025f);

				if(used[i].transform.position.z < -28)
				{
					unUsed.Add(used[i]);
					used.Remove(used[i]);

					foreach(GameObject _unUsed in unUsed)
					{
						_unUsed.SetActive(false);
					}

					int tempRand = Random.Range(0, unUsed.Count - 1);
					used.Add(unUsed[tempRand]);
					unUsed[tempRand].transform.position = new Vector3(0, 0, 112);
					foreach(GameObject _used in used)
					{
						_used.SetActive(true);
					}

					unUsed.Remove(unUsed[tempRand]);
				}
			}
		}
	}
}