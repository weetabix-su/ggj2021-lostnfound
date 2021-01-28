using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject panelToShow;
    public float tweenTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TweenPanel(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            TweenPanel(false);
        }
    }

    private void TweenPanel(bool tf)
    {
        LeanTween.scale(panelToShow, tf ? Vector3.one : Vector3.zero, tweenTime);
    }
}
