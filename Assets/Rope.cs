using Obi;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ObiRopeにアタッチする
/// </summary>
[RequireComponent(typeof(ObiRope))]
public class Rope : MonoBehaviour
{
    public RopeGrabInteractable LeftRopeGrabInteractable => this.leftRopeGrabInteractable;

    public RopeGrabInteractable RightRopeGrabInteractable => this.rightRopeGrabInteractable;

    [SerializeField] RopeGrabInteractable leftRopeGrabInteractable;

    [SerializeField] RopeGrabInteractable rightRopeGrabInteractable;

    ObiRope obiRope;

    Dictionary<RopeGrabInteractable, ObiParticleAttachment> attachimentDict = new Dictionary<RopeGrabInteractable, ObiParticleAttachment>();

    private void Awake()
    {
        this.obiRope = this.GetComponent<ObiRope>();
    }

    /// <summary>
    /// ObiParticleAttachmentをtargetに対して有効にする
    /// </summary>
    /// <param name="ropeGrabInteractable"></param>
    /// <param name="target"></param>
    public void AddOrEnableParticleAttachment(RopeGrabInteractable ropeGrabInteractable, Transform target)
    {
        if (!this.attachimentDict.ContainsKey(ropeGrabInteractable))
        {
            var particleAttachment = this.AddComponent<ObiParticleAttachment>();
            particleAttachment.target = target;
           
            particleAttachment.attachmentType = ObiParticleAttachment.AttachmentType.Dynamic;
            particleAttachment.particleGroup = FindNearObiParticleGroup(target);
            this.attachimentDict[ropeGrabInteractable] = particleAttachment;
        }
        else
        {
            this.attachimentDict[ropeGrabInteractable].particleGroup = FindNearObiParticleGroup(target);
            this.attachimentDict[ropeGrabInteractable].enabled = true;
        }
    }

    /// <summary>
    /// ObiParticleAttachmentをtarget無効にする
    /// </summary>
    /// <param name="ropeGrabInteractable"></param>
    public void DisableParticleAttachment(RopeGrabInteractable ropeGrabInteractable)
    {
        if (this.attachimentDict.ContainsKey(ropeGrabInteractable))
        {
            this.attachimentDict[ropeGrabInteractable].enabled = false;
        }
    }

    private ObiParticleGroup FindNearObiParticleGroup(Transform target)
    {
        var distance = 100000f;
        ObiParticleGroup findParticleGroup = null;
        foreach(var group in this.obiRope.blueprint.groups)
        {
            foreach(var particleindex in group.particleIndices)
            {
                var particlePosition = this.obiRope.GetParticleWorldPosition(particleindex);
                var currentDistance = Vector3.Distance(target.position, particlePosition);
                if(currentDistance <= distance)
                {
                    distance = currentDistance;
                    findParticleGroup = group;
                }
            }
        }

        return findParticleGroup;
    }
}
