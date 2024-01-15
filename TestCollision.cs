using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"collision! @{collision.gameObject.name}");//부딛힌 오브젝트의 이름을 출력
        //호출 조건
        //1. 나 혹은 상대에게 RigidBody가 있어야 함(isKinematic : off)
        //2. 나에게 Collider가 있어야 함(isTrigger : off)
        //3. 상대에게 Collider가 있어야 함(isTrigger : off) 

    }

    private void OnTriggerEnter(Collider other)
        //충돌 범위 내에 물체가 들어갔는지를 판단하는 것(물리와 상관없이)이 트리거
    {
        Debug.Log($"trigger! @{other.gameObject.name}");
        //호출 조건
        //1. 나와 상대에게 모두 Collider가 있어야 함
        //2. 둘 중 하나는 isTrigger : on
        //3. 둘 중 하나는 RigidBody가 있어야 함
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //RayCasting

        //Vector3.forward를 쓰면 z방향 한방향으로만 Ray가 발사되므로, Player가 바라보는 방향으로 Ray를 쏘기 위해 로컬좌표를 월드좌표로 변환해본다
        //transform.TransformDirection을 사용하여 방향 기준을 Player 시선으로 해준다
        Vector3 look = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);


        //Debug.DrawRay(transform.position + Vector3.up, transform.forward*10, Color.red);
        //DrawRay 인자 : 시작위치 Start, Ray크기 Start+direction, Ray의 컬러-->Start+dir 이기때문에 방향과 크기 모두 고려(forward단위벡터 크기 = 1)
        //그냥 transform.position만 넣으면 Player의 가장 아래(발에 해당)에서 Ray가 시작되므로 up (0,1,0)으로 시작위치를 올려준다


       RaycastHit hit;//Ray가 닿은 물체를 리턴할 변수-->out타입으로 연결될 것.

                       //if( Physics.Raycast(transform.position, Vector3.forward, out hit, 10 ))
                       //Vector3.origin : 시작좌표-->Player의 위치 / Vector3.direction : 내가 원하는 방향-->forward단방향 / Maxdistance : 최대 갈 수 있는 거리
                       //bool값을 리턴함-->Ray를 쐈을 때 물체에 닿았으면 true

       if (Physics.Raycast(transform.position + Vector3.up, look, out hit, 10))
       {
            Debug.Log($"RayCast{hit.collider.gameObject.name}!");
            //Ray가 충돌한 물체를 hit라고 하고, 이것의 컬라이더가 적용된 게임오브젝트의 이름을 표시하도록
        }
     
        //한 직선 위의 두 물체를 관통하는 Ray를 원할 때 :

        //RaycastHit[] hits; //레이캐스트 배열 사용
       // hits = Physics.RaycastAll(transform.position + Vector3.up, look, 10);
       // foreach(RaycastHit hit in hits)
       // {
       //     Debug.Log($"RayCast{hit.collider.gameObject.name}!");
        //}
    //RayCast 응용 : Player와 카메라 사이가 벽으로 막혀있을 때-->Ray를 쏘아 벽에 닿으면 카메라 위치를 벽보다 Player에게 가까이 할 수 있음 
    }
}
