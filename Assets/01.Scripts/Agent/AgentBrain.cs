public interface AgentBrain 
{
    public AgentSO GetAgentSO();
    public AgentMovement GetAgentMovement();
    public AgentAnimator GetAgentAnimator();
    public AgentSmash GetAgentSmash();
    public void Reset();
    public void RewardAdd(float value);
    public void EpisodeEnd();
}
