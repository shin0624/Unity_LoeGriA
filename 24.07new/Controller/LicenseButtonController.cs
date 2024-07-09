using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LicenseButtonController : MonoBehaviour
{
    //���̼��� �˾� ���� ��ũ��Ʈ. ���̼����� ���� �ƴ� �˾����� �����ֱ� ���� ��ư�� �� ĵ������ �Ҵ�
    public Canvas UICanvas;
    public Canvas PopupCanvas;
    public Button LicenseButton;

    private void Start()
    {
        PopupCanvas.gameObject.SetActive(false);//��ư�� Ŭ������ ������ �˾�â�� false
        LicenseButton.onClick.AddListener(OpenPopup);//��ư Ŭ�� �� �˾�â true
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))//escŰ�� ������ �˾� ����
        {
            if(PopupCanvas.gameObject.activeSelf)
            {
                ClosePopup();
            }
        }
    }

    void OpenPopup()
    {
        UICanvas.gameObject.SetActive(false);
        PopupCanvas.gameObject.SetActive(true);
    }
    void ClosePopup()
    {
        PopupCanvas.gameObject.SetActive(false);
        UICanvas.gameObject.SetActive(true);
    }
}
