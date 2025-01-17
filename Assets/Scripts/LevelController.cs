﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public delegate void FadeStart();
    public static event FadeStart OnStart;
    public delegate void FadeEnd();
    public static event FadeEnd OnEnd;

    [Header("UI reference and parameter")]
    public Image fadeImg;
    public GameObject filmFadeUp;
    public GameObject filmFadeDown;

    [SerializeField] [Range(0.5f, 4f)] float fadeSpd;

    [Header("Level value")]
    public int currentLevelNumber;
    public bool isReturning;

    [Header("Game constant")]
    public Transform[] spawnPoint;

    [Header("Object reference")]
    public GameObject playerObj;

    [Header("CheckPoint reference")]
    public Vector3[] checkPointRef;

    [Header("Scene 1 reference")]
    //Store
    public List<GameObject> scene1Go;
    //Ref
    public GameObject queueGo;
    public Vector3 queueGoSpawnLocation;
    public GameObject dreamGo;
    public Vector3 dreamGoSpawnLocation;


    [Header("Scene 2 reference")]
    //Store
    public List<GameObject> scene2Go;
    //Ref
    public GameObject coinGo;
    public Vector3 coinGoSpawnLocation;
    public GameObject coinColliderGo;
    public Vector3 coinColliderGoSpawnLocation;
    public GameObject coinControlGo;
    public Vector3 coinControlGoSpawnLocation;

    [Header("End Scene reference")]
    public Animator boatManAnim;
    public GameObject endGameCam;
    public GameObject endGameText;
    public GameObject endGameGate;
    public GameObject endGameCol;
    public Transform endSpawn;

    public GameObject endGameShip;
    public GameObject endGameShipman;
    public GameObject endGameBridge;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("FAILURE");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LevelRetry();
    }

    public void Fade(bool isFadeIn, bool isEndGame)
    {
        if(OnStart!= null)
            OnStart();
        if (isFadeIn)
        {
            Color tempColor = Color.clear;

            LeanTween.value(this.gameObject, tempColor, Color.black, fadeSpd).setEase(LeanTweenType.easeOutQuint).setOnUpdate((Color val) =>
            {
                fadeImg.color = val;
            }).setOnComplete(()=> {
                if (!isEndGame)
                    Fade(false, false);
                else
                    endGameText.SetActive(true);
            });
        }
        else
        {
            Color tempColor = Color.clear;

            if (OnEnd != null)
                OnEnd();
            LeanTween.value(this.gameObject, Color.black, tempColor, fadeSpd).setEase(LeanTweenType.easeOutQuint).setOnUpdate((Color val) =>
            {
                fadeImg.color = val;
            }).setOnComplete(()=> {
                //OnEnd();
                isReturning = false;
            });
        }
    }

    public void FilmFade(bool isFadeIn)
    {
        Debug.Log("film");
        if (!isFadeIn)
        {
            if(OnEnd!= null)
                OnEnd();
            LeanTween.moveLocalY(filmFadeUp, 615 , 1.5f);
            LeanTween.moveLocalY(filmFadeDown, -615, 1.5f);
        }
        else
        {
            if(OnStart!= null)
                OnStart();
            LeanTween.moveLocalY(filmFadeUp, 465, 1.5f);
            LeanTween.moveLocalY(filmFadeDown, -465, 1.5f);
        }
    }

    public void LevelRetry()
    {
        if (!isReturning)
        {
            Fade(true, false);
            LevelSetUp(currentLevelNumber);
        }

    }

    public void LevelSetUp(int levelNumber)
    {
        switch (levelNumber)
        {
            case 1:
                {
                    LeanTween.delayedCall(1.1f, () => { LevelOneSetUp(); });
                }
                break;
            case 2:
                {
                    LeanTween.delayedCall(1.1f, () => { LevelTwoSetUp(); });
                }
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }
        LeanTween.delayedCall(1f, () => { PlayerSpawn(); });
    }


    public void LevelSetUpCheckPoint(int levelNumber)
    {
        switch (levelNumber)
        {
            case 1:
                {
                    LeanTween.delayedCall(1.1f, () => { LevelOneSetUp(); });
                }
                break;
            case 2:
                {
                    LeanTween.delayedCall(1.1f, () => { LevelTwoSetUp(); });
                }
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }
    }

    public void PlayerSpawn()
    {
        //playerObj.GetComponent<PlayerGeneral>().playerHealth = 300;
        playerObj.transform.position = spawnPoint[currentLevelNumber - 1].position;

    }

    public void LevelOneSetUp()
    {
        int idx = 0;
        foreach (GameObject obj in scene1Go)
        {
            DestroyImmediate(obj);
            idx++;
        }
        for (int i = 0; i < idx; i++)
        {
            scene1Go.RemoveAt(0);
        }
        scene1Go.Add(Instantiate(queueGo, queueGoSpawnLocation, Quaternion.identity));
        scene1Go.Add(Instantiate(dreamGo, dreamGoSpawnLocation, Quaternion.identity));
    }

    public void LevelTwoSetUp()
    {
        int idx = 0;
        foreach (GameObject obj in scene2Go)
        {
            DestroyImmediate(obj);
            idx++;
        }
        for (int i = 0; i < idx; i++)
        {
            scene2Go.RemoveAt(0);
        }
        scene2Go.Add(Instantiate(coinGo, coinGoSpawnLocation, Quaternion.Euler(0, 90, 0)));
        scene2Go.Add(Instantiate(coinColliderGo, coinColliderGoSpawnLocation, Quaternion.identity));
        scene2Go.Add(Instantiate(coinControlGo, coinControlGoSpawnLocation, Quaternion.identity));
    }

    public void StartEndGame()
    {
        int idx = 0;
        foreach (GameObject obj in scene1Go)
        {
            DestroyImmediate(obj);
            idx++;
        }
        for (int i = 0; i < idx; i++)
        {
            scene1Go.RemoveAt(0);
        }
        endGameGate.SetActive(true);
        endGameCol.SetActive(true);
        endGameShip.SetActive(true);
        endGameBridge.SetActive(true);
        Fade(true, false);
        LeanTween.delayedCall(1f, () => { SnapPos(); });
    }

    public void SnapPos()
    {
        playerObj.transform.position = endSpawn.position;
    }

    public void EndGame()
    {
        Destroy(playerObj);
        endGameShipman.SetActive(true);
        endGameCam.SetActive(true);
        boatManAnim.enabled = true;
        FilmFade(true);
        LeanTween.delayedCall(5f, () => { Fade(true, true); });

    }

    public void DreamBallGet()
    {
        GameObject dreamBall = Instantiate(dreamGo, boatManAnim.gameObject.transform.position + new Vector3(0.5f, 0.2f, 0),Quaternion.identity);
        dreamBall.transform.parent = boatManAnim.gameObject.transform;
        LeanTween.moveLocalY(dreamBall, 1.32f, 1f);
    }
}
