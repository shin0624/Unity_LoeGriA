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
        
    }
}
