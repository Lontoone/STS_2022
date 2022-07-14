using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HD.Singleton;

public class ObjectPool : TSingletonMonoBehavior<ObjectPool>
{
    //藍色音符子彈庫
    public List<GameObject> BluePooledObjects;
    public GameObject BlueObjectToPool;

    //紅色音符子彈庫
    public List<GameObject> RedPooledObjects;
    public GameObject RedObjectToPool;

    //the number you need to generate
    public int amountToPool;

    public GameObject GetBluePooledObject(){
        
        for ( int i = 0; i < amountToPool; i++ ) {                       
            //Use it if you are not using it
            if(BluePooledObjects[i] && !BluePooledObjects[i].activeInHierarchy){
               return BluePooledObjects[i];
            }
        }
        return null;
    }

    public GameObject GetRedPooledObject(){
        for ( int i = 0; i < amountToPool; i++ ) {
            //Use it if you are not using it
            if(RedPooledObjects[i] && !RedPooledObjects[i].activeInHierarchy){
                return RedPooledObjects[i];
            }
        }
        return null;
    }      

    void Awake()
    {
        BluePooledObjects = new List<GameObject>();
        RedPooledObjects = new List<GameObject>();

        GameObject tmp;
        GameObject tmp2;

        //create Objects and add into  pooledObjects
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(BlueObjectToPool);
            tmp.SetActive(false);
            BluePooledObjects.Add(tmp);             
        }            

        //create Objects and add into  TargetPooledObjects
        for(int i = 0; i < amountToPool; i++)
        {
            tmp2 = Instantiate(RedObjectToPool);
            tmp2.SetActive(false);
            RedPooledObjects.Add(tmp2);     
        }    

    }
}
