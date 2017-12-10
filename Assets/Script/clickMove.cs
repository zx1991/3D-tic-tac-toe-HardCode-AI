using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickMove : MonoBehaviour {


    public TakeTurn tk;
    public Text text;

	// Use this for initialization
	void Start () {

      
		
	}
	
	// Update is called once per frame
	void Update () {

        

        if (Input.GetMouseButtonDown(0)) {

            if (tk.currentPlayer != TakeTurn.Turn.player)
                return;

            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, 100.0f)) {

                changeColor(hit.transform.gameObject);

            }

        }


	}

    private void changeColor(GameObject go) {

        if (go.GetComponent<color>().Side != 0)
            return;

        go.GetComponent<color>().ObjectColor = tk.playerColor;

        go.GetComponent<color>().Side = 1;

        tk.currentPlayer = TakeTurn.Turn.computer;

        text.text = "computer";
        tk.checkWin();



    }
}
