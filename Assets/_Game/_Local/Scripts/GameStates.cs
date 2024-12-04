using System;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public event Action<EGameState> OnChangeGameState;

    public void SetGameState(EGameState gamaState)
    {
        switch (gamaState)
        {
            case EGameState.Loading:
                break;
            case EGameState.Selecting:
                break;
            case EGameState.Ending:
                break;
            default:
                break;
        }
        OnChangeGameState?.Invoke(gamaState);
    }
}
