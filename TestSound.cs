using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public AudioClip audioClip;//오디오클립을 선택할 수 있도록 선언->선택된 오디오클립을 아래 메서드의 오디오소스 객체가 받아 사운드가 재생될 것.
    public AudioClip audioClip2;
    int i = 0;
    private void OnTriggerEnter(Collider other)//Player가 OnTrigger되어있는 상태에서 AudioSource와 본 스크립트가 붙은 오브젝트에 충돌할 시 사운드 발생
    {
        
        // AudioSource audio = GetComponent<AudioSource>();//오브젝트에 붙여놓은 오디오소스를 추출
      //  audio.PlayClipAtPoint();//매개변수 : 오디오클립, 위치 --> 특정 위치에 특정 소리가 나도록 할 수 있음
        // audio.PlayOneShot(audioClip);
        /*
        만약 오디오클립 두개, PlayOneShot()메서드 두개 실행 시 사운드 2개가 동시에 발생
        audioClip.length : 오디오클립의 길이를 반환
        audio.PlayOneShot(audioClip);
        audio.PlayOneShot(audioClip2);
        float lifetime = Mathf.Max(audioClip.length, audioClip2.length);
        GameObject.Destroy(gameObject, lifetime); ==> 오디오클립 2개의 길이중 더 긴 쪽을 lifetime으로 받아 Destroy에 넣고 실행시키면, 오디오클립 두개 실행 후 더 긴쪽의 사운드 재생 종료 후 오브젝트가 삭제됨
         */
        i++;

        if(i%2==0)
           Managers.Sound.Play(audioClip, Define.Sound.Bgm);
        else
           Managers.Sound.Play(audioClip2, Define.Sound.Bgm);
    }
}
