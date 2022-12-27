using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace _3_TextToSpeech
{
 
    public class Program
    {

        private static string suscriptionKey = "YourSuscriptionKey";
        private static string serviceRegion = "YourServiceRegion";


        //static async void Main(string[] args) {} //Da Error
        static async Task Main()
        {
            Console.WriteLine("Bienvenido al servicio de Speech");
            await SynthesizeAudioToSpeakerAsync();
            await SynthesizeAudioToFileAsync();
            Console.ReadLine();
        }            

 

        static async Task SynthesizeAudioToSpeakerAsync()
        {
            var config = SpeechConfig.FromSubscription(suscriptionKey, serviceRegion);
            using var synthesizer = new SpeechSynthesizer(config);
            await synthesizer.SpeakTextAsync("Hello, i´m testing the service text to speech from my work.");
        }

        static async Task SynthesizeAudioToFileAsync()
        {
            var config = SpeechConfig.FromSubscription(suscriptionKey, serviceRegion);
            config.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Riff24Khz16BitMonoPcm);
  
            using var synthesizer = new SpeechSynthesizer(config, null);           

            var ssml = File.ReadAllText("ssml.xml");//leer el archivo de texto
            var resultssml = await synthesizer.SpeakSsmlAsync(ssml);

            using var stream = AudioDataStream.FromResult(resultssml);
            await stream.SaveToWaveFileAsync("output-test.wav");//nombre del archivo de audio/salida

        }



    }
}
