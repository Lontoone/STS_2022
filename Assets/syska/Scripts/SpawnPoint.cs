using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject g1;
    public GameObject g2;
    public GameObject g3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            g1.SetActive(false);
            g2.SetActive(false);
            g3.SetActive(false);
            Player.DisablePlayer();
            if (ScoreManager.allScore == ScoreManager.Note_Count)
            {
                PlayerHUD.ShowEnding(ScoreManager.Code_Percent >= 1);
            }
            Debug.Log(other.gameObject.name);
        }
    }
}
