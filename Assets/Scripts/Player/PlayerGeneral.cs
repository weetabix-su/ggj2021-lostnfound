﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGeneral : MonoBehaviour
{
    public Transform snapPosition;

    public int playerHealth;
    public Slider hpBar;


    private void OnEnable()
    {
        SmallCoins.Picked += SnapLocation;
    }

    private void OnDestroy()
    {
        SmallCoins.Picked -= SnapLocation;
    }

    void SnapLocation()
    {
        transform.position = snapPosition.position;
    }

    public void Start()
    {
        InvokeRepeating("HealthReduction", 3f, 1f);
    }

    public void HealthReduction()
    {
        playerHealth--;
        hpBar.value = playerHealth;
    }
}
