using UnityEngine;

public class PlayerController : MonoBehaviour, AgentBrain
{
    [Header ("SO")]
    public AgentSO AgentData;

    [Header("Components")]
    protected AgentMovement _agentMovement;
    protected AgentAnimator _agentAnimator;
    protected AgentSmash _agentSmash;

    [Header("Other")]
    [SerializeField] private Vector3 _resetPos;

    [Header("Input")]
    [SerializeField] private InputReaderSO _inputReader;

    private void Awake() 
    {
        _agentMovement = GetComponent<AgentMovement>();
        _agentAnimator = transform.Find("Visual").GetComponent<AgentAnimator>();
        _agentSmash = GetComponentInChildren<AgentSmash>();
    }

    private void FixedUpdate() 
    {
        if (GameManager.Instance)
        {
            if (!GameManager.Instance.CanMove)
                return;
        }

        _agentMovement.Movement(_inputReader.DirectionX);
        _agentAnimator.RunAnim(_inputReader.DirectionX != 0);
    }
    
    private void OnEnable() 
    {
        _inputReader.Jump += _agentMovement.Jump;
        _inputReader.SmashStart += _agentSmash.SmashStart;
        _inputReader.SmashEnd += _agentSmash.SmashEnd;
    }

    private void OnDisable() 
    {
        _inputReader.Jump -= _agentMovement.Jump;
        _inputReader.SmashStart -= _agentSmash.SmashStart;
        _inputReader.SmashEnd -= _agentSmash.SmashEnd;
    }

    public void Reset()
    {
        transform.position = _resetPos;
        _agentMovement.Reset();
    }

#region AgentBrain

    public AgentSO GetAgentSO() => AgentData;
    public AgentMovement GetAgentMovement() => _agentMovement;
    public AgentAnimator GetAgentAnimator() => _agentAnimator;
    public AgentSmash GetAgentSmash() => _agentSmash;

    public void RewardAdd(float value) { }
    public void EpisodeEnd() { }

    #endregion
}
