using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstLevelController : MonoBehaviour
{
    public List<ball_scritp_3d> inGameObstacles;
    // Start is called before the first frame update

    private void Start()
    {
        foreach (var item in inGameObstacles)
        {
            item.spawnEvent.AddListener(AddToControllerList);
            item.destroyEvent.AddListener(RemoveFromControllerList);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inGameObstacles.Count==0)
        {
            NextLevel();
        }
    }
    public void AddToControllerList(ball_scritp_3d toAdd)
    {
        inGameObstacles.Add(toAdd);
        toAdd.spawnEvent.AddListener(AddToControllerList);
        toAdd.destroyEvent.AddListener(RemoveFromControllerList);
    }
    public void RemoveFromControllerList(ball_scritp_3d toAdd)
    {
        inGameObstacles.Remove(toAdd);
        toAdd.spawnEvent.RemoveAllListeners();
        toAdd.destroyEvent.RemoveAllListeners();
        //toAdd.gameObject);
    }
    public void Dosome()
    {
        
    }
    public void NextLevel()
    {
        GameManager.instace.StartLevel1();
    }
}
