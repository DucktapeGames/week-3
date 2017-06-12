using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour {


	void Awake(){
		Exit.loadEnding += LoadEnding; 
	}

	public void LoadTutorialRoom(){
		SceneManager.LoadScene (1); 
	}

	public void LoadEnding(){
		SceneManager.LoadScene (2); 
	}

	public void LoadMainMenu(){
		SceneManager.LoadScene (0); 
	}

}
