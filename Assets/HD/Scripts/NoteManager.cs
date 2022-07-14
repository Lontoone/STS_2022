using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    //將音符子彈裝入父物件
    public GameObject container;

    //將子彈裝入子彈庫
    public List<GameObject> NoteLibrary;

    float length;
    float unitLength;
    int lengthNumber = 0;

    private void Awake() {

        length = container.GetComponent<PlaneManager>().length;          
        unitLength = container.GetComponent<PlaneManager>().unitLength;        

        //除法的轉換過程 float to int
        float a = length / unitLength;
        lengthNumber = (int) a ;

        NoteLibrary = new List<GameObject>();        
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

    void CreateBlueNote(int a, int b){
        GameObject noteNPC = ObjectPool.Instance.GetBluePooledObject();
        if(noteNPC){
            noteNPC.transform.parent = container.transform;
            noteNPC.transform.position = container.transform.position + new Vector3( -length / 2 + a * unitLength + unitLength / 2 , length / 2, length / 2 - b * unitLength - + unitLength / 2);
            noteNPC.SetActive(true); 
            NoteLibrary.Add(noteNPC);
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
            NoteLibrary.Add(noteNPC);
        }else{
            Debug.Log("Produce Error!");
        }
    }
}
