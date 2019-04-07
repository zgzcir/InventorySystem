using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolTip : MonoBehaviour
{
    private Text infoText;
    private CanvasGroup canvasGroup;
    private float smoothing = 4f;
    private void Start()
    {
        infoText = GetComponentInChildren<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    void Update()
    {


    }
    public void Show(string text)
    {
        infoText.text = text;
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void SetLocalPotion(Vector2 position)
    {
        transform.localPosition = position;
    }
}
