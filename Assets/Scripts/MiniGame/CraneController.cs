using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CraneController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // �¿� �̵� �ӵ�
    [SerializeField] private float dropSpeed = 2f; // ���� �������� �ӵ�
    [SerializeField] private float dropDelay = 3f; // �ڵ����� ���� �������� �ð�
    private bool isDropping = false; // ���԰� �������� ������ Ȯ��

    private Vector3 startPosition; // ���� ��ġ ����

    [SerializeField] private GameObject joyStick; // �ڵ����� ���� �������� �ð�
    [SerializeField] private float joyStickRotation;    // ȸ�� �ð�
    [SerializeField] private float joyStickRotationSpeed;   // ȸ�� �ӵ�

    private Transform joyStickTransform;
    private CraneManager craneManager;


    private void Start()
    {
        startPosition = transform.position; // ũ������ �ʱ� ��ġ ����
        joyStickTransform = joyStick.transform;
        craneManager = FindObjectOfType<CraneManager>();
    }

    private void Update()
    {
        if (!isDropping) // ���԰� �������� ���� �ƴ� ���� ���� ����
        {
            float move = Input.GetAxisRaw("Horizontal"); // �¿� �Է� �ޱ�
            transform.position += new Vector3(move * moveSpeed * Time.deltaTime, 0, 0);
            if (joyStickTransform != null)
            {
                float targetRz = -move * joyStickRotation;
                Quaternion targetRotation = Quaternion.Euler(0, 0, targetRz);
                joyStickTransform.rotation = Quaternion.Lerp(joyStickTransform.rotation, targetRotation, Time.deltaTime * joyStickRotationSpeed);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !isDropping)
        {
            StartCoroutine(DropCrane());
        }
        if (Input.GetKeyDown(KeyCode.V))
        { 
            craneManager.ReleaseGift();
        }
    }

    // �ڵ����� ���� �ð� �� ���԰� ���������� ����
    private IEnumerator AutoDropCrane()
    {
        yield return new WaitForSeconds(dropDelay);
        StartCoroutine(DropCrane());
    }

    // ���Ը� ������ �ڷ�ƾ
    private IEnumerator DropCrane()
    {
        isDropping = true;
        Vector3 targetPosition = transform.position + new Vector3(0, -3f, 0); // ���԰� ������ ��ġ ����

        while (transform.position.y > targetPosition.y)
        {
            transform.position -= new Vector3(0, dropSpeed * Time.deltaTime, 0);
            yield return null;
        }
        craneManager.CatchGift();
        yield return new WaitForSeconds(0.5f); // ���԰� ��� �ӹ���

        // ���� ���Ը� �ٽ� �ø���
        StartCoroutine(RaiseCrane());
    }

    // ���Ը� ���� ��ġ�� �ø��� �ڷ�ƾ
    private IEnumerator RaiseCrane()
    {
        Vector3 targetPosition = startPosition; // ���� ��ġ�� ���ư���

        while (transform.position.y < targetPosition.y)
        {
            transform.position += new Vector3(0, dropSpeed * Time.deltaTime, 0);
            yield return null;
        }

        isDropping = false;
        
    }
}
