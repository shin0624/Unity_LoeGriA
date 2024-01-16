using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
     Define.CameraMode _mode = Define.CameraMode.QuarterView;//Define에서 정의한 카메라모드 중 쿼터뷰를 기본으로 적용
    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);//Player 기준으로 얼마나 떨어져있는지에 대한 방향벡터
    [SerializeField]
    GameObject _player = null;//카메라가 적용될 플레이어

    
    void Start()
    {
        
    }

    
    void LateUpdate()//카메라 컨트롤 연산을 Update문에 넣으면, PlayerController의 Update문 내에 있는 버튼이벤트와 실행 순서가 섞임-->플레이어 이동 시 떨림 발생
        //LateUpdate()는 무조건 Update()문이 끝난 후에 실행되므로 실행 순서를 고정할 수 있다.
    {
        if (_mode == Define.CameraMode.QuarterView)
        {
            transform.position = _player.transform.position + _delta;//카메라 포지선 = 플레이어 포지션 + 방향벡터-->카메라가 플레이어를 따라 이동
            transform.LookAt(_player.transform);//LookAt()함수 : 카메라가 무조건 플레이어의 좌표를 주시하도록 함
        }

        
    }

    public void SetQuarterView(Vector3 delta)//QuarterView를 코드상으로 세팅하고자 할 때 사용
    {
        _mode = Define.CameraMode.QuarterView;
        _delta = delta;
    }
}
