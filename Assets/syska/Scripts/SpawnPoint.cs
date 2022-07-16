using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static SpawnPoint instance;
    public GameObject g1;
    public GameObject g2;
    public GameObject g3;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(ScoreManager.Code_Percent);
            Debug.Log(ScoreManager.Note_Count);
            if (ScoreManager.allScore == ScoreManager.Note_Count) End();
            Debug.Log(other.gameObject.name);
        }
    }

    public static void End()
    {
        PlaneManager.Instance.isEnd = true;
        instance.g1.SetActive(false);
        instance.g2.SetActive(false);
        instance.g3.SetActive(false);
        Player.DisablePlayer();
        PlayerHUD.ShowEnding(ScoreManager.Code_Percent >= 100);
    }
}
