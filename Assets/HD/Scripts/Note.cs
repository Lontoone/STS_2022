using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.Return)){
            float emissiveIntensity = 10;
            Color emissiveColor = Color.green;
            this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);
        }
        */
    }

    void ChangeColor(){
         float emissiveIntensity = 10;
         Color emissiveColor = Color.green;
         this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);
    }
}
