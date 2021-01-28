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

    [Header("Object reference")]
    public GameObject playerObj;



    public void Fade(bool isFadeIn)
    {
        if (isFadeIn)
        {
            Color tempColor = Color.clear;

            LeanTween.value(this.gameObject, tempColor, Color.black, fadeSpd).setEase(LeanTweenType.easeOutQuint).setOnUpdate((Color val) =>
            {
                fadeImg.color = val;
            }).setOnComplete(()=> { 
                Fade(false); 
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

    public void LevelRetry()
    {
        Fade(true);
        LevelSetUp(currentLevelNumber);
    }

    public void LevelSetUp(int levelNumber)
    {
        switch (levelNumber)
        {
            case 1:

                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }
        LeanTween.delayedCall(0.5f, () => { PlayerSpawn(); });
    }
    public void PlayerSpawn()
    {
        playerObj.transform.position = spawnPoint[currentLevelNumber - 1].position;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            LevelRetry();
        }
    }
}
