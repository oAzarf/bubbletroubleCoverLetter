using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExercicePlan : MonoBehaviour
{
    [SerializeField]
    float x, y, frequenci;
    [SerializeField]
    float amplitudeMin, amplitudeMax, amplitudeNow;

    float startX;
    // Start is called before the first frame update
    
    private void Start()
    {
        startX = transform.position.x;
        amplitudeNow = amplitudeMax;
    }

    private float Coseno(float value)
    {
        return Mathf.Cos(value);
    }
    private float Seno(float value)
    {
        return Mathf.Sin(value);
    }
    public bool now = false;
    public bool scaleup = false;
    [SerializeField]
    List<Vector2> dots=new List<Vector2>();
    float theta = 2 * Mathf.PI;


    public float rightM, rightS, leftM, leftS;

    bool Go;

    public void GOGO()
    {
        Go=true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!Go)
        {
            Debug.DrawRay(Vector2.zero, Vector3.forward*100,Color.red,50f);
            return;
        }
        //x = Time.time-Mathf.Sin(Time.time*frequenci)* amplitudeNow;
        //y = amplitudeMin-Mathf.Cos(Time.time*frequenci)*amplitudeNow;
        //y = transform.position.y;
        //x = 2 + 4 * Coseno(theta);
        //y = 4- 4 * Seno(theta);
        //transform.position = new Vector2(x, y);
        //if (!dots.Contains(transform.position))
        //{
        //    dots.Add(transform.position);
        //}
        //if (theta>0)
        //{
        //    theta = frequenci;
        //}
        //if (dots.Count==0)
        //{
        //    Debug.Log("one only");
        //}
        //foreach (var item in dots)
        //{
        //    Debug.DrawRay(item, Vector3.forward);
        //}

        //if (transform.position.x-0.01<startX && transform.position.x + 0.01 > startX)
        //{
        //    now = true;
        //    //amplitudeNow = amplitudeMin;
        //    Debug.Log("Go");
        //}
        //if (now)
        //{
        //    if (amplitudeNow>amplitudeMin)
        //    {
        //        Debug.Log("Srinking");
        //        amplitudeNow -= 0.01f;
        //    }
        //    else
        //    {
        //        amplitudeNow = amplitudeMin;
        //        now = false;
        //    }
            
        //}
        //if (scaleup)
        //{
        //    if (amplitudeNow < amplitudeMax)
        //    {
        //        Debug.Log("Srinking");
        //        amplitudeNow += 0.01f;
        //    }
        //    else
        //    {
        //        amplitudeNow = amplitudeMin;
        //        scaleup = false;
        //    }

        //}

    }

    //private void OnGUI()
    //{
    //    OnGUI
    //}
}
