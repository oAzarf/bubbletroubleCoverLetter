using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyGo : MonoBehaviour
{
    public Image image;
    public Text texto;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(REadyGo());
        Time.timeScale = 0;
    }

   IEnumerator REadyGo()
    {
        texto.text = "Ready";
        yield return new WaitForSecondsRealtime(1);
        texto.text = "3, 2, 1";
        yield return new WaitForSecondsRealtime(.6f);
        texto.text = "....2, 1";
        yield return new WaitForSecondsRealtime(.6f);
        texto.text = "........1";
        yield return new WaitForSecondsRealtime(.6f);
        texto.text = "Go";
        yield return new WaitForSecondsRealtime(.5f);
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(.2f);
        image.transform.parent.gameObject.SetActive(false);

    }
}
