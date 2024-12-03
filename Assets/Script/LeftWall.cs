using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWall : MonoBehaviour, Square
{
    //オブジェクトの座標
    float x;
    float y;
    //エリアの幅
    float height;
    //エリアの高さ
    [SerializeField] float width;
    [SerializeField] GameObject player;

    AreaCheck areaCheck;

    public void ManualStart()//オブジェクトの座標をフィールドに入れる
    {
        x = this.gameObject.transform.position.x-1;
        y = this.gameObject.transform.position.y;
        height = this.gameObject.transform.lossyScale.y;
        areaCheck = AreaCheck.GetInstance(); 
    }

    public bool AreaChecker()//プレイヤーがエリアに入っているか確認する
    {
        if(areaCheck.GetIsInArea())
            return true;

        
        //プレイヤーの座標
        float posX = player.transform.position.x;
        float posY = player.transform.position.y;
        //Debug.Log(x);
        if((y - (height / 2) <= posY && posY <= y + (height / 2)) && (x <= posX && posX<= x + width - 1))
        {
            //Debug.Log("入ってる");
            return true;
        }else{
            //Debug.Log("入ってない");
            return false;
        }
    }
}