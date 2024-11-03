using OutSystems.ExternalLibraries.SDK;

namespace Without.Systems.Polly
{
    [OSInterface(
        Name = "AWSPolly",
        Description = "Amazon Polly is a cloud service that converts text into lifelike speech.",
        IconResourceName = "Without.Systems.Polly.Resources.Polly.png")]
    public interface IPolly
    {
    
        [OSAction(
            Description = "Synthesizes UTF-8 input, plain text or SSML, to a stream of bytes. SSML input must be valid, well-formed SSML",
            ReturnName = "result",
            ReturnDescription = "Result contains bucket, key, version identifier and content length of written audio.",
            ReturnType = OSDataType.InferredFromDotNetType,
            IconResourceName = "Without.Systems.Polly.Resources.Polly.png")]
        Structures.SynthesizeSpeechResponse SynthesizeSpeech(
            [OSParameter(
                Description = "AWS Account Credentials",
                DataType = OSDataType.InferredFromDotNetType)]
            Structures.Credentials credentials,
            [OSParameter(
                Description = "AWS Region System Name",
                DataType = OSDataType.Text)]
            string region,
            [OSParameter(
                Description = "Synthesize Speech Reques",
                DataType = OSDataType.InferredFromDotNetType)]
            Structures.SynthesizeSpeechRequest request);
    }
}