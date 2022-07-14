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

    public void ChangeColor(){
         float emissiveIntensity = 10;
         Color emissiveColor = Color.green;
         this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);
    }

    public void BlueResetColor(){
        float emissiveIntensity = 10;
         Color emissiveColor = Color.blue;
         this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);
    }

    public void RedResetColor(){
        float emissiveIntensity = 10;
         Color emissiveColor = Color.red;
         this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);
    }
}
