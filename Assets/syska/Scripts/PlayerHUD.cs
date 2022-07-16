using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]

public class PlayerHUD : MonoBehaviour
{
    public AudioClip impact;
    AudioSource audiosource;

    public static PlayerHUD instance;
    public Image ProgressBar;
    public Text Lifes;
    public Text NoteCount;
    public Text percen;

    private void Awake() { instance = this; SetProgressBar(0); SetNoteCount(0); }
    private void Start() {
        audiosource = GetComponent<AudioSource>();
    }
    public static void UpdateLifes() { instance.Lifes.text = $"Lifes: {ScoreManager.Life}/3"; }
    public static void SetProgressBar(float percent) { instance.ProgressBar.fillAmount = percent; instance.percen.text = $"{percent * 100}%"; }
    public static void SetNoteCount(int count) { instance.NoteCount.text = count.ToString(); }

    public RawImage b;
    bool se = false;
    public RawImage e1;
    public RawImage e2;
    public RawImage s1;
    public RawImage s2;
    public RawImage s3;
    public RawImage s4;
    public RawImage s5;
    public RawImage s6;
    public Image b1;
    public RawImage lb;
    public RawImage l1;
    public RawImage l2;
    public RawImage l3;

    public static void ShowRemainLife()
    {
        instance.StartCoroutine(instance.IShowRemainLife());
    }

    public IEnumerator IShowRemainLife()
    {
        Player.DisablePlayer();
        lb.DOFade(1, 0);
        yield return new WaitForSecondsRealtime(3);
        if (ScoreManager.Life == 2)
        {
            l2.DOFade(1, 0);
            l3.DOFade(1, 0);
        }
        else
        {
            l1.DOFade(1, 0);
        }
        yield return new WaitForSecondsRealtime(3);
        lb.DOFade(0, 0);
        l1.DOFade(0, 0);
        l2.DOFade(0, 0);
        l3.DOFade(0, 0);
        Player.EnablePlayer();
    }

    public static void ShowEnding(bool s)
    {              
        instance.audiosource.PlayOneShot(instance.impact);
        Debug.Log("播放end");
        instance.se = s;        
        instance.StartCoroutine(instance.IShowEnding());
    }

    public IEnumerator IShowEnding()
    {
        yield return instance.b.DOFade(1, 0.5f).WaitForCompletion();
        yield return new WaitForSecondsRealtime(3);
        yield return instance.e1.DOFade(1, 0.5f).WaitForCompletion();
        yield return new WaitForSecondsRealtime(1);
        s1.DOFade(1, 0);
        yield return new WaitForSecondsRealtime(3);
        s1.DOFade(0, 0);
        yield return new WaitForSecondsRealtime(1);
        s2.DOFade(1, 0);
        yield return new WaitForSecondsRealtime(3);
        s2.DOFade(0, 0);
        yield return new WaitForSecondsRealtime(1);
        s3.DOFade(1, 0);
        yield return new WaitForSecondsRealtime(3);
        s3.DOFade(0, 0);
        if (se)
        {
            yield return new WaitForSecondsRealtime(3);
            yield return instance.e1.DOFade(0, 0.5f).WaitForCompletion();
            yield return new WaitForSecondsRealtime(3);
            instance.e2.DOFade(1, 0.5f);
            yield return new WaitForSecondsRealtime(1);
            s4.DOFade(1, 0);
            yield return new WaitForSecondsRealtime(3);
            s4.DOFade(0, 0);
            yield return new WaitForSecondsRealtime(1);
            s5.DOFade(1, 0);
            yield return new WaitForSecondsRealtime(3);
            s5.DOFade(0, 0);
            yield return new WaitForSecondsRealtime(1);
            s6.DOFade(1, 0);
            yield return new WaitForSecondsRealtime(3);
            s6.DOFade(0, 0);
            yield return new WaitForSecondsRealtime(1);
        }

        yield return b1.DOFade(1, 3).WaitForCompletion();
    }
    public void back()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
