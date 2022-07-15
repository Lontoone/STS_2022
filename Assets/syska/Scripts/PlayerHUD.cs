using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public static PlayerHUD instance;
    public Image ProgressBar;
    public Text Lifes;
    public Text NoteCount;

    private void Awake() { instance = this; SetProgressBar(0); SetNoteCount(0); }

    public static void UpdateLifes() { instance.Lifes.text = $"Lifes: {"1"}/3"; }
    public static void SetProgressBar(float percent) { instance.ProgressBar.fillAmount = percent; }
    public static void SetNoteCount(int count) { instance.NoteCount.text = count.ToString(); }
}
