using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPersonCut : MonoBehaviour
{



    [Tooltip("Animated Object")]
    public GameObject animatedObject;

    [Tooltip("Scene to Load")]
    public string sceneToLoad;

    public string stateName;
    private Animator animator;
    private bool hasSwitched = false;


    void Start()
    {
        if (animatedObject != null)
        {
            animator = animatedObject.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (hasSwitched || animator == null)
            return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        bool isCorrectState = string.IsNullOrEmpty(stateName) || stateInfo.IsName(stateName);

        if (isCorrectState && stateInfo.normalizedTime >= 1f && !animator.IsInTransition(0))
        {
            hasSwitched = true;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

