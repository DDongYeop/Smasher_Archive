using UnityEngine;

public class AgentAnimator : MonoBehaviour
{
    [Header("Components")]
    private Animator _animator;
    private AgentSmash _agentSmash;

    [Header("Parameters")]
    private int _runHash = Animator.StringToHash("IsRun");
    private int _hitHash = Animator.StringToHash("IsHit");
    private int _concededHash = Animator.StringToHash("Conceded");
    private int _scoreHash = Animator.StringToHash("Score");
    private int _defeatHash = Animator.StringToHash("Defeat");
    private int _winHash = Animator.StringToHash("Win");
    private int _blendHash = Animator.StringToHash("Blend");

    private void Awake() 
    {
        _animator = GetComponent<Animator>();
        _agentSmash = transform.GetComponentInChildren<AgentSmash>();
    }

    public void RunAnim(bool value) => _animator.SetBool(_runHash, value);
    public void SamshAnim(bool value) => _animator.SetBool(_hitHash, value);
    public void ConcededAnim() => _animator.SetTrigger(_concededHash);
    public void ScoreAnim() => _animator.SetTrigger(_scoreHash);
    public void DefeatAnim() => _animator.SetTrigger(_defeatHash);
    public void WinAnim() => _animator.SetTrigger(_winHash);
    public void BlendAnim(int value) => _animator.SetFloat(_blendHash, value);

    #region UnityEvent

    public void NotSmash() => _agentSmash.NotSmash();

    #endregion
}
