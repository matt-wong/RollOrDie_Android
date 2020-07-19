using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuScript : MonoBehaviour
{
    public void PlayGame(){
        Debug.Log("PLAY GAME");
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
