using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyColor : MonoBehaviour
{
    /// <summary>
    /// マテリアルの色を変更するための変数
    /// </summary>
    [SerializeField] private float NRedNum;
    [SerializeField] private float NGreenNum;
    [SerializeField] private float NBlueNum;
    [SerializeField] private float SRedNum;
    [SerializeField] private float SGreenNum;
    [SerializeField] private float SBuleNum;
    [SerializeField] private GameObject LeftArm;
    [SerializeField] private GameObject RightArm;
    [SerializeField] private GameObject LeftLeg;
    [SerializeField] private GameObject RightLeg;
    [SerializeField] private GameObject Head;
    MagnetManager magnetManager = MagnetManager.GetInstance();
    private GameObject[] parts;

    public void ManualStart()
    {
        parts = new GameObject[] { LeftArm, RightArm, LeftLeg, RightLeg, Head };
    }
    /// <summary>
    /// マテリアルの色を変更するメソッド
    /// </summary>
    public void ManualUpdate()
    {
        Material mat = this.GetComponent<Renderer>().material;
        if (magnetManager.GetNSNum() == 0)
        {
            mat.color = new Color(NRedNum, NGreenNum, NBlueNum, 1.0f);
        }
        else
        {
            mat.color = new Color(SRedNum, SGreenNum, SBuleNum, 1.0f);
        }
        for (int i = 0; i < 5; i++)
        {
            mat = parts[i].GetComponent<Renderer>().material;
            if (magnetManager.GetNSNum() == 0)
            {
                mat.color = new Color(NRedNum, NGreenNum, NBlueNum, 1.0f);
            }
            else
            {
                mat.color = new Color(SRedNum, SGreenNum, SBuleNum, 1.0f);
            }
        }

    }
}