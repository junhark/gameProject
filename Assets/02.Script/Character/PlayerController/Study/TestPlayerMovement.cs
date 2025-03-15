using UnityEngine;
using UnityEngine.UI;

// 플레이어 컨트롤러
public class TestPlayerMovement : MonoBehaviour
{
    // 이동 속도 설정
    public float moveSpeed = 5f;

    // 달리기 속도 설정
    public float sprintSpeed = 8f;

    // 카메라의 Transform (총알 발사 방향 등을 위해 사용)
    public Transform cameraTransform;

    // 체력을 나타낼 하트 UI
    public Image[] hearts;
    public Sprite fullHeart;  // 가득 찬 하트
    public Sprite emptyHeart; // 빈 하트

    // 특수 총알 프리팹과 쿨타임
    public GameObject[] specialBulletPrefabs;
    public float[] bulletCooldowns;
    public float bulletSpeed = 10f; // 총알 속도

    // 스태미나 바 (UI)
    public Slider staminaBar;

    // 이동, 체력, 스태미나, 총알 관리 객체
    private IMovement movementHandler;
    private HealthManager healthManager;
    private StaminaManager staminaManager;
    private BulletManager bulletManager;

    // 이동 입력과 현재 선택된 총알 인덱스
    private Vector3 movementInput;
    private int currentBulletIndex = 0;

    // Rigidbody 컴포넌트
    private Rigidbody rb;

    void Start()
    {
        // 컴포넌트 초기화
        rb = GetComponent<Rigidbody>(); // Rigidbody 가져오기
        movementHandler = new RigidbodyMovement(rb); // RigidbodyMovement 클래스 생성
        healthManager = new HealthManager(5, hearts, fullHeart, emptyHeart); // 체력 매니저 초기화
        staminaManager = new StaminaManager(100f, 10f, 5f, 2f); // 스태미나 매니저 초기화
        bulletManager = new BulletManager(specialBulletPrefabs, bulletCooldowns); // 총알 매니저 초기화

        // 마우스 커서 잠금
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 입력 처리 및 스태미나 처리
        HandleInput();
        HandleStamina();

        // 좌클릭 시 총알 발사
        if (Input.GetMouseButtonDown(0) && bulletManager.CanFire(currentBulletIndex))
        {
            FireBullet(); // 총알 발사
        }
    }

    void FixedUpdate()
    {
        // 물리 업데이트에서 플레이어 이동 처리
        MovePlayer();
    }

    // 입력 처리: 플레이어의 이동 입력을 받음
    private void HandleInput()
    {
        // 수평(X) 및 수직(Z) 입력 값 받기
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    // 이동 처리: 플레이어가 이동하도록 처리
    private void MovePlayer()
    {
        // 이동 속도 결정: 왼쪽 Shift 키를 누르면 달리기, 아니면 일반 이동
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        // 이동 방향 계산: 입력된 이동 값을 월드 좌표로 변환하여 정규화
        Vector3 moveDirection = transform.TransformDirection(movementInput).normalized;

        // 이동 처리
        movementHandler.Move(moveDirection, currentSpeed);
    }

    // 총알 발사: 카메라의 방향을 따라 총알 발사
    private void FireBullet()
    {
        // 카메라의 forward 방향으로 총알 발사
        Vector3 bulletDirection = cameraTransform.forward;
        bulletManager.FireBullet(currentBulletIndex, cameraTransform.position, bulletDirection, bulletSpeed);
    }

    // 스태미나 처리: 이동 및 스프린트에 따른 스태미나 변화
    private void HandleStamina()
    {
        // 왼쪽 Shift 키를 누르고 있으면 스태미나 소모, 아니면 스태미나 회복
        if (Input.GetKey(KeyCode.LeftShift))
        {
            staminaManager.DrainStamina(Time.deltaTime); // 스태미나 소모
        }
        else
        {
            staminaManager.RegenStamina(Time.deltaTime); // 스태미나 회복
        }

        // 스태미나 UI 업데이트
        staminaBar.value = staminaManager.CurrentStamina;
    }
}