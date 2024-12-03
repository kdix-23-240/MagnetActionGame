using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetManager
{
    private static MagnetManager magnetManager = null;
    private MagnetPlayer magnetPlayer = new MagnetPlayer();
    //private MagnetStage magnetStage = new MagnetStage();
    int stageMagnetNum;
    private AreaCheck areaCheck = AreaCheck.GetInstance();
    private GameObject player;

    private MagnetManager(){}

    public void ManualStart()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public static MagnetManager GetInstance()
    {
        if (magnetManager == null)
        {
            magnetManager = new MagnetManager();
        }
        return magnetManager;
    }
    public void ChangeNS() // N,Sを変更するメソッド
    {
        //Debug.Log("おされてる");
        if(magnetPlayer.GetNSNum() == 0) 
        {
            magnetPlayer.SetNSNum(1);
            Material mat = player.GetComponent<Renderer>().material;
            mat.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            //Debug.Log(magnetPlayer.GetNSNum());
        }
        else 
        {
            magnetPlayer.SetNSNum(0);
            Material mat = player.GetComponent<Renderer>().material;
            mat.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            //Debug.Log(magnetPlayer.GetNSNum());
        }
    }

    public int NSChecker()  // 同極の場合 0 異極の場合 1 普通の床との場合 -1
    {
        stageMagnetNum = areaCheck.GetNSCheckNum();
        //Debug.Log(stageMagnetNum);
        //Debug.Log(magnetPlayer.GetNSNum());
        if(magnetPlayer.GetNSNum() == stageMagnetNum)
        {
            return 0;
            //Debug.Log(0);
        }
        else if((magnetPlayer.GetNSNum() == 1 && stageMagnetNum == 0) || (magnetPlayer.GetNSNum() == 0 && stageMagnetNum == 1))
        {
            //Debug.Log(5);
            return 1;
        }
        else 
        {
            //Debug.Log(15);
            return -1;
        }
    }

    public void SetStartNS()
    {
        magnetPlayer.SetNSNum(0);
    }

    public int GetNSNum()
    {
        return magnetPlayer.GetNSNum();
    }
}