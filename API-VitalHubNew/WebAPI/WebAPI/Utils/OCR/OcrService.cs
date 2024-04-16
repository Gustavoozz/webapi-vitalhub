using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace WebAPI.Utils.OCR
{
    public class OcrService
    {
        private readonly string _subscriptKey = "e343ac0b30564737a6f5b3d12fe4b04d";
        private readonly string _endpoint = "https://cvvitalhubg8.cognitiveservices.azure.com/";

        public async Task<string> RecognizeTextAsync(Stream imageStream)
        {
            try
            {
                var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(_subscriptKey))
                {
                    Endpoint = _endpoint
                };

                var ocrResult = await client.RecognizePrintedTextInStreamAsync(true, imageStream);

                return ProcessRecognitionResult(ocrResult);
            }
            catch (Exception ex)
            {
                return "Erro ao recohecer o texto!" + ex.Message;
            }
        }

        private static string ProcessRecognitionResult(OcrResult result)
        {
            try
            {
                string recognizedText = "";

                // Percorrer cada bloco ( Regions ), linha ( Lines ) e letra ( Words ), organizar e extrair as palavras lidas ( 3 Foreachs para cada um ):
                foreach (var region in result.Regions)
                {
                    foreach (var line in region.Lines)
                    {
                        foreach (var word in line.Words)
                        {
                            // Operador de incremento = "+=".
                            // " " = Espaçar os elementos.
                           recognizedText += word.Text + " ";
                        }

                        // Quebrar linha.
                        recognizedText += "\n";
                    }
                }
                return recognizedText;
       
            }
            catch (Exception ex)
            {
                return "Erro" + ex.Message;
            }
        }
    }
}
