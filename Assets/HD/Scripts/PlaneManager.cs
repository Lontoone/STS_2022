using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public static PlaneManager Instance;
    //生成場地的長寬
    public float length = 10.0f;
    //單位格子長
    public float unitLength = 1.0f;
    //要轉的角度
    public float rotateAngle = 180.0f;
    //要旋轉的時間
    public float rotateTime = 2.0f;
    float org_z;    
    bool iskeydown = false;
    bool isrotate = false;

    //時間間隔
    public float periodTime = 10.0f;

    //判斷是否為ShowTime
    public bool isShowTime = false;
    public bool isBlueTurn = true;

    float timer;
    //判斷是藍色ShowTime還是紅色ShowTime
    float timer2;
    private void Awake() {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        org_z = transform.eulerAngles.z;
    }

    void Update () {        
        CountTime();  
        if (Input.GetKeyDown("escape"))
        {
            QuitGame();
        }     
    }    

    void QuitGame(){
        Application.Quit();
    }

    void CountTime(){
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;

        if(timer >= periodTime){
            isShowTime = !isShowTime;
            timer = 0;
        }

        if(timer2 >= (periodTime * 2)){
            isBlueTurn = !isBlueTurn;
            timer2 = 0;
        }
    }
    
}
