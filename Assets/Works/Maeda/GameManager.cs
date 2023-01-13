using UniRx;
public enum GameState
{
    WaitGame,
    PlayGame,
    Fevar,
    Finish
}
public class GameManager
{
    static GameManager _instanceGM = new GameManager();

    static SoundManager _instanceSM = new SoundManager();

    public UiManager _uiManager = default;
    //static SceneManager _instanceScene = null;

    GameState _gameState = GameState.PlayGame;

    public GameState State { get => _gameState; set => _gameState = value; }

    public static SoundManager InstanceSM => _instanceSM;

    public static GameManager InstanceGM
    {
        get
        {
            if(_instanceGM == null)
            {
                _instanceGM = new GameManager();
            }
            return _instanceGM;
        }
    }
    public void UIManagerSet(UiManager ui)
    {
        _uiManager = ui;
    }

    public void SoundManagerSet(SoundManager sound)
    {
        _instanceSM = sound;
    }
    public void ChangeState(GameState gameState)
    {
        _gameState = gameState;
        switch (gameState)
        {
            case GameState.WaitGame:
                break;

            case GameState.PlayGame:
                _instanceSM.CallSound(SoundType.BGM,0);
                break;

            case GameState.Fevar:
                _instanceSM.CallSound(SoundType.BGM,3);
                break;

            case GameState.Finish:
                break;
        }
    }
}


