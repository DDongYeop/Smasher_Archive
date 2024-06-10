using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [Header("Camera")]
    [SerializeField] private List<CinemachineVirtualCamera> _cameras;
    private CameraType _cameraType;

    private void Awake() 
    {
        if (Instance != null)
            Debug.LogError("Multipel CameraManager is running");
        Instance = this;
    }

    private void Start() 
    {
        SetCamera(CameraType.TARGETGROUP);
    }

    private void SetCamera(CameraType type)
    {
        foreach(var cam in _cameras)
            cam.gameObject.SetActive(false);

        _cameraType = type;
        _cameras[(int)type].gameObject.SetActive(true);
    }

    public void WinCam(Transform trm)
    { 
        SetCamera(CameraType.WIN);
        StartCoroutine(WinCamCo(trm));
    }

    private IEnumerator WinCamCo(Transform trm)
    {
        //7.5
        _cameras[(int)CameraType.WIN].Follow = trm;
        _cameras[(int)CameraType.WIN].LookAt = trm;

        CinemachineTransposer transposer = _cameras[(int)CameraType.WIN].GetCinemachineComponent<CinemachineTransposer>();
        float currentTime = 0;

        while (true)
        {
            yield return null;
            currentTime += Time.deltaTime * 1.5f;

            float t = (Mathf.Sin(currentTime) + 1.0f) * .5f; 
            transposer.m_FollowOffset = new Vector3(Mathf.Lerp(-6.5f, 6.5f, t), 3f, -10f);
        }
    }
}
