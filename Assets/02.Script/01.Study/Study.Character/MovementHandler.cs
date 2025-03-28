using UnityEngine;

public class MovementHandler
{
    private Rigidbody rb;
    private float moveSpeed;
    private float sprintSpeed;

    // ������: Rigidbody�� �̵� �ӵ�, ������Ʈ �ӵ��� �ʱ�ȭ
    public MovementHandler(Rigidbody rb, float moveSpeed, float sprintSpeed)
    {
        this.rb = rb;
        this.moveSpeed = moveSpeed;
        this.sprintSpeed = sprintSpeed;
    }

    // �÷��̾ �̵���Ű�� �Լ�
    public void Move(Vector3 movementInput, bool isSprinting)
    {
        // ���� �ӵ� ����: ������Ʈ ���ο� ���� �̵� �ӵ� ����
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        // �̵� ���� ���: �Էµ� �̵� ���� ���� ��ǥ�� ��ȯ�Ͽ� ����ȭ
        Vector3 moveDirection = rb.transform.TransformDirection(movementInput).normalized;

        // �̵� ó��: Rigidbody�� ����Ͽ� �̵�
        rb.MovePosition(rb.position + moveDirection * currentSpeed * Time.fixedDeltaTime);
    }
}