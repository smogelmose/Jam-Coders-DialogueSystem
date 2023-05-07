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
    [SerializeField] private AudioSource audioSource; // Audio source for playing synthesized speech.
    private DialogueManager dialogueManager; // Reference to the DialogueManager component.

    // Start is called before the first frame update
    private async void Start()
    {
        if (dialogueManager == null)
        {
            dialogueManager = FindObjectOfType<DialogueManager>(); // Find the DialogueManager component in the scene.
        }

        if (dialogueManager == null)
        {
            Debug.LogError("Failed to find DialogueManager.");
            return;
        }

        var lines = dialogueManager.lines; // Get the dialogue lines from the DialogueManager.

        if (lines == null || lines.Length == 0)
        {
            Debug.LogError("No dialogue lines to synthesize.");
            return;
        }

        var credentials = new BasicAWSCredentials("xxx", "xxx"); // Replace with your own AWS credentials.
        var client = new AmazonPollyClient(credentials, RegionEndpoint.EUCentral1); // Create an Amazon Polly client.

        foreach (var line in lines) // Loop over each line of dialogue.
        {
            var text = line.Trim(); // Trim whitespace from the line of dialogue.

            if (string.IsNullOrEmpty(text))
            {
                Debug.LogWarning("Skipping empty line.");
                continue; // Skip empty lines.
            }

            var request = new SynthesizeSpeechRequest()
            {
                Text = text,
                Engine = Engine.Neural, // Use the neural text-to-speech engine.
                VoiceId = VoiceId.Justin, // Use the "Justin" voice.
                OutputFormat = OutputFormat.Mp3 // Synthesize speech as an MP3 file.
            };

            var response = await client.SynthesizeSpeechAsync(request); // Call the Amazon Polly API to synthesize speech from the text.

            WriteIntoFile(response.AudioStream); // Write the synthesized speech to a file.

            using (var www = UnityWebRequestMultimedia.GetAudioClip("file://" + Path.Combine(Application.temporaryCachePath, "audio.mp3"), AudioType.MPEG))
            {
                var op = www.SendWebRequest();

                while (!op.isDone) await Task.Yield(); // Wait for the audio clip to finish downloading.

                var clip = DownloadHandlerAudioClip.GetContent(www); // Get the downloaded audio clip.

                audioSource.clip = clip; // Assign the downloaded audio clip to the audio source.
                audioSource.Play(); // Play the synthesized speech.

                while (audioSource.isPlaying) await Task.Yield(); // Wait for the speech to finish playing.
            }
        }
    }

    private void WriteIntoFile(Stream stream)
    {
        using (var fileStream = new FileStream(Path.Combine(Application.temporaryCachePath, "audio.mp3"), FileMode.Create))
        {
            byte[] buffer = new byte[8 * 1024];

            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fileStream.Write(buffer, 0, bytesRead); // Write the synthesized speech to a file.
            }
        }
    }
}

// Inspired by https://www.youtube.com/watch?v=rdHqRRzltTo&list=PLCDqd_BDUOne2-xb6PtOGhhNA8Qoh1G4V&index=8