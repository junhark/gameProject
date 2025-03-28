using UnityEngine;

public class MovementHandler
{
    private Rigidbody rb;
    private float moveSpeed;
    private float sprintSpeed;

    // 생성자: Rigidbody와 이동 속도, 스프린트 속도를 초기화
    public MovementHandler(Rigidbody rb, float moveSpeed, float sprintSpeed)
    {
        this.rb = rb;
        this.moveSpeed = moveSpeed;
        this.sprintSpeed = sprintSpeed;
    }

    // 플레이어를 이동시키는 함수
    public void Move(Vector3 movementInput, bool isSprinting)
    {
        // 현재 속도 결정: 스프린트 여부에 따라 이동 속도 설정
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        // 이동 방향 계산: 입력된 이동 값을 월드 좌표로 변환하여 정규화
        Vector3 moveDirection = rb.transform.TransformDirection(movementInput).normalized;

        // 이동 처리: Rigidbody를 사용하여 이동
        rb.MovePosition(rb.position + moveDirection * currentSpeed * Time.fixedDeltaTime);
    }
}