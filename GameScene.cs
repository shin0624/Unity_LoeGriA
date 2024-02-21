using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
    // "Game" 씬에서 사용할 스크립트
{
    void Start()// GameScene컴포넌트를 들고있는 오브젝트를 off한 상태에서도 ui 등이 작동되도록 하려면 Start 대신 Awake를 사용
    {
        Init();
    }


    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();//게임 시작 시 기본 UI를 불러오는 코드 등, 시작 시 구현될 액션은 이곳에 작성


    }

    public override void Clear()
    {
        
    }
}
