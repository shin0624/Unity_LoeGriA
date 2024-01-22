using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCotroller : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;//접근지정자를 public으로 하거나 SerializeField를 사용하면 인스펙터에서 속도값을 수정 가능
                         // float _yAngle = 0.0f;//로테이션 조작을 위한 변수 생성

    bool _moveToDest = false;//_desPos 사용 여부(목적지로 이동하는지)
    Vector3 _destPos;//목적지

    float wait_run_ratio = 0;

    void Start()
    {
        Managers.Input.KeyAction -= OnKeyboard;//중복 호출을 방지하기 위한 마이너스
        Managers.Input.KeyAction += OnKeyboard;

        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    // 업데이트 메서드는 프레임 당 한번씩, 즉 1/60초마다 한번씩 실행되므로
    //실제 사용에 맞추기 위해 이전 프레임과 지금 프레임의 시간 차이(Time.deltaTime)를 이용한다
    //시간 * 속도 = 거리 를 이용
    void Update()
    {

#if WASD_Move_1
        //전후좌우 이동 : new Vector3(0.0f, 0.0f, 1.0f) 사용 또는 예약어 사용(월드좌표계)  
        // 로컬 좌표계(Player의 시선을 기준으로 한 전후좌우 : transform.TransformDirection(로컬->월드)
        //(월드->로컬) : InverseTransformDirection
        //ex)  transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed)(이동거리 * 현재 시간과 비례한 수치 delaTime * 속도)


        //if (Input.GetKey(KeyCode.W))//앞
        //     transform.Translate(Vector3.forward * Time.deltaTime * _speed);  //transform.Translate 를 쓰면 플레이어 시선 기준 좌표이동을 바로 가능하게 해줌

        // if (Input.GetKey(KeyCode.S))//뒤
        //    transform.Translate(Vector3.back * Time.deltaTime * _speed);

        //  if (Input.GetKey(KeyCode.A))//좌
        //     transform.Translate(Vector3.left * Time.deltaTime * _speed);

        //if (Input.GetKey(KeyCode.D))//우
        //     transform.Translate(Vector3.right * Time.deltaTime * _speed);

        //2. rotation 조작  
        //1)절대 회전값 고정(transform.eulerAngles 사용)
        // transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);//오일러 각 사용

        //2) +-delta (transform.Rotate 사용)
        //transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));

        //3)  Quaternion 사용--> Vector3 이용 시 발생할 수 있는 짐벌록 예방
        //ex) transform.rotation = Quaternion.Euler(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));
        //Quaternion.Euler : Vector3값(오일러 각)을 넣으면 쿼터니언 값으로 변환해줌

        //-->원하는 특정 방향으로 돌아보게 할 때 : Quaternion.LookRotation 사용
        //-->원하는 방향으로 부드럽게 돌아보게 할 때, 즉 앞에서 우측을 볼 때 x축과 y축의 중간을 거쳐 돌아보게
        //-->Quaternion.Slerp 사용(매개변수 : 쿼터니언 a, b, float c), c에는 0.0f ~ 1.0f 사이 값을 넣는데, 0.0은 회전 불가, 1.0은 너무 빠른 회전이므로 중간 값을 적절히 찾아서 넣는다

        //전후좌우 GetKey if문을 수정

#endif

        if (_moveToDest)
        {
            Vector3 dir = _destPos - transform.position;//Click한 목적지 좌표 - Player좌표 = 이동할 방향벡터(방향+거리)
            if(dir.magnitude < 0.0001f)
            {
                _moveToDest = false;//Player가 destPos로 도착하면 다시 moveToDest를 false로 바꾼다.
            }
            else
            {
                float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude); //-->연산하는 거리가, 현재 Player와 destPos 간 거리보다 적어야함을 보장하는 Clamp(value, min, max)-->value값 정도에 따라 min,max로 자동조절
                //transform.position+= dir.normalized* _speed * Time.deltaTime;//(거리 = 속도*시간)
                transform.position+= dir.normalized* moveDist;


                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);//만약 Player회전이 느리다면 상수값을 20정도로 늘림

                //transform.LookAt(_destPos);//이동할 때 destPos 방향으로 시선 고정
            }
        
        
        }
        //애니메이션 적용

        if (_moveToDest)//Player가 dest까지 움직이고 있다면
        {
            wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10.0f * Time.deltaTime);//Lerp함수를 이용하여 Player움직임을 부드럽게 표현-->변수 wait_run_ratio가 10.0f * deltatime의 간격으로 조금씩 1에 가까워져 RUN 상태로.
            Animator anim = GetComponent<Animator>();//GetComponent로 Animator를 가져온다
            anim.SetFloat("wait_run_ratio", wait_run_ratio);//애니메이터에서 wait_run_ratio라는 이름으로 블렌딩한 파라미터(float)를 세트시켜준다. 1에 가까울수록 RUN애니메이션 실행
            anim.Play("WAIT_RUN");
        }
        else
        {
            wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 10.0f * Time.deltaTime);//Lerp함수를 이용하여 Player움직임을 부드럽게 표현-->변수 wait_run_ratio가 10.0f * deltatime의 간격으로 조금씩 0에 가까워져 WAIT상태로.
            Animator anim = GetComponent<Animator>();//GetComponent로 Animator를 가져온다
            anim.SetFloat("wait_run_ratio", wait_run_ratio);//애니메이터에서 wait_run_ratio라는 이름으로 블렌딩한 파라미터(float)를 세트시켜준다. 0에 가까울수록 WAIT애니메이션 실행--->Player의 움직임이 더 부드러워질 것.
            anim.Play("WAIT_RUN");
        }

    }

    void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.W))//앞     
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.1f);
            // transform.rotation = Quaternion.LookRotation(Vector3.forward);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.S))//뒤
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.1f);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.A))//좌 
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.1f);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))//우    
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.1f);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }

        _moveToDest = false;//키보드로 조작할 때는 마우스 클릭으로 이동처럼 destPos를 정하고 움직이지 않으므로 false.

    }

    void OnMouseClicked(Define.MouseEvent evt)//InputManager 스크립트에서 Action<Define.MouseEvent> MouseAction 으로 선언하였으므로, 마우스이벤트 객체를 인자로 넘겨줌
    {
        //---마우스 클릭, 클릭 유지 중에도 달리는 애니메이션이 재생될 수 있도록 주석처리
        //if (evt != Define.MouseEvent.Click)// 들어온 이벤트가 Click이 아니면 무시
        //    return;

        //--TestCollision에서 이동한 Raycasting 부분 --->LOL처럼, 마우스로 바닥을 클릭하면 클릭한 곳을 destPos로 하여 그곳까지 Player를 움직이게 할 것임.

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        RaycastHit hit;
      
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))//인자에 LayerMask를 바로 추가
        {
           
            _destPos = hit.point;//hit.point = Ray가 Hit한 컬라이더의 월드좌표. 이를 목적지로 하여 Player를 이동시킬 것
            _moveToDest = true;
            //-->Update()에서 목적지로 이동시키면 된다.
        }


    }
}
