using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;
using Whisper;
using Whisper.Utils;

[RequireComponent(typeof(WhisperManager))]
[RequireComponent(typeof(MicrophoneRecord))]
public class VoiceToText : MonoBehaviour
{
    [SerializeField]
    bool logOutput = true;
    
    WhisperManager whisper;
    MicrophoneRecord microphoneRecord;
    WhisperStream stream;

    string text;
    
    void Awake()
    {
        whisper = GetComponent<WhisperManager>();
        microphoneRecord = GetComponent<MicrophoneRecord>();
    }

    async void Start()
    {
        stream = await whisper.CreateStream(microphoneRecord);
        stream.OnSegmentFinished += OnSegmentFinished;
        
        StartListening();
    }

    public void StartListening()
    {
        stream.StartStream();
        microphoneRecord.StartRecord();
    }

    public void StopListening()
    {
        microphoneRecord.StopRecord();
        stream.StopStream();
    }

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            Debug.Log("The most frequently used word is: " + SortTextByWordUsage(text)[0]);
        }

        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            foreach (string word in SortTextByWordUsage(text))
            {
                Debug.Log(word);
            }
        }
    }

    void OnSegmentFinished(WhisperResult segment)
    {
        string result = segment.Result;
        string pattern = @"\[.*?\]";
        string filteredResult = Regex.Replace(result, pattern, "");
        
        text += filteredResult;
        
        if (logOutput)
        {
            Debug.Log(segment.Result);
        }
    }
    
    string[] SortTextByWordUsage(string text)
    {
        string[] words = text.Split(new[] { ' ', '.', ',', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        var wordCount = new Dictionary<string, int>();

        string[] ignoreWords =
        {
            "a", "an", "the", "at", "by",
            "of", "on", "to", "it",
            "is", "am", "are", "be",
            "of", "to", "its", "it's"
        };

        foreach (string word in words)
        {
            string lowercaseWord = word.ToLower();

            if (ignoreWords.Contains(lowercaseWord))
            {
                continue;
            }

            if (wordCount.ContainsKey(lowercaseWord))
            {
                wordCount[lowercaseWord]++;
            }
            else
            {
                wordCount[lowercaseWord] = 1;
            }
        }

        string[] sortedWords = wordCount.OrderByDescending(kv => kv.Value)
            .ThenBy(kv => kv.Key)
            .Select(kv => kv.Key)
            .ToArray();

        return sortedWords;
    }
}