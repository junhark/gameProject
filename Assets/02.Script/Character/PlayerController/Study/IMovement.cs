using UnityEngine;

// 플레이어 이동 인터페이스
public interface IMovement
{
    void Move(Vector3 direction, float speed);
}