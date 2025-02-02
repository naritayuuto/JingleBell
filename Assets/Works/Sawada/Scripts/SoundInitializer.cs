using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInitializer : MonoBehaviour
{
    [SerializeField]
    int soundNum = 0;
    [SerializeField, Tooltip("サウンドの情報を持つScriptableObject")]
    SoundScriptable _soundScriptableObj = null;

    private void Awake()
    {
        if (GameManager.InstanceSM.SoundDataBase == null)
        {
            GameManager.InstanceSM.SoundDataBase = _soundScriptableObj;
        }
        GameManager.InstanceSM.CallSound(SoundType.BGM, soundNum);
        Destroy(gameObject);
    }
}
