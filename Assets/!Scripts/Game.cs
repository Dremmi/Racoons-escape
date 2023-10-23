using UniRx;
using UnityEngine;
using Application = UnityEngine.Device.Application;

public readonly struct OnGameStartMessage
{ }

public readonly struct OnGamePauseMessage
{ }

public readonly struct OnPlayerDefeatedMessage
{ }

public enum GameState
{
    Pause,
    Action,
    Defeat
}
public class Game : MonoBehaviour
{
    private GameState _gameState = GameState.Pause;
    
    private BlockSpawner _blockSpawner;

    public GameState GetGameState()
    {
        return _gameState;
    }
    private void Start()
    {
        _blockSpawner = GetComponent<BlockStorageCreation>().BlockStorage;
        
        MessageBroker
            .Default
            .Receive<OnGameStartMessage>()
            .Subscribe(message => _gameState = GameState.Action);

        MessageBroker
            .Default
            .Receive<OnGamePauseMessage>()
            .Subscribe(message => _gameState = GameState.Pause);

        MessageBroker
            .Default
            .Receive<OnPlayerDefeatedMessage>()
            .Subscribe(message => _gameState = GameState.Defeat);
        
        MessageBroker
            .Default
            .Receive<OnLoseScreenExitMessage>()
            .Subscribe(message => RestartGame());
    }

    private void RestartGame()
    {
        _blockSpawner.ReloadBlocks();
    }
}
