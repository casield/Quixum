using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    // Start is called before the first frame update
    private MeshRenderer rend;
    public ObjectDesigner designer;
    public Collider mCollider;

    // Start is called before the first frame update
    void Awake()
    {
        rend = gameObject.GetComponent<MeshRenderer>();
    }



    // Update is called once per frame
    void Update()
    {

    }
}

[CustomEditor(typeof(BoundingBox)), CanEditMultipleObjects]
public class BoundingBoxEditor : Editor
{
    // Custom in-scene UI for when ExampleScript
    // component is selected.
    private BoundingBox boundingBox;
    private string Error = null;
    private SerializedObject designer;

    void OnEnable()
    {
        // Setup the SerializedProperties.
        designer = new SerializedObject(target);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        boundingBox = ((BoundingBox)designer.targetObject);
        if (Error != null)
        {
            EditorGUILayout.HelpBox(Error, MessageType.Error);
            boundingBox.designer.boundingBox = null;
            Error = null;
        }
        else
        {
            if (boundingBox.designer != null)
            {
                boundingBox.designer.boundingBox = boundingBox;
            }
            else
            {
                Error = "No Designer";
                boundingBox.designer = boundingBox.GetComponent<ObjectDesigner>();
            }
            if(boundingBox.mCollider== null){
                Error = "No collider";
            }
            EditorGUILayout.HelpBox("Hello", MessageType.Info);

        }


        ButtonCreateCollider();

        Collider();

        serializedObject.ApplyModifiedProperties();
    }

    private void ButtonCreateCollider()
    {

        if (designer != null)
        {
            
            var dd = designer.targetObjects;

            foreach (var obj in dd)
            {
             
                var boundingBox = ((BoundingBox)obj);
                if (boundingBox.mCollider == null)
                {
                    if (GUILayout.Button("Create Collider"))
                    {

                        if (boundingBox.designer.type != "sphere")
                        {
                            boundingBox.mCollider = boundingBox.gameObject.AddComponent<BoxCollider>();
                        }
                        else
                        {
                            boundingBox.mCollider = boundingBox.gameObject.AddComponent<SphereCollider>();
                        }
                    }

                }
            }
            //   BoundingBox bb = ((ObjectDesigner)designer.objectReferenceValue).boundingBox;
            // QuixConsole.Log("Designer",designer.objectReferenceValue);

        }

    }

    private void Collider()
    {
        if (boundingBox.designer != null)
        {
            if (boundingBox.mCollider != null)
            {
                if (boundingBox.designer.type == "sphere" && !(boundingBox.mCollider is SphereCollider))
                {
                    Error = "Distintos colliders en " + boundingBox.designer.gameObject.name;
                }
            }


        }
        else
        {
            if (boundingBox.designer == null)
            {
                ObjectDesigner ob;
                if (boundingBox.gameObject.TryGetComponent<ObjectDesigner>(out ob))
                {
                    boundingBox.designer = ob;
                }
                else
                {
                    Error = "No object designer";
                }
            }
            else
            {

            }

        }

    }
}
