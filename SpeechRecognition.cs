using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class SpeechRecognition : MonoBehaviour
{
    string command = "stop";
    KeywordRecognizer kwR;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    void Start()
    {
        keywords.Add("run", () => { command = "run"; });
        keywords.Add("left", () => { command = "left"; });
        keywords.Add("right", () => { command = "right"; });
        keywords.Add("back", () => { command = "back"; });
        keywords.Add("stop", () => { command = "stop"; });
        keywords.Add("laugh", () => { command = "laugh"; }); // added laugh because left sounds like laugh
        kwR = new KeywordRecognizer(keywords.Keys.ToArray());
        kwR.OnPhraseRecognized += Keywordrecognizer_OnPhraseRecognized;
        kwR.Start();
    }
    void Keywordrecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
            keywordAction.Invoke();
    }    
    void FixedUpdate()
    {
        if (command == "stop")
            transform.Translate(0, 0, 0);
        else if (command == "run")
            transform.Translate(0, 0, .5f);
        else if (command == "back")
            transform.Translate(0, 0, -.5f);
        else if (command == "left")
            transform.Translate(-.5f, 0, 0);
        else if (command == "laugh") // added laugh because left sounds like laugh
            transform.Translate(-.5f, 0, 0);
        else if (command == "right")
            transform.Translate(.5f, 0, 0);
    }
}
