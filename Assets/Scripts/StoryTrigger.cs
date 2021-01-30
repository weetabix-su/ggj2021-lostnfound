using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    public bool isTriggered;

    [Header("Parameter")]
    public bool isFilmPerspective;

    public void OnTriggerEnter(Collider other)
    {
        if (isTriggered == false && other.tag == "Player")
        {
            if (isFilmPerspective)
                LevelController.instance.FilmFade(true);
        }
    }
}
