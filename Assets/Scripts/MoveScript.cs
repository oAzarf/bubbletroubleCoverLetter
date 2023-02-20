
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleInputNamespace;
using UnityEngine.SceneManagement;



public class MoveScript : MonoBehaviour
{
    public GameObject Joystic;
    public List<GameObject> cameras;
    public GameObject gameObjectARROW;
    public Transform shootPoint;
    private Rigidbody rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public float speed=5f;
    public BoxCollider2D left;
    public BoxCollider2D right;
    public Collider forward;

    public float facingRight = 0;
    const string IDLE="Idle";
    const string MOVE="Move";
    bool canGoRight = true;
    bool canGoLEft = true;

    string currentState=IDLE;
    private bool shooted=false;

    public bool Mode3D_On = false;
    public static MoveScript player;
    private void Awake()
    {
        player = this;
    }

    public bool simulateWeb;
    // Start is called before the first frame update
    void Start()
    {
        if (Mode3D_On)
        {
            Mode3D();
        }
        animator = GetComponent<Animator>();
        if (animator==null)
        {
            Debug.LogWarning("não tem animator");
        }
        rigidbody2D = GetComponent<Rigidbody>();
        if (rigidbody2D==null)
        {
            gameObject.AddComponent<Rigidbody>();
            rigidbody2D = GetComponent<Rigidbody>();
        }
        spriteRenderer = GetComponent<SpriteRenderer>();

        Debug.LogWarning(Application.platform);

        if (simulateWeb || (Application.platform!= RuntimePlatform.WindowsPlayer && Application.platform != RuntimePlatform.WindowsEditor))
        {
            Joystic.SetActive(true);
        }
        else
        {
            Joystic.SetActive(false);
        }

    }

    // Update is called once per frame

    bool pause = false;
    void Update()
    {
        if (Time.timeScale==0)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q) && !Mode3D_On)
        {
            Mode3D();
        }

        if (shooted)
        {
            return;
        }
        canGoRight = canGoLEft = true;
        if (left.IsTouchingLayers())
        {
            Debug.LogWarning("esquerda");
            canGoLEft = false;
        }
        if (right.IsTouchingLayers())
        {
            Debug.LogWarning("direita");
            canGoRight = false;
        }

        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit info= new RaycastHit();
        if (forward.Raycast(ray,out info, 100f))
        {
            Debug.Log(info.collider);
        }

        float facingRight = Input.GetAxisRaw("Horizontal");
        float moveForward = Input.GetAxisRaw("Vertical");

        if (simulateWeb)
        {
            facingRight = Joystick.instance.xAxis.value>0? 1: Joystick.instance.xAxis.value < 0 ? -1 :0;
            moveForward = Joystick.instance.yAxis.value > 0 ? 1 : Joystick.instance.yAxis.value < 0 ? -1 : 0;
        }
        else
        {
            facingRight = Input.GetAxisRaw("Horizontal");
            moveForward = Input.GetAxisRaw("Vertical");

        }

        
        if (facingRight !=0 || moveForward!=0)
        {
            ChangeAnimState(MOVE);
            bool moveMe = false;
            if (facingRight <0)
            {
                spriteRenderer.flipX = true;
                if (canGoLEft)
                {
                    moveMe = true;
                }
            }
            else
            {
                spriteRenderer.flipX = false;
                if (canGoRight)
                {
                    moveMe = true;
                }
            }
            if (moveMe)
            {
                rigidbody2D.velocity = Vector3.right * facingRight*speed;

            }
            else
            {
                rigidbody2D.velocity = Vector2.zero;
            }
            if (Mode3D_On)
            {
                Mode3D();
                if (moveForward != 0)
                {
                    ChangeAnimState(MOVE);
                    moveMe = false;
                    if (moveForward < 0)
                    {
                        //spriteRenderer.flipX = true;
                        //if (canGoLEft)
                        //{
                        //    moveMe = true;
                        //}
                        moveMe = true;
                    }
                    else
                    {
                        //spriteRenderer.flipX = false;
                        //if (canGoRight)
                        //{
                        //    moveMe = true;
                        //}
                        moveMe = true;
                    }
                    if (moveMe)
                    {
                        Debug.LogWarning("MEXE");
                        rigidbody2D.velocity = new Vector3(facingRight, 0,moveForward).normalized* speed;

                    }
                }
            }


        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
            ChangeAnimState(IDLE);
        }

        if (Input.GetButton("Jump"))
        {
            if (FindObjectOfType<Arrow>()!=null)
            {
                return;
            }
            rigidbody2D.velocity = Vector2.zero;
            StartCoroutine(ShootedNowWait());
            ChangeAnimState(IDLE);
            shooted = true;
            var a =Instantiate(gameObjectARROW, shootPoint.position,Quaternion.identity);
            var script = a.GetComponent<Arrow>();
            script.player = this;
        }
    }

    public IEnumerator ShootedNowWait()
    {
        
        yield return new WaitForSeconds(0.125f);
        shooted = false;
    }
    public void ArrowDestroyed()
    {
        StopCoroutine(ShootedNowWait());
        shooted = false;
    }
    public bool once = false;
    public void Mode3D()
    {
        if (once)
        {
            return;
        }
        Mode3D_On = true;
        cameras[0].SetActive(false) ;
        cameras[1].SetActive(true);

    }

    private void ChangeAnimState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("bateu");
        string date = System.DateTime.Now.ToString();
        date = date.Replace("/", "-");
        date = date.Replace(" ", "_");
        date = date.Replace(":", "-");
        var path = Application.dataPath;
        path += "/ScreenShoots/ole.png";

        GameManager.instace.GameLost(path);
        
    }
        

        
    



    private void OnCollisionEnter(Collision collision)
    {

        //var a =collision.gameObject.GetComponent<ball_scritp_3d>();
        //if (a!=null)
        //{

        //}
       
    }
}
