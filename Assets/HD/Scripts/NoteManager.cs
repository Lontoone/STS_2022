using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    //將音符子彈裝入父物件
    public GameObject container;

    //將子彈裝入子彈庫
    public List<GameObject> BlueNoteLibrary;
    public List<GameObject> RedNoteLibrary;

    float length;
    float unitLength;
    int lengthNumber = 0;

    //哪種子彈showTime
    bool blueShowTime = true;

    private void Awake() {

        length = container.GetComponent<PlaneManager>().length;          
        unitLength = container.GetComponent<PlaneManager>().unitLength;        

        //除法的轉換過程 float to int
        float a = length / unitLength;
        lengthNumber = (int) a ;

        BlueNoteLibrary = new List<GameObject>(); 
        RedNoteLibrary = new List<GameObject>();       
    }

    // Start is called before the first frame update
    void Start()
    {              
        for (int a = 0; a < lengthNumber; a++)
        {
            for (int b = 0; b < lengthNumber; b++)
            {
                CreateBlueNote(a,b);
                CreateRedNote(a,b);
            }            
        } 
        
    }

    private void Update() {
        LightManage();
    }

    void CreateBlueNote(int a, int b){
        GameObject noteNPC = ObjectPool.Instance.GetBluePooledObject();
        if(noteNPC){
            noteNPC.transform.parent = container.transform;
            noteNPC.transform.position = container.transform.position + new Vector3( -length / 2 + a * unitLength + unitLength / 2 , (-length / 2) + 2, length / 2 - b * unitLength - + unitLength / 2);
            noteNPC.SetActive(true); 
            BlueNoteLibrary.Add(noteNPC);
        }else{
            Debug.Log("Produce Error!");
        }
    }

    void CreateRedNote(int a, int b){
        GameObject noteNPC = ObjectPool.Instance.GetRedPooledObject();
        if(noteNPC){
            noteNPC.transform.parent = container.transform;
            noteNPC.transform.position = container.transform.position + new Vector3( -length / 2 + a * unitLength + unitLength / 2 , -length / 2, length / 2 - b * unitLength - + unitLength / 2);
            noteNPC.SetActive(true); 
            RedNoteLibrary.Add(noteNPC);
        }else{
            Debug.Log("Produce Error!");
        }
    }

    void LightManage(){
        //isShowTime in PlaneManager
        if(PlaneManager.Instance.isShowTime && PlaneManager.Instance.isBlueTurn){
            for (int i = 0; i < BlueNoteLibrary.Count; i++)
            {
                if(BlueNoteLibrary[i] != null){
                    BlueNoteLibrary[i].GetComponent<Note>().BlueChangeIntensity();
                }                
            }
        }else if(PlaneManager.Instance.isShowTime && !PlaneManager.Instance.isBlueTurn){
            for (int i = 0; i < RedNoteLibrary.Count; i++)
            {
                if(RedNoteLibrary[i] != null){
                    RedNoteLibrary[i].GetComponent<Note>().RedChangeIntensity();
                }                
            }
        }else{
            for (int i = 0; i < BlueNoteLibrary.Count; i++)
            {
                if(BlueNoteLibrary[i] != null){
                    BlueNoteLibrary[i].GetComponent<Note>().BlueResetColor();
                }
            }

            for (int i = 0; i < RedNoteLibrary.Count; i++)
            {
                if(RedNoteLibrary[i] != null){
                    RedNoteLibrary[i].GetComponent<Note>().RedResetColor();
                }
            }            
        }
    }
}
