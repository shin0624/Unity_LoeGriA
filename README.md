# 3D AdventureExploration
    Unity Engine을 사용한 PC기반 3D 오픈월드 탐험

# 1. 개발 기간
    2023.12 ~ 2024.03

# 2. 개발 환경
**Unity 2023.1.5f1**

# 3. 사용 에셋
   - Fantasy landscape
     published by Pxltiger
     License agreement : Standard Unity Asset Store EULA

   - Dog Knight PBR Polyart
    published by Dungeon Mason
    License agreement : Standard Unity Asset Store EULA

# 4. 기능
  ##  Controller
    - Player Controller : WASD- 상하좌우 이동

    -Camera Controller : 카메라 시점은 QuaterView를 정의. Player 기준 거리 방향벡터_delta를 선언하여 Player Position에 더해 카메라 위치를 설정하고, LookAt()함수로 Player 좌표를 주시하도록 함.
        + 카메라 시야를 벽 등의 오브젝트가 막고있을 때 대처법 : 
        a. RaycastHit hit를 선언
        b. Player와 오브젝트 간 거리 dist를 [Raycast의 히트포인트 - 플레이어 위치] 방향벡터에 magnitude를 적용하여 방향벡터 크기를 구함.
        c.  _delta의 normalized된 방향 * dist *상수값을 카메라 위치로 반환한다. 즉, 방향벡터보다 조금 더 앞으로 당겨서 Player를 비추게 할 것.
  ## Manager  
    -Managers : 최상위 스크립트. 각 매니저 스크립트의 인스턴스를 선언, 사용 시 널체크 필요. 
        + Init() 함수 선언 : Hirerarchy에 Managers를 위치시켜 관리해야하기 때문에, GameObject.Find()로 오브젝트 탐색, null이라면 Managers 오브젝트를 새로 생성 후 AddComponent<Managers>() 수행.
        + Clear() 함수 선언 : 사운드, 입력, 씬, UI 스크립트에서 사용할 반환함수

    - Input Manager : 마우스 입력을 받아 이벤트 처리
        + UI 버튼 클릭과 화면 클릭 구분을 위해 EventSystem을 사용 조건 추가
        
    - Resource Manager : prefab을 로드하는 메서드 선언
         + Load<T>(string path) - path에서 오브젝트 로드, GameObject타입일 경우 Pool에서 원본 반환, 이외 타입은 리소스에서 직접 로드
         + Instantiate(string path, Transform parent = null) - 내부적으로 Load를 호출하여 프리팹 로드, 로드된 프리팹을 인스턴스화 후 반환. 
         + Destroy(GameObject go) - Pooling 여부 확인 후, Pooling 대상 오브젝트이면 Pool에 반환하여 재사용 촉진, 아니라면 파괴

    - Data Manager :  데이터를 직렬화하여 JSON 파일로 저장, 런타임에 로드하여 게임에서 사용
         + ILoader<Key, Value> - 데이터를 로드하는 인터페이스. 딕셔너리 형태로 데이터 반환
         + StatData - ILoader 인터페이스 구현. JSON파일로 읽어온 데이터 저장
         + DataManager - 게임 시작 시 Init()를 호출하여  JSON파일 로드 후 statDict에 저장

    - Pool Manager : Object Pooling을 관리
         + Pool - 특정 타입의 GameObject를 관리. 각 Pool은 원본 GameObject의 복사본 생성 후 Pool에 Push, 또는 Pool에서 복사본을 Pop.
         + PoolManager - 모든 Pool객체 관리, Pooling된 GameObject 생성 및 반환.

    - Sound Manager : 오디오소스 관리
    - UI Manager : 각 UI컴포넌트의 SortOrder관리. 스택 구조를 통해 가장 마지막에 띄워진 UI Popup을 먼저 삭제.
    기본 UI 호출, Popup UI 호출, 차례대로 Popup을 삭제
    - Scene ManagerEX : 기존 SceneManager를 확장한 Scene 관리
         + CurrentScene - 현재 실행 중인 Scene의 BaseScene컴포넌트 반환
         + LoadScene - Define에 정의된 Scene 열거형을 받아 해당 Scene 로드
         + GetSceneName - 해당 Scene 이름 반환. Reflection(System.Enum.GetName)을 이용하여 지정된 Enum타입과 해당 값으로부터 string을 가져와 LoadScene에 전달

  ## Scene
    - Login Scene : 
    - Game Scene : 
    - Base Scene : 

  ## UI
    - UI Base : 
    - UI Button :
    - UI_EventHandler : 
    - UI_Inven : 
    - UI Inven Item : 
    - UI Popup : 
    - UI Scene : 

  ## Utils
    - Utils : 기능성 함수 관리.
        + GetOrAddComponent<T>(GameObject go) - 특정 타입 컴포넌트를 가져오거나, 없는 경우 해당 컴포넌트 추가 후 반환.
        + FindChild(GameObject go, string name = null, bool recursive = false) -  GameObject의 자식 중에서 특정 이름의 자식을 찾거나, 특정 타입의 컴포넌트를 갖는 자식을 찾음.
        recursive를 사용하여 재귀적으로 자식을 검색할지 여부를 지정 가능.
        재귀적으로 자식을 검색하는 경우 해당 GameObject의 모든 하위 자식까지 검색.
        + FindChild<T>(GameObject go, string name = null, bool recursive = false) -  FindChild 메서드를 오버로드, 이름 대신 컴포넌트 타입에 따라 자식을 찾음

    - StatData : 

    - Define : MouseEvent, CameraMode를 종류별로 열거. 향후 더 추가

    - Extension : Extension Method 사용으로 기존 클래스를 수정하지 않고 새로운 메서드를 추가하기 위해 작성.
        + GetOrAddComponent<T>(this GameObject go) - 특정 타입의 컴포넌트를 가져오거나 추가
        + BindEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click) -  UI_Base의 BindEvent를 호출하여 UI 이벤트를 바인딩.  UI 요소에 대한 이벤트 핸들러를 추가할 때 사용. 매개변수로 이벤트 핸들러를 나타내는 Action<PointerEventData>, UI 이벤트 타입을 나타내는 Define.UIEvent를 전달