using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager 
{
    
    public T Load<T>(string path) where T : Object//프리팹을 로드하는 메서드는 제네릭타입
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");//Prefabs폴더 내에 있는 프리팹을 가져오도록 지정
        //ResourceManager를 이용한 Instantiate()를 선언할 때는 Prefabs/ 를 안붙여도 될 것.
        if(prefab== null)
        {
            Debug.Log($"Failed to load prefab : {path}");//만약 프리팹이 null이면 경로와 함꼐 로그표시
            return null;
        }
    
        return Object.Instantiate(prefab, parent);//프리팹과 프리팹을 생성해서 붙일 parent를 리턴
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy (go);
    }

    //-->이후 코드 상에서 사용 시 GameObject Tank;   Tank = Managers.Resource.Instantiate("Tank"); 형식으로 사용하면 됨
}
