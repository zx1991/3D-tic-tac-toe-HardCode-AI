using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraDrag : MonoBehaviour {



    public GameObject target;

    public float rotateSpeed = 1f;

    Vector3 mouseOrigin;
    private void Start() {



        target = GameObject.Find("Balls ");
        if (!target) {

            Debug.Log("NOT FOUND!");
        }
        
    }


    private void Update() {

        if (!target) {
            target = GameObject.Find("Balls ");

        }

        if (Input.GetMouseButtonDown(0)) {

            mouseOrigin =  Camera.main.ScreenToViewportPoint(Input.mousePosition);

        }

        if (Input.GetMouseButton(0)) {
           float xx =  Camera.main.ScreenToViewportPoint(Input.mousePosition).x - mouseOrigin.x;


            transform.LookAt(target.transform);
            transform.RotateAround(target.transform.position, Vector3.up, rotateSpeed * Time.deltaTime*xx);

            float yy = Camera.main.ScreenToViewportPoint(Input.mousePosition).y - mouseOrigin.y;
            transform.RotateAround(target.transform.position, Vector3.right, rotateSpeed * Time.deltaTime * yy);


        }


       

    }
}
