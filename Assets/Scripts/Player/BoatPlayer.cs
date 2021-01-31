using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatPlayer : MonoBehaviour
{
    public Transform boat;
    public Animator boatPlayerAnim;
    public Animator boatAnim;
    public Animator camAnim;

    public bool activated = false;

    public void FinishAbord()
    {
        boatPlayerAnim.enabled = false;
        transform.SetParent(boat);
        activated = true;
        transform.localPosition = new Vector3(0, 0.67f, -0.6f);
        boatAnim.enabled = true;
        camAnim.enabled = true;
    }

    private void Update()
    {
        if (activated)
        {
            transform.localPosition = new Vector3(0, 0.67f, -0.6f);
        }
    }
}
