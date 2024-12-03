using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCheck
{
    private bool isInArea;//エリアに入っているのかを保存する
    private static AreaCheck areaCheck = null;
    private GameObject[] floorMap;
    private GameObject[] ceilingMap;
    private GameObject[] leftWallMap;
    private GameObject[] rightWallMap;
    bool b = false;
    int NSCheckNum;
    int JudgSquare;//床か天井か壁か判断するための変数

    private AreaCheck() { }
    public static AreaCheck GetInstance()//シングルトンのためのやつ
    {
        if (areaCheck == null)
            areaCheck = new AreaCheck();
        return areaCheck;
    }

    public void ManualStart()
    {
        floorMap = GameObject.FindGameObjectsWithTag("Floor");
        ceilingMap = GameObject.FindGameObjectsWithTag("Ceiling");
        leftWallMap = GameObject.FindGameObjectsWithTag("LeftWall");
        rightWallMap = GameObject.FindGameObjectsWithTag("RightWall");
    }

    public void ManualUpdate()
    {
        AreaChecker();
    }

    public void AreaChecker()//エリアに入っているのか取ってくる
    {
        isInArea = false;
        MapAreaCheck(floorMap);
        MapAreaCheck(ceilingMap);
        MapAreaCheck(leftWallMap);
        MapAreaCheck(rightWallMap);
    }

    public void MapAreaCheck(GameObject[] squareMap)
    {
        foreach (GameObject gameObject in squareMap)
        {
            if (!isInArea) b = true;
            isInArea = gameObject.GetComponent<Square>().AreaChecker();
            if (isInArea && b)
            {
                NSCheckNum = gameObject.GetComponent<MagnetStage>().GetNSNum();
                if (gameObject.tag == "Floor")
                {
                    JudgSquare = 1;
                }
                else if (gameObject.tag == "Ceiling")
                {
                    JudgSquare = 2;
                }
                else if (gameObject.tag == "LeftWall")
                {
                    JudgSquare = 3;
                }
                else if (gameObject.tag == "RightWall")
                {
                    JudgSquare = 4;
                }
            }
            b = false;
        }
    }

    public bool GetIsInArea()//ゲッター
    {
        return isInArea;
    }

    public int GetNSCheckNum()
    {
        return NSCheckNum;
    }

    public GameObject[] GetFloorMap()
    {
        return floorMap;
    }

    public GameObject[] GetCeilingMap()
    {
        return ceilingMap;
    }

    public GameObject[] GetLeftWallMap()
    {
        return leftWallMap;
    }

    public GameObject[] GetRightWallMap()
    {
        return rightWallMap;
    }

    public int GetJudgSquare()
    {
        return JudgSquare;
    }


}
