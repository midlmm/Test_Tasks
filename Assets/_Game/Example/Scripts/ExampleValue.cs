using TMPro;
using UnityEngine;

public class ExampleValue : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Initialize(string text)
    {
        _text.text = text;
    }
}
