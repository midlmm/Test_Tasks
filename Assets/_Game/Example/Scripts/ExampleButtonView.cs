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
    [SerializeField] private float _timeSlerp;
    [SerializeField] private float _speedSlerp;

    private ExampleView _exampleView;
    private RectTransform _rectTransformText;

    public void Initialize(string value, ExampleView exampleView)
    {
        _text.text = value;
        _exampleView = exampleView;

        _rectTransformText = _text.GetComponent<RectTransform>();
    }

    public void Click()
    {
        OnClick?.Invoke();
    }

    public void SetInteractable(bool isActive)
    {
        _button.interactable = isActive;
    }

    public void Clear()
    {
        _rectTransformText.anchoredPosition = Vector2.zero;
        _image.DOColor(Color.white, _timeFade);
        _text.gameObject.SetActive(true);
    }

    public void DisplayCorrectButton(bool isCorrect, float position)
    {
        _image.color = isCorrect ? _colorCorrect : _colorWrong;

        if (isCorrect)
        {
            _text.gameObject.SetActive(false);
            SpawnValue(position);
        }
        else
        {
            _rectTransformText.DOAnchorPos(new Vector2(_rectTransformText.anchoredPosition.x - _offset, _rectTransformText.anchoredPosition.y), _timeFade);
        }
    }

    public void SpawnValue(float position)
    {
        var value = Instantiate(_prefabValue, transform.parent);
        value.GetComponent<ExampleValue>().Initialize(_text.text);

        value.anchoredPosition = _rectTransform.anchoredPosition;
        StartCoroutine(Move(_rectTransform.anchoredPosition, new Vector2(_exampleView.Panel.anchoredPosition.x + position, _exampleView.Panel.anchoredPosition.y), value));
    }

    private IEnumerator Move(Vector2 start, Vector2 target, RectTransform value)
    {
        float timePassed = 0f;

        while (timePassed < _timeSlerp)
        {
            timePassed += Time.deltaTime;

            value.anchoredPosition = Vector3.Slerp(value.anchoredPosition, target, _speedSlerp);

            yield return null;
        }
    }
}

