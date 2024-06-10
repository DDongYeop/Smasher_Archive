using UnityEngine;

public class ShuttleCockMLAgent : MonoBehaviour
{
    [SerializeField] private Vector3 _startPos;

    private EnemyController _enemyController;
    private Rigidbody _rigidbody;
    private bool _isCollision = false;

    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody>();
        _enemyController = FindObjectOfType<EnemyController>();
    }

    private void Update() 
    {
        _enemyController.RewardAdd(Time.deltaTime * .25f);
    }

    public void SetStart()
    {
        _rigidbody.velocity = _rigidbody.angularVelocity = Vector3.zero;
        _isCollision = false;

        transform.position = _startPos;
        transform.rotation = Quaternion.identity;

        _rigidbody.AddForce(new Vector2(0.5f, 0.5f) * Random.Range(560.0f, 850.0f));
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (_isCollision)
            return;
        _isCollision = true;

        if (other.transform.CompareTag("Court"))
        {
            if (transform.position.x >= 0) //상대가 내 코드에 안착 
            {
                _enemyController.RewardAdd(-5);
                _enemyController.EpisodeEnd();
            }
            else //내가 쳐서 상대 코드에 안착
            {
                _enemyController.AddReward(5);
                SetStart();
            }
        }
        else if (other.transform.CompareTag("Floor"))
        {
            if (transform.position.x >= 0) //플레이어가 울 바닥 넘김 
            {
                _enemyController.AddReward(1);
                SetStart();
            }
            else //플레이어쪽 바닥으로 넘어감 
            {
                _enemyController.RewardAdd(-2);
                _enemyController.EpisodeEnd();
            }
        }
        else 
            _isCollision = false;
    }
}
