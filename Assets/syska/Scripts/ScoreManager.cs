using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float Note_Normal_Multipy = 0.5f;
    public float Note_ShowTime_Multipy = 1.5f;

    private float Code_Percent = 0f;

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
        PlayerHUD.SetProgressBar(Code_Percent / 100f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BlueNote" || other.gameObject.tag == "RedNote")
        {
            Noted(other.gameObject.tag == "BlueNote");
            Debug.Log("吃到音符");
            Destroy(other.gameObject);
        }
    }
}
