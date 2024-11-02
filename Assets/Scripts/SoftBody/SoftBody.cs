using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SoftBody : MonoBehaviour
{
    [SerializeField] private List<Rigidbody2D> boneList;
    [SerializeField] SpringSettings springSettings;

    private void Awake()
    {
        CreateBoneSprings();
    }

    // работает только для чётных количеств
    public void CreateBoneSprings()
    {
        for (int i = 0; i < boneList.Count; i++)
        {
            var bone = boneList[i];

            var leftSpring = bone.gameObject.AddComponent<SpringJoint2D>();
            var rightSpring = bone.gameObject.AddComponent<SpringJoint2D>();
            var opositeSpring = bone.gameObject.AddComponent<SpringJoint2D>();

            leftSpring.connectedBody = i == 0 ? boneList[boneList.Count - 1] : boneList[i - 1];
            rightSpring.connectedBody = i == boneList.Count - 1 ? boneList[i] : boneList[i - 1];
            opositeSpring.connectedBody = i >= boneList.Count / 2 ? boneList[i - boneList.Count / 2] : boneList[i + boneList.Count / 2];


            leftSpring.frequency = springSettings.Frequency;
            rightSpring.frequency = springSettings.Frequency;
            opositeSpring.frequency = springSettings.Frequency;
        }
    }
}
