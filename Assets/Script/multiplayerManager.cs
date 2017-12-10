using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class multiplayerManager : NetworkBehaviour {


   // public enum Turn { player, computer, gameOver };

    public Text text;

    public List<GameObject> spheres;

    public List<int[]> lineList = new List<int[]>();

    public int currentPlayerValue = 0;

    public bool get2players = false;

    public bool IsGameOver = false;

    public GameObject[] players;

    // Use this for initialization
    void Start() {

        if (!isServer) {
            Debug.Log("not Server");

            Destroy(this);

        }
        spheres = GameObject.FindGameObjectsWithTag("ball").ToList<GameObject>();
        Debug.Log(spheres.Count());

        players = GameObject.FindGameObjectsWithTag("Player");

        
        if (players.Count() != 2) {

            get2players = false;

        } else {

            Startgame();
            get2players = true;

        }
       

        checkLine();


    }

    // Update is called once per frame

    void Startgame() {


        currentPlayerValue = Random.Range(0, 2) *2 -1;
        for (int i = 0; i < players.Count(); i++) {

            players[i].GetComponent<multiClick>().playerValue = i*2 -1;

            players[i].GetComponent<multiClick>().currentPlayer = currentPlayerValue;


        }



    }
    void Update() {

        if (IsGameOver) {
            return;
        }

        //wait for two players.
        if (!get2players) {

            players = GameObject.FindGameObjectsWithTag("Player");


            if (players.Count() != 2) {

                get2players = false;

                return;

            } else {

                Startgame();
                get2players = true;

            }


        }




        checkWin();

    }

 
    public void checkWin() {



        for (int i = 0; i < spheres.Count; i++) {

            if (spheres[i].GetComponent<color>().Side != players[0].GetComponent<multiClick>().playerValue)
                continue;

            Vector3 trans1 = spheres[i].transform.position;

            for (int ii = i + 1; ii < spheres.Count; ii++) {
                if (spheres[ii].GetComponent<color>().Side != players[0].GetComponent<multiClick>().playerValue)
                    continue;

                Vector3 trans2 = spheres[ii].transform.position;
                for (int iii = ii + 1; iii < spheres.Count; iii++) {
                    if (spheres[iii].GetComponent<color>().Side != players[0].GetComponent<multiClick>().playerValue)
                        continue;
                    Vector3 trans3 = spheres[iii].transform.position;

                    for (int iiii = iii + 1; iiii < spheres.Count; iiii++) {
                        if (spheres[iiii].GetComponent<color>().Side != players[0].GetComponent<multiClick>().playerValue)
                            continue;

                        Vector3 trans4 = spheres[iiii].transform.position;

                        int[] a = new int[] { i, ii, iii, iiii };


                        foreach (var aline in lineList) {
                            if (aline.SequenceEqual(a)) {

                                Debug.Log("Player 2 Win");
                                players[0].GetComponent<multiClick>().RpcCheckwin();
                                IsGameOver = true;
                               
                            }


                        }


                    }

                }


            }


        }

        for (int i = 0; i < spheres.Count; i++) {

            if (spheres[i].GetComponent<color>().Side != players[1].GetComponent<multiClick>().playerValue)
                continue;

            Vector3 trans1 = spheres[i].transform.position;

            for (int ii = i + 1; ii < spheres.Count; ii++) {
                if (spheres[ii].GetComponent<color>().Side != players[1].GetComponent<multiClick>().playerValue)
                    continue;

                Vector3 trans2 = spheres[ii].transform.position;
                for (int iii = ii + 1; iii < spheres.Count; iii++) {
                    if (spheres[iii].GetComponent<color>().Side != players[1].GetComponent<multiClick>().playerValue)
                        continue;
                    Vector3 trans3 = spheres[iii].transform.position;

                    for (int iiii = iii + 1; iiii < spheres.Count; iiii++) {
                        if (spheres[iiii].GetComponent<color>().Side != players[1].GetComponent<multiClick>().playerValue)
                            continue;

                        Vector3 trans4 = spheres[iiii].transform.position;

                        int[] a = new int[] { i, ii, iii, iiii };


                        foreach (var aline in lineList) {
                            if (aline.SequenceEqual(a)) {
                                //player[1] won!
                                Debug.Log("Player 1 Win");
                                players[1].GetComponent<multiClick>().RpcCheckwin();

                                IsGameOver = true;
                                
                            }


                        }


                    }

                }


            }


        }

        foreach(var s in spheres) {

            if (s.GetComponent<color>().Side == 0)
                return;
        }

        IsGameOver = true;
        // game Tie;
        players[0].GetComponent<multiClick>().RpcTie();
    }

    void checkLine() {


        Debug.Log(players.Length);


        for (int i = 0; i < spheres.Count; i++) {

            Vector3 trans1 = spheres[i].transform.position;

            for (int ii = i + 1; ii < spheres.Count; ii++) {

                Vector3 trans2 = spheres[ii].transform.position;
                for (int iii = ii + 1; iii < spheres.Count; iii++) {

                    Vector3 trans3 = spheres[iii].transform.position;

                    for (int iiii = iii + 1; iiii < spheres.Count; iiii++) {

                        Vector3 trans4 = spheres[iiii].transform.position;
                        Vector3 line1 = (trans1 - trans2).normalized;
                        Vector3 line2 = (trans2 - trans3).normalized;
                        Vector3 line3 = (trans3 - trans4).normalized;

                        if ((line1 == line2) && (line2 == line3)) {
                            int[] a = new int[] { i, ii, iii, iiii };
                            lineList.Add(a);


                        }




                    }

                }


            }



        }





    }

    public void Restart() {

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);


    }
}
