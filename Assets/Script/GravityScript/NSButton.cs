using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSButton
{
    MagnetManager magnetManager;

    public void ManualStart()
    {
        magnetManager = MagnetManager.GetInstance(); //シングルトンにしてる
    }

    public void ManualUpdate() // ChangeNSを呼び出したい,よべた
    {
        if(Input.GetKeyDown(KeyCode.J)||Input.GetKeyDown(KeyCode.U)||Input.GetKeyDown(KeyCode.I)||Input.GetKeyDown(KeyCode.K)||Input.GetKeyDown(KeyCode.M)||Input.GetKeyDown(KeyCode.Y) ||Input.GetKeyDown(KeyCode.N) ||Input.GetKeyDown(KeyCode.H))
        {
            magnetManager.ChangeNS(); 
        }
        
    }
}