using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int checkPointNumber;

    public delegate void EnterCheckPoint();
    public static event EnterCheckPoint OnCheck;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("ontriggerEnter");
            LevelController.instance.currentLevelNumber = checkPointNumber + 1;
            if (checkPointNumber == 2)
            {
                if(OnCheck!= null)
                    OnCheck();
            }
        }
    }
}
