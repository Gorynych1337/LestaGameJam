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
    [SerializeField] private float frequency;
    [SerializeField] private float distance;

    /*private void Update()
    {
        boneList.ForEach(bone => {
            Vector3 Look = bone.transform.InverseTransformPoint(midBone.transform.position);
            float Angle = Mathf.Atan2(Look.y, Look.x) * Mathf.Rad2Deg;
            bone.transform.Rotate(0, 0, Angle);
        });
    }*/

    private void SettingSpring(SpringJoint2D spring, Rigidbody2D body)
    {
        spring.connectedBody = body;
        spring.frequency = frequency;
        //spring.autoConfigureDistance = false;
        //spring.distance = distance;
    }

    public void CreateMidBoneSprings()
    {
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
            var opositeSpring = bone.gameObject.AddComponent<SpringJoint2D>();

            SettingSpring(leftSpring, i == 0 ? boneList[boneList.Count - 1] : boneList[i - 1]);
            SettingSpring(rightSpring, i == boneList.Count - 1 ? boneList[0] : boneList[i + 1]);
            SettingSpring(opositeSpring, midBone);
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
