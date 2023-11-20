using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

namespace WallRunner
{
	public class Score : MonoBehaviour
	{
	

		private TextMeshProUGUI value;
		private float score;
		public float finalScore;

		void Start()
		{
			value = GetComponent<TextMeshProUGUI>();
		}

		
		void Update()
		{
			// Converting a float to a string with 0 decimal places
			value.text = score.ToString("0");
			score += 10f * Time.deltaTime;
		}

		/// <summary>Gets the current score</summary>
		/// <returns>float</returns>
		public float GetScore() => score;
	}
}