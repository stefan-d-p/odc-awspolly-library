using Microsoft.Extensions.Configuration;
using Without.Systems.Polly.Structures;

namespace  Without.Systems.Polly.Test;

public class Tests
{
    private static readonly IPolly _actions = new Polly();

    private Credentials _credentials;
    private readonly string _region = "eu-central-1";

    [SetUp]
    public void Setup()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddUserSecrets<Tests>()
            .AddEnvironmentVariables()
            .Build();

        string awsAccessKey = configuration["AWSAccessKey"] ?? throw new InvalidOperationException();
        string awsSecretAccessKey = configuration["AWSSecretAccessKey"] ?? throw new InvalidOperationException();

        _credentials = new Credentials(awsAccessKey, awsSecretAccessKey);
    }

    [Test]
    public void Successful_Write()
    {
        SynthesizeSpeechRequest request = new SynthesizeSpeechRequest
        {
            Text = "<speak>Hello my friend, how are you today?</speak>",
            TextType = "ssml",
            Engine = "generative",
            OutputFormat = "mp3",
            VoiceId = "Joanna",
            OutputS3Key = "test/sample-joanna.mp3",
            OutputS3BucketName = "osdemostore"
        };
        var result = _actions.SynthesizeSpeech(_credentials, _region, request);
    }
}