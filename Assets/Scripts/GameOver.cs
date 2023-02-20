using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndGame
{

    public static string endgameScreen = Application.dataPath+ "/ScreenShoots/ole.png";
}

public class VectorOperator
{
    public static Vector3 VectorParamnsMultiplication(Vector3 vectorA, Vector3 vectorB)
    {
        
        return new Vector3(vectorA.x * vectorB.x, vectorA.y * vectorB.y, vectorA.z * vectorB.z);
    }
    public static Vector2 VectorParamnsMultiplication(Vector2 vectorA, Vector2 vectorB)
    {

        return new Vector2(vectorA.x * vectorB.x, vectorA.y * vectorB.y);
    }
}

public class GameOver : MonoBehaviour
{
    public static GameOver instace;

    public Image top,down;
    public Image endGameImage;
    private Sprite last_screenshot_save;

    private void Awake()
    {
        if (instace != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instace = this;
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        LoadImage();

        top.rectTransform.anchoredPosition =  Vector2.up* endGameImage.preferredHeight/2 + VectorOperator.VectorParamnsMultiplication (top.rectTransform.anchoredPosition, Vector2.right);
        down.rectTransform.anchoredPosition = Vector2.down * endGameImage.preferredHeight/2 +VectorOperator.VectorParamnsMultiplication (down.rectTransform.anchoredPosition, Vector2.right);
        
        
    }
    public void LoadImage()
    {
        string path = EndGame.endgameScreen;

        last_screenshot_save = LoadSprite(path);
        endGameImage.sprite = last_screenshot_save;
    }

    private Sprite LoadSprite(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;
        if (System.IO.File.Exists(path))
        {
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }
        return null;
    }
    public float movingAmount=0.1f;
    bool closed = false;

    public void CloseIt()
    {

        StartCoroutine(SlamDoors());
    }
    public void OpenIt(float waitTime)
    {
        StartCoroutine(OpenDoors(waitTime));
    }
    public  IEnumerator  SlamDoors()
    {
       yield return new WaitForEndOfFrame();
        top.rectTransform.anchoredPosition += Vector2.down * movingAmount;
        down.rectTransform.anchoredPosition += Vector2.up * movingAmount;
        if (top.rectTransform.anchoredPosition.y>=endGameImage.preferredHeight/2)
        {
            StartCoroutine(SlamDoors());
        }
        else
        {
            closed = true;
            
            endGameImage.gameObject.SetActive(false);
            StartCoroutine(OpenDoors(2f));
        }
    }
    public IEnumerator OpenDoors(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);

        if (closed)
        {
            GameManager.instace.playButton.transform.parent.gameObject.SetActive(true);
            closed = false;
        }

        top.rectTransform.anchoredPosition -= Vector2.down * movingAmount;
        down.rectTransform.anchoredPosition -= Vector2.up * movingAmount;
        if (top.rectTransform.anchoredPosition.y < endGameImage.preferredHeight )
        {
            StartCoroutine(OpenDoors(0f)); ;
        }
        else
        {
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
