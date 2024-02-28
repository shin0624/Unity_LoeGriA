using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SoundManager //오디오소스 : mp3플레이어, 오디오클립 : mp3음원, 오디오리스너 : 귀
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];//이후에 분류가 필요할 수 있으니 BGM용, EFFECT용 두개 생성

    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();//Effect효과음 반복을 위한 딕셔너리<경로, 오디오클립>
    //-->문제점 : SoundManager는 Managers 아래에 있어, Don`t Destroy로 취급되어 메모리가 낭비될 것.
    //-->해결 : Clear()를 이용하여 씬 이동시마다 메모리를 비워야함

    public void Init()//_audioSources 배열을 채우기 위해서, 빈 게임 오브젝트를 생성한 후 AudioSource컴포넌트를 붙여야 함
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);// 씬 이동 후에도 없어지지 않게 되도록

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));//리플렉션을 이용하여 Sound에 있는 이름을 추출
            for(int i = 0; i < soundNames.Length - 1; i++)//Sounds의 마지막인 MaxCount는 불필요하므로 -1
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();//오디오소스 컴포넌트를 붙인 후 sources 배열에 넣는다.
                go.transform.parent = root.transform;//root의 트랜스폼으로 부모 설정
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;//배경음악은 loop를 true로 하여 반복재생
        }
    }

    public void Clear()//_audioClips의 메모리를 비우는 함수
    {
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();     
    }

    //audioClip을 받는 Play와, path를 받는 play 총 2개의 버전을 생성--> path를 받는 버전 내에서 다른 버전을 호출하도록 하여, 코드 추가제거 시 발생할 수 있는 오류를 방지.

    public void Play( string path, Define.Sound type = Define.Sound.Effect,  float pitch = 1.0f)//string으로 AudioSource 경로를 받고, pitch로 속도조절. type의 기본값은 Effectfh
    {

#if 오디오클립재생부분_중복제거
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)//이미 Bgm이 실행중이라면, Stop한 후 다른 Bgm으로 변경할 수 있도록 한다.
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;//Load()로 가져온 오디오클립 객체를 audioSource의 clip으로 설정
            audioSource.Play();//위에서 Bgm의 Loop를 true로 해주었으니, Play()로 재생만 시켜주면 됨
#endif

#if 오디오클립재생부분_중복제거
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
#endif
        AudioClip audioClip = GetOrAddAudioClip(path, type);

        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)//오디오클립을 직접 audioClip으로 받는 형태의 Play()-->일일히 경로를 넣어 실행하는 것이 번거롭다면 사용
    {
        if (audioClip == null)
            return;

        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)//이미 Bgm이 실행중이라면, Stop한 후 다른 Bgm으로 변경할 수 있도록 한다.
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;//Load()로 가져온 오디오클립 객체를 audioSource의 clip으로 설정
            audioSource.Play();//위에서 Bgm의 Loop를 true로 해주었으니, Play()로 재생만 시켜주면 됨
        }

        else
        {

            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }

    }
          
    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)//오디오클립이 있다면 추가, 없다면 생성 후 추가
    {
        if (path.Contains("Sounds/") == false)//만약 Sounds/로 시작하는 경로가 없다면 경로를 생성해줌
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (type == Define.Sound.Bgm)
        {
           audioClip = Managers.Resource.Load<AudioClip>(path);//오디오클립 객체를 생성하여 Load명령어로 오디오클립을 가져온다.
            
        }
        else//반복하여 오디오클립을  Load하는 것 보다, 딕셔너리를 사용하여 캐싱한다
        {
            // AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);//오디오클립 객체를 생성하여 Load명령어로 오디오클립을 가져온다.
            if (_audioClips.TryGetValue(path, out audioClip) == false)//캐싱하고 있던 path를 키로 하여 audioClip을 받으면 return한다. 
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);// false이면(오디오클립이 없다면) Load()하여 딕셔너리에 {키 : path, value : 오디오클립}으로 넣어준다.
                _audioClips.Add(path, audioClip);
            }
        }
        if (audioClip == null)//오디오클립 유무를 확인
        
            Debug.Log($"AudioClip Missing {path}");
        

        return audioClip;
    }
}
