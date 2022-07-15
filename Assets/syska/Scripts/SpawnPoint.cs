using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (ScoreManager.allScore == ScoreManager.Note_Count)
            {
                //WIN
                //fade to black
                //show ending cg
            }
            Debug.Log(other.gameObject.name);
        }
    }
}
