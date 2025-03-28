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
        // ������Ʈ �ʱ�ȭ
        rb = GetComponent<Rigidbody>();
        movementHandler = new MovementHandler(rb, moveSpeed, sprintSpeed);
        healthManager = new HealthManager(5, hearts, fullHeart, emptyHeart);
        staminaManager = new StaminaManager(100f, 10f, 5f, 2f, staminaBar);
        bulletManager = new BulletManager(specialBulletPrefabs, bulletCooldowns, bulletSpeed);

        // ���콺 Ŀ�� ���
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // �Է� ó�� �� ���¹̳� ó��
        HandleInput();
        HandleStamina();

        // ��Ŭ�� �� �Ѿ� �߻�
        if (Input.GetMouseButtonDown(0) && bulletManager.CanFire(currentBulletIndex))
        {
            FireBullet();
        }
    }

    void FixedUpdate()
    {
        // ���� ������Ʈ���� �÷��̾� �̵� ó��
        MovePlayer();
    }

    // �Է� ó��: �÷��̾��� �̵� �Է��� ����
    private void HandleInput()
    {
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    // �̵� ó��: �÷��̾ �̵��ϵ��� ó��
    private void MovePlayer()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        movementHandler.Move(movementInput, isSprinting);
    }

    // �Ѿ˹߻�
    private void FireBullet()
    {
        Vector3 bulletDirection = cameraTransform.forward;
        bulletManager.FireBullet(currentBulletIndex, cameraTransform.position, bulletDirection);
    }

    // ������Ʈ �� ���¹̳� ���ҿ� �ڿ�ȸ��
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

    // ü�°���
    public void TakeDamage(int damage)
    {
        healthManager.TakeDamage(damage);
    }

    // ü��ȸ��
    public void Heal(int amount)
    {
        healthManager.Heal(amount);
    }
}