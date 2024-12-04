using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class ExampleButtonView : MonoBehaviour
{
    public event Action OnClick;

    [SerializeField] private RectTransform _rectTransform;

    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;

    [SerializeField] private Color _colorCorrect;
    [SerializeField] private Color _colorWrong;

    [SerializeField] private float _timeFade;
    [SerializeField] private float _offset;

    [SerializeField] private RectTransform _prefabValue;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _duraction;

    private RectTransform _paneExample;

    public void Initialize(string value, ExampleView exampleView)
    {
        _text.text = value;
        _paneExample = exampleView.Panel;
    }

    public void Click()
    {
        OnClick?.Invoke();
    }

    public void SetInteractable(bool isActive)
    {
        _button.interactable = isActive;
    }

    public void Restart()
    {
        _image.DOColor(Color.white, _timeFade);
        _text.DOFade(0, _timeFade);
    }

    public void DisplayCorrectButton(bool isCorrect, float position)
    {
        _image.color = isCorrect ? _colorCorrect : _colorWrong;

        if (isCorrect)
        {
            _text.alpha = 0;
            SpawnValue(position);
        }
        else
        {
            var rectTransform = _text.GetComponent<RectTransform>();

            rectTransform.DOAnchorPos(new Vector2(rectTransform.anchoredPosition.x - _offset, rectTransform.anchoredPosition.y), _timeFade);
        }
    }

    public void SpawnValue(float position)
    {
        var value = Instantiate(_prefabValue, transform.parent);
        value.GetComponent<ExampleValue>().Initialize(_text.text);

        value.anchoredPosition = _rectTransform.anchoredPosition;
        StartCoroutine(Move(_rectTransform.anchoredPosition, new Vector2(_paneExample.anchoredPosition.x + position, _paneExample.anchoredPosition.y), value));
    }

    private IEnumerator Move(Vector2 start, Vector2 target, RectTransform value)
    {
        float timePassed = 0f;

        Vector2 end = target;

        while (timePassed < _duraction)
        {
            timePassed += Time.deltaTime;

            float linearT = timePassed / _duraction;
            float heightT = _curve.Evaluate(linearT);

            float height = Mathf.Lerp(0f, linearT, heightT);

            value.anchoredPosition = Vector2.Lerp(start, end, linearT) + new Vector2(0f, height);

            yield return null;
        }
    }
}

