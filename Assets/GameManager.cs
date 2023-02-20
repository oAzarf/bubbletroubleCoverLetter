using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public SceneManager scene;

    public static GameManager instace;
    public Button playButton;
    private Image playButtonImage;
    private void Awake()
    {
        if (instace!=null)
        {
            instace.playButton = playButton;
            //next, any of these will work:
            instace.playButton.onClick.AddListener(instace.PlayButton);
            GameOver.instace.LoadImage();
            instace.playButtonImage = instace.playButton.gameObject.GetComponent<Image>();
            instace.playButton.transform.parent.gameObject.SetActive(false);
            GameOver.instace.endGameImage.gameObject.SetActive(true);
            instace.StartCoroutine(GameOver.instace.SlamDoors());
            Destroy(gameObject);
        }
        else
        {
            instace = this;
            playButtonImage = playButton.gameObject.GetComponent<Image>();
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameOver.instace.OpenDoors(1f));
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (playButton!=null)
        {
            if (playButton.colors.selectedColor ==playButtonImage.color)
            {
                Debug.Log("Go;");
                
            }

        }
        
        
    }

    bool firstTime=true;
    public void PlayButton()
    {
        Debug.LogWarning($"Há {SceneManager.sceneCount} cenas loaded");
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        Debug.LogWarning($"Agora Há {SceneManager.sceneCount} cenas loaded");

    }
    public void GameLost(string where)
    {
        ScreenCapture.CaptureScreenshot(where);
        if (firstTime)
        {

            firstTime = false;
            //show insert mail

        }
        //GameOver.instace.CloseIt();
        SceneManager.LoadScene(2);

    }

    public void StartLevel1()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }


}
