using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCoins : MonoBehaviour
{
    public static SmallCoins instance;

    public delegate void AllCoinsPicked();
    public static event AllCoinsPicked Picked;


    public int numberOfCoins;
    public Transform[] positionHolder;

    public GameObject coinPrefab;

    public int counter;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("Failure");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        counter = 0;
        SpawnNext();
    }

    private void OnEnable()
    {
        CheckPoint.OnCheck += Check;
    }

    private void OnDestroy()
    {
        CheckPoint.OnCheck -= Check;
    }

    void Check()
    {
        if (counter == numberOfCoins)
        {
            Picked();
        }
        else
        {
            LevelController.instance.LevelRetry();
        }
    }

    public void SpawnNext()
    {
        if (counter == numberOfCoins)
            return;
        Instantiate(coinPrefab, positionHolder[counter++].position, Quaternion.Euler(0,90,0));
    }
}
