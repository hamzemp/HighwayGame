using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    static public CameraFollow instance;
    [SerializeField] private GameObject Player;

    public bool CamBehindCar = false;
    public bool camReachedtoPos = false;
    float Zdistance = 5f;
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        CamToPos();
    }
    void Update()
    {
        if (camReachedtoPos)
        {
            transform.DOMove(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z - Zdistance), 1).SetDelay(0.2f);
            transform.DOLookAt(Player.transform.position, 1).SetDelay(0.2f);
        }
        if (!CamBehindCar)
        {
            StartCoroutine(ActiveCamCollision());
        }
    }
    IEnumerator ActiveCamCollision()
    {
        yield return new WaitForSeconds(1);
        CamBehindCar = true;

    }
    void CamToPos()
    {
        transform.DOMove(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z - Zdistance), 3).SetDelay(3f);
        transform.DOLookAt(Player.transform.position, 1).SetDelay(1f);
    }
   
}
