using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension //Extension Method 테스트
{

    public static void AddUIEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_Base.AddUIEvent(go, action, type);//-->UI_Button에서 AddUIEvent를 붙이는 작업을 단순화하기 위해 사용, Click에서 사용함
    }


}
