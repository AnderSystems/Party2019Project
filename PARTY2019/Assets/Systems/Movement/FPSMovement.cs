using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    public Rigidbody rb { get; set; }
    public Animator anim { get; set; }
    [System.Serializable]
    public class _Physics
    {
        [Header("GroundDetecton")]
        public LayerMask GroundLayers;
        public float GroundDistance;
    }
    [SerializeField]
    public _Physics PlayerPhysics;
    [System.Serializable]
    public class _FPSView
    {
        public Transform FPS_Neck;
        public Transform FPS_Head;
        public float SlopeZ;
        [Header("HeadConfiguration")]
        public Transform Head;
        [Space]
        public Vector2 Min = new Vector2(-70, -Mathf.Infinity);
        public Vector2 Max = new Vector2(80, Mathf.Infinity);
        [System.Serializable]
        public enum _Invert
        {
            X, Y, Both, None
        }
        [SerializeField]
        public _Invert invert = _Invert.None;
        public Vector2 HeadMov { get; set; }
        [Space]
        [Space]
        [Header("IK")]
        public float IKWeight;
        public float HeadIK;
        [Space]
        public Transform FPSHandPos;
        //public float LFootIK;
        //public float RFootIK;
    }
    [SerializeField]
    public _FPSView FPSView;

    [System.Serializable]
    public class _Movement
    {
        public Vector3 Mov { get; set; }
        public float Speed;
    }
    [SerializeField]
    public _Movement Movement;

    //CallVoids
    void Start()
    {
        //Get Componets
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        PlaceOnGround();
        Quaternion Rotations = Quaternion.Euler(0, FPSView.Head.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, Rotations, 8 * Time.deltaTime);
        MoveChar();
        Animate();
    }
    void Update()
    {
        MoveHead();
    }

    //MoveChar
    void MoveChar()
    {
        Vector3 HMov = new Vector3(Input.GetAxis("Horizontal"), 0,0);
        Vector3 VMov = new Vector3(0, 0, Input.GetAxis("Vertical"));

        Movement.Mov = Vector3.Lerp(Movement.Mov, Vector3.ClampMagnitude(HMov + VMov, Movement.Speed), 5 * Time.deltaTime);
        transform.Translate(Movement.Mov * 0.1f);


        FPSView.FPS_Neck.transform.localEulerAngles = new Vector2(Input.GetAxis("Mouse Y") * -1, Input.GetAxis("Mouse X") * 0.5f);
    }
    void Animate()
    {
        anim.SetFloat("MovX", Movement.Mov.x);
        anim.SetFloat("MovY", Movement.Mov.z);
    }

    //IK
    void OnAnimatorIK(int layerIndex)
    {
        float Head = anim.GetFloat("HeadIK") * (FPSView.HeadIK * FPSView.IKWeight);
        //float Lfoot = anim.GetFloat("LFootIK") * (AnimationIK.LFootIK * AnimationIK.IKWeight);
        //float Rfoot = anim.GetFloat("RFootIK") * (AnimationIK.RFootIK * AnimationIK.IKWeight);

        FPSView.Head.position = anim.GetBoneTransform(HumanBodyBones.Neck).position;
        FPSView.FPS_Neck.position = anim.GetBoneTransform(HumanBodyBones.Head).position;
        FPSView.FPS_Head.localPosition = new Vector3(0,FPSView.SlopeZ / 100,0);

        FPSView.FPS_Neck.rotation = Quaternions.Add(anim.GetBoneTransform(HumanBodyBones.Head).rotation,
            Quaternion.Euler(Input.GetAxis("Mouse Y") * -1, Input.GetAxis("Mouse X") * 0.5f,0));
        anim.SetBoneLocalRotation(HumanBodyBones.Head, Quaternions.Subtract(FPSView.Head.rotation,
            anim.GetBoneTransform(HumanBodyBones.Neck).rotation));
        if (anim.GetBool("GunEquip") == true)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKPosition(AvatarIKGoal.RightHand, FPSView.FPSHandPos.position);
        }
    }

    //AnimationIK
    public void MoveHead()
    {
        if (FPSView.invert == _FPSView._Invert.None)
        {
            FPSView.HeadMov = new Vector2(
                Mathf.Clamp(FPSView.HeadMov.x - Input.GetAxis("Mouse Y") * 1.5f, FPSView.Min.x,
                FPSView.Max.x),
                Mathf.Clamp(FPSView.HeadMov.y + Input.GetAxis("Mouse X") * 1.5f, FPSView.Min.y,
                FPSView.Max.y));
        }

        if (FPSView.invert == _FPSView._Invert.X)
        {
            FPSView.HeadMov = new Vector2(
                Mathf.Clamp(FPSView.HeadMov.x - Input.GetAxis("Mouse Y") * 1.5f, FPSView.Min.x,
                FPSView.Max.x),
                Mathf.Clamp(FPSView.HeadMov.y - Input.GetAxis("Mouse X") * 1.5f, FPSView.Min.y,
                FPSView.Max.y));
        }

        if (FPSView.invert == _FPSView._Invert.Y)
        {
            FPSView.HeadMov = new Vector2(
                Mathf.Clamp(FPSView.HeadMov.x + Input.GetAxis("Mouse Y") * 1.5f, FPSView.Min.x,
                FPSView.Max.x),
                Mathf.Clamp(FPSView.HeadMov.y + Input.GetAxis("Mouse X") * 1.5f, FPSView.Min.y,
                FPSView.Max.y));
        }

        if (FPSView.invert == _FPSView._Invert.Both)
        {
            FPSView.HeadMov = new Vector2(
                Mathf.Clamp(FPSView.HeadMov.x + Input.GetAxis("Mouse Y") * 1.5f, FPSView.Min.x,
                FPSView.Max.x),
                Mathf.Clamp(FPSView.HeadMov.y + Input.GetAxis("Mouse X") * 1.5f, FPSView.Min.y,
                FPSView.Max.y));
        }


        FPSView.Head.eulerAngles =
            new Vector2(FPSView.HeadMov.x, FPSView.HeadMov.y);

    }

    //Physics
    void PlaceOnGround()
    {
        if (GroundHit(PlayerPhysics.GroundLayers, PlayerPhysics.GroundDistance))
        {
            transform.position = new Vector3(transform.position.x, HitDetection.point.y + (PlayerPhysics.GroundDistance * 0.9f)
                , transform.position.z);
            //rb.useGravity = false;
            rb.velocity = Vector3.zero;
        } else
        {
            rb.useGravity = true;
        }
    }
    public bool GroundHit(LayerMask Layers, float Distance)
    {
        return Physics.Linecast(transform.position + Vector3.up, transform.position - (Vector3.up * Distance), out HitDetection, Layers);
    }
    public RaycastHit HitDetection;
}
