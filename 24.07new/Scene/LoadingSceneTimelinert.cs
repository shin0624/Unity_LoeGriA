using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LoadingSceneTimelinert : MonoBehaviour
{
    //�ε� �� Ÿ�Ӷ��� ����� ���� ��ũ��Ʈ.
    public PlayableDirector pd;

    void Start()
    {
        //���ҽ� ������ �ִ� Ÿ�Ӷ��� ������ �ҷ��� ���.
        PlayableAsset timeline = Resources.Load<PlayableAsset>("Prefabs/LoadingTimelinerTimeline");
        if(timeline!=null && pd!=null)
        {
            pd.playableAsset = timeline;
            pd.Play();
        }
        else
        {
            Debug.LogError("Ÿ�Ӷ��� �Ǵ� Playable Director�� ã�� �� �����ϴ�.");
        }
    }


}
