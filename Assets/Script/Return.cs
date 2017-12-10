using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Return : MonoBehaviour {

    public void backtoLobby() {
        Debug.Log("quit");

        FindObjectOfType<NetworkLobbyManager>().ServerReturnToLobby();
    }


}
