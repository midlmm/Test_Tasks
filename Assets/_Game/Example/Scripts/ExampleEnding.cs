using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;

public class ExampleEnding : MonoBehaviour
{
    [SerializeField] private GameStates _gameStates;

    [SerializeField] private ExampleAnimatorController _animatorController;

    [SerializeField] private PostProcessVolume _postProcessVolume;
    [SerializeField] private float _maxIntensity;
    [SerializeField] private float _speedIntensity;

    [SerializeField] private float _delayEnding;
    [SerializeField] private float _timeEnding;

    private Bloom _bloom;
    private float _startIntensity;

    private void Start()
    {
        _bloom = _postProcessVolume.profile.GetSetting<Bloom>();
        _startIntensity = _bloom.intensity.value;

        _gameStates.OnChangeGameState += OnChangeGameState;
    }

    private void OnDestroy()
    {
        _gameStates.OnChangeGameState += OnChangeGameState;
    }

    private void OnChangeGameState(EGameState gameState)
    {
        if (gameState != EGameState.Ending)
            return;

        StartCoroutine(Ending());
    }

    private IEnumerator Ending()
    {
        yield return new WaitForSeconds(_delayEnding);
        _animatorController.OnEnded();
        DOTween.To(() => _bloom.intensity.value, x => _bloom.intensity.value = x, _maxIntensity, _speedIntensity);
        yield return new WaitForSeconds(_timeEnding);
        _gameStates.SetGameState(EGameState.Loading);
        DOTween.To(() => _bloom.intensity.value, x => _bloom.intensity.value = x, _startIntensity, _speedIntensity);
    }
}
