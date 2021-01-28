using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelController : MonoBehaviour
{
    [Header("UI reference and parameter")]
    public Image fadeImg;
    [SerializeField] [Range(0.5f, 4f)] float fadeSpd;

    [Header("Level value")]
    public int currentLevelNumber;

    [Header("Game constant")]
    public Transform[] spawnPoint;





    public void Fade(bool isFadeIn)
    {
        if (isFadeIn)
        {
            Color tempColor = Color.clear;

            LeanTween.value(this.gameObject, tempColor, Color.black, fadeSpd).setEase(LeanTweenType.easeOutQuint).setOnUpdate((Color val) =>
            {
                fadeImg.color = val;
            });
        }
        else
        {
            Color tempColor = Color.clear;

            LeanTween.value(this.gameObject, Color.black, tempColor, fadeSpd).setEase(LeanTweenType.easeOutQuint).setOnUpdate((Color val) =>
            {
                fadeImg.color = val;
            });
        }
    }

    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        Fade(true);
    //    }
    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        Fade(false);
    //    }
    //}
}
