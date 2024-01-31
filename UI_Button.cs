using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

//개선사항 --> 1) Button의 Onclick 연결 시 하이어라키에서 인자를 드래그드롭하는 상황
// --> 하이어라키의 오브젝트 이름만 건네주면 자동으로 Onclick에서 매핑되도록 자동화 ->void Bind()로 자동화 할 것

// 2) Text 사용 시 SerializeField로 하나하나 변수 선언 후 인스펙터에서 연결하는 상황

public class UI_Button : MonoBehaviour
{
#if UI인자전달예시
    //[SerializeField]
    //Text _text;
    //TextMeshProUGUI _text;//버튼 클릭 시 canvas 상의 Text 숫자가 증가하도록 하기 위해, 유니티 내에서 인자를 넘겨줄 텍스트 변수를 설정
    //TextMeshPro를 사용하므로, Text타입 인자는 인스펙터에서 넘겨줄 수 없음-->TMPro 네임스페이스를 선언하고, 텍스트를 TextMeshProUGUI  타입으로 선언해야 함.
#endif

    //여러가지 Type을 넣었으니 Dictionary로 관리. Button타입, Text타입을 유니티엔진 오브젝트의 리스트로 관리함
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();


    enum Buttons 
    {
         PointButton
    }

    enum Texts
    {
        PointText,
        ScoreText,
    }

    private void Start()
    {
        Bind<Button>(typeof(Buttons));//Buttons열거체 형식을 넘기겠다고 호출-->Buttons 열거체 타입의 Button이라는 컴포넌트를 찾아 해당하는 것을 매핑한다
        Bind<Text>(typeof(Texts));//Texts열거체 형식을 넘기겠다고 호출
    }

    void Bind<T>(Type type) where T : UnityEngine.Object//Buttons값을 넘겨주면 값과 겹치는 오브젝트를 자동 저장하도록 하는 함수. Reflection을 이용할 것
    {
        //Button 또는 Text를 자식으로 두고 있는 오브젝트를 찾아야 하므로, Bind 함수를 제네릭으로 선언
       string[] names  =  Enum.GetNames(type);//C#에만 있는 기능. 열거체 항목을 string 배열로 반환할 수 있다.

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];//열거체 항목을 string으로 변환했으니 dictionary에 넣기 위해 key, value가 필요->key는 제네릭타입, value는 오브젝트 배열
        _objects.Add(typeof(T), objects);

        //개선사항(1)의 자동화를 위해 루프 선언
        for(int i=0;i<names.Length;i++)
        {
            objects[i] = Util.FindChild<T>(gameObject, names[i], true);//최상위 부모, 이름을 인자로 넣는다.
            //루프를 돌며 찾은 오브젝트 이름을 objects배열에 넣어줘야 함-->GameObject에 접근할 수 있음을 이용하여 최상위 객체(UI_Button)의 자식 중 같은 이름이 있는 지 찾아야 함
          
        }
    }


    int _score = 0;

  public void OnButtonClicked()//꼭 public으로 해주어야 UI에서 실행됨
    {
       
        _score++;
        //_text.text = $"Score : {_score}"; 
    }
}
