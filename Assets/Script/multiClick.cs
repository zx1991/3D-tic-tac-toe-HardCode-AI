using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class multiClick :  NetworkBehaviour{

    [SyncVar]
    public Color Color1;
    [SyncVar]
    public int playerValue;
    [SyncVar]
    public int currentPlayer;

    private Text text;
    private Text text1;

    public GameObject target;

    public float rotateSpeed = 100f;

    Vector3 mouseOrigin;


    // Use this for initialization
    void Start() {

        text = GameObject.Find("PlayerTurn").GetComponent<Text>();
        text1 = GameObject.Find("textOver").GetComponent<Text>();

        if (!isLocalPlayer) {
            return;
        }

       // tk = GameObject.Find("GameManager").GetComponent<multiplayerManager>();


        target = GameObject.Find("Balls ");
        if (!target) {

      
        }

    }

    // Update is called once per frame
    void Update() {


        if (!isLocalPlayer) {
            return;
        }

        if (currentPlayer == 0)
          
            return;


        if (currentPlayer == playerValue) {

            text.text = "Your Turn";
        } else {


            text.text = "Opponent's Turn";
        }

        if (!target) {

            target = GameObject.Find("Balls ");

        }

        if (Input.GetMouseButtonDown(0)) {

            mouseOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        }

        if (Input.GetMouseButton(0)) {
            float xx = Camera.main.ScreenToViewportPoint(Input.mousePosition).x - mouseOrigin.x;


            transform.LookAt(target.transform);
            transform.RotateAround(target.transform.position, Vector3.up, rotateSpeed * Time.deltaTime * xx);

            float yy = Camera.main.ScreenToViewportPoint(Input.mousePosition).y - mouseOrigin.y;
            transform.RotateAround(target.transform.position, Vector3.right, rotateSpeed * Time.deltaTime * yy);


        }


        if (Input.GetMouseButtonDown(0)) {

            //if (tk.currentPlayer != multiplayerManager.Turn.player)
            //    return;


            if (currentPlayer != playerValue)
                return;
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f)) {

                CmdchangeColor(hit.transform.gameObject, Color1, playerValue);
              
            }

        }


       

    }
    [Command]
    private void CmdchangeColor(GameObject go, Color color, int value) {

        RpcColor(go,color, value);




    }

    [ClientRpc]
    public void RpcTie() {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var p in players) {

            p.GetComponent<multiClick>().currentPlayer = 0;
        }

        text.text = "TIE";
        Invoke("backtoLobby", 15);

    }

    [ClientRpc]
    public void RpcCheckwin() {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var p in players) {

            p.GetComponent<multiClick>().currentPlayer = 0 ;
        }

        if (isLocalPlayer) {

            text1.text = "YOU Won!";
            text.enabled = false;


        } else {


            text1.text = "You Lost";
            text.enabled = false;

        }

        Invoke("backtoLobby", 15);



    }

    [ClientRpc]
    public void RpcColor(GameObject go,Color color, int value) {

        //currentPlayer = 0 - value;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(var p in players) {

            p.GetComponent<multiClick>().currentPlayer = 0 - value;
        }

        if (go.GetComponent<color>().Side != 0)
            return;

        go.GetComponent<color>().ObjectColor = color;

        

        go.GetComponent<color>().Side = value;
      

    }
  
    public void backtoLobby() {


        FindObjectOfType<NetworkLobbyManager>().ServerReturnToLobby();
    }
     
    
}
