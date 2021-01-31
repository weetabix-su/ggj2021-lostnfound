using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneHandler : MonoBehaviour
{
    [SerializeField] string GameSceneName = "MainScene";
    Animator anim;
    float clock;

    IEnumerator startGame()
    {
        anim.SetBool("isStart", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(GameSceneName);
        yield return null;
    }

    void Start()
    {
        clock = 0;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (clock >= 2f)
        {
            if (Input.anyKeyDown)
            {
                StartCoroutine(startGame());
            }
        }
        else clock += Time.deltaTime;
    }
}
