using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour
{
    private Animator animator;

    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ButtonActive()
    {
        animator.SetBool("scaleUp", true);
    }

    public void ButtonNoActive()
    {
        animator.SetBool("scaleUp", false);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
