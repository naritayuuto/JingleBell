using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;

public class UiManager : MonoBehaviour
{
    [SerializeField, Header("リザルトのキャンバス")]
    GameObject _resultCanvas;
    [SerializeField, Header("スコアを表示するテキスト")]
    Text _scoreText;
    [SerializeField, Header("ゲームの制限時間を表示するテキスト")]
    Text _timeText;
    [SerializeField, Header("煙のイメージ画像")]
    Image _smongImage;
    [SerializeField,Header("表示するスコア")]
    float _score = 0f;
    [SerializeField, Header("扇ゲージの値")]
    int _fanScore = 0;
    [SerializeField, Header("はゲージの値")]
    float _hagageScore = 0f;
    [SerializeField, Header("煙が出る時間")]
    float _smokeTime = 10f;
    [SerializeField, Header("スコアの変化にかける時間")]
    float _changeValueInterval;
    [SerializeField, Header("ゲームの制限時間")]
    float _gameTime = 60f;
    [SerializeField, Header("煙草を表示する時間")]
    float _smongTime = 5f;
    [SerializeField, Header("フィーバーを表示する時間")]
    float _fevarTime = 10f;
    bool isSmoke = false;
    bool _scoreCheck = false;
    //扇ゲージの最大
    const int _fanSliderValueMax = 3;
    //フィーバーゲージの最大
    const int _fevarSliderValueMax = 100;
    [SerializeField]
    Animator _hagageAnim;
    //ゲーム時間の計測用
    float _timer;
    //煙を表示している時間
    float _eventInterval;
    //フィーバーしている時間
    float _fevarTimer;
    //画像表示の計測用
    float _eventTimer = 0;
    //煙草の煙のアニメーション
    Animator _smongAni;
    public float FanSliderValueMax => _fanSliderValueMax;

    private void Awake()
    {
        //時間を表示
        _timeText.text = _gameTime.ToString("00");

        _smongAni = _smongImage.gameObject.GetComponent<Animator>();

        _resultCanvas.SetActive(false);

        GameManager.InstanceGM.UIManagerSet(this);
    }

    private void Update()
    {
        _gameTime -= Time.deltaTime;
        if (_gameTime >= 0)
        {
            _timeText.text = _gameTime.ToString("00");
        }
        else
        {
            if(!_scoreCheck)
            {
                //フェードアウトしてからが望ましい
                _resultCanvas.GetComponent<ResultChange>().Result((int)_score);
                _scoreCheck = true;
            }
        }
        if(isSmoke)
        {
            SmokeAppearance();
        }
        if (GameManager.InstanceGM.State == GameState.Fevar)
        {
            IndicateFevar();
        }
    }
    /// <summary>
    /// スコアをDotweenで動的に表示する
    /// </summary>
    public void ScoreInterpolation(float scoreValue)
    {
        float sliderValue = float.Parse(_scoreText.text);

        DOTween.To(() => sliderValue, // 連続的に変化させる対象の値
            x => sliderValue = x, // 変化させた値 x をどう処理するかを書く
            scoreValue, // x をどの値まで変化させるか指示する
            _changeValueInterval)
            .OnUpdate(() => _scoreText.text = sliderValue.ToString("000"));
    }
    /// <summary>
    /// フィーバー時の処理
    /// </summary>
    void IndicateFevar()
    {
        _fevarTimer += Time.deltaTime;
        if(_fevarTimer >= _fevarTime)
        {
            GameManager.InstanceGM.State = GameState.PlayGame;
            _fevarTimer = 0;
            _hagageScore = 0;
            _hagageAnim.SetFloat("hagageScore", _hagageScore);
        }
    }

    void SmokeAppearance()
    {
        _eventInterval += Time.deltaTime;
        if (_eventInterval < _smongTime)
        {
            _smongAni.SetBool("isSmoke", isSmoke);
        }
        else
        {
            isSmoke = false;
            _smongAni.SetBool("isSmoke", isSmoke);
        }
    }
    public void Fan()//Itemの効果
    {
        if (_fanScore >= _fanSliderValueMax)
        {
            if (_smongImage.enabled)
            {
                isSmoke = false;
                _smongAni.SetBool("isSmoke", isSmoke);
                _fanScore = 0;
            }
        }
    }
    /// <summary>
    /// 煙草の接触を検知したときに呼ぶ
    /// </summary>
    public void AddCigarettes()
    {
        if (!isSmoke)
            isSmoke = true;
        else
            _eventInterval = 0;
    }

    /// <summary>引数をスコアに加算する</summary>
    public void AddScore(float scoreValue)
    {
        _score += scoreValue;
        ScoreInterpolation(_score);
    }

    /// <summary>引数を扇のカウントに加算</summary>
    public void AddFanValue(int fanVlue)
    {
        if(_fanScore < _fanSliderValueMax)
        {
            _fanScore += fanVlue;
        }
    }

    /// <summary>引数をフィーバー</summary>
    public void AddFevarValue(float fevarValue)
    {
        if (GameManager.InstanceGM.State != GameState.Fevar || _hagageScore > _fevarSliderValueMax)
        {
            _hagageScore += fevarValue;
            _hagageAnim.SetFloat("hagageScore", _hagageScore);//アニメーション内でイラストを変更。
            if(_hagageScore >= _fevarSliderValueMax)
            {
                GameManager.InstanceGM.State = GameState.Fevar;
            }
        }
    }
}
