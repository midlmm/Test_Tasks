using UnityEngine;
using System.Collections;

public class ExampleLoader : MonoBehaviour
{
    [SerializeField] private GameStates _gameStates;
    [SerializeField] private ExampleAnimatorController _animatorController;

    [SerializeField] private float _delayStarting;

    private void Start()
    {
        _gameStates.OnChangeGameState += OnChangeGameState;
    }

    private void OnDestroy()
    {
        _gameStates.OnChangeGameState -= OnChangeGameState;
    }

    private void OnChangeGameState(EGameState gameState)
    {
        if (gameState != EGameState.Loading)
            return;

        StartCoroutine(Starting());
    }

    private IEnumerator Starting()
    {
        _animatorController.OnStarted();
        yield return new WaitForSeconds(_delayStarting);
        _gameStates.SetGameState(EGameState.Selecting);
    }
}
