using UnityEngine;

public class ExitQuestion : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader;
    private GameObject _userInterface;

    private void Awake() 
    {
        _userInterface = transform.GetChild(0).gameObject;
    }

    private void OnEnable() 
    {
        _inputReader.Esc += EscInput;
    }
    
    private void OnDisable() 
    {
        _inputReader.Esc -= EscInput;
    }

    private void EscInput()
    {
        if (SceneTransition.Instance.IsTransition)
            return;

        _userInterface.SetActive(!_userInterface.activeSelf);
        Time.timeScale = _userInterface.activeSelf ? 0 : 1;
    }

    public void YesButton()
    {
        Time.timeScale = 1;
        SceneTransition.Instance.SceneChange(1);
    }

    public void NoButton()
    {
        Time.timeScale = 1;
        _userInterface.SetActive(false);
    }
}
