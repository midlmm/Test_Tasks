using System.Collections;
using TMPro;
using UnityEngine;

public class ExampleValue : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _time;

    public void Initialize(string text)
    {
        _text.text = text;

        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(_time);
        Destroy(gameObject);
    }
}
