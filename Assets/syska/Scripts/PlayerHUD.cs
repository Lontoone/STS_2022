using DG.Tweening;
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

    public static void UpdateLifes() { instance.Lifes.text = $"Lifes: {ScoreManager.Life}/3"; }
    public static void SetProgressBar(float percent) { instance.ProgressBar.fillAmount = percent; }
    public static void SetNoteCount(int count) { instance.NoteCount.text = count.ToString(); }

    public RawImage b;
    bool se = false;
    public RawImage e1;
    public RawImage e2;

    public static void ShowEnding(bool s)
    {
        instance.se = s;
        instance.StartCoroutine(instance.IShowEnding());
    }

    public IEnumerator IShowEnding()
    {
        yield return instance.b.DOFade(1, 0.5f).WaitForCompletion();
        yield return new WaitForSecondsRealtime(3);
        yield return instance.e1.DOFade(1, 0.5f).WaitForCompletion();
        if (se)
        {
            yield return new WaitForSecondsRealtime(7);
            yield return instance.e1.DOFade(0, 0.5f).WaitForCompletion();
            yield return new WaitForSecondsRealtime(3);
            instance.e2.DOFade(1, 0.5f);
        }
    }
}
