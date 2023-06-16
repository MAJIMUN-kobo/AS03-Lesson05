using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// === ���R�ړ��ł���J�����̃��C���X�N���v�g
public class AdvansedCamera : MonoBehaviour
{
    // === �p�����[�^�[�Ǘ��̓����N���X
    [System.Serializable]
    public class Params
    {
        public Transform lookTarget;        // �ǂ�������Ώۂ̃I�u�W�F�N�g
        public float tiltMin = 0;           // �����p�x�i�ŏ��l�j
        public float tiltMax = 90;          // �����p�x�i�ő�l�j
        public float distance = 0;          // �^�[�Q�b�g�Ƃ̋���
        public float distanceMin = 0;       // �^�[�Q�b�g�Ƃ̋����i�ŏ��l�j
        public float distanceMax = 50;      // �^�[�Q�b�g�Ƃ̋����i�ő�l�j
        public float fieldOfView = 45;      // ����p
        public Vector3 position;            // ���W
        public Vector3 angles;              // �p�x
        public Vector3 offsetPosition;      // ���W�i���炵�j
        public Vector3 offsetAngles;        // ���W�i���炵�j
    }

    public Transform worldPoint;        // �u�e(WorldPoint)�v�I�u�W�F�N�g
    public Transform localPoint;        // �u�q(LocalPoint�v�I�u�W�F�N�g
    public Camera mainCamera;           // �u�J����(Main Camera)�v�I�u�W�F�N�g
    public Params parameter;            // �p�����[�^�[���

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputDistance();        // �������͂̎��s
        InputRotation();        // ��]���͂̎��s
    }

    // ���C�t�T�C�N���̍Ō�ɍX�V�����֐�
    void LateUpdate()
    {
        if (worldPoint == null || localPoint == null || mainCamera == null)
        {
            Debug.LogError("�I�u�W�F�N�g���ݒ肳��Ă��܂���B");
            return;     // �֐��������I��
        }

        // === �^�[�Q�b�g��ǂ�������
        if(parameter.lookTarget != null)
        {   // �^�[�Q�b�g������ꍇ
            parameter.position = Vector3.Lerp(parameter.position, parameter.lookTarget.position, Time.deltaTime);
        }

        // === �e�ɍ��W/�p�x�̌��ʂ𔽉f������
        worldPoint.position = parameter.position;
        worldPoint.eulerAngles = parameter.angles;

        // === �q�ɋ����̌��ʂ𔽉f������
        Vector3 p = localPoint.localPosition;
        p.z = parameter.distance;
        localPoint.localPosition = p;

        // === ���C���J�����{�̂̐ݒ�
        mainCamera.fieldOfView = parameter.fieldOfView;
        mainCamera.transform.localPosition = parameter.offsetPosition;
        mainCamera.transform.localEulerAngles = parameter.offsetAngles;
    }

    void InputDistance ()
    {
        float wheel = Input.GetAxis("Mouse ScrollWheel");
        parameter.distance += wheel * 5;

        // ��������
        parameter.distance = Mathf.Clamp( parameter.distance,
                                          parameter.distanceMin,
                                          parameter.distanceMax );
    }

    void InputRotation()
    {
        float rotSpeed = 90;        // ��]���x

        if(Input.GetKey(KeyCode.I) == true)
        {   // [I�L�[]�ŏ�ɌX��
            parameter.angles.x += rotSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.J) == true)
        {   // [J�L�[]�ō��ɌX��
            parameter.angles.y -= rotSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.K) == true)
        {   // [K�L�[]�ŉ��ɌX��
            parameter.angles.x -= rotSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.L) == true)
        {   // [L�L�[]�ŉE�ɌX��
            parameter.angles.y += rotSpeed * Time.deltaTime;
        }
    }
}
