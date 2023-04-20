using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerInput : MonoBehaviour
{
    //for ensuring that player starts the game
    bool canStartEngine = false;
    [SerializeField]
    GameObject carBody;

    // checking for avoiding weird behaviour
    bool LimittedByLeftSide = false;
    bool LimittedByRightSide = false;

    Rigidbody rb;


    private void Start()
    {
        rb= GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            CameraFollow.instance.camReachedtoPos= true;
            canStartEngine= true;
        }
        if (!canStartEngine) {

            return;
        }
        else if(Input.GetKey(KeyCode.W))
        {
            transform.DOMoveZ(transform.position.z + 2f, 1);
            transform.DOLocalRotate(new Vector3(transform.localRotation.x, 0, transform.localRotation.z), 0);

                if (Input.GetKey(KeyCode.A) && !LimittedByLeftSide)
                {
                    transform.DOMoveX(transform.position.x - 1f, 1);
                    carBody.transform.DOLocalRotate(new Vector3(0, -10, 0), 0.5f);
                    StartCoroutine(RotationReseterEnum());

                }
                else if (Input.GetKeyUp(KeyCode.A))
                {
                    StartCoroutine(RotationReseterEnum());
                }
                if (Input.GetKey(KeyCode.D) && !LimittedByRightSide)
                {
                    transform.DOMoveX(transform.position.x + 1f, 1);
                    carBody.transform.DOLocalRotate(new Vector3(0, 10, 0), 0.5f);
                    StartCoroutine(RotationReseterEnum());
                }
                else if (Input.GetKeyUp(KeyCode.D))
                {
                    StartCoroutine(RotationReseterEnum());
                }
            
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            StartCoroutine(RotationReseterEnum());
            StartCoroutine(killTweenerEnum());
        }
        if(transform.position.x <= -8 )
        {
            LimittedByLeftSide = true;
            LimittedByRightSide = false;
        }
        else
        {
            LimittedByLeftSide = false;
        }
        if(transform.position.x >= 4.5f)
        {
            LimittedByRightSide = true;
            LimittedByLeftSide = false;
        }
        else
        {
            LimittedByRightSide = false;
        }
    }
    IEnumerator RotationReseterEnum()
    {
        yield return new WaitForSeconds(0.5f);
        carBody.transform.DOLocalRotate(Vector3.zero, 1f);
    }
    IEnumerator killTweenerEnum()
    {
        yield return new WaitForSeconds(1);
        transform.DOKill();

    }
}
