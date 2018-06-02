using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextFromFile : MonoBehaviour {

    public TextAsset textFile;

    void Start() {
        Text text = GetComponent<Text>();
        text.text = textFile.text;
    }
}
