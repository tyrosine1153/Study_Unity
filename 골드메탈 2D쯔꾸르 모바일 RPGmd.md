# 골드메탈 2D쯔꾸르 모바일 RPG



## 3. 대화창 UI 구축하기

### 대화창 UI

- \* canvas 컴프논트에 Pixel Perfect 체크하기

- canvas의 image에 소스 이미지에 준비된 리소스를 넣는다.

- \* 대화창 UI의 경우 리소스가 깨지지 않기 위해서는 이미지 타입을 sliced로 하고, Sprite설정을 해야 한다. Sprite설정에서 Border값을 조정해 크기 변환에 영향을 받지 않는 테두리를 설정할 수 있다.
- 엥커를 사용해 항상 밑에 있도록 설정한다 푸른 화살표(stretch)로 가로에 맞게 설정할 수 있다.
- shift+alt로 쉽게 피벗을 설정할 수 있고, left, right값을 조정해 양 옆의 여백을 맞출 수 있다.

### 데이터 전달

- gameManager 스크립트를 이용해 텍스트를 동적으로 편집한다

  ```c#
  using UnityEngine.UI;
  ...
  public GameObject talkPanel;
  public Text talkText; // 편집할 텍스트
  public GameObject scanObject;
  public bool isAction;
  
  public void Action(Gameobject scanObj)
  {
      if(isAction)  // Exit Action
      {
          isAction = false;
      }
      else
      {  // Enter Action
          isAction = true;
          // scanObject = scanObj;  // 이거 왜있어야 하지?
          talkText.text = "이것의 이름은 "+scanObj.name+"이다."; 
      }
      talkPanel.SetActive(isAction);
  }
  ```

  - 개체마다 빈 텍스트 컴포넌트를 달아서 Action에서 그 컴포넌트 값을 말하게 하면 어떨까?

  - isAction 변수를 이용해 그 여부에 따라 플레이어 이동을 제한할 수 있음 -> 대화 시 움직이지 않게 하기 위함

    예시 `h = gameManager.isAction ? 0 : Input.GetAxisRaw("Horizontal");`

### 애니메이션

- \* 이미지UI 컴포넌트 중 Set Native Image -> 원본 크기대로 이미지 사이즈를 맞춤
- UI도 애니메이션 가능함 :  Animator 컴포넌트를 만들고 -> 에니메이션 컨트롤러 파일을 만들어 컴포넌트에 넣음 -> 애니메이션 파일을 만듬 -> 컨트롤러에서 State 생성해서 Motion에 만든 애니메이션 파일을 넣음 -> 애니메이션 적용된 오브젝트를 선택하면 애니메이션창에서 애니메이션 편집이 가능함 -> Add Property -> Rect Transform(위치 이동) -> 엥커드 포지션(엥커 기준으로 이동) -> 만들어진 프로퍼티에 애니메이션에 각 초마다(키프레임 마다) 바뀔 값을 지정 -> 속도조절은 애니메이터에서 조절가능, Loop time옵션은 켜놓기

## RPG 대화 시스템 구현하기

### 오브젝트 관리

- 오브젝트마다 `ObjData.cs` 스크립트를 만들어서넣는다. 
- 변수 `int id`와 `bool isNPC`를 만들고 오브젝트마다 id를 다르게 한다. (모두 public)

### 대화 시스템

- TalkManager 오브젝트와 스크립트를 만든다. (싱글톤)

- `Dictionary<int, string[]> talkData;` 하고 생성자 호출

  - 분명히 string 배열이다. 문장의 배열!

- `talkData.Add(id, new string[] {"와랄랄"});` 을 통해 오브젝트당 대사를 넣을 수 있다

- ```
  public string GetTalk(int id, int talkIndex)
  {
  	if(talkIndex==talkData[id].Length)
  		return null;
  	else
  		return talkData[id][talkIndex];
  }  // 객체에 저장된 한 문장씩만 반환
  ```

- collider 함수에서 닿은 오브젝트의 id 값과 bool 값을 가져와 토크매니저의 함수를 호출해 사용한다. 

  예시

  ```
  
  string talkData = talkManager.instance.GetTalk(id, talkIndex);
  if(talkData == null)
  {
  	isTalk = false;
  	talkIndex = 0;
  	return;
  }
  
  isTalk = true;
  talkIndex++;  // 전역변수로 미리 선언
  ```

  

## RPG 대화 시스템 구현하기

### 퀘스트 시스템

