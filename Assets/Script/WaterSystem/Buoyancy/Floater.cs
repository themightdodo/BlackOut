using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody body;
    public float depthBefreSubmerged = 1F;
    public float displacementAmount = 3f;



    private void FixedUpdate()
    {
        float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x);
        if(transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01(waveHeight - transform.position.y / depthBefreSubmerged) * displacementAmount;
            body.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), ForceMode.Acceleration);
          
        }
    }
}
