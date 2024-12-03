using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStartButton : MonoBehaviour
{
  public void OnClick() 
  {
    //Debug.Log("押した");
    //SceneManager.LoadScene("MagnetAction");
    //SceneManager.LoadScene("MagnetRun");
    //SceneManager.LoadScene("MagnetUp");
    SceneManager.LoadScene("TestStage");
  }
}
