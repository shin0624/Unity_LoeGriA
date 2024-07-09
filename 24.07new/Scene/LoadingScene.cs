using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{

    public static string nextScene;

   [Header("Loading")]
    public Image Progress;

    private void Start()
    {
        StartCoroutine(LoadSceneCoroutine()) ;//�ڷ�ƾ ȣ��->�ε� �� ���� �� ���� ���� �񵿱������� �ε�
    }
    public static void LoadScene(string SceneName)//�������� �����Ͽ� �ٸ� ��ũ��Ʈ���� ���� ȣ�� ����
    {
        nextScene = SceneName;
        SceneManager.LoadScene("Loading");
    }

    IEnumerator LoadSceneCoroutine()//�ε��� �ڷ�ƾ 
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);//�񵿱� ����� �� �ε带 ����Ͽ�, �� �ε� �߿��� �ٸ� �۾��� ����
        //�ε��� �������� op�� AcyncOperation ������ ��ȯ
        //LoadScene : ������ -> �ҷ��� ���� �Ѳ����� �ҷ����� �ٸ� ��� ���� �ҷ����� ���� ��ٸ��� ���. �ε� �� �ٸ� �۾� �Ұ�
        op.allowSceneActivation = false;
        //op.allowSceneActivation --> ���� �ε��� ������ �ڵ����� �ҷ��� ������ �̵��� ���ΰ��� ���� �ɼ�. false�� �����Ͽ� �ε� �Ϸ� �� ���� ������ ��ȯ���� �ʰ� ��� -> true�� �� �� ������ �ε� �� �� ��ȯ

        float timer = 0.0f;
        while(!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                Progress.fillAmount = Mathf.Lerp(Progress.fillAmount, op.progress, timer);
                if (Progress.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                Progress.fillAmount = Mathf.Lerp(Progress.fillAmount, 1f, timer);
                if (Progress.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }  
    }
}
