using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class ScoreManager : MonoBehaviour
{
    public static int allScore = 124;
    public AudioClip impact;
    AudioSource audiosource;

    public float Note_Normal_Multipy = 0.5f;
    public float Note_ShowTime_Multipy = 1.5f;

    public static int Life = 3;
    public static float Code_Percent = 0f;
    public static int Note_Count = 0;
    public bool DEBUG_NOTE_FULL;
    public bool DEBUG_CODE_FULL;

    public static event System.Action FEVER_TIME;

    private void Start() {
        audiosource = GetComponent<AudioSource>();
        if (DEBUG_NOTE_FULL) Note_Count = allScore;
        if (DEBUG_CODE_FULL) Code_Percent = 1f;
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
        PlayerHUD.UpdateLifes();
        PlayerHUD.SetNoteCount(Note_Count);
        PlayerHUD.SetProgressBar(Code_Percent / 100f);
        if (Note_Count >= allScore) FEVER_TIME?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BlueNote" || other.gameObject.tag == "RedNote")
        {
            Noted(other.gameObject.tag == "BlueNote");
            audiosource.PlayOneShot(impact);
            //allScore -= 1 ;
            //Debug.Log("吃到音符");
            Destroy(other.gameObject);
        }
    }
}
