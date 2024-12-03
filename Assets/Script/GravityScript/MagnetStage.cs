using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetStage : MonoBehaviour
{
   [SerializeField]private int nsNum;  // 0をN極、1をS極で考えてる

   public int GetNSNum()
   {
      return nsNum;
   }
}