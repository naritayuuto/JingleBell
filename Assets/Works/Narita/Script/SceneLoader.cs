using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    string _loadSceneName = "scene�̖��O";
    [SerializeField]
    Image _fadePanel = null;
    [SerializeField]
    float _fadetime = 1f;
    bool _loadStart = false;


    private void Start()
    {//�����瓧��
        StartFade();
    }
    // Update is called once per frame
    void Update()
    {
        if (_loadStart)//true��������
        {
            LoadPlay(_loadSceneName);
        }
    }
    void StartFade()
    {
        if(_fadePanel)
        _fadePanel.DOColor(Color.clear, _fadetime);
    }
    public void LoadScene()
    {//�����[�h�ȊO�͂��̊֐����Ă�
        _loadStart = true;
    }

    public void LeloadScene()
    {
        LoadPlay(SceneManager.GetActiveScene().name);
    }
    void LoadPlay(string sceneName)
    {//���̒��Ŏw�肳�ꂽ���O�̃V�[���ɔ�ԁB
        if (_fadePanel)
        {
            _fadePanel.DOColor(Color.black, _fadetime).OnComplete(() => SceneManager.LoadScene(sceneName));
            _loadStart = false;
            Debug.Log("�J�ڊ���");
        }
        else
        {
            SceneManager.LoadScene(sceneName);
            _loadStart = false;
        }
    }
}