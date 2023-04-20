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
    void FixedUpdate()
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
            Acceleration();

            if (Input.GetKey(KeyCode.A) && !LimittedByLeftSide)
            {
                LeftTurn();

            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                LeftTurnEnd();
                StartCoroutine(RotationReseterEnum());
            }
            if (Input.GetKey(KeyCode.D) && !LimittedByRightSide)
            {
                RightTurn();
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                RightTurnEnd();
                StartCoroutine(RotationReseterEnum());
            }
            
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            Brake();
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

        if (forwardIsPressed)
        {
            transform.DOMoveZ(transform.position.z + 4f, 1).SetEase(Ease.Linear);
        }


        if (rightBtnPressed)
        {
            LeftBtnPressed = false;
            print("right checked");
            transform.DOMoveX(transform.position.x + 1f, 1);
            carBody.transform.DOLocalRotate(new Vector3(0, 10, 0), 0.5f);
            StartCoroutine(RotationReseterEnum());
        }
        if (LeftBtnPressed)
        {
            rightBtnPressed = false;
            transform.DOMoveX(transform.position.x - 1f, 1);
            carBody.transform.DOLocalRotate(new Vector3(0, -10, 0), 0.5f);
            StartCoroutine(RotationReseterEnum());
        }
    }

    public void ResetRotation()
    {
        StartCoroutine(RotationReseterEnum());
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


    bool forwardIsPressed = false;
    public void Acceleration()
    {
        if (CameraFollow.instance.camReachedtoPos == false)
        {
            CameraFollow.instance.camReachedtoPos = true;
            canStartEngine = true;
        }
        forwardIsPressed = true;
        //transform.DOLocalRotate(new Vector3(transform.localRotation.x, 0, transform.localRotation.z), 0);
    }

    public void Brake() 
    {
        forwardIsPressed = false;
        StartCoroutine(RotationReseterEnum());
        StartCoroutine(killTweenerEnum());
    }
    bool LeftBtnPressed = false;

    public void LeftTurn()
    {
        if (forwardIsPressed)
        {
            LeftBtnPressed = true;
        }
    }
    public void LeftTurnEnd()
    {
        LeftBtnPressed = false;
    }
    bool rightBtnPressed = false;
    public void RightTurn()
    {
        if (forwardIsPressed)
        {
            rightBtnPressed = true;
        }
    }
    public void RightTurnEnd()
    {
        rightBtnPressed = false;
    }
  
}
