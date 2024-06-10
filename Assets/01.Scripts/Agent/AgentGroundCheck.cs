using UnityEngine;

public class AgentGroundCheck : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) 
    {
        if (other.transform.CompareTag("Court") && PoolManager.Instance)
        {
            Transform trm = PoolManager.Instance.Pop("FallParticle").transform;
            trm.position = transform.position + new Vector3(0, 0.15f, 0);
        }
    }
}
