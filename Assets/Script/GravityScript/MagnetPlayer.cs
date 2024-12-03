using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPlayer
{
   protected int nsNum; // 0をN極、1をS極で考えてる

   public int GetNSNum() 
     {
          return nsNum;
     }

   public void SetNSNum(int nsNum) 
     {
          this.nsNum = nsNum;
     }
}
