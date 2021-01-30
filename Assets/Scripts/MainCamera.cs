using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public bool isFollowingChara;
    public GameObject characterObj;

    private int tweenId;
    private int tweenId2;

    [Header("Constant")]
    public new Vector3 rot1;
    public new Vector3 rot2;

    public void Update()
    {
        if (isFollowingChara)
        {
            this.gameObject.transform.position = new Vector3(characterObj.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
    }

    public void CameraTween(Transform trans)
    {
        LeanTween.cancel(tweenId);
        LeanTween.cancel(tweenId2);

        tweenId = LeanTween.move(this.gameObject, trans.position, 1f).id;
        if (isFollowingChara)
            tweenId2 = LeanTween.rotate(this.gameObject, rot1, 1f).id;
        else
            tweenId2 = LeanTween.rotate(this.gameObject, rot2, 1f).id;
    }

}
