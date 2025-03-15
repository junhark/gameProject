using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // �̵� �ӵ� ���� ����
    public float moveSpeed = 300f; //�̵��ӵ�
    public float sprintSpeed = 550f; //�޸��� �ӵ�
    private float currentSpeed;
    
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    // ü�� ���� ����
    public int maxHealth = 5;
    public int currentHealth;

    public Image[] hearts; // ü�� UI
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Ư�� źȯ ���� ����
    public GameObject[] specialBulletPrefabs; // Ư�� źȯ ������
    public Sprite[] bulletTypeSprites; // źȯ Ÿ�� ��������Ʈ
    public Image bulletTypeUI; // źȯ Ÿ�� UI �̹���

    private int currentBulletIndex = 0; // ���� ���õ� źȯ �ε���
    public float[] bulletFireCooldowns; // źȯ�� ��Ÿ��
    private float[] lastFireTimes; // ������ �߻� �ð� ���

    public float bulletSpeed = 10f; // źȯ �ӵ�

    // ���¹̳� ���� ����
    public float maxStamina = 100f;
    private float currentStamina;
    public float staminaDrainRate = 10f;
    public float staminaRegenRate = 5f;
    public Slider staminaBar; // ���¹̳� UI
    private bool isStaminaDepleted = false; // ���¹̳� �� ����
    private float staminaRegenDelay = 2f; // ���¹̳� ��� ��� �ð�
    private float staminaRegenTimer = 0f;

    public Image staminaIcon;
    public Sprite normalStaminaIcon;
    public Sprite depletedStaminaIcon;

    // Ư�� źȯ ��Ÿ�� �����̴�
    public Slider bulletCooldownSlider;

    // �������� ���� �г� ���� ����
    public GameObject stageFailPanel; // �������� ���� �г�

    private bool isGameOver = false; // ���� ���� ���� Ȯ��

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
        bulletCooldownSlider.value = bulletCooldownSlider.maxValue; // ó������ ���� �� ����
    }

    void Update()
    {
        if (isGameOver)
        {
            return; // ���� ���� ���¶�� ������Ʈ�� �ߴ�
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

        // ��Ÿ�� �����̴� ������Ʈ
        UpdateCooldownSlider();

        if (Input.GetMouseButtonDown(0) && bulletCooldownSlider.value >= bulletCooldownSlider.maxValue)
        {
            SpawnSpecialBullet();
            lastFireTimes[currentBulletIndex] = Time.time;
            bulletCooldownSlider.value = 0f; // �߻� �� ��Ÿ�� �ʱ�ȭ
        }

        // �����̽��ٸ� ������ Ư�� źȯ ���� ����
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
            return; // �̹� ���� ���� ���¶�� �������� ����
        }

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHearts();

        if (currentHealth <= 0)
        {
            GameOver(); // ���� ���� ó��
        }
    }

    void GameOver()
    {
        isGameOver = true;
        stageFailPanel.SetActive(true); // �������� ���� �г� Ȱ��ȭ
        animator.SetBool("isMoving", false); // �̵� �ִϸ��̼� ����
        movement = Vector2.zero; // ������ �ʱ�ȭ
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

        // �� źȯ�� ��Ÿ�ӿ� �°� �����̴� �ִ밪�� ������Ʈ
        bulletCooldownSlider.maxValue = bulletFireCooldowns[currentBulletIndex];
        bulletCooldownSlider.value = bulletFireCooldowns[currentBulletIndex]; // �ʱⰪ���� �����̴��� ���� ä��
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
            Debug.Log("PWall�� �浹!");
        }
    }
}