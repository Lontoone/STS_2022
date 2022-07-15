using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class ScoreManager : MonoBehaviour
{
    public AudioClip impact;
    AudioSource audiosource;

    public float Note_Normal_Multipy = 0.5f;
    public float Note_ShowTime_Multipy = 1.5f;

    private float Code_Percent = 0f;
    private int Note_Count = 0;

    private void Start() {
        audiosource = GetComponent<AudioSource>();
    }

    private void Noted(bool isBlue)
    {
        if (PlaneManager.Instance.isShowTime && PlaneManager.Instance.isBlueTurn == isBlue)
        {
            Code_Percent += Note_ShowTime_Multipy;
        }
        else
        {
            Code_Percent += Note_Normal_Multipy;
        }

        Note_Count++;
        PlayerHUD.SetNoteCount(Note_Count);
        PlayerHUD.SetProgressBar(Code_Percent / 100f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BlueNote" || other.gameObject.tag == "RedNote")
        {
            Noted(other.gameObject.tag == "BlueNote");
            audiosource.PlayOneShot(impact);
            Debug.Log("吃到音符");
            Destroy(other.gameObject);
        }
    }
}
