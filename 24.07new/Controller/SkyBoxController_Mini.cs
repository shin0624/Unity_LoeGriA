using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxController_Mini : MonoBehaviour
{
    //��ī�̹ڽ� ��Ʈ�ѷ� - �ɼ� â �� �ұԸ� â�������.
    public Material dayMat;
    public Material nightMat;

    private float rotationSpeed = 1.5f;//��ī�̹ڽ� �����̼� �ӵ�
    private float currentRotation = 0f;//���� �����̼� ��(0 ~ 360) --> 360�� �ʹ� �� 160������ �����ҵ�..
    private bool isDay = true;//������ ������ ������ ���� ����

    void Update()
    {
        currentRotation += Time.deltaTime * rotationSpeed;
        RenderSettings.skybox.SetFloat("_Rotation", currentRotation);//��ī�̹ڽ��� _Rotation ���� �����ؼ� float���� �ٲپ��ش�. ���� �ð� * 1.5f�� �ð��� ���� ��ȭ�ϵ��� ����

        if (currentRotation >= 166f)//��ī�̹ڽ� �����̼��� 160���� �Դٸ� �ʱ�ȭ �� �ٸ� ��ī�̹ڽ��� ����.
        {
            currentRotation = 0f;
            ToggleSkybox();

        }
    }

    private void ToggleSkybox()
    {
        isDay = !isDay;//���� ��ȯ
        if (isDay)
        {
            RenderSettings.skybox = dayMat;//������ ����
           
        }
        else
        {
            RenderSettings.skybox = nightMat;//������ ����
          
        }
    }
}
