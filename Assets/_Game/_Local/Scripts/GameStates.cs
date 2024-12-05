using System;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public event Action<EGameState> OnChangeGameState;

    private void Start()
    {
        SetGameState(EGameState.Loading);
    }

    public void SetGameState(EGameState gamaState)
    {
        switch (gamaState)
        {
            case EGameState.Loading:
                Debug.Log("Game State: Loading");
                break;
            case EGameState.Selecting:
                Debug.Log("Game State: Selecting");
                break;
            case EGameState.Ending:
                Debug.Log("Game State: Ending");
                break;
            default:
                break;
        }
        OnChangeGameState?.Invoke(gamaState);
    }
}
