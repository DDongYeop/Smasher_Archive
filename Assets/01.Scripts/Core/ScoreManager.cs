using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance = null;

    [Header("Score")]
    [SerializeField] private int _agent01 = 0;
    [SerializeField] private int _agent02 = 0;

    [Header("Components")]
    private AgentAnimator _agent01Anim;
    private AgentAnimator _agent02Anim;

    [Header("UserInterface")]
    [SerializeField] private TextMeshProUGUI _agent01Score;
    [SerializeField] private TextMeshProUGUI _agent02Score;

    [Header("Other")]
    private GameObject _endPanel;

    private void Awake() 
    {
        if (Instance != null)
            Debug.LogError("Multiple SocreManager is running");
        Instance = this;
    }

    private void Start() 
    {
        _agent01Anim = GameObject.Find("Player").transform.Find("Visual").GetComponent<AgentAnimator>();
        _agent02Anim = GameObject.Find("Enemy").transform.Find("Visual").GetComponent<AgentAnimator>();
        _endPanel = GameObject.Find("End");
        _endPanel.SetActive(false);
    }

    /// <summary>
    /// Score 증가 시켜주는 함수
    /// </summary>
    /// <param name="agent">-1은 1번 agent, 1은 2번 agent</param>
    public void AddScore(int agent)
    {
        if (agent == -1)
        {
            ++_agent01;
            _agent01Anim.BlendAnim(Random.Range(0, 3));
            _agent01Anim.ScoreAnim();
            _agent02Anim.ConcededAnim();
        }
        else
        {
            ++_agent02;
            _agent02Anim.BlendAnim(Random.Range(0, 3));
            _agent02Anim.ScoreAnim();
            _agent01Anim.ConcededAnim();
        }

        _agent01Score.text = _agent01.ToString();
        _agent02Score.text = _agent02.ToString();
        
        GameManager.Instance.ResetPos(agent, VictoryCheck());
    }

    private bool VictoryCheck()
    {
        if (_agent01 >= 11 || _agent02 >= 11)
        {
            int value = _agent01 - _agent02;
            if (Mathf.Abs(value) >= 2)
            {
                // 승부가 남 
                if (_agent01 > _agent02)
                {
                    Debug.Log("Agent01 승리");
                    _agent01Anim.BlendAnim(Random.Range(0, 3));
                    _agent01Anim.WinAnim();
                    _agent02Anim.DefeatAnim();

                    CameraManager.Instance.WinCam(_agent01Anim.transform.root);
                }
                else 
                {
                    Debug.Log("Agent02 승리");
                    _agent02Anim.BlendAnim(Random.Range(0, 3));
                    _agent02Anim.WinAnim();
                    _agent01Anim.DefeatAnim();

                    CameraManager.Instance.WinCam(_agent02Anim.transform.root);
                }

                _endPanel.SetActive(true);
                GameObject.Find("Canvas").SetActive(false);
                return true;
            }
        }

        return false;
    }
}
