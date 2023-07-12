using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// === 自由移動できるカメラのメインスクリプト
public class AdvansedCamera : MonoBehaviour
{
    // === パラメーター管理の内部クラス
    [System.Serializable]
    public class Params
    {
        public Transform lookTarget;        // 追いかける対象のオブジェクト
        public float tiltMin = 0;           // 垂直角度（最小値）
        public float tiltMax = 90;          // 垂直角度（最大値）
        public float distance = 0;          // ターゲットとの距離
        public float distanceMin = 0;       // ターゲットとの距離（最小値）
        public float distanceMax = 50;      // ターゲットとの距離（最大値）
        public float fieldOfView = 45;      // 視野角
        public Vector3 position;            // 座標
        public Vector3 angles;              // 角度
        public Vector3 offsetPosition;      // 座標（ずらし）
        public Vector3 offsetAngles;        // 座標（ずらし）
    }

    public Transform worldPoint;        // 「親(WorldPoint)」オブジェクト
    public Transform localPoint;        // 「子(LocalPoint」オブジェクト
    public Camera mainCamera;           // 「カメラ(Main Camera)」オブジェクト
    public Params parameter;            // パラメーター情報

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputDistance();        // 距離入力の実行
        InputRotation();        // 回転入力の実行
    }

    // ライフサイクルの最後に更新される関数
    void LateUpdate()
    {
        if (worldPoint == null || localPoint == null || mainCamera == null)
        {
            Debug.LogError("オブジェクトが設定されていません。");
            return;     // 関数を強制終了
        }

        // === ターゲットを追いかける
        if(parameter.lookTarget != null)
        {   // ターゲットがいる場合
            parameter.position = Vector3.Lerp(parameter.position, parameter.lookTarget.position, Time.deltaTime);
        }

        // === 親に座標/角度の結果を反映させる
        worldPoint.position = parameter.position;
        worldPoint.eulerAngles = parameter.angles;

        // === 子に距離の結果を反映させる
        Vector3 p = localPoint.localPosition;
        p.z = parameter.distance;
        localPoint.localPosition = p;

        // === メインカメラ本体の設定
        mainCamera.fieldOfView = parameter.fieldOfView;
        mainCamera.transform.localPosition = parameter.offsetPosition;
        mainCamera.transform.localEulerAngles = parameter.offsetAngles;
    }

    void InputDistance ()
    {
        float wheel = Input.GetAxis("Mouse ScrollWheel");
        parameter.distance += wheel * 5;

        // 距離制限
        parameter.distance = Mathf.Clamp( parameter.distance,
                                          parameter.distanceMin,
                                          parameter.distanceMax );
    }

    void InputRotation()
    {
        float rotSpeed = 90;        // 回転速度

        if(Input.GetKey(KeyCode.I) == true)
        {   // [Iキー]で上に傾く
            parameter.angles.x += rotSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.J) == true)
        {   // [Jキー]で左に傾く
            parameter.angles.y -= rotSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.K) == true)
        {   // [Kキー]で下に傾く
            parameter.angles.x -= rotSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.L) == true)
        {   // [Lキー]で右に傾く
            parameter.angles.y += rotSpeed * Time.deltaTime;
        }
    }
}
