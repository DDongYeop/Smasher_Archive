using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private float _waitTime;

    private bool _canMove = false;
    public bool CanMove => _canMove;
    private List<AgentBrain> _agents = new List<AgentBrain>();
    private ShuttleCock _shuttleCook;

    private void Awake() 
    {
        if (Instance != null)
            Debug.LogError("Multiple GameManager is running");
        Instance = this;
    }

    private void Start() 
    {
        var movements = FindObjectsOfType<AgentMovement>();
        foreach (var movement in movements)
            _agents.Add(movement.Brain);

        _shuttleCook = FindObjectOfType<ShuttleCock>();
        ResetPos(-1);
    }

    public void ResetPos(float pos, bool isEnd = false)
    {
        StartCoroutine(ResetPosCo(pos, isEnd));
    }

    public IEnumerator ResetPosCo(float pos, bool isEnd)
    {
        _canMove = false;
        foreach(var agent in _agents)
            agent.Reset();
        _shuttleCook.SetStart(pos);

        yield return new WaitForSeconds(_waitTime);

        if (!isEnd)
        {
            _canMove = true;
            _shuttleCook.ShuttleStart();
        }
    }
}
