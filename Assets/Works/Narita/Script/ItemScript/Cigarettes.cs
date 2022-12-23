using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cigarettes : ItemBase
{
    public override void ItemAction()
    {
        GameManager.InstanceGM._uiManager.AddCigarettes();
        Destroy(gameObject);
        //GameManagerの関数を呼ぶ。
    }
}
