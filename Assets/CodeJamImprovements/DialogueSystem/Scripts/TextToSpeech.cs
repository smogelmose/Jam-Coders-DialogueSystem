using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using UnityEngine;
using UnityEngine.Networking;


public class TextToSpeech : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public DialogueManager dialogueManager; // Reference to DialogueManager text lines.


    // Start is called before the first frame update
    private async void Start() // private async for handling awaitable tasks.
    {
        // Achtung! Really bad practice for live product as access keys can be compromised.
        // Should be handled with audio being generated at server side.

        var credentials = new BasicAWSCredentials("xxx", "xxx");
        // Credentials takes two variables: AWS access key and secret key.

        var client = new AmazonPollyClient(credentials, RegionEndpoint.EUCentral1);
        //Client takes two variables: AWS credentials and region.

        var request = new SynthesizeSpeechRequest() // Sets initial values from Amazon Polly UI options.
        {
            Text = dialogueManager.lines[0], // Reference to DialogueManager lines.
            Engine = Engine.Neural, // Uses Amazon Polly Neural TTS system that uses concatenative synthesis of the phonemes of recorded speech, producing very natural-sounding synthesized speech. 
            VoiceId = VoiceId.Justin, // Uses the NTTS child voiceId Justin 
            OutputFormat = OutputFormat.Mp3 // Outputs the audio in MP3 format.
        };
        
        var response = await client.SynthesizeSpeechAsync(request); // Handling the synthesize requests using async method
        // Unity can not convert audiostream in to an audioclip automatically. Audiostream is written in to a MP3 file. Using Unity webrequest to load it locally to an audioclip.

        WriteIntoFile(response.AudioStream); // Write audiostream in to local file.

        using (var www = UnityWebRequestMultimedia.GetAudioClip($"{Application.persistentDataPath}/audio.mp3", AudioType.MPEG)) // Get the audiofile 
        {
            var op = www.SendWebRequest(); // Cache webrequest in to variable

            while (!op.isDone) await Task.Yield(); // Using System.Threading.Tasks 

            var clip = DownloadHandlerAudioClip.GetContent(www);

            audioSource.clip = clip; // When scene is loaded the audio will play
            audioSource.Play();
        }


    }

    private void WriteIntoFile(Stream stream) // Using System.IO
    {
        using (var fileStream = new FileStream($"{Application.persistentDataPath}/audio.mp3", FileMode.Create)) // Puts audiofile in to datafolder
        {
            byte[] buffer = new byte[8 * 1024]; // Create Databuffer in chunks

            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)// As long as there are bytes to read it will write to filestream.
            {
            fileStream.Write(buffer, 0, bytesRead);        
            }
        }
    }
}




// Inspired by https://www.youtube.com/watch?v=rdHqRRzltTo&list=PLCDqd_BDUOne2-xb6PtOGhhNA8Qoh1G4V&index=9