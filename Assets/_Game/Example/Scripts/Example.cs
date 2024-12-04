using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;

public class Example : MonoBehaviour
{
    [SerializeField] private GameStates _gameStates;

    [SerializeField] private PostProcessVolume _postProcessVolume;
    [SerializeField] private float _maxIntensity;
    [SerializeField] private float _speedIntensity;

    [SerializeField] private ExampleView _exampleView;
    [SerializeField] private ExamplesData _examplesData;

    [SerializeField] private float _errorResponse;

    [SerializeField] private ExampleButtonView[] _buttonViews;
    [SerializeField] private ExampleAnimatorController _animatorController;

    [SerializeField] private float _delayEnding;

    private List<ExampleButton> _buttons = new List<ExampleButton>();
    private ExampleConfig _currentConfig;

    private void Start()
    {
        _currentConfig = _examplesData.ExampleConfigs[Random.Range(0, _examplesData.ExampleConfigs.Length)];
        _exampleView.DisplayExample(_currentConfig.Example);

        InitializeButtons();

        _gameStates.OnChangeGameState += OnChangeGameState;
    }

    private void OnDestroy()
    {
        foreach (var item in _buttons)
        {
            item.OnCorrect -= OnCorrect;
        }

        _gameStates.OnChangeGameState -= OnChangeGameState;
    }

    private void InitializeButtons()
    {
        var indexCorrect = Random.Range(0, _buttonViews.Length);

        for (int i = 0; i < _buttonViews.Length; i++)
        {
            var isCorrect = i == indexCorrect;
            var button = new ExampleButton(_buttonViews[i], isCorrect, _currentConfig);

            var randomValue = Random.Range(_currentConfig.CorrectResponse / _errorResponse, _currentConfig.CorrectResponse * _errorResponse);

            if (_currentConfig.CorrectResponse % 1 == 0)
                randomValue = Mathf.Ceil(randomValue);

            _buttonViews[i].Initialize(isCorrect ? _currentConfig.CorrectResponse.ToString() : randomValue.ToString("#.##"), _exampleView);

            _buttons.Add(button);
            button.SetActive(false);

            button.OnCorrect += OnCorrect;
        }
    }

    private void OnCorrect()
    {
        _gameStates.SetGameState(EGameState.Ending);
        _exampleView.OnCorrect();
        StartCoroutine(Ending());

        foreach (var item in _buttonViews)
        {
            item.Restart();
        }

        StartCoroutine(Intensity());
    }

    private IEnumerator Intensity()
    {
        var bloom = _postProcessVolume.profile.GetSetting<Bloom>();

        while (bloom.intensity < _maxIntensity)
        {
            bloom.intensity.value += _speedIntensity * Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator Ending()
    {
        yield return new WaitForSeconds(_delayEnding);
        _animatorController.OnEnded();
    }
    
    private void OnChangeGameState(EGameState gameState)
    {
        foreach (var item in _buttons)
        {
            item.SetActive(gameState == EGameState.Selecting);
        }
    }
}
