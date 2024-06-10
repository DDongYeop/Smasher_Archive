using UnityEngine;

public class EndPanel : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader;

    private void OnEnable() 
    {
        _inputReader.Esc += GameEnd;
    }

    private void OnDisable() 
    {
        _inputReader.Esc -= GameEnd;
    }

    private void GameEnd()
    {
        SceneTransition.Instance.SceneChange(1);
    }
}
