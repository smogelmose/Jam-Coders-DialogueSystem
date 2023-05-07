using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using System.IO;
using UnityEngine.Networking;
using System.Threading.Tasks;
using TMPro;

public class TextToSpeech : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private DialogueManager dialogueManager;

    // Start is called before the first frame update
    private async void Start()
    {
        if (dialogueManager == null)
        {
            dialogueManager = FindObjectOfType<DialogueManager>();
        }

        if (dialogueManager == null)
        {
            Debug.LogError("Failed to find DialogueManager.");
            return;
        }

        var lines = dialogueManager.lines;

        if (lines == null || lines.Length == 0)
        {
            Debug.LogError("No dialogue lines to synthesize.");
            return;
        }

        var text = lines[0].Trim();

        if (string.IsNullOrEmpty(text))
        {
            Debug.LogError("Input text is empty or null.");
            return;
        }

        var credentials = new BasicAWSCredentials("xxx", "xxx");
        var client = new AmazonPollyClient(credentials, RegionEndpoint.EUCentral1);

        var request = new SynthesizeSpeechRequest()
        {
            Text = text,
            Engine = Engine.Neural,
            VoiceId = VoiceId.Justin,
            OutputFormat = OutputFormat.Mp3
        };

        var response = await client.SynthesizeSpeechAsync(request);

        WriteIntoFile(response.AudioStream);

        using (var www = UnityWebRequestMultimedia.GetAudioClip($"{Application.persistentDataPath}/audio.mp3", AudioType.MPEG))
        {
            var op = www.SendWebRequest();

            while (!op.isDone) await Task.Yield();

            var clip = DownloadHandlerAudioClip.GetContent(www);

            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private void WriteIntoFile(Stream stream)
    {
        using (var fileStream = new FileStream($"{Application.persistentDataPath}/audio.mp3", FileMode.Create))
        {
            byte[] buffer = new byte[8 * 1024];

            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fileStream.Write(buffer, 0, bytesRead);
            }
        }
    }
}
