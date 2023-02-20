using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola2 : MonoBehaviour
{
    // Start is called before the first frame update
    private float t;
    private float x;
    private float y;
    private bool check;


    void Start()
    {
        transform.position = new Vector2(3, 9);
        t = 3f;
        check = true;
    }

    // Update is called once per frame
    void Update()
    {
        //-----------------decreasing-----------------------
        if(t>-3 && check)
        {
            t -= 0.01f;
            x = t;
            y = t * t;
            transform.position = new Vector2(x, y);
        }
        //-----------------switch-------------------------
        if (t <= -3) { check = false; }
        //-----------------increasing-----------------------------
        if(t<=3 && !check)
        {
            t += 0.01f;
            x = t;
            y = t * t;
            transform.position = new Vector2(x, y);
        }
        //------------------switch-------------------
        if (t > 3) { check = true; }
    }
}
