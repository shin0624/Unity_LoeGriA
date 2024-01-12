using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCotroller : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;//접근지정자를 public으로 하거나 SerializeField를 사용하면 인스펙터에서 속도값을 수정 가능
   // float _yAngle = 0.0f;//로테이션 조작을 위한 변수 생성
    
    void Start()
    {
        Managers.Input.KeyAction -= OnKeyboard;//중복 호출을 방지하기 위한 마이너스
        Managers.Input.KeyAction += OnKeyboard;
    }

    // 업데이트 메서드는 프레임 당 한번씩, 즉 1/60초마다 한번씩 실행되므로
    //실제 사용에 맞추기 위해 이전 프레임과 지금 프레임의 시간 차이(Time.deltaTime)를 이용한다
    //시간 * 속도 = 거리 를 이용
    void Update()
    {
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
    }
}
