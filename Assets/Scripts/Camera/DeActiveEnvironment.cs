using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActiveEnvironment : MonoBehaviour
{

    private void Update()
    {
        if (CameraFollow.instance.CamBehindCar && !GetComponent<BoxCollider>().enabled)
        {
            GetComponent<BoxCollider>().enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeActiveEnv"))
        {
           
            other.gameObject.SetActive(false);
            GameManager.instance.PoolRoad();
        }
        if (other.CompareTag("Block"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
