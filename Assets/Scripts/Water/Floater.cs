using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody rb;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    public float increaseHeight;

    private void FixedUpdate()
    {
        float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x);
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, (WaveManager.instance.GetWaveHeight(transform.position.x)/ 14.28572f)  + increaseHeight, this.transform.localPosition.z);

        //if (transform.position.y < waveHeight)
        //{
        //    float displacementMultiplier = Mathf.Clamp01((waveHeight-transform.position.y)/depthBeforeSubmerged) * displacementAmount;
        //    //rb.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f));
        //    this.transform.position = new Vector3(this.transform.position.x, WaveManager.instance.GetWaveHeight(transform.position.x), this.transform.position.z);

        //}
    }


}
