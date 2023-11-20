using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

namespace WallRunner
{
	public class ScoreOnDeath : MonoBehaviour
	{
		private TextMeshProUGUI value;


		void Start()
		{
			// Gets the final score and destroys the player
			PlayerController player = FindObjectOfType<PlayerController>();

			value = GetComponent<TextMeshProUGUI>();
			SaveScores();
			value.text = player.score.finalScore.ToString("0");
			Destroy(player.gameObject);
		}


		/// <summary>Saves the score if it is high enough for the leaderboard</summary>
		private void SaveScores()
		{
			PlayerController player = FindObjectOfType<PlayerController>();
			float score = player.score.finalScore;
			for(int i = 1; i < 11; i++)
			{
				float temp = PlayerPrefs.GetFloat($"score{i}", 0);
				

				if(temp < score)
				{
					PlayerPrefs.SetFloat($"score{i}", score);
					score = temp;
					
					
				}
			}
			PlayerPrefs.Save();

		
		}
	}
}