using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    //發光漸變頻率
    public float ChangeRate = 1.0f;
    public float emissiveIntensity = 10;
    float timer = 0.01f;
    bool addLight = true;
    public float rotateSpeed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(addLight){
            timer += Time.deltaTime;
            if(timer >= ChangeRate){
                addLight = false;
            }
        }else{
            timer -= Time.deltaTime;
            if(timer <= 0.0f){
                addLight = true;
            }
        }

       this.gameObject.transform.Rotate( 0.0f, 0.0f, rotateSpeed * Time.deltaTime);

        /*
        if(Input.GetKeyDown(KeyCode.Return)){
            float emissiveIntensity = 10;
            Color emissiveColor = Color.green;
            this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);
        }
        */
    }

    public void BlueChangeIntensity(){
         emissiveIntensity = 10;
         Color emissiveColor = new Color(0.1f, 0.1f, 1, 1);
         this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity * timer);
    }

    public void RedChangeIntensity(){
         emissiveIntensity = 10;
         Color emissiveColor = new Color(0.1f, 1.0f, 0.1f, 1);
         this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity * timer);
    }

    public void BlueResetColor(){
         emissiveIntensity = 1;
         Color emissiveColor = new Color(0.556f, 0.592f, 1f, 1);
         this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);
    }

    public void RedResetColor(){
         emissiveIntensity = 1;
         Color emissiveColor = new Color(0.7f, 1f, 0.772f, 1);
         this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);
    }

     private void OnTriggerEnter(Collider other) {  
        //牆壁要掛載 tag Wall, isTrigger, Rigibody
        if(other.tag == "Wall"){
            Destroy(this.gameObject);
        }
    }
}
