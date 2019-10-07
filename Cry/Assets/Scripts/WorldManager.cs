using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    public GameObject poolmanager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void NextSceneGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void NextScenePlayground()
    {
        SceneManager.LoadScene("PlaygroundScene");
    }
}
