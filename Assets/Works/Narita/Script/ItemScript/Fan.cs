using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : ItemBase
{
    int _gageScore = 1;
    public override void ItemAction()
    {
        GameManager.InstanceGM._uiManager.AddFanValue(_gageScore);
        Destroy(gameObject);
        //GameManagerのゲージ加算関数を呼び、引数に自身が持つ値セット。
    }
}
