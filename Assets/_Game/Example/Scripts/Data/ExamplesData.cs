using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Exaples", fileName = "ExaplesData")]
public class ExamplesData : ScriptableObject
{
    public ExampleConfig[] ExampleConfigs => _exampleConfigs;

    [SerializeField] private ExampleConfig[] _exampleConfigs;
}
