using UnityEngine;

[CreateAssetMenu(menuName = "SO/Agent/AgentData", fileName = "AgentData")]
public class AgentSO : ScriptableObject
{
    [Header ("Agent")]
    public float Speed;
    public float JumpPower;
    public float Smash;
    public float PowerSmash;

    public LayerMask WhatIsCourt;
    public LayerMask WhatIsFloor;
}
