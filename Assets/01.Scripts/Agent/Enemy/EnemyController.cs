using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEditor;

public class EnemyController : Agent, AgentBrain
{
    [Header ("SO")]
    public AgentSO AgentData;

    [Header("Components")]
    protected AgentMovement _agentMovement;
    protected AgentAnimator _agentAnimator;
    protected AgentSmash _agentSmash;

    [Header("Other")]
    [SerializeField] private Vector3 _resetPos;

    [Header ("ML-Agent")]
    [SerializeField] private bool _isLearning;
    private ShuttleCockMLAgent _shuttleCook;

    //초기 설정
    public override void Initialize()
    {
        _agentMovement = transform.parent.GetComponent<AgentMovement>();
        _agentAnimator = transform.parent.Find("Visual").GetComponent<AgentAnimator>();
        _agentSmash = transform.parent.GetComponentInChildren<AgentSmash>();
        if (_isLearning)
            _shuttleCook = FindObjectOfType<ShuttleCockMLAgent>();
    }
 
    //에피소드 종료 될때마다 세팅
    public override void OnEpisodeBegin()
    {
        if (_isLearning)
            _shuttleCook.SetStart();
    }
 
    //에이전트 행동을 설정
    public override void OnActionReceived(ActionBuffers actions)
    {
        if (!GameManager.Instance.CanMove)
            return;
        
        var discreteActions = actions.DiscreteActions;

        _agentMovement.Movement(discreteActions[0] - 1);
        _agentAnimator.RunAnim(discreteActions[0] != 1);

        if (discreteActions[1] == 1)
            _agentMovement.Jump();
        
        if (discreteActions[2] == 1)
            _agentSmash.SmashStart();
        else if (discreteActions[2] == 2)
            _agentSmash.SmashEnd();
    }

    //사용자가 에이전트 행동을 직접 조절 
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreateActionsOut = actionsOut.DiscreteActions;

        discreateActionsOut[0] = (int)Input.GetAxisRaw("Horizontal") + 1;

        if (Input.GetKeyDown(KeyCode.Space))
            discreateActionsOut[1] = 1;

        if (Input.GetMouseButtonDown(0))
            discreateActionsOut[2] = 1;
        else if (Input.GetMouseButtonUp(0))
            discreateActionsOut[2] = 2;
    }

    public void Reset()
    {
        _agentMovement.Reset();
        transform.parent.position = _resetPos;
    }

#region AgentBrain

    public AgentSO GetAgentSO() => AgentData;
    public AgentMovement GetAgentMovement() => _agentMovement;
    public AgentAnimator GetAgentAnimator() => _agentAnimator;
    public AgentSmash GetAgentSmash() => _agentSmash;

    public void RewardAdd(float value) { AddReward(value); }
    public void EpisodeEnd() { EndEpisode(); }

#endregion
}

// 셔틀콕 랜덤으로 날림
// 
// 시간 * 0.5f로 곱해서 리워드 
// if (바닥에 충돌)
// {
//      리워드 주기 (1)
//      다시 날림 
// }
// else if (그라운드 충돌)
// {
//      -리워드 주기 (-2)
//      End
// }
// else if (채에 맞고 그라운드에 출돌)
// { 
//      리워드 주기 (2)
//      다시 날림 
// }
// else if (채에 맞고 바닥에 출돌)
// { 
//      -리워드 주기 (-1)
//      End
// }


/*
mlagents-learn "D:\Dev\GitHub\ML-Agent_Badminton\ml-agents-release_20\config\ppo\Badminton.yaml" --run-id=Badminton --results-dir="D:\Dev\GitHub\ML-Agent_Badminton\ml-agents-release_20\results"
*/