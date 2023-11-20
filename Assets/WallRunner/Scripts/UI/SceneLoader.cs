using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

using UnityEngine.SceneManagement;

namespace WallRunner
{
	public class SceneLoader : MonoBehaviour
	{
		/// <summary> Allows button on click events to change scenes </summary>
		public void LoadScene(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}

		/// <summary> Handles closing the game on different platforms </summary>
		public void QuitGame()
		{
			#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
			#else
	    Application.Quit();
			#endif
		}
	}
}