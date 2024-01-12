using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager//싱글톤으로 구현된 Managers가 이미 있으므로 InputManager는 일반 스크립트로 생성
{
    public Action KeyAction = null;//델리게이트 -->업데이트에서 인풋매니저가 입력을 체크하고 입력이 있다면 전파함

  
    public void OnUpdate()
    {
        if (Input.anyKey == false)
        {
            return;
        }
        if (KeyAction != null)
        {
            KeyAction.Invoke();
        }
    }
}
