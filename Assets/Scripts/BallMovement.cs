using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BallMovement : MonoBehaviour
{
    public GameObject ballsToSpawn;
    public float thisMuchToTheSides = 1.5f;
    public float thisMuchUPUPUP = 1.5f;
    public BallSize mySize = BallSize.BIG;
    Rigidbody2D rigidbody2D;
    public Collider2D collider2D;
    public float speed;

    public LayerMask ceillingsMask;
    public LayerMask wallsMask;
    public LayerMask floorMask;
    public LayerMask playerMask;
    public float topSpeed;
    SpriteRenderer spriteRenderer;
    public SpriteRenderer Mysprite { get{  return spriteRenderer; } }

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();


    }
    private void Start()
    {
        rigidbody2D.velocity = Vector2.left * speed;
    }
    int count = 0;

    private void Update()
    {
        if (count==1)
        {
            if (rigidbody2D.velocity.y < 0)
            {
                count++;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (count == 2)
        {
            Destroy(rigidbody2D);
            Destroy(this);
        }
        if (rigidbody2D.velocity.y > topSpeed)
        {
            rigidbody2D.velocity *= Vector2.right + Vector2.up * (1 - 0.1f);
        }
        if (Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Abs(speed))
        {
            rigidbody2D.velocity *= Vector2.right * (1 - 0.01f) + Vector2.up;
        }
        //if (left.IsTouchingLayers(wallsMask))
        //{
        //    Debug.LogWarning("wallleft");
        //    speed = -startingSpeed;
        //}
        //if (right.IsTouchingLayers(wallsMask))
        //{
        //    Debug.LogWarning("wallRight");
        //    speed = startingSpeed;
        //}
        //if (collider2D.IsTouchingLayers(floorMask))
        //{
        //    Debug.LogWarning("floor");
        //}
        //if (collider2D.IsTouchingLayers(playerMask))
        //{
        //    Debug.LogWarning("killPlayer");
        //}

        //rigidbody2D.velocity = Vector2.left * speed;
    }

    public Rigidbody2D GetRigidbody{ get { return rigidbody2D; } }
    public void PopOutDestroy()
    {
        switch (mySize)
        {
            case BallSize.BIG:
                SpawnNewBalls();
                break;
            case BallSize.MEDIUM:
                SpawnNewBalls();
                break;
            case BallSize.SMALL:
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }

    private void SpawnNewBalls()
    {
        var ball = Instantiate(ballsToSpawn, transform.position + Vector3.left * thisMuchToTheSides, Quaternion.identity);
        var ballSript = ball.GetComponent<BallMovement>();
        ballSript.GetRigidbody.AddRelativeForce(Vector2.up*thisMuchUPUPUP, ForceMode2D.Force);
        ballSript.speed *= -1f;
        ballSript.Mysprite.color = Mysprite.color;
        ball = Instantiate(ballsToSpawn, transform.position + Vector3.right * thisMuchToTheSides, Quaternion.identity);
        ballSript = ball.GetComponent<BallMovement>();
        ballSript.GetRigidbody.AddRelativeForce(Vector2.up*thisMuchUPUPUP, ForceMode2D.Force);
        ballSript.spriteRenderer.color = spriteRenderer.color;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var isIt= collider2D.IsTouchingLayers(ceillingsMask);
        if (isIt)
        {
            Destroy(this.gameObject);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        count++;
    }
}
