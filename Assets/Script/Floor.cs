using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour,Square
{
    //オブジェクトの座標
    float x;
    float y;
    //エリアの幅
    float width;
    //エリアの高さ
    [SerializeField] float height;
    [SerializeField] GameObject player;

    AreaCheck areaCheck;

    public void ManualStart()//オブジェクトの座標をフィールドに入れる
    {
        x = this.gameObject.transform.position.x;
        y = this.gameObject.transform.position.y-1;
        width = this.gameObject.transform.lossyScale.x;
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
        if((x - (width / 2) <= posX  && posX <= x + (width / 2)) && (y <= posY && posY  <= y + height))
        {
            //Debug.Log("入ってる");
            return true;
        }else{
            //Debug.Log("入ってない");
            return false;
        }
    }
}
