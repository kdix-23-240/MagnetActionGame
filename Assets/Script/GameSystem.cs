using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    private MagnetManager magnetManager;
    private AreaCheck areaCheck;
    [SerializeField]GameObject player;
    //[SerializeField]GameObject camera;
    private NSButton nSButton = new NSButton();
    [SerializeField]GameObject body;
    private BodyColor bodyColor;


    void Start()
    {
        player.GetComponent<Player>().ManualStart();
        nSButton.ManualStart();
        areaCheck = AreaCheck.GetInstance();
        areaCheck.ManualStart();
        magnetManager = MagnetManager.GetInstance();
        magnetManager.ManualStart();
        //this.gameObject.GetComponent<Camera1>().ManualStart();

        GameObject[] floorMap= areaCheck.GetFloorMap();
        foreach(GameObject square in floorMap)
        {
            square.GetComponent<Square>().ManualStart();
        }

        GameObject[] ceilingMap= areaCheck.GetCeilingMap();
        foreach(GameObject square in ceilingMap)
        {
            square.GetComponent<Square>().ManualStart();
        }

        GameObject[] leftWallMap= areaCheck.GetLeftWallMap();
        foreach(GameObject square in leftWallMap)
        {
            square.GetComponent<Square>().ManualStart();
        }

        GameObject[] rightWallMap= areaCheck.GetRightWallMap();
        foreach(GameObject square in rightWallMap)
        {
            square.GetComponent<Square>().ManualStart();
        }
        bodyColor = body.GetComponent<BodyColor>();
        bodyColor.ManualStart();
    }


    void Update()
    {
        player.GetComponent<Player>().ManualUpdate();
        //magnetManager.ManualUpdate();
        areaCheck.ManualUpdate();
        nSButton.ManualUpdate();
        //this.gameObject.GetComponent<Camera1>().ManualUpdate();
        //camera.GetComponent<Camera1>().ManualUpdate();
        bodyColor.ManualUpdate();
    }

    void FixedUpdate()
    {
        player.GetComponent<Player>().ManualFixedUpdate();
    }
}
