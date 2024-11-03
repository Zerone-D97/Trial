using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// A class for grabbing the bridge object with the ObiRope.
/// </summary>
public class RopeGrabInteractable : XRGrabInteractable
{
    enum FollowState
    {
        No,
        Follow
    }

    enum GrabState
    {
        No,
        Grab
    }

    [SerializeField] private FollowState followState;

    [SerializeField] private GrabState grabState = GrabState.No;

    [SerializeField] private Rope rope;

    private Rigidbody selfRigidbody;

    private XRDirectInteractor interactor = null;

    private Vector3 grabRopePosition;

    private Vector3 grabRopeDirection;

    protected override void Awake()
    {
        base.Awake();
        this.followState = FollowState.No;
        this.interactor = null;
        this.selfRigidbody = GetComponent<Rigidbody>();
        this.rope = this.GetComponentInParent<Rope>();
        if(this.rope!=null) this.transform.parent = this.rope?.transform.parent;

    }

    protected override void Grab()
    {
        this.grabState = GrabState.Grab;
        base.Grab();
        if (firstInteractorSelecting.hasSelection)
        {
            this.rope.AddOrEnableParticleAttachment(this, this.transform);
        }
    }

    protected override void Drop()
    {
        this.grabState = GrabState.No;
        base.Drop();
        this.rope.DisableParticleAttachment(this);
    }

    public void OnObiCollisionEnter(XRDirectInteractor xRDirectInteractor, Vector3 ropePoint, Vector3 ropeDirection)
    {
        if (this.interactor != null) return;
        this.followState = FollowState.Follow;
        this.interactor = xRDirectInteractor;
        SetFollowParameter(ropePoint, ropeDirection);
    }

    public void OnObiCollisionStay(XRDirectInteractor xRDirectInteractor, Vector3 ropePoint, Vector3 ropeDirection)
    {
        SetFollowParameter(ropePoint, ropeDirection);
    }

    public void OnObiCollisionExit(XRDirectInteractor xRDirectInteractor, Vector3 ropePoint, Vector3 ropeDirection)
    {
        this.interactor = null;
        this.followState = FollowState.No;
    }

    private void SetFollowParameter(Vector3 grabRopePosition, Vector3 grabRopeDirection)
    {
        this.grabRopePosition = grabRopePosition;
        this.grabRopeDirection = grabRopeDirection;
    }

    /// <summary>
    /// Move in conjunction with the controller
    /// </summary>
    private void FixedUpdate()
    {
        if(this.followState == FollowState.Follow && this.grabState == GrabState.No)
        {
            this.selfRigidbody.MovePosition(grabRopePosition);
        }
    }

}

