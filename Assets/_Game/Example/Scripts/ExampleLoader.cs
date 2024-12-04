using UnityEngine;
using System.Collections;

public class ExampleLoader : MonoBehaviour
{
    [SerializeField] private GameStates _gameStates;
    [SerializeField] private ExampleAnimatorController _animatorController;

    [SerializeField] private float _delayStarting;

    private void Start()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        _animatorController.OnStarted();
        yield return new WaitForSeconds(_delayStarting);
        _gameStates.SetGameState(EGameState.Selecting);
    }
}
