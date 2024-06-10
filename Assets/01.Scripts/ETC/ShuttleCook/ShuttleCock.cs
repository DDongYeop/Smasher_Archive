using UnityEngine;
 
public class ShuttleCock : MonoBehaviour
{
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private bool _isTitle;

    private GameObject _posText;
    private Rigidbody _rigidbody;
    private bool _isCollision = false;

    private void Awake() 
    {
        _posText = transform.Find("Text").gameObject;
        _rigidbody = GetComponent<Rigidbody>();

        SetStart(-1);
    }

    private void Update() 
    {
        ShuttleCockRotation();
    }

    private void ShuttleCockRotation()
    {
        Vector2 pos = _rigidbody.velocity + transform.position;
        float angle = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }

    public void SetStart(float x)
    {
        _isCollision = _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _posText.SetActive(true);

        Vector3 pos = _startPos;
        pos.x *= x;
        transform.position = pos;
        transform.rotation = Quaternion.identity;
    }

    public void ShuttleStart() => _isCollision = false;

    private void OnTriggerEnter(Collider other) 
    {
        _rigidbody.useGravity = true;
        _posText.SetActive(false);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (_isCollision || _isTitle) 
            return;

        if (other.transform.CompareTag("Court"))
        {
            if (transform.position.x >= 0)
                ScoreManager.Instance.AddScore(-1);
            else 
                ScoreManager.Instance.AddScore(1);
            _isCollision = true;
        }
        else if (other.transform.CompareTag("Floor"))
        {
            if (transform.position.x >= 0)
                ScoreManager.Instance.AddScore(1);
            else 
                ScoreManager.Instance.AddScore(-1);
            _isCollision = true;
        }
    }
}
