using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BGM
{
    GamePlay,
    FeverTime
}

public enum SE
{
    Item,
    Gage,
    Cigarettes,
}
public class MusicManager : MonoBehaviour
{
    [SerializeField]
    AudioSource _bgmSource;
    [SerializeField]
    AudioSource _seSource;
    [SerializeField, Header("gameplay,fever")]
    AudioClip[] _bgmClips;
    [SerializeField, Header("Item,gage,tabako")]
    AudioClip[] _seClips;


    private void Awake()
    {
        GameManager.InstanceGM.MusicManagerSet(this);
    }
    private void Start()
    {
        GameManager.InstanceGM.ChangeState(GameManager.InstanceGM.State);
    }
    public void PlayBGM(BGM type)
    {
        if (_bgmSource.clip != _bgmClips[(int)type])
        {
            _bgmSource.clip = _bgmClips[(int)type];
            _bgmSource.Play();
        }
    }

    public void PlaySE(SE Type)
    {
        _seSource.clip = _seClips[(int)Type];
        _seSource.Play();
    }
}
