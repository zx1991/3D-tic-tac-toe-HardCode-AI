using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuScript : MonoBehaviour {
   

	// Use this for initialization
	void Start () {
        Screen.orientation = ScreenOrientation.Landscape;
        //go = FindObjectOfType<>
	}
	
	// Update is called once per frame


    public void SinglePlayer() {

        SceneManager.LoadScene("SinglePlayer");
    }

    public void multiplePlayer() {

        
        SceneManager.LoadScene("NetworkLobby");

    }

    public  void quitGame() {

        Application.Quit();
        
    }

    public void LoadMainScene() {

        SceneManager.LoadScene("Menu");
    }
}
