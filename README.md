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

    - Input Manager : 
    - Resource Manager : 
    - Data Manager : 
    - Pool Manager : 
    - Sound Manager : 
    - UI Manager
    - Scene Manager : 

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
    - Utils :
    - StatData : 
    - Define : 
    - Extension : 