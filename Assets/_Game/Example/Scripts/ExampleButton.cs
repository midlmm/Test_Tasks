using System;

public class ExampleButton
{
    public event Action OnCorrect;

    private ExampleButtonView _view;
    private bool _isCorrect;
    private ExampleConfig _exampleConfig;

    public ExampleButton(ExampleButtonView view, bool isCorrect, ExampleConfig exampleConfig)
    {
        _view = view;
        _isCorrect = isCorrect;
        _exampleConfig = exampleConfig;

        _view.OnClick += ProcessingClick;
    }

    public void SetActive(bool isActive)
    {
        _view.SetInteractable(isActive);
    }
    
    private void ProcessingClick()
    {
        _view.OnClick -= ProcessingClick;

        _view.DisplayCorrectButton(_isCorrect, _exampleConfig.PositionResponseX);
        if (_isCorrect) OnCorrect.Invoke();
    }
}