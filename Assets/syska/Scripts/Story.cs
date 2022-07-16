using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Story : MonoBehaviour
{
    public GameObject bg;
    public GameObject l1;
    public GameObject l2;
    public GameObject l3;
    public GameObject l4;

    public void StartS()
    {
        StartCoroutine(IStartS());
    }
    public IEnumerator IStartS()
    {
        bg.SetActive(true);
        l1.SetActive(true);
        l2.SetActive(true);
        l3.SetActive(true);
        l4.SetActive(true);

        bg.GetComponent<RawImage>().DOFade(1, 0);
        yield return new WaitForSecondsRealtime(1);
        yield return l1.GetComponent<RawImage>().DOFade(1, 2).WaitForCompletion();
        yield return new WaitForSecondsRealtime(2);
        yield return l1.GetComponent<RawImage>().DOFade(0, 1).WaitForCompletion();
        yield return new WaitForSecondsRealtime(0.5f);
        yield return l2.GetComponent<RawImage>().DOFade(1, 1).WaitForCompletion();
        yield return new WaitForSecondsRealtime(2);
        yield return l2.GetComponent<RawImage>().DOFade(0, 1).WaitForCompletion();
        yield return new WaitForSecondsRealtime(0.5f);
        yield return l3.GetComponent<RawImage>().DOFade(1, 1).WaitForCompletion();
        yield return new WaitForSecondsRealtime(2);
        yield return l3.GetComponent<RawImage>().DOFade(0, 1).WaitForCompletion();
        yield return new WaitForSecondsRealtime(0.5f);
        yield return l4.GetComponent<RawImage>().DOFade(1, 1).WaitForCompletion();
        yield return new WaitForSecondsRealtime(2);
        yield return l4.GetComponent<RawImage>().DOFade(0, 1).WaitForCompletion();
        yield return new WaitForSecondsRealtime(3);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
