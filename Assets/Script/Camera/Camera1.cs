using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : MonoBehaviour {

    [SerializeField] Transform player; // プレイヤーのTransformをInspectorから入れる

    private void Update() {
        // カメラをプレイヤーの場所へ
        //transform.position = new Vector3(player.position.x + 5, player.position.y, -10f);//MagnetRun
        transform.position = new Vector3(player.position.x, player.position.y+1.5f, -10f);//MagnetUp
    }

    public void ManualStart()
    {
         // カメラをプレイヤーの場所へ
         transform.position = new Vector3(player.position.x, player.position.y, -10f);
     }

    public void ManualUpdate() {
        // カメラをプレイヤーの場所へ
         //transform.position.x = player.position.x;

        if(player.position.y - 3 >= this.transform.position.y)
        {
            this.transform.position = new Vector3(player.position.x, player.position.y/* / 3*/, -10f);
        }
        else
        {
            this.transform.position = new Vector3(player.position.x, this.transform.position.y, -10f);
        }
    }
}
