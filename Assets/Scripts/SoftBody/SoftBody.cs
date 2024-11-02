using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

public class SoftBody : MonoBehaviour
{
    [SerializeField] private Rigidbody2D midBone;
    [SerializeField, Tooltip("Работает только для чётных количеств")] private List<Rigidbody2D> boneList;
    [SerializeField] private float solidFrequency;
    [SerializeField] private float liquidFrequency;

    private List<SpringJoint2D> innerJoints;

    private void Awake()
    {
        CreateMidBoneSprings();
    }

    public void SetSolid()
    {
        innerJoints.ForEach(joint => {joint.frequency = solidFrequency;});
    }

    public void SetLiquid()
    {
        innerJoints.ForEach(joint => { joint.frequency = liquidFrequency;});
    }

    private void SettingSpring(SpringJoint2D spring, Rigidbody2D body, bool isInner)
    {
        spring.connectedBody = body;
        spring.frequency = isInner ? liquidFrequency : liquidFrequency * 3;
    }

    public void CreateMidBoneSprings()
    {
        innerJoints = new List<SpringJoint2D>();

        for (int i = 0; i < boneList.Count; i++)
        {
            var bone = boneList[i];

            var oldSprings = bone.GetComponents<Joint2D>();
            foreach (var item in oldSprings)
            {
                DestroyImmediate(item);
            }

            var leftSpring = bone.gameObject.AddComponent<SpringJoint2D>();
            var rightSpring = bone.gameObject.AddComponent<SpringJoint2D>();
            var innerSpring = bone.gameObject.AddComponent<SpringJoint2D>();

            innerJoints.Add(innerSpring);

            SettingSpring(leftSpring, i == 0 ? boneList[boneList.Count - 1] : boneList[i - 1], false);
            SettingSpring(rightSpring, i == boneList.Count - 1 ? boneList[0] : boneList[i + 1], false);
            SettingSpring(innerSpring, midBone, true);
        }
    }
}

[CustomEditor(typeof(SoftBody))]
[CanEditMultipleObjects]
class DecalMeshHelperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var script = target as SoftBody;

        if (GUILayout.Button("Generate springs"))
        {
            script.CreateMidBoneSprings();
        }
    }
}
