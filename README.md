# 🎮 2D 미니게임 프로젝트

![1 (1)](https://github.com/user-attachments/assets/a2ce33f4-a878-4fc1-bd96-2cc9d414f642)



- 플레이어는 맵을 자유롭게 이동하면서 특정 장소에서 미니게임을 시작할 수 있습니다.
- 미니게임을 통해 점수를 획득하고, 기록을 갱신할 수 있습니다.

---
 
## 🛠️ 개발 환경

- **개발 엔진**: Unity (C#)

---

## 🔹 중요 기술

- Unity Tilemap을 활용한 배경 및 맵 구성
- Singleton 패턴을 적용한 게임 매니저 시스템
- 이벤트 기반 UI 관리 및 씬 전환 로직 적용
- 비동기 씬 로드

---

## 🏠 메인 맵
![2](https://github.com/user-attachments/assets/d6f9629c-62e0-4cd7-8802-3c095bd6c2f2)



- 방향키로 조작할 수 있습니다.
- `F` 키로 다른 오브젝트들과 상호작용할 수 있습니다.
- 자연스러운 카메라 움직임을 구현했습니다.
- 플레이어와 오브젝트가 서로 뚫지 않습니다.


<details>
<summary>🔎 자세히 보기</summary>

### 🚪 플레이어가 오브젝트를 뚫지 않습니다.

![image](https://github.com/user-attachments/assets/562a3727-e3ea-48b5-b450-89824189d9cd)

플레이어의 **콜라이더를 2개로 나누고**, 집 타일은 앞/뒤로 만들어서  
**앞에서는 플레이어가 앞에서 보이고, 뒤에서는 플레이어가 안 보이도록** 구현했습니다.

![image](https://github.com/user-attachments/assets/f9222298-f6cf-4362-a6be-fbf2a54d3665)


---

#### 🎥 카메라 범위 지정

```csharp
Bounds bounds = mapBounds.localBounds;

```

이렇게 타일맵이 차지하는 공간을 Bounds 형태로 반환해서
타일맵의 크기와 위치 정보를 저장한 후, 카메라 범위를 지정해주었습니다.

</details>

---

## 🕊️ 미니게임 - 오래오래 날기! (플래피 버드 스타일)


![p](https://github.com/user-attachments/assets/0b82e8da-8297-4a1b-815c-83a481301771)

마우스 클릭으로 조작합니다.
장애물을 피해 오래 살아남을수록 / 코인을 많이 먹을수록 점수가 올라갑니다.

<details> <summary>🔎 자세히 보기</summary>
 
#### 🔄 장애물 루프 시스템

![image](https://github.com/user-attachments/assets/3aad55e5-3f24-4de1-a68c-7931ce6c1f88)

카메라에 bgLooper를 추가하여 장애물들이 루퍼에 닿으면
일정 위치 앞으로 다시 배치되도록 설정하였습니다.

```csharp
if (obstacle.transform.position.x < resetPositionX)
{
    obstacle.transform.position = new Vector3(newSpawnX, obstacle.transform.position.y, obstacle.transform.position.z);
}
```

</details>

## 🏆 점수 확인판

![4](https://github.com/user-attachments/assets/7f585c7b-5652-4248-a8ae-4ffb791770de)
![image](https://github.com/user-attachments/assets/4d71a4ae-22a7-4823-9633-02fbf28a6a8a)

"오래오래 날기!" 게임의 점수를 확인할 수 있습니다.
오름차순으로 정렬되어 표시됩니다.

---

## 🏗️ 미니게임 - 인형뽑기
![cr](https://github.com/user-attachments/assets/b959683b-4c30-4f04-a29b-a96e8536fcc8)


좌/우 방향키를 눌러 크레인을 이동할 수 있습니다.
크레인의 움직임에 따라 조이스틱도 회전합니다.
아래키(⬇)로 아이템을 집을 수 있으며, V 키를 통해 아이템을 떨어뜨릴 수 있습니다.
애니메이션 타일을 활용해 레일이 실제로 움직이는 것처럼 표현했습니다.
아이템들이 레일을 따라 자연스럽게 이동합니다.

<details> <summary>🔎 자세히 보기</summary>
 
#### 🎢 아이템이 레일을 따라 움직이도록 구현
![image](https://github.com/user-attachments/assets/d6361b72-6625-40cf-bc30-989e3b28cc4e)
레일에 빈 오브젝트를 여러 개 배치하여 아이템들이 이 위치들을 따라가도록 설정하였습니다.
이를 통해 아이템들이 레일 위에서 도는 것처럼 보이게 만들었습니다.

```csharp
[SerializeField] private Transform[] pathPoints;
private int currentIndex = 0;

void Update()
{
    if (pathPoints.Length == 0) return;
    
    Vector3 targetPosition = pathPoints[currentIndex].position;
    transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

    if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
    {
        currentIndex = (currentIndex + 1) % pathPoints.Length; // 순환 이동
    }
}
```

</details>
