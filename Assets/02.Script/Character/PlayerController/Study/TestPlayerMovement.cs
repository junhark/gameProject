using UnityEngine;
using UnityEngine.UI;

// �÷��̾� ��Ʈ�ѷ�
public class TestPlayerMovement : MonoBehaviour
{
    // �̵� �ӵ� ����
    public float moveSpeed = 5f;

    // �޸��� �ӵ� ����
    public float sprintSpeed = 8f;

    // ī�޶��� Transform (�Ѿ� �߻� ���� ���� ���� ���)
    public Transform cameraTransform;

    // ü���� ��Ÿ�� ��Ʈ UI
    public Image[] hearts;
    public Sprite fullHeart;  // ���� �� ��Ʈ
    public Sprite emptyHeart; // �� ��Ʈ

    // Ư�� �Ѿ� �����հ� ��Ÿ��
    public GameObject[] specialBulletPrefabs;
    public float[] bulletCooldowns;
    public float bulletSpeed = 10f; // �Ѿ� �ӵ�

    // ���¹̳� �� (UI)
    public Slider staminaBar;

    // �̵�, ü��, ���¹̳�, �Ѿ� ���� ��ü
    private IMovement movementHandler;
    private HealthManager healthManager;
    private StaminaManager staminaManager;
    private BulletManager bulletManager;

    // �̵� �Է°� ���� ���õ� �Ѿ� �ε���
    private Vector3 movementInput;
    private int currentBulletIndex = 0;

    // Rigidbody ������Ʈ
    private Rigidbody rb;

    void Start()
    {
        // ������Ʈ �ʱ�ȭ
        rb = GetComponent<Rigidbody>(); // Rigidbody ��������
        movementHandler = new RigidbodyMovement(rb); // RigidbodyMovement Ŭ���� ����
        healthManager = new HealthManager(5, hearts, fullHeart, emptyHeart); // ü�� �Ŵ��� �ʱ�ȭ
        staminaManager = new StaminaManager(100f, 10f, 5f, 2f); // ���¹̳� �Ŵ��� �ʱ�ȭ
        bulletManager = new BulletManager(specialBulletPrefabs, bulletCooldowns); // �Ѿ� �Ŵ��� �ʱ�ȭ

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
            FireBullet(); // �Ѿ� �߻�
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
        // ����(X) �� ����(Z) �Է� �� �ޱ�
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    // �̵� ó��: �÷��̾ �̵��ϵ��� ó��
    private void MovePlayer()
    {
        // �̵� �ӵ� ����: ���� Shift Ű�� ������ �޸���, �ƴϸ� �Ϲ� �̵�
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        // �̵� ���� ���: �Էµ� �̵� ���� ���� ��ǥ�� ��ȯ�Ͽ� ����ȭ
        Vector3 moveDirection = transform.TransformDirection(movementInput).normalized;

        // �̵� ó��
        movementHandler.Move(moveDirection, currentSpeed);
    }

    // �Ѿ� �߻�: ī�޶��� ������ ���� �Ѿ� �߻�
    private void FireBullet()
    {
        // ī�޶��� forward �������� �Ѿ� �߻�
        Vector3 bulletDirection = cameraTransform.forward;
        bulletManager.FireBullet(currentBulletIndex, cameraTransform.position, bulletDirection, bulletSpeed);
    }

    // ���¹̳� ó��: �̵� �� ������Ʈ�� ���� ���¹̳� ��ȭ
    private void HandleStamina()
    {
        // ���� Shift Ű�� ������ ������ ���¹̳� �Ҹ�, �ƴϸ� ���¹̳� ȸ��
        if (Input.GetKey(KeyCode.LeftShift))
        {
            staminaManager.DrainStamina(Time.deltaTime); // ���¹̳� �Ҹ�
        }
        else
        {
            staminaManager.RegenStamina(Time.deltaTime); // ���¹̳� ȸ��
        }

        // ���¹̳� UI ������Ʈ
        staminaBar.value = staminaManager.CurrentStamina;
    }
}