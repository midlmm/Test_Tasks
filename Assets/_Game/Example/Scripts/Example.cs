using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Example : MonoBehaviour
{
    [SerializeField] private GameStates _gameStates;

    [SerializeField] private ExampleView _exampleView;
    [SerializeField] private ExamplesData _examplesData;

    [SerializeField] private float _errorResponse;

    [SerializeField] private ExampleButtonView[] _buttonViews;

    [SerializeField] private float _delayClearButtons;

    private List<ExampleButton> _buttons = new List<ExampleButton>();
    private ExampleConfig _currentConfig;

    private void Start()
    {
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

    private void OnChangeGameState(EGameState gameState)
    {
        if (gameState == EGameState.Selecting)
        {
            _currentConfig = _examplesData.ExampleConfigs[Random.Range(0, _examplesData.ExampleConfigs.Length)];
            _exampleView.DisplayExample(_currentConfig.Example);

            InitializeButtons();
        }

        foreach (var item in _buttons)
            item.SetActive(gameState == EGameState.Selecting);
    }

    private void InitializeButtons()
    {
        var indexCorrect = Random.Range(0, _buttonViews.Length);

        for (int i = 0; i < _buttonViews.Length; i++)
        {
            var isCorrect = i == indexCorrect;
            var button = new ExampleButton(_buttonViews[i], isCorrect, _currentConfig);

            var randomValue = Random.Range(_currentConfig.CorrectResponse / _errorResponse, _currentConfig.CorrectResponse * _errorResponse);
            var text = "";

            if (_currentConfig.CorrectResponse % 1 == 0)
            {
                randomValue = Mathf.Ceil(randomValue);
                text = randomValue.ToString();
            }
            else
            {
                text = randomValue.ToString("F2");
            }
                
            _buttonViews[i].Initialize(isCorrect ? _currentConfig.CorrectResponse.ToString() : text, _exampleView);

            _buttons.Add(button);
            button.SetActive(false);

            button.OnCorrect += OnCorrect;
        }
    }

    private void OnCorrect(float value)
    {
        _gameStates.SetGameState(EGameState.Ending);
        StartCoroutine(_exampleView.ClosePanel());
        StartCoroutine(ClearButtons());
        _exampleView.DisplayResponse();
    }

    private IEnumerator ClearButtons()
    {
        yield return new WaitForSeconds(_delayClearButtons);
        foreach (var item in _buttons)
            item.Clear();
    }
}
