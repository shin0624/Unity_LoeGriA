using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    //Login 씬에서 사용할 스크립트. login 씬에서 특정 액션 발동 시 Game씬으로 넘어가도록 구현 / 유니티 File->BuildSetting 필수

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Login;

       
            
    }

    private void Update()
    {
        //특정 키 입력 시 다음 씬으로 이동
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // SceneManager.LoadScene("Game");//LoadScene : 기존에 켜진 Scene을 날리고 지정된 Scene을 차례로 로딩-->씬 규모와 대기시간이 비례함

            Managers.Scene.LoadScene(Define.Scene.Game);//SceneManangerEX에서 새로 정의한 LoadScene(인자 : Define enum)사용
            //*Async 함수 등을 이용하면 로그인 창에서부터 백그라운드로 다음 씬 리소스를 조금씩 로드할 수 있으므로 유용하게 사용 가능
        }
    }


    public override void Clear()
    {
        Debug.Log("Login Scene Clear");
    }

}
