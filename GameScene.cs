using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
#if UNITY_EDITOR

using UnityEditor;
#endif

public class GameScene : BaseScene
// "Game" 씬에서 사용할 스크립트
{
#if Coroutine
    //Coroutine : 서브루틴을 일시 정지하고 재개할 수 있음 -->일시 정지 시 함수 내 변수에 저장되었던 값이 그대로 이어짐(사라지지 않음)
    //기능 1. 오래 걸리는 작업을 잠시 끊거나 원하는 타이밍에 함수를 잠시 stop/복원-->시간 관리에 유용
    // 2. 원하는 타입이나 클래스로 return 가능

    class CoroutineTest : IEnumerable//코루틴 구현 시 붙여주어야 할 인터페이스 IEnumerable
    {
        public IEnumerator GetEnumerator()
        {//코루틴 리턴 시 붙이는 yield --> yield return 1,2,3,4.. 할 경우 return 1 실행 후 return 2, 3, 4 하나하나씩 복원하여 다음 상태로 넘어갈 수 있음.
          // 코루틴에서의 완전 종료는 yield break(일반 함수에서의 return 에 해당)

            for( int i=0;i<100000;i++)
            {
                if (i % 10000 == 0)
                    yield return null;//100000 번 반복에서 10000번째 마다 휴식 후 아래의 foreach문으로 값이 넘어감. foreach문에서 다음 동작을 결정한 뒤 수행 후 다시 코루틴으로 돌아옴
            }
           


        }
    }

     IEnumerator ExplodeAfterSeconds(float seconds)//coroutine응용-> 일정 초 후에 발동하는 스킬
    {
        Debug.Log("explode endter");
        yield return new WaitForSeconds(seconds);//일정시간(second) 만큼 대기 후 리턴.-->StartCoroutine(실행할 스킬, 대기시간)실행.
        Debug.Log("explode execute!");
        
    }
#endif

    private GameObject pauseMenu;
    private bool isPaused = false;

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;

        // UI 오브젝트를 찾고 비활성화합니다.
        pauseMenu = GameObject.Find("UI");
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        else
        {
            Debug.LogError("Pause menu not found! Make sure there is a GameObject named 'UI' in the scene.");
        }



        //Managers.UI.ShowSceneUI<UI_Inven>();//게임 시작 시 기본 UI를 불러오는 코드 등, 시작 시 구현될 액션은 이곳에 작성

        // StartCoroutine("ExplodeAfterSeconds", 4.0f);//원하는 코루틴을 실행하는 함수. 4초 후에 아래 스킬 발동
        //만약 코루틴을 취소하고자 하면 StartCoroutine을 Coroutine타입의 co로 만들고 StopCoroutin(co)를 사용하면 됨.



#if coroutineTest
        CoroutineTest test = new CoroutineTest();
        foreach (var t in test)
        {//반환되는 t는 오브젝트타입이므로, yield return은 어느 타입이던 가능(null도 가능).
           
            Debug.Log(t);
        }
#endif

#if StopCoroutine
        Coroutine co;
        co = StartCoroutine("ExplodeAfterSeconds", 4.0f);
        StartCoroutine("CoStopExplode", 2.0f);

        IEnumerator CoStopExplode(float seconds)
        {
            Debug.Log("stop endter");
             yield return new WaitForSeconds(seconds);
             Debug.Log("stop execute!");
             if(co!=null)
             {
             StopCoroutine(co);
             co = null;
             }
         }
    
#endif
    }

    private void Update()
    {
        // Esc 키 입력 처리
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ExitGame();
            }
        }

        // Space 키 입력 처리
        if (isPaused && Input.GetKeyDown(KeyCode.Space))
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        if (pauseMenu != null)
        {
            isPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f; // 게임을 일시 정지합니다.
        }
    }

    private void ResumeGame()
    {
        if (pauseMenu != null)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f; // 게임을 재개합니다.
        }
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public override void Clear()
    {
        Debug.Log("Game End");
    }
}
