using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;
    public Transform cameraTransform;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public GameObject[] specialBulletPrefabs;
    public float[] bulletCooldowns;
    public float bulletSpeed = 10f;
    public Slider staminaBar;

    private Rigidbody rb;
    private MovementHandler movementHandler;
    private HealthManager healthManager;
    private StaminaManager staminaManager;
    private BulletManager bulletManager;
    private Vector3 movementInput;
    private int currentBulletIndex = 0;

    void Start()
    {
        // 컴포넌트 초기화
        rb = GetComponent<Rigidbody>();
        movementHandler = new MovementHandler(rb, moveSpeed, sprintSpeed);
        healthManager = new HealthManager(5, hearts, fullHeart, emptyHeart);
        staminaManager = new StaminaManager(100f, 10f, 5f, 2f, staminaBar);
        bulletManager = new BulletManager(specialBulletPrefabs, bulletCooldowns, bulletSpeed);

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
            FireBullet();
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
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    // 이동 처리: 플레이어가 이동하도록 처리
    private void MovePlayer()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        movementHandler.Move(movementInput, isSprinting);
    }

    // 총알발사
    private void FireBullet()
    {
        Vector3 bulletDirection = cameraTransform.forward;
        bulletManager.FireBullet(currentBulletIndex, cameraTransform.position, bulletDirection);
    }

    // 스프린트 시 스태미나 감소와 자연회복
    private void HandleStamina()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            staminaManager.DrainStamina(Time.deltaTime);
        }
        else
        {
            staminaManager.RegenStamina(Time.deltaTime);
        }
    }

    // 체력감소
    public void TakeDamage(int damage)
    {
        healthManager.TakeDamage(damage);
    }

    // 체력회복
    public void Heal(int amount)
    {
        healthManager.Heal(amount);
    }
}