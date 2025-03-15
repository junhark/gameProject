using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // 이동 속도 관련 변수
    public float moveSpeed = 300f; //이동속도
    public float sprintSpeed = 550f; //달리기 속도
    private float currentSpeed;
    
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    // 체력 관련 변수
    public int maxHealth = 5;
    public int currentHealth;

    public Image[] hearts; // 체력 UI
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // 특수 탄환 관련 변수
    public GameObject[] specialBulletPrefabs; // 특수 탄환 프리팹
    public Sprite[] bulletTypeSprites; // 탄환 타입 스프라이트
    public Image bulletTypeUI; // 탄환 타입 UI 이미지

    private int currentBulletIndex = 0; // 현재 선택된 탄환 인덱스
    public float[] bulletFireCooldowns; // 탄환별 쿨타임
    private float[] lastFireTimes; // 마지막 발사 시간 기록

    public float bulletSpeed = 10f; // 탄환 속도

    // 스태미나 관련 변수
    public float maxStamina = 100f;
    private float currentStamina;
    public float staminaDrainRate = 10f;
    public float staminaRegenRate = 5f;
    public Slider staminaBar; // 스태미나 UI
    private bool isStaminaDepleted = false; // 스태미나 고갈 상태
    private float staminaRegenDelay = 2f; // 스태미나 재생 대기 시간
    private float staminaRegenTimer = 0f;

    public Image staminaIcon;
    public Sprite normalStaminaIcon;
    public Sprite depletedStaminaIcon;

    // 특수 탄환 쿨타임 슬라이더
    public Slider bulletCooldownSlider;

    // 스테이지 실패 패널 관련 변수
    public GameObject stageFailPanel; // 스테이지 실패 패널

    private bool isGameOver = false; // 게임 오버 상태 확인

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        UpdateHearts();
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = currentStamina;
        UpdateBulletTypeUI();

        lastFireTimes = new float[specialBulletPrefabs.Length];
        for (int i = 0; i < lastFireTimes.Length; i++)
        {
            lastFireTimes[i] = -bulletFireCooldowns[i];
        }

        bulletCooldownSlider.maxValue = bulletFireCooldowns[currentBulletIndex];
        bulletCooldownSlider.value = bulletCooldownSlider.maxValue; // 처음에는 가득 찬 상태
    }

    void Update()
    {
        if (isGameOver)
        {
            return; // 게임 오버 상태라면 업데이트를 중단
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            currentSpeed = sprintSpeed;
            DrainStamina();
        }
        else
        {
            currentSpeed = moveSpeed;
            if (!isStaminaDepleted)
            {
                RegenStamina();
                staminaIcon.sprite = normalStaminaIcon;
            }
            else
            {
                staminaRegenTimer += Time.deltaTime;
                staminaIcon.sprite = depletedStaminaIcon;
                if (staminaRegenTimer >= staminaRegenDelay)
                {
                    isStaminaDepleted = false;
                    staminaRegenTimer = 0f;
                }
            }
        }

        animator.SetFloat("Move_X", movement.x);
        animator.SetFloat("Move_Y", movement.y);
        animator.SetBool("isMoving", movement != Vector2.zero);

        // 쿨타임 슬라이더 업데이트
        UpdateCooldownSlider();

        if (Input.GetMouseButtonDown(0) && bulletCooldownSlider.value >= bulletCooldownSlider.maxValue)
        {
            SpawnSpecialBullet();
            lastFireTimes[currentBulletIndex] = Time.time;
            bulletCooldownSlider.value = 0f; // 발사 후 쿨타임 초기화
        }

        // 스페이스바를 누르면 특수 탄환 종류 변경
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeBulletType();
        }

        staminaBar.value = currentStamina;
    }

    void FixedUpdate()
    {
        if (!isGameOver)
        {
            rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
        }
    }

    void UpdateCooldownSlider()
    {
        float timeSinceLastFire = Time.time - lastFireTimes[currentBulletIndex];
        bulletCooldownSlider.value = Mathf.Clamp(timeSinceLastFire, 0, bulletFireCooldowns[currentBulletIndex]);
    }

    void DrainStamina()
    {
        currentStamina -= staminaDrainRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        if (currentStamina <= 0 && !isStaminaDepleted)
        {
            isStaminaDepleted = true;
        }
    }

    void RegenStamina()
    {
        currentStamina += staminaRegenRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }

    public void TakeDamage(int damage)
    {
        if (isGameOver)
        {
            return; // 이미 게임 오버 상태라면 실행하지 않음
        }

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHearts();

        if (currentHealth <= 0)
        {
            GameOver(); // 게임 오버 처리
        }
    }

    void GameOver()
    {
        isGameOver = true;
        stageFailPanel.SetActive(true); // 스테이지 실패 패널 활성화
        animator.SetBool("isMoving", false); // 이동 애니메이션 중지
        movement = Vector2.zero; // 움직임 초기화
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
        }
    }

    void ChangeBulletType()
    {
        currentBulletIndex = (currentBulletIndex + 1) % specialBulletPrefabs.Length;
        UpdateBulletTypeUI();

        // 새 탄환의 쿨타임에 맞게 슬라이더 최대값을 업데이트
        bulletCooldownSlider.maxValue = bulletFireCooldowns[currentBulletIndex];
        bulletCooldownSlider.value = bulletFireCooldowns[currentBulletIndex]; // 초기값으로 슬라이더를 가득 채움
    }

    void UpdateBulletTypeUI()
    {
        bulletTypeUI.sprite = bulletTypeSprites[currentBulletIndex];
    }

    void SpawnSpecialBullet()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        GameObject bullet = Instantiate(specialBulletPrefabs[currentBulletIndex], transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PWall"))
        {
            Debug.Log("PWall과 충돌!");
        }
    }
}