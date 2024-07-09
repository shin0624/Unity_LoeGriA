using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox_SunLightController : MonoBehaviour
{
    // ��ī�̹ڽ��� _Rotation ��ȭ�� Directional Light x�� ȸ���� ��ȭ�� �߰��Ͽ� ���� ǥ���� ���Ǽ��� ���ϱ� ���� ��ũ��Ʈ
    //���� �� ���̿� Ȳȥ�� �߰���

    public Material dayMat;
    public Material nightMat;
    public Material twilightMat;

    public Light dirctionalLight;//���� ������ ���
    public Color dayLightColor = Color.white;
    public Color nightLightColor = Color.blue;
    public Color twilightLightColor = new Color(1f, 0.5f, 0.5f);//Ȳȥ ���׸���� ������ ���� �������� ������ ���� 
    
    public Color nightFog;
    public Color twilightFog = new Color(1f, 0.5f, 0.5f, 0.5f);//Ȳȥ �Ȱ���(���� ��������)

    private float rotationSpeed = 1.5f;//��ī�̹ڽ� �����̼� �ӵ�
    private float currentRotation = 0f;//���� �����̼� ��(0 ~ 360) --> Ȳȥ�� �߰��Ͽ����� 360 ������ Ȳȥ �� 360���� �� ������.
   
    private enum TimeOfDay { Day, Twilight, Night }
    private TimeOfDay currentTimeOfDay = TimeOfDay.Day;
   
    void Update()
    {
        currentRotation += Time.deltaTime * rotationSpeed;
        RenderSettings.skybox.SetFloat("_Rotation", currentRotation);//��ī�̹ڽ��� _Rotation ���� �����ؼ� float���� �ٲپ��ش�. ���� �ð� * 1.5f�� �ð��� ���� ��ȭ�ϵ��� ����

        //���� ȸ������ ��ī�̹ڽ� �����̼ǿ� ���߾� ����
        dirctionalLight.transform.rotation = Quaternion.Euler(currentRotation - 90f, 170f, 0f);

        if (currentRotation >= 360f)//��ī�̹ڽ� �����̼��� 160���� �Դٸ� �ʱ�ȭ �� �ٸ� ��ī�̹ڽ��� ����.
        {
            currentRotation = 0f;
            ToggleSkybox();

        }
        else if(currentRotation >=250f && currentRotation < 360f && currentTimeOfDay==TimeOfDay.Day)
        {
            //��ī�̹ڽ� �����̼��� 250<=_Rotation<360�̸� Ȳȥ�� �ƴ� ��
            ToggleSkybox();
        }

    }

    private void ToggleSkybox()
    {
        if (currentTimeOfDay == TimeOfDay.Day)//���� �� -> Ȳȥ����
        {
            currentTimeOfDay = TimeOfDay.Twilight;
            SetTwilight();
            Debug.Log($"CurrentTimeOfDay : {currentTimeOfDay}");
        }
        else if(currentTimeOfDay == TimeOfDay.Twilight)//Ȳȥ�� �� -> ������
        {
            currentTimeOfDay = TimeOfDay.Night;
            SetNight();
            Debug.Log($"CurrentTimeOfDay : {currentTimeOfDay}");
        }
        else if(currentTimeOfDay == TimeOfDay.Night)//���� �� -> ������
        {
            currentTimeOfDay = TimeOfDay.Day;
            SetDay();
            Debug.Log($"CurrentTimeOfDay : {currentTimeOfDay}");
        }
    }
    
    private void SetDay()
    {
        RenderSettings.skybox = dayMat;//������ ����
        dirctionalLight.color = dayLightColor;
        RenderSettings.fogColor = Color.clear;
    }
    private void SetTwilight()
    {
        currentTimeOfDay = TimeOfDay.Twilight;//Ȳȥ���� ����
        RenderSettings.skybox = twilightMat;
        dirctionalLight.color = twilightLightColor;
        RenderSettings.fogColor = twilightFog;
    }
    private void SetNight()
    {
        RenderSettings.skybox = nightMat;//������ ����
        dirctionalLight.color = nightLightColor;
        RenderSettings.fogColor = nightFog;
    }
}
