using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public static PlayerHUD instance;
    public Image ProgressBar;
    public Text Lifes;

    private void Awake() { instance = this; SetProgressBar(0); }

    public static void UpdateLifes() { instance.Lifes.text = $"Lifes: {0}/3"; }
    public static void SetProgressBar(float percent) { instance.ProgressBar.fillAmount = percent; }
}
