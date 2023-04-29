using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextManager : MonoBehaviour
{

    public static TextManager instance;
    public TextMeshProUGUI Texi;

    private Queue<TextDisplay> textDisplays = new Queue<TextDisplay>();
    public GameObject image;

    public TextManager()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(ShowTextEnumerator());
    }

    public void ShowText(string Text, float Time, Action action = null)
    {
        Action a = () => { };
        textDisplays.Enqueue(new TextDisplay() { Text = Text, time = Time, action = action??a});
    }

    public IEnumerator ShowTextEnumerator()
    {
        while(true)
        {
            if (textDisplays.Count > 0)
            {
                var TD = textDisplays.Dequeue();
                Texi.text = "";
                image.SetActive(true);
                foreach (var c in TD.Text)
                {
                    Texi.text += c;
                    yield return new WaitForSeconds(0.03f);
                }
                yield return new WaitForSeconds(TD.time);
                TD.action();
                Texi.text = "";
                image.SetActive(false);
            }
            yield return null;
        }
    }
}

public class TextDisplay
{
    public string Text { get; set; }
    public float time { get; set; }
    public Action action { get; set; } = () => { };
}
