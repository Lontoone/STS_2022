using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerMove : MonoBehaviour
{
    RectTransform m_RectTransform;
    public static DangerMove Instance;
    public float timer = 3.0f;

    public bool isMove = false;

    private void Start() {
        m_RectTransform = GetComponent<RectTransform>();
    }

    private void Awake() {
        
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {   
        timer += Time.deltaTime;
        if(timer >= 3.0f){
            isMove = false;
            m_RectTransform.anchoredPosition = new Vector2(2000.0f, 0.0f);
        }
        if(isMove){
            m_RectTransform.anchoredPosition += new Vector2(-20.0f, 0.0f);            
        }        
    }
}
