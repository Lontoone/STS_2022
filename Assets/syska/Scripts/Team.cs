using UnityEngine;

public class Team : MonoBehaviour
{
    RectTransform rect;
    public float sf = -1000;
    public float se = 3080;
    public float speed = 1;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, sf);
    }

    public void Update()
    {
        if (Input.anyKeyDown)
        {
            Destroy(this.gameObject);
        }
        rect.anchoredPosition += speed * Time.deltaTime * Vector2.up;
        if (rect.anchoredPosition.y >= se)
        {
            rect.anchoredPosition = new Vector2(0, -1300);
        }
    }
}
