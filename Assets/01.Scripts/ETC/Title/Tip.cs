using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tip : MonoBehaviour
{
    [SerializeField] private List<string> _tips = new List<string>();
    private TextMeshPro _text;

    private void Awake() 
    {
        _text = GetComponent<TextMeshPro>();
    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(1))
            TipChange();
    }

    private void TipChange()
    {
        int index = Random.Range(0, _tips.Count);
        _text.text = _tips[index];
    }
}
