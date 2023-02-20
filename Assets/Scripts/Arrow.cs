using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rigidbody;
    public MoveScript player;
    public float speed=5f;

    // Start is called before the first frame update
    void Start()
    {
        
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.up * speed;
    }
    private void FixedUpdate()
    {
        rigidbody.velocity = Vector3.up * speed;
    }

    // Update is called once per frame
    int count = 0;
    private void OnTriggerEnter(Collider collision)
    {
        var ball = collision.GetComponent<ball_scritp_3d>();
        if (ball != null)
        {
            
            if (count>0)
            {
                return;
            }
            count++;
            //destroy ball
            if (ball.enabled)
            {
                ball.PopOutDestroy();
            }
            else
            {
                Destroy(ball.gameObject);
            }
            
            player.ArrowDestroyed();
            Destroy(this.gameObject);
        }
        else
        {
            player.ArrowDestroyed();
            Destroy(this.gameObject);
        }
    }
}
