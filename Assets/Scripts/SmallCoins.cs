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
    public GameObject effectPrefab;
    public GameObject effect;

    public GameObject currentCoin;

    public int counter = 0;

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
        Debug.Log("Started");
        counter = 0;
        SpawnNext();
        effect = Instantiate(effectPrefab, positionHolder[0].position, Quaternion.identity);
    }

    private void Update()
    {
        if (counter >= numberOfCoins)
            return;
        LeanTween.move(effect, positionHolder[counter-1].position, 1f);
    }

    private void OnEnable()
    {
        CheckPoint.OnCheck += Check;
    }

    private void OnDestroy()
    {
        CheckPoint.OnCheck -= Check;
        Destroy(currentCoin);
        Destroy(effect);
    }

    void Check()
    {
        if (counter == numberOfCoins)
        {
            Picked();
        }
        else
        {
            Debug.Log("8");
            LevelController.instance.LevelRetry();
        }
    }

    public void SpawnNext()
    {
        Debug.Log("CALLED SHIT");
        if (counter == numberOfCoins)
            return;
        if (counter == 0)
        {
            currentCoin = Instantiate(coinPrefab, positionHolder[counter++].position, Quaternion.Euler(0, 90, 0));
            return;
        }
        currentCoin = Instantiate(coinPrefab, positionHolder[counter++].position, Quaternion.Euler(0,90,0));
    }
}
