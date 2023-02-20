

// UnComment bellow if you want to use limits by gameobjects location

//#define useTransformsDefinition


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    
public class HoldCameraHere : MonoBehaviour
{
    [SerializeField]
    bool useTransForms;
    // Start is called before the first frame update
    [SerializeField]
    float leftLimit, rightLimit,frontLimit,backLimit;


   

    
#if useTransformsDefinition
    [SerializeField]
    Transform leftLimitTransform, rightLimitTransform, frontLimitTransform, backLimitTransform;

    private void Awake()
    {
        leftLimit=leftLimitTransform.position.x;
        rightLimit=rightLimitTransform.position.x;

        frontLimit = frontLimitTransform.position.z;
        backLimit = backLimitTransform.position.z;

    }
#endif


    Vector3 rightLeftStop;
    Vector3 frontBackStop;
    Vector3 startLocalPos;
    MoveScript player;

   
    private void Start()
    {
        player = MoveScript.player;
        startLocalPos = transform.localPosition;

    }


    bool lockHereLeftRight=false;
    bool lockHereFrontBack=false;
    // Update is called once per frame
    void Update()
    {
        bool left, right, forwadr, backwards;

        left = player.transform.position.x < leftLimit;
        right = player.transform.position.x > rightLimit;
        forwadr = player.transform.position.z > frontLimit;
        backwards = player.transform.position.z < backLimit;



        if ((left && !right) || (!left && right))
        {
            if (!lockHereLeftRight)
            {
                lockHereLeftRight = true;
                rightLeftStop = transform.position;
            }
            transform.position = new Vector3(rightLeftStop.x, rightLeftStop.y,transform.position.z) ;
        }
        else
        {
            lockHereLeftRight = false;

        }
        if ((forwadr && !backwards) || (!forwadr && backwards))
        {
            if (!lockHereFrontBack)
            {
                lockHereFrontBack = true;
                frontBackStop = transform.position;
            }
            transform.position = new Vector3(transform.position.x, frontBackStop.y, frontBackStop.z);
        }
        else
        {
            lockHereFrontBack = false;
        }
    }
}
