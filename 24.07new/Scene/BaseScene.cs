using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    //����� ��� scene�� �ֻ��� �θ� Ŭ����

    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;//get�� public���� ����ΰ�, set�� protected�� ���Ƶд�

    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        // ������ȭ �� EventSystem�� ���⼭ ȣ��--> EventSystem�� ����ִ� ������Ʈ�� �ִ��� üũ �� ���ٸ� ��������
       Object obj =  GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }

    public abstract void Clear();//scene�� ����Ǿ��� �� ���
 
}
