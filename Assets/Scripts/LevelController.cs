using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    [Header("UI reference and parameter")]
    public Image fadeImg;
    [SerializeField] [Range(0.5f, 4f)] float fadeSpd;

    [Header("Level value")]
    public int currentLevelNumber;

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
    public GameObject guardGo;
    public Vector3 guardGoSpawnLocation;

    [Header("Scene 2 reference")]
    //Store
    public List<GameObject> scene2Go;
    //Ref
    public GameObject coinGo;
    public Vector3 coinGoSpawnLocation;
    public GameObject coinColliderGo;
    public Vector3 coinColliderGoSpawnLocation;

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
                {
                    LeanTween.delayedCall(0.6f, () => { LevelOneSetUp(); });
                }
                break;
            case 2:
                {
                    foreach (GameObject obj in scene2Go)
                    {
                        Destroy(obj);
                    }
                    //Spawn coin
                    scene2Go.Add(Instantiate(coinGo, coinGoSpawnLocation, Quaternion.Euler(0,90,0)));
                    //Spawn coin fall collider
                    scene2Go.Add(Instantiate(coinColliderGo, coinColliderGoSpawnLocation, Quaternion.identity));
                }
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

    public void LevelOneSetUp()
    {
        foreach (GameObject obj in scene1Go)
        {
            Destroy(obj);
        }
        //Spawn queue
        scene1Go.Add(Instantiate(queueGo, queueGoSpawnLocation, Quaternion.identity));
        //Spawn guard
        scene1Go.Add(Instantiate(guardGo, guardGoSpawnLocation, Quaternion.identity));
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            LevelRetry();
        }
    }
}
