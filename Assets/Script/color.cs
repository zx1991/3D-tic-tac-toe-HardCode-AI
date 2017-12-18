using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

 public class color : MonoBehaviour {
    public Color ObjectColor;
    public Int64 Side = 0;


    private Color currentColor;
    private Material materialColored;


    private void Start() {
        materialColored = GetComponent<Renderer>().material;
    }

    void Update() {
        if (ObjectColor != currentColor) {
            
          
           
            materialColored.color = currentColor = ObjectColor;
           
        }
    }
}