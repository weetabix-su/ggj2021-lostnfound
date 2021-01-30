using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GodDialogue : MonoBehaviour
{
    [Header("Dialogue content")]
    public string[] sentences;

    [Header("Parameter")]
    public float fadeSpeed;
    public float interval;
    public bool isPlaying;
    public bool isTriggered;

    [Header("Object reference")]
    public TMP_Text tmp;

    [Header("Reference for developer")]
    public Queue<string> currentQueue = new Queue<string>();

    public void OnTriggerEnter(Collider other)
    {
        if (isTriggered == false && other.tag == "Player")
        {
            isTriggered = true;
            LevelController.instance.FilmFade(true);
            DialogueSetUp();
        }
    }

    public void DialogueSetUp()
    {
        for (int i = 0; i < sentences.Length; i++)
        {
            currentQueue.Enqueue(sentences[i]);
        }
        DialoguePlay();
    }

    public void DialoguePlay()
    {
        if (currentQueue.Count > 0)
        {
            isPlaying = true;
            tmp.text = currentQueue.Dequeue();
            Color tempColor = new Color(1, 1, 1, 0);

            LeanTween.value(this.gameObject, tempColor, Color.white, fadeSpeed).setOnUpdate((Color val)=> {
                tmp.color = val;
            }).setOnComplete(()=> {
                LeanTween.delayedCall(interval, () =>
                {
                    LeanTween.value(this.gameObject, Color.white, tempColor, fadeSpeed).setOnUpdate((Color val) =>
                    {
                        tmp.color = val;
                    }).setOnComplete(() =>
                    {
                        DialoguePlay();
                    });
                });
            });

        }
        else
        {
            isPlaying = false;
            Debug.Log("There is no sentence left.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            DialogueSetUp();
        }
    }

}
