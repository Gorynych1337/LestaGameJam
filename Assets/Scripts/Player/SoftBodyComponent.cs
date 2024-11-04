using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SoftBodyComponent : MonoBehaviour
{
    [SerializeField] private Rigidbody2D midBone;
    [SerializeField, Tooltip("Only for even numbers")] private List<Rigidbody2D> boneList;
    [SerializeField] private float solidFrequency;
    [SerializeField] private float liquidFrequency;

    private List<SpringJoint2D> joints;

    private void Awake()
    {
        CreateMidBoneSprings();
        
        joints = new List<SpringJoint2D>();
        foreach (var joint in boneList.SelectMany(bone => bone.GetComponents<SpringJoint2D>()))
        {
            joints.Add(joint);
        }
        
    }

    public void SetSolid()
    {
        joints.ForEach(joint => {joint.frequency = solidFrequency;});
    }

    public void SetLiquid()
    {
        joints.ForEach(joint => { joint.frequency = liquidFrequency;});
    }
    
    public void ResizeSpringDistance(float percent)
    {
        foreach (var joint in joints)
        {
            joint.distance -= joint.distance * percent;
        }
    }

    private void SettingSpring(SpringJoint2D spring, Rigidbody2D body, float frequency, float dampingRatio)
    {
        spring.connectedBody = body;
        spring.frequency = frequency;
        spring.dampingRatio = dampingRatio;
        spring.autoConfigureDistance = false;
    }
    
    public void CreateMidBoneSprings()
    {
        for (var i = 0; i < boneList.Count; i++)
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

            SettingSpring(leftSpring, i == 0 ? boneList[boneList.Count - 1] : boneList[i - 1], liquidFrequency, 1);
            SettingSpring(rightSpring, i == boneList.Count - 1 ? boneList[0] : boneList[i + 1], liquidFrequency, 1);
            SettingSpring(innerSpring, midBone, liquidFrequency, 1);
            
            leftSpring = bone.gameObject.AddComponent<SpringJoint2D>();
            rightSpring = bone.gameObject.AddComponent<SpringJoint2D>();

            SettingSpring(leftSpring, i < 4 ? boneList[boneList.Count - 4 + i] : boneList[i - 4], liquidFrequency, 1);
            SettingSpring(rightSpring, i >= boneList.Count - 4 ? boneList[i - boneList.Count + 4] : boneList[i + 4], liquidFrequency, 1);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SoftBodyComponent))]
[CanEditMultipleObjects]
class DecalMeshHelperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var script = target as SoftBodyComponent;

        if (GUILayout.Button("Generate springs"))
        {
            script.CreateMidBoneSprings();
        }
    }
}
#endif