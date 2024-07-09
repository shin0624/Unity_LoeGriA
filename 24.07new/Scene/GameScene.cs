using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : BaseScene
// "Game" ������ ����� ��ũ��Ʈ
{
#if Coroutine
    //Coroutine : �����ƾ�� �Ͻ� �����ϰ� �簳�� �� ���� -->�Ͻ� ���� �� �Լ� �� ������ ����Ǿ��� ���� �״�� �̾���(������� ����)
    //��� 1. ���� �ɸ��� �۾��� ��� ���ų� ���ϴ� Ÿ�ֿ̹� �Լ��� ��� stop/����-->�ð� ������ ����
    // 2. ���ϴ� Ÿ���̳� Ŭ������ return ����

    class CoroutineTest : IEnumerable//�ڷ�ƾ ���� �� �ٿ��־�� �� �������̽� IEnumerable
    {
        public IEnumerator GetEnumerator()
        {//�ڷ�ƾ ���� �� ���̴� yield --> yield return 1,2,3,4.. �� ��� return 1 ���� �� return 2, 3, 4 �ϳ��ϳ��� �����Ͽ� ���� ���·� �Ѿ �� ����.
          // �ڷ�ƾ������ ���� ����� yield break(�Ϲ� �Լ������� return �� �ش�)

            for( int i=0;i<100000;i++)
            {
                if (i % 10000 == 0)
                    yield return null;//100000 �� �ݺ����� 10000��° ���� �޽� �� �Ʒ��� foreach������ ���� �Ѿ. foreach������ ���� ������ ������ �� ���� �� �ٽ� �ڷ�ƾ���� ���ƿ�
            }
           


        }
    }

     IEnumerator ExplodeAfterSeconds(float seconds)//coroutine����-> ���� �� �Ŀ� �ߵ��ϴ� ��ų
    {
        Debug.Log("explode endter");
        yield return new WaitForSeconds(seconds);//�����ð�(second) ��ŭ ��� �� ����.-->StartCoroutine(������ ��ų, ���ð�)����.
        Debug.Log("explode execute!");
        
    }
#endif
    private bool isPaused = false;
    private GameObject OptionPopup;

    //�ΰ��� �Ͻ����� ȭ�� ��Ʈ�ѷ� ��ũ��Ʈ
    public Canvas PauseUI;
    public Button QuitButton;
    public Button ResumeButton;
    public Button OptionButton;

    public Sprite QuitButtonClicked;
    public Sprite ResumeButtonClicked;
    public Sprite OptionButtonClicked;

    private Image QuitButtonImage;
    private Image ResumeButtonImage;
    private Image OptionButtonImage;

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;  
    }

    void Start()
    {
        if(PauseUI==null)
        {
            PauseUI = GetComponentInChildren<Canvas>();
            Debug.Log($"Canvas : {PauseUI}");
            if(PauseUI==null)
            {
                Debug.LogError("Pause UI Canvas not found!");
                return;
            }
        }
        PauseUI.gameObject.SetActive(false);

        QuitButtonImage = QuitButton.GetComponent<Image>();
        ResumeButtonImage = ResumeButton.GetComponent<Image>();
        OptionButtonImage = OptionButton.gameObject.GetComponent<Image>();

        QuitButton.GetComponent<Button>().onClick.AddListener(OnQuitButtonClicked);
        ResumeButton.GetComponent<Button>().onClick.AddListener(OnResumeButtonClicked);
        OptionButton.GetComponent<Button>().onClick.AddListener(OnOptionButtonClicked);
    }

    private void Update()
    {
        // Esc Ű �Է� ó��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                if (OptionPopup != null)
                {
                    ClearPopup();
                }
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        if (PauseUI != null)
        {
            isPaused = true;
            PauseUI.gameObject.SetActive(true);
            Time.timeScale = 0f; // ������ �Ͻ� ����
        }
    }


    private void ResumeGame()
    {
        if (PauseUI != null)
        {
            isPaused = false;
            PauseUI.gameObject.SetActive(false);
            Time.timeScale = 1f; // ������ �簳�մϴ�.
        }
    }


    void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void OnResumeButtonClicked()
    {
        ResumeGame();
    }
    void OnOptionButtonClicked()
    { 
        if(OptionPopup ==null)
        {
            GameObject OptionPrefab = Resources.Load<GameObject>("Prefabs/UI/UI_Option");
            if(OptionPrefab!=null)
            {
                OptionPopup = Instantiate(OptionPrefab, transform);//��λ� �̸� �����صξ��� �ɼ� �˾��� ������ȭ�Ͽ� ȭ�鿡 ����.
                PauseUI.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("UI_Option prefab not found at Assets/Resources/Prefabs/UI/");
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUI.gameObject.SetActive(true) ;
            ClearPopup();
        }
    }

    void ClearPopup()
    {
        if(OptionPopup!=null)
        {
            Destroy(OptionPopup);
            OptionPopup = null;
        }
    }

    public override void Clear()
    {
        Debug.Log("Game End");
    }

}
