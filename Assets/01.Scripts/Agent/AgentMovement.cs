using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    [Header("Components")]
    [HideInInspector] public AgentBrain Brain;
    private Rigidbody _rigidbody;

    [Header("Other")]
    [SerializeField] private Vector2 _limitXPos;

    private void Awake() 
    {
        TryGetComponent<AgentBrain>(out AgentBrain brain);
        Brain = brain != null ? brain : GetComponentInChildren<AgentBrain>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Movement(float x)
    {
        _rigidbody.velocity = new Vector3(x * Brain.GetAgentSO().Speed, _rigidbody.velocity.y, _rigidbody.velocity.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _limitXPos.x, _limitXPos.y), transform.position.y, transform.position.z);
    }

    public void Jump()
    {
        if (GroundCheck())
        {
            _rigidbody.AddForce(Vector3.up * Brain.GetAgentSO().JumpPower);
            PoolManager.Instance.Pop("JumpSound");
        }
    }

    private bool GroundCheck()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        bool isHitCourt = Physics.Raycast(ray, 0.1f, Brain.GetAgentSO().WhatIsCourt);
        bool isHitFloor = Physics.Raycast(ray, 0.1f, Brain.GetAgentSO().WhatIsFloor);

        return (isHitCourt || isHitFloor) && Mathf.Abs(_rigidbody.velocity.y) < 0.1f;
    }

    public void Reset()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}
