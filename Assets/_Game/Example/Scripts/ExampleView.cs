using TMPro;
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ExampleView : MonoBehaviour
{
    public RectTransform Panel => _panelExample;

    [SerializeField] private TMP_Text _text;

    [SerializeField] private RectTransform _panelExample;
    [SerializeField] private float _delayStarting;
    [SerializeField] private float _delayEnding;
    [SerializeField] private float _timeFade;

    [SerializeField] private string _symbol;

    private Vector2 _startScalePanel;

    private void Start()
    {
        _startScalePanel = _panelExample.sizeDelta;
    }

    public void DisplayExample(string text)
    {
        StartCoroutine(OpenPanel(text));
    }

    private IEnumerator OpenPanel(string text)
    {
        _text.text = text;
        _text.alpha = 0;
        yield return new WaitForSeconds(_delayStarting);
        _panelExample.DOSizeDelta(new Vector2(_text.preferredWidth, _panelExample.sizeDelta.y), _timeFade).OnComplete(() => _text.DOFade(1, _timeFade));
    }

    private IEnumerator ClosePanel()
    {
        yield return new WaitForSeconds(_delayEnding);
        _text.DOFade(0, _timeFade).OnComplete(() => _panelExample.DOSizeDelta(_startScalePanel, _timeFade));
    }

    public void OnCorrect()
    {
        _text.text = _text.text.Replace(_symbol, "  ");
        StartCoroutine(ClosePanel());
    }
}
