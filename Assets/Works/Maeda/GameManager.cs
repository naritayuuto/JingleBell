﻿using UniRx;

public class GameManager
{
    static GameManager _instanceGM;

    static SoundManager _instanceSM = new SoundManager();
    static SceneManager _instanceScene = null;


    ReactiveProperty<int> _touchCigarettes
        = new ReactiveProperty<int>();

    ReactiveProperty<float> _sumScore
        = new ReactiveProperty<float>();

    ReactiveProperty<float> _fanValue
        = new ReactiveProperty<float>();

    ReactiveProperty<float> _fevarValue
        = new ReactiveProperty<float>();

    public static GameManager InstanceGM => _instanceGM;

    public static SoundManager InstanceSM => _instanceSM;
    public static SceneManager InstanceScene => _instanceScene;

    public IReadOnlyReactiveProperty<int> TouchCigarettes => _touchCigarettes;

    public IReadOnlyReactiveProperty<float> SumScore => _sumScore;

    public IReadOnlyReactiveProperty<float> FanValue => _fanValue;

    public IReadOnlyReactiveProperty<float> FeverValue => _fevarValue;

    /// <summary>
    /// 煙草の接触を検知
    /// intで加算されたら検知判定
    /// </summary>
    public void AddCigarettes(int touchCigarettes)
    {
        _touchCigarettes.Value += touchCigarettes;
    }

    /// <summary>引数をスコアに加算する</summary>
    public void AddScore(float scoreValue)
    {
        _sumScore.Value += scoreValue;
    }

    /// <summary>引数を扇のカウントに加算</summary>
    public void AddFanValue(float fanVlue)
    {
        _fanValue.Value += fanVlue;
    }

    /// <summary>引数をフィーバー</summary>
    public void AddFevarValue(float fevarValue)
    {
        _fevarValue.Value += fevarValue;
    }

    public void ChangeState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.WaitGame:
                break;

            case GameState.PlayGame:
                break;

            case GameState.Fevar:
                break;

            case GameState.Finish:
                break;
        }
    }
}

public enum GameState 
{
    WaitGame,
    PlayGame,
    Fevar,
    Finish
}
