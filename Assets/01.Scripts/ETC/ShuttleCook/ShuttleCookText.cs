using UnityEngine;

public class ShuttleCookText : MonoBehaviour
{
    [SerializeField] private Vector3 _minPos;
    [SerializeField] private Vector3 _maxPos;

    private float _currentTime = 0;
    private RectTransform rect;

    private void Awake() 
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update() 
    {
        _currentTime += Time.deltaTime * 5;
        Movement();
    }

    private void Movement()
    {
        float t = Mathf.Sin(_currentTime);
        t = (t + 1) * 0.5f;

        rect.localPosition = Vector3.Lerp(_minPos, _maxPos, t);
    }
}
