using OutSystems.ExternalLibraries.SDK;

namespace Without.Systems.Polly.Structures;

[OSStructure(Description = "Synthesize Speech Output Structure")]
public struct SynthesizeSpeechResponse
{
    [OSStructureField(Description = "Amazon S3 bucket name.",
        DataType = OSDataType.Text)]
    public string Bucket;
    
    [OSStructureField(Description = "Amazon S3 key.",
        DataType = OSDataType.Text)]
    public string Key;
    
    [OSStructureField(Description = "Amazon S3 Version Id",
        DataType = OSDataType.Text)]
    public string VersionId;
}