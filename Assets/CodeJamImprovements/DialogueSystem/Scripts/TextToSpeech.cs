using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeJamImprovements.DialogueSystem.Scripts
{
    public class TextToSpeech : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource; // Audio source for playing synthesized speech.
        private DialogueManager _dialogueManager; // Reference to the DialogueManager component.

        // Start is called before the first frame update
        private async void Start()
        {
            if (_dialogueManager == null)
            {
                _dialogueManager = FindObjectOfType<DialogueManager>(); // Find the DialogueManager component in the scene.
            }

            if (_dialogueManager == null)
            {
                Debug.LogError("Failed to find DialogueManager."); // If the DialogueManager component cannot be found, log an error.
                return;
            }

            var lines = _dialogueManager.lines; // Get the dialogue lines from the DialogueManager.

            if (lines == null || lines.Length == 0)
            {
                Debug.LogError("No dialogue lines to synthesize."); // If there are no dialogue lines, log an error and return.
                return;
            }

            var credentials = new BasicAWSCredentials("xxx", "xxx"); // Replace with AWS credentials - removed for security reasons.
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
                    Text = text, // Text to synthesize.
                    Engine = Engine.Neural, // Use the neural text-to-speech engine.
                    VoiceId = VoiceId.Justin, // Use the "Justin" child voice.
                    OutputFormat = OutputFormat.Mp3 // Synthesize speech as an MP3 file.
                };

                var response = await client.SynthesizeSpeechAsync(request); // Call the Amazon Polly API to synthesize speech from the text.

                WriteIntoFile(response.AudioStream); // Write the synthesized speech to a file.

                using var www = UnityWebRequestMultimedia.GetAudioClip("file://" + Path.Combine(Application.temporaryCachePath, "audio.mp3"), AudioType.MPEG); // Create a Unity web request to download the synthesized speech.
                var op = www.SendWebRequest(); // Download the synthesized speech.

                while (!op.isDone) await Task.Yield(); // Wait for the audio clip to finish downloading.

                var clip = DownloadHandlerAudioClip.GetContent(www); // Get the downloaded audio clip.

                audioSource.clip = clip; // Assign the downloaded audio clip to the audio source.
                audioSource.Play(); // Play the synthesized speech.

                while (audioSource.isPlaying) await Task.Yield(); // Wait for the speech to finish playing.
            }
        }

        private void WriteIntoFile(Stream stream)
        {
            using var fileStream = new FileStream(Path.Combine(Application.temporaryCachePath, "audio.mp3"), FileMode.Create); // Create a file stream to write the synthesized speech to a file.
            byte[] buffer = new byte[8 * 1024]; // 8K buffer.

            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0) // Read the synthesized speech from the stream.
            {
                fileStream.Write(buffer, 0, bytesRead); // Write the synthesized speech to a file.
            }
        }
    }
}

// Inspired by https://www.youtube.com/watch?v=rdHqRRzltTo&list=PLCDqd_BDUOne2-xb6PtOGhhNA8Qoh1G4V&index=8