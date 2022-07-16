using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// This script changes Scene, return to start and finish game 
public class ChangeScene : MonoBehaviour
{
    //"Scene in build" number array
    public int[] SceneNumber;

    //i is the 'i'th one in SceneNumber array 
    public void OnButtonClick(int i)
    {
        SceneManager.LoadScene(SceneNumber[i]);
    }

    public void Leave()
    {
         Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.H))
        {
            //print H return to the Scene "Home" 
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown("escape"))
        {
            //print ESC to quit the game      
            Application.Quit();
        }
    }

    public AudioSource ass;
    public Story s;
    public void StartStory()
    {
        s.StartS();
        Destroy(ass);
        Destroy(this);
    }

    public GameObject TeamObject;
    public void SpawnTeam()
    {
        Instantiate(TeamObject, transform);
    }
}
