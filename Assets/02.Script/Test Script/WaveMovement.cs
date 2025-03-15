using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    public float waveFrequency = 100f; // ���� ���ļ�
    public float waveMagnitude = 25f; // ���� ����
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float wave = Mathf.Sin(Time.time * waveFrequency) * waveMagnitude;
        transform.position = initialPosition + new Vector3(wave, 0, 0);
        initialPosition += Vector3.down * Time.deltaTime * 500f; // �Ʒ��� �̵�
    }
}