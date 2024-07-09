using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScene : BaseScene
{
    //Login ������ ����� ��ũ��Ʈ. login ������ Ư�� �׼� �ߵ� �� Loading Scene�� ���� Game������ �Ѿ���� ���� / ����Ƽ File->BuildSetting �ʼ�

    //��ư��ü
    public Button StartButton;
    public Button OptionButton;
    public Button QuitButton;
    public AudioSource ado;

    //Ŭ�� �� �̹��� ����ǵ��� �� �� �ִ� Ŭ���� ��������Ʈ ��ü
    public Sprite StartButtonClicked;
    public Sprite OptionButtonClicked;
    public Sprite QuitButtonClicked;
    

    //������ �̹��� ��ü
    private Image StartButtonImage;
    private Image OptionButtonImage;
    private Image QuitButtonImage;


    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Login;  
    }
    private void Start()
    {
        if(ado!=null)
        {
            ado.Play();
            ado.loop = true;
        }
        StartButtonImage = StartButton.GetComponent<Image>();
        OptionButtonImage = OptionButton.GetComponent<Image>();
        QuitButtonImage = QuitButton.GetComponent<Image>();

        StartButton.GetComponent<Button>().onClick.AddListener(OnStartButtonClicked);
        OptionButton.GetComponent<Button>().onClick.AddListener(OnOptionButtonClicked);
        QuitButton.GetComponent<Button>().onClick.AddListener(OnQuitButtonClicked);
    }

    private void Update()
    {
    
    }

    void OnStartButtonClicked()
    {
        StartButtonImage.sprite = StartButtonClicked;
        LoadingScene.LoadScene("Game");//�� ��ư Ŭ�� �� Scenemanager.Loadscene�� �ƴ� �ε����� �޼��带 ȣ��.
    }
    void OnOptionButtonClicked()
    {
        OptionButtonImage.sprite = OptionButtonClicked;
        LoadingScene.LoadScene("Options");
    }
    void OnQuitButtonClicked()
    {
        QuitButtonImage.sprite = QuitButtonClicked;
        Application.Quit();
        // �����Ϳ��� ���� ���� ���, �����͵� ����ǵ��� ���� (UnityEditor ���ӽ����̽� ���)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
 
    public override void Clear()
    {
        Debug.Log("Login Scene Clear");
    }

}
