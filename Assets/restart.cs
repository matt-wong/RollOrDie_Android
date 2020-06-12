using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restart : MonoBehaviour
{
    public void RestartAction(){
        gameManager.Instance.Restart();
    }
}
