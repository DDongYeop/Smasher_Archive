using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionArea : MonoBehaviour
{
    [SerializeField] private CollisionType _type;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Shuttlecock"))
            Collision();
    }

    private void Collision()
    {
        switch (_type)
        {
            case CollisionType.START:
                SceneTransition.Instance.SceneChange(3);
                break;
            case CollisionType.PRACTICE:
                SceneTransition.Instance.SceneChange(2);
                break;
            case CollisionType.EXIT:
                SceneTransition.Instance.SceneChange(-1);
                break;
        }
    }
}
