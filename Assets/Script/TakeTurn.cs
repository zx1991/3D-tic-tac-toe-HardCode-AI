using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class TakeTurn : MonoBehaviour {

    public enum Turn { player, computer, gameOver ,wait};

    public Text text;

    public List<GameObject> spheres;

    public List<int[]> lineList = new List<int[]>();

    public LineRenderer lr;

    public Color playerColor = Color.red;
    public Color AIColor = Color.blue;

    public Animator anim;


    public Turn currentPlayer;

    public int Flashindex = -1;
    public float flashDelay = 0.5f;
    public bool flash = false;

    private float startTime;
    private bool isWaiting = false;



    // Use this for initialization
    void Start() {

        currentPlayer = (Turn)Random.Range(0, 2);




        text.text = currentPlayer.ToString();

        checkLine();


    }

    // Update is called once per frame
    void Update() {




        //text.text = currentPlayer.ToString();
        if (currentPlayer == Turn.computer) {

            if (!isWaiting) {

                startTime = Time.time;
                isWaiting = true;

            } else {

                if (startTime + 1 < Time.time) {

                    AIMove();
                    isWaiting = false;
                }
            }
          

        }
       

        


    }

    private void AIMove() {
        //currentPlayer = Turn.wait;

        //float t = Time.time + 1;

        //do { } while (t > Time.time);

        int maxValue = -1;

        int index = 0;


        for (int i = 0; i < spheres.Count; i++) {

            if (spheres[i].GetComponent<color>().Side == 0) {

                int value = pointValue(i);

               

                if (value > maxValue) {


                    index = i;
                    maxValue = value;
                }


            }

        }

        spheres[index].GetComponent<color>().ObjectColor = AIColor;
        spheres[index].GetComponent<color>().Side = -1;

        Flashindex = index;
        
        currentPlayer = Turn.player;
        text.text = "player";
        checkWin();


    }

    private int pointValue(int index) {

        int value = 0;

        foreach (var line in lineList) {

            if (line.Contains(index)) {

                
                value += lineValue(line);


            }



        }

        return value;



    }

    private int lineValue(int[] line) {

        

        int PlayerCount = 0;

        int AICount = 0;

        for (int i = 0; i < line.Length; i++) {



            if (spheres[line[i]].GetComponent<color>().Side == 1) {


                PlayerCount++;


            } else if (spheres[line[i]].GetComponent<color>().Side == -1) {


                AICount++;

            }





        }

        if (PlayerCount > 0 && AICount > 0) {

          //  Debug.Log("both");
            return 0;
        }


        if (PlayerCount == 0 && AICount == 0) {
           

            return 10;
        } else if (AICount == 3) {

          //  Debug.Log("A3");

            return 1000000000;
        } else if (AICount == 2) {
         //   Debug.Log("A2");
            return 10000;

        } else if (AICount == 1) {
        //    Debug.Log("A1");
            return 1000;


        } else if (PlayerCount == 3) {
        //    Debug.Log("p3");
            return 1000000;


        } else if (PlayerCount == 2) {

         //   Debug.Log("p2");
            return 9999;


        } else if (PlayerCount == 1) {

        //    Debug.Log("p1");

            return 100;

        }
        return 0;

    }

   public void checkWin() {

        for (int i = 0; i < spheres.Count; i++) {

            if (spheres[i].GetComponent<color>().ObjectColor != playerColor)
                continue;
            
            Vector3 trans1 = spheres[i].transform.position;

            for (int ii = i + 1; ii < spheres.Count; ii++) {
                if (spheres[ii].GetComponent<color>().ObjectColor != playerColor)
                    continue;

                Vector3 trans2 = spheres[ii].transform.position;
                for (int iii = ii + 1; iii < spheres.Count; iii++) {
                    if (spheres[iii].GetComponent<color>().ObjectColor != playerColor)
                        continue;
                    Vector3 trans3 = spheres[iii].transform.position;

                    for (int iiii = iii + 1; iiii < spheres.Count; iiii++) {
                        if (spheres[iiii].GetComponent<color>().ObjectColor != playerColor)
                            continue;

                        Vector3 trans4 = spheres[iiii].transform.position;

                        int[] a = new int[] { i, ii, iii, iiii };


                        foreach (var aline in lineList) {
                            if (aline.SequenceEqual(a)) {
                                lr.SetPosition(0, trans1);
                                lr.SetPosition(1, trans4);
                                lr.enabled = true;
                                currentPlayer = Turn.gameOver;
                                text.text = "Game Over\nYou Win!";
                                anim.SetTrigger("gameover");
                                
                            }


                        }


                    }

                }


            }


        }



        for (int i = 0; i < spheres.Count; i++) {

            if (spheres[i].GetComponent<color>().ObjectColor != AIColor)
                continue;

            Vector3 trans1 = spheres[i].transform.position;

            for (int ii = i + 1; ii < spheres.Count; ii++) {
                if (spheres[ii].GetComponent<color>().ObjectColor != AIColor)
                    continue;

                Vector3 trans2 = spheres[ii].transform.position;
                for (int iii = ii + 1; iii < spheres.Count; iii++) {
                    if (spheres[iii].GetComponent<color>().ObjectColor != AIColor)
                        continue;
                    Vector3 trans3 = spheres[iii].transform.position;

                    for (int iiii = iii + 1; iiii < spheres.Count; iiii++) {
                        if (spheres[iiii].GetComponent<color>().ObjectColor != AIColor)
                            continue;

                        Vector3 trans4 = spheres[iiii].transform.position;

                        int[] a = new int[] { i, ii, iii, iiii };


                        foreach (var aline in lineList) {
                            if (aline.SequenceEqual(a)) {
                                lr.SetPosition(0, trans1);
                                lr.SetPosition(1, trans4);
                                lr.enabled = true;
                                currentPlayer = Turn.gameOver;

                                text.text = "Game Over\nYou Lost!";

                                anim.SetTrigger("gameover");
                            }


                        }


                    }

                }


            }


        }

        foreach (var s in spheres) {

            if (s.GetComponent<color>().Side == 0)
                return;
        }

        currentPlayer = Turn.gameOver;

        text.text = "Game Over\nTIE!";

        anim.SetTrigger("gameover");


    }

    void checkLine() {


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

    public void exit() {
        SceneManager.LoadScene("Menu");

        
    }
}
