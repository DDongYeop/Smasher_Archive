using UnityEngine;

public class AgentSmash : MonoBehaviour
{
    [Header ("Agent Smash")]
    private float _currentTime;
    private float _currentPower;
    [SerializeField] private bool _isSmash = false;
    private bool _isUIPowerChange = false;
    [SerializeField] private bool _isRight;
    [SerializeField] private float _minPower;
    [SerializeField] private float _maxPower;

    [Header ("Component")]
    private AgentAnimator _agentAnimator;
    private AgentGauge _agentGauge;

    private void Awake() 
    {
        _agentAnimator = transform.root.GetChild(0).GetComponent<AgentAnimator>();
        _agentGauge = transform.root.GetComponent<AgentGauge>();
    }

    private void Update() 
    {
        _currentTime += Time.deltaTime * 5f;
        float t = (Mathf.Sin(_currentTime) +1) * .5f;

        if (_isUIPowerChange)
        {
            _currentPower = Mathf.Lerp(_minPower, _maxPower, t);
            _agentGauge.SliderValue((_currentPower - _minPower)/(_maxPower - _minPower));
        }
    }

    public void SmashStart()
    {
        _agentAnimator.SamshAnim(true);
        _agentGauge.CanvasEnable(true);
        _currentTime = 0;
        _isUIPowerChange = _isSmash = true;
    }

    public void SmashEnd()
    {
        _agentAnimator.SamshAnim(false);
        _isUIPowerChange = false;
    }

    public void NotSmash()
    {
        _currentTime = _currentPower = 0;
        _agentGauge.CanvasEnable(false);
        _isSmash = false;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Shuttlecock") && _isSmash)
        {
            Rigidbody rigid = other.GetComponent<Rigidbody>();
            rigid.velocity = Vector3.zero;
            rigid.AddForce(new Vector3(.5f * (_isRight ? -1f : 1f), .5f, 0) * _currentPower);

            PoolManager.Instance.Pop("HitSound");
            Transform particle = PoolManager.Instance.Pop("HitParticle").transform;
            particle.position = other.transform.position;
        }
    }
}
