using UnityEngine;

public class ShuttleCockPractice : MonoBehaviour
{
    [SerializeField] private Vector3 _startPos;

    private Rigidbody _rigidbody;

    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start() 
    {
        SetStart();
    }

    public void SetStart()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        transform.position = _startPos;
        transform.rotation = Quaternion.identity;

        _rigidbody.AddForce(new Vector2(-0.5f, 0.5f) * Random.Range(560.0f, 850.0f));
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.transform.CompareTag("Court"))
            SetStart();
        else if (other.transform.CompareTag("Floor"))
            SetStart();
    }
}
