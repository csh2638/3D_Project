using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    public Settings.CameraMode _CameraMode = Settings.CameraMode.QuarterView;
    [SerializeField]
    public Vector3 _delta = new Vector3(0.0f, 7.0f, -6.0f); // 플레이어와 카메라 위치관계
    [SerializeField]
    public GameObject _player = null;

    // Start is called before the first QuarterViewframe update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(_CameraMode == Settings.CameraMode.QuarterView)
        {
            RaycastHit hit;
            if(Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
            {
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
            }
            else
            {
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);
            }

        }
    }

    public void SetQuarterView(Vector3 delta)
    {
        _CameraMode = Settings.CameraMode.QuarterView;
        _delta = delta;
    }
}
