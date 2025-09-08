using UnityEngine;
using System.Collections.Generic;

public class AnimeController : MonoBehaviour
{
    public GameObject eyeGlass;
    public GameObject camerPos1;
    public GameObject camerPos2;
    public GameObject playerPos1;
    public GameObject playerPos2;
    public GameObject eyeGlassHandler;
    private GameObject oldParentHandler;
    private Animator animator;
    private bool[] toggle = new bool[10];
    private Dictionary<KeyCode, (string start, string stop, int index, string animName)> animMap;

    void Start()
    {
        Camera.main.transform.SetPositionAndRotation(
            camerPos1.transform.position,
            camerPos1.transform.rotation
        );

        transform.SetPositionAndRotation(
            playerPos1.transform.position,
            playerPos1.transform.rotation
        );

        eyeGlass = GameObject.Find("EyeGlass");
        eyeGlassHandler = GameObject.Find("EyeGlassHandler");
        oldParentHandler = eyeGlass.transform.parent.gameObject;

        animator = GetComponent<Animator>();

        for (int i = 0; i < toggle.Length; i++)
            toggle[i] = true;

        animator.SetLayerWeight(1, 1f);

        animMap = new Dictionary<KeyCode, (string, string, int, string)>()
        {
            { KeyCode.T, ("Start_Talk", "Stop_Talk", 0, "Armature_Talk") },
            { KeyCode.W, ("Start_Wave", "Stop_Wave", 1, "Armature_Wave") },
            { KeyCode.N, ("Start_No", "Stop_No", 2, "Armature_No") },
            { KeyCode.M, ("Start_No2", "Stop_No2", 3, "Armature_No 2") },
            { KeyCode.P, ("Start_Pointing", "Stop_Pointing", 4, "Armature_Pointing") },
            { KeyCode.U, ("Start_TurnPoint", "Stop_TurnPoint", 5, "Armature_TurnPoint") },
            { KeyCode.D, ("Start_Doff", "Stop_Doff", 6, "Armature_Doff") },
            { KeyCode.F, ("Start_Funny", "Stop_Funny", 7, "Armature_Funny") },
        };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            Camera.main.transform.SetPositionAndRotation(
                camerPos2.transform.position,
                camerPos2.transform.rotation
            );
            transform.SetPositionAndRotation(
                playerPos2.transform.position,
                playerPos2.transform.rotation
            );
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Camera.main.transform.SetPositionAndRotation(
                camerPos1.transform.position,
                camerPos1.transform.rotation
            );
            transform.SetPositionAndRotation(
                playerPos1.transform.position,
                playerPos1.transform.rotation
            );
        }
        
        print("anar lalar");
        foreach (var entry in animMap)
        {
            if (Input.GetKeyDown(entry.Key))
            {
                int idx = entry.Value.index;

                if (toggle[idx])
                {
                    animator.SetTrigger(entry.Value.start);
                    toggle[idx] = false;

                }
                else
                {
                    animator.SetTrigger(entry.Value.stop);
                    toggle[idx] = true;
                }
            }
        }

        print(Vector3.Distance(eyeGlass.transform.position, eyeGlassHandler.transform.position));

        if (Vector3.Distance(eyeGlass.transform.position, eyeGlassHandler.transform.position) < 0.18f)
        {
            eyeGlass.transform.SetParent(eyeGlassHandler.transform, true);
        }
    }
}
