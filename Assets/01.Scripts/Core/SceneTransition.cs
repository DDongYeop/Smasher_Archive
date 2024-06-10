using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance = null;

    [SerializeField] private float _transitionTime;
    private Material _transitionMat;
    private int _shaderValue = Shader.PropertyToID("_Value");
    private bool _isTransition = false;
    public bool IsTransition => _isTransition;

    private void Awake() 
    {
        if (Instance != null)
            Debug.LogError("Multiple SceneTransition is running");
        Instance = this;
    }

    private void Start() 
    {
        _transitionMat = GetComponent<SpriteRenderer>().material;
        StartCoroutine(SceneChangeCo(0, 1.5f, () =>{}));
    }

    public void SceneChange(int index)
    {
        if (_isTransition)
            return;

        if (index == -1)
        {
            StartCoroutine(SceneChangeCo(1.5f, 0, () => 
            {
                
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }));
        }
        else 
            StartCoroutine(SceneChangeCo(1.5f, 0, () => SceneManager.LoadScene(index)));
    }

    private IEnumerator SceneChangeCo(float start, float end, Action action)
    {
        _isTransition = true;
        float currentTime = 0;

        while (currentTime < _transitionTime)
        {
            yield return null;
            currentTime += Time.deltaTime;
            float t = currentTime / _transitionTime;
            t *= t * t;

            _transitionMat.SetFloat(_shaderValue, Mathf.Lerp(start, end, t));
        }
        _transitionMat.SetFloat(_shaderValue, end);
        action?.Invoke();
        _isTransition = false;
    }
}
