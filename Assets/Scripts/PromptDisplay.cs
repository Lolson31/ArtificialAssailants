using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptDisplay : MonoBehaviour
{
    public ImageRetriever imageRetriever;
    private InputField input;
    // Start is called before the first frame update
    void Start()
    {
        input= GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        imageRetriever.input = input.text;
    }
}
