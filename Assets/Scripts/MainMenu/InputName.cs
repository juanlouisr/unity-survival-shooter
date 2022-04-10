using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputName : MonoBehaviour
{

    public InputField inputName;
    // Start is called before the first frame update
    void Start()
    {
        inputName.text = PlayerPrefs.GetString("name");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveName()
    {
        PlayerPrefs.SetString("name", inputName.text);
    }
}
