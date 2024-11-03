using OutSystems.ExternalLibraries.SDK;

namespace Without.Systems.Polly.Structures;

[OSStructure(Description = "Synthesizes UTF-8 input, plain text or SSML, to a stream of bytes. SSML input must be valid, well-formed SSML. Result is written to S3.")]
public struct SynthesizeSpeechRequest
{
    [OSStructureField(
        Description = "Specifies the engine (standard, neural, long-form, or generative) for Amazon Polly to use when processing input text for speech synthesis.",
        DataType = OSDataType.Text,
        IsMandatory = true)]
    public string Engine;
    
    [OSStructureField(
        Description = "Optional language code for the Synthesize Speech request.",
        DataType = OSDataType.Text,
        IsMandatory = false)]
    public string LanguageCode;
    
    [OSStructureField(
        Description = "List of one or more pronunciation lexicon names you want the service to apply during synthesis.",
        DataType = OSDataType.InferredFromDotNetType,
        IsMandatory = false)]
    public List<string> LexiconNames;
    
    [OSStructureField(
        Description = "The format in which the returned output will be encoded",
        DataType = OSDataType.Text,
        IsMandatory = true)]
    public string OutputFormat;

    [OSStructureField(Description = "Amazon S3 bucket name to which the output file will be saved",
        DataType = OSDataType.Text,
        IsMandatory = true)]
    public string OutputS3BucketName;
    
    [OSStructureField(Description = "Amazon S3 key under which the output file will be saved",
        DataType = OSDataType.Text,
        IsMandatory = true)]
    public string OutputS3Key;
    
    [OSStructureField(
        Description = "The audio frequency specified in Hz.",
        DataType = OSDataType.Text,
        IsMandatory = false)]
    public string SampleRate;
    
    [OSStructureField(
        Description = "The type of speech marks returned for the input text.",
        DataType = OSDataType.InferredFromDotNetType,
        IsMandatory = false)]
    public List<string> SpeechMarkTypes;
    
    [OSStructureField(
        Description = "Input text to synthesize. If you specify ssml as the TextType, follow the SSML format for the input text.",
        DataType = OSDataType.Text,
        IsMandatory = true)]
    public string Text;
    
    [OSStructureField(
        Description = "Specifies whether the input text is plain text or SSML.",
        DataType = OSDataType.Text,
        IsMandatory = false)]
    public string TextType;
    
    [OSStructureField(
        Description = "Voice ID to use for the synthesis.",
        DataType = OSDataType.Text,
        IsMandatory = true)]
    public string VoiceId;
    
    
    
}