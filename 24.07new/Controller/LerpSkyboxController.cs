using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpSkyboxController : MonoBehaviour
{
    //���������� ����� ��ī�̹ڽ� ��Ʈ�ѷ�. --> ���� ��ī�̹ڽ� ��Ʈ�ѷ��� ������ �ε巴�� ���� ��ȯ�� ���ĺ��� ���� ����

    public Material dayMat;
    public Material nightMat;

    public GameObject dayLight;
    public GameObject nightLight;

    public Color dayFog;
    public Color nightFog;

    private float rotationSpeed = 1.5f;//��ī�̹ڽ� �����̼� �ӵ�
    private float currentRotation = 0f;//���� �����̼� ��(0 ~ 360) --> 360�� �ʹ� �� 160������ �����ҵ�..
    private bool isDay = true;//������ ������ ������ ���� ����
    private bool isTransitioning = false;//�ڿ������� ��ȯ�� ����, ��ȯ�� ���� ������ ��Ÿ���� �÷��� ����->�ߺ����� ����


    void Update()
    {
        currentRotation += Time.deltaTime * rotationSpeed;
        RenderSettings.skybox.SetFloat("_Rotation", currentRotation);//��ī�̹ڽ��� _Rotation ���� �����ؼ� float���� �ٲپ��ش�. ���� �ð� * 1.5f�� �ð��� ���� ��ȭ�ϵ��� ����

        if (currentRotation >= 166f)//��ī�̹ڽ� �����̼��� 160���� �Դٸ� �ʱ�ȭ �� �ٸ� ��ī�̹ڽ��� ����.
        {
            currentRotation = 0f;
            if(!isTransitioning)
            {
                StartCoroutine(TransitionSkybox());
            }

        }
    }

    private IEnumerator TransitionSkybox()//���������� ���� �ڿ������� ��ī�̹ڽ� ��ȯ�� �����ϵ��� �ϴ� �ڷ�ƾ ����
    {
        isTransitioning = true;
        //���۰� �����
        Material startMat = RenderSettings.skybox;
        Material endMat = isDay ? nightMat : dayMat;

        Color startFog = RenderSettings.fogColor;
        Color endFog = isDay ? nightFog : dayFog;

        GameObject startLight = isDay ? dayLight: nightLight;
        GameObject endLight = isDay ? nightLight: dayLight;

        float duration = 2.0f;//��ȯ�� �ɸ��� �ð�(��)
        float elapsed = 0f;//����ð� �ʱ�ȭ = 0

        while(elapsed < duration)//����ð��� ��ȯ�ð����� ���� ���� ���� ����
        {
            elapsed += Time.deltaTime;//�ð��� ����Կ� ���� ������Ʈ
            float t = elapsed / duration;//���� ��ȯ���¸� 0 ~ 1 ���� ������ ��Ÿ����. elapsed = duration�� �Ǹ� t = 1

            RenderSettings.skybox.Lerp(startMat, endMat, t);//����ð�/��ȯ�ð��� ���� ��������
            RenderSettings.fogColor = Color.Lerp(startFog, endFog, t);
            startLight.SetActive(false);
            endLight.SetActive(true);
            yield return null;//�� ������ ��� �� ���� �����ӿ��� ���� �簳
        }

        //��ȯ �Ϸ� �� ���� ����
        RenderSettings.skybox = endMat;
        RenderSettings.fogColor = endFog;
        startLight.SetActive(false);
        endLight.SetActive(true);
        isDay = !isDay;
        isTransitioning = false;
    }

   
}
