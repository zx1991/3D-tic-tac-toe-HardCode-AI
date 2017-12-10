using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Lobbyscript : NetworkBehaviour {
    public GameObject go;
    public GameObject button;

    public ScriptableObject[] gos;
    // Use this for initialization
    void Start() {

        button = GameObject.Find("MenuButton");
        go = GameObject.Find("LobbyManager");
        
        button.GetComponent<Button>().onClick.AddListener(LoadMainScene);
        go.GetComponent<Canvas>().enabled = true; ;
       
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

    public void quitGame() {

        Application.Quit();

    }

    public void LoadMainScene() {

        go.GetComponent<Canvas>().enabled = false;
        SceneManager.LoadScene("Menu");
    }
}
