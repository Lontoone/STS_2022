using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject[] UI;
    public GameObject start;
    int i = 0;

    public void Click()
    {
        UI[i].SetActive(false);
        i++;
        UI[i].SetActive(true);
    }

    private void Update() {
        if(i >= 3){
            start.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
