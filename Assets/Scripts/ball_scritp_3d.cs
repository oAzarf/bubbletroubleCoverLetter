using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BallSize { None,BIG, MEDIUM, SMALL }
public  enum randomWay {frente, esquerda, direita, traz,tamanho }
public class ball_scritp_3d : MonoBehaviour
{
    public UnityEvent<ball_scritp_3d> spawnEvent = new UnityEvent<ball_scritp_3d>();
    public UnityEvent<ball_scritp_3d> destroyEvent = new UnityEvent<ball_scritp_3d>();
    public ParticleSystem spanParticle;
    Rigidbody Rigidbody;
    public float topY;
    public float force;
    public GameObject ballsToSpawn;
    public float thisMuchToTheSides = 1.5f;
    public float thisMuchUPUPUP = 1.5f;
    public BallSize mySize = BallSize.BIG;
    Vector3 direct = Vector3.zero;
    public  bool spawned=false;
    public bool twoD;

    // Start is called before the first frame update
    void Awake()
    {
        Rigidbody=GetComponent<Rigidbody> ();
    }
    private void Start()
    {
        randomWay chosed = randomWay.direita;
        if (spawned)
        {
            if (twoD)
            {
                return;
            }
            else
            {
                int choosing = (int)randomWay.tamanho;
                chosed = (randomWay)Random.Range(0, choosing);
                DirectionChoosing(chosed);
            }
            return;
        }
        if (twoD)
        {
            chosed = randomWay.direita;
        }
        else
        {
            int choosing = (int)randomWay.tamanho;
            chosed = (randomWay)Random.Range(0, choosing);
        }
        if (false)
        {
            DirectionChoosing(randomWay.frente);
            return;
        }

        DirectionChoosing(chosed);
       
    }

    public void DirectionChoosing(randomWay chosed)
    {
        switch (chosed)
        {

            case randomWay.frente:
                direct = Vector3.forward;
                break;
            case randomWay.esquerda:
                direct = Vector3.left;
                break;
            case randomWay.direita:
                direct = Vector3.right;
                break;
            case randomWay.traz:
                direct = Vector3.back;
                break;
            default:
                break;
        }

        Rigidbody.AddForce(direct * force, ForceMode.VelocityChange);
    }

    // Update is called once per frame


    public bool Mode3D = false;
    void FixedUpdate()
    {
        
        if (Rigidbody.velocity.y > topY)
        {
            //Rigidbody.velocity *=   Vector2.right + Vector2.up * (1 - 0.01f);
            Rigidbody.velocity =  new Vector3(Rigidbody.velocity.x, topY, Rigidbody.velocity.z);
            
        }
        if (Rigidbody.velocity.x > 3f)
        {
            //Rigidbody.velocity *=   Vector2.right + Vector2.up * (1 - 0.01f);
            Rigidbody.velocity = new Vector3(3f, Rigidbody.velocity.y, Rigidbody.velocity.z);

        }
        if (Rigidbody.velocity.z > 3f && Mode3D)
        {
            //Rigidbody.velocity *=   Vector2.right + Vector2.up * (1 - 0.01f);
            Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, Rigidbody.velocity.y, 3f);

        }
        else if (Rigidbody.velocity.z !=0 && !Mode3D)
        {
            Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, Rigidbody.velocity.y, 0);
        }
    }

    public Rigidbody GetRigidbody { get { return Rigidbody; } }
    private void SpawnNewBalls()
    {
        if (twoD)
        {
            var ball = Instantiate(ballsToSpawn, transform.position + Vector3.left * thisMuchToTheSides, Quaternion.identity);
            var ballSript = ball.GetComponent<ball_scritp_3d>();
            ballSript.twoD = twoD;
            ballSript.Mode3D = Mode3D;
            ballSript.spawned = true;
            ballSript.GetRigidbody.velocity = Vector3.zero;
            ballSript.GetRigidbody.AddForce(Vector3.up * thisMuchUPUPUP, ForceMode.Force);
            ballSript.GetRigidbody.AddForce(Vector3.left * force, ForceMode.VelocityChange);
            spawnEvent.Invoke(ballSript);
            ball = Instantiate(ballsToSpawn, transform.position + Vector3.right * thisMuchToTheSides, Quaternion.identity);
            ballSript = ball.GetComponent<ball_scritp_3d>();
            ballSript.twoD = twoD;
            ballSript.Mode3D = Mode3D;
            ballSript.spawned = true;
            ballSript.GetRigidbody.velocity = Vector3.zero;
            ballSript.GetRigidbody.AddForce(Vector3.up * thisMuchUPUPUP, ForceMode.Force);
            ballSript.GetRigidbody.AddForce(Vector3.right * force, ForceMode.VelocityChange);
            spawnEvent.Invoke(ballSript);
            destroyEvent.Invoke(this);
        }
        else
        {
            var ball = Instantiate(ballsToSpawn, transform.position + Vector3.left * thisMuchToTheSides, Quaternion.identity);
            var ballSript = ball.GetComponent<ball_scritp_3d>();
            ballSript.twoD = twoD;
            ballSript.Mode3D = Mode3D;
            ballSript.spawned = true;
            ballSript.GetRigidbody.velocity = Vector3.zero;
            ballSript.GetRigidbody.AddForce(Vector3.up * thisMuchUPUPUP, ForceMode.Force);
            spawnEvent.Invoke(ballSript);
            ball = Instantiate(ballsToSpawn, transform.position + Vector3.right * thisMuchToTheSides, Quaternion.identity);
            ballSript = ball.GetComponent<ball_scritp_3d>();
            ballSript.twoD = twoD;
            ballSript.Mode3D = Mode3D;
            ballSript.spawned = true;
            ballSript.GetRigidbody.velocity = Vector3.zero;
            ballSript.GetRigidbody.AddForce(Vector3.up * thisMuchUPUPUP, ForceMode.Force);
            spawnEvent.Invoke(ballSript);
            destroyEvent.Invoke(this);
        }

        Destroy(this.gameObject);
    }
    public void PopOutDestroy()
    {
        var part = FindObjectOfType<ParticleSystem>();
        if (part==null)
        {
           var particle= Instantiate<ParticleSystem>(spanParticle,transform.position,Quaternion.identity);
            particle.transform.eulerAngles += (Vector3.right * -90);

        }
        else
        {
            part.transform.position = transform.position;
            part.Play();
        }
        switch (mySize)
        {
            case BallSize.BIG:
                SpawnNewBalls();
                break;
            case BallSize.MEDIUM:
                SpawnNewBalls();
                break;
            case BallSize.SMALL:
                destroyEvent.Invoke(this);
                Destroy(this.gameObject);
                break;
            default:
                Debug.LogError("erro");
                break;
        }
    }


    public LayerMask ceiling;
    private void OnCollisionEnter(Collision collision)
    {
        if (Mathf.Log(ceiling.value,2)== collision.gameObject.layer)
        {
            PopOutDestroy();
        }
    }
}
