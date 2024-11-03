using System.Net;
using Amazon;
using Amazon.Polly;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using OutSystems.ExternalLibraries.SDK;
using Without.Systems.Polly.Extensions;
using Without.Systems.Polly.Util;

namespace Without.Systems.Polly;

public class Polly : IPolly
{

    private readonly IMapper _mapper;

    public Polly()
    {
        MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AllowNullCollections = true;
            cfg.AllowNullDestinationValues = true;

            cfg.CreateMap<Structures.SynthesizeSpeechRequest, Amazon.Polly.Model.SynthesizeSpeechRequest>()
                .ForMember(dest => dest.Engine, opt => opt.MapFrom(src => Engine.FindValue(src.Engine)))
                .ForMember(dest => dest.LanguageCode,
                    opt =>
                    {
                        opt.PreCondition(src => !string.IsNullOrEmpty(src.LanguageCode));
                        opt.MapFrom(src => LanguageCode.FindValue(src.LanguageCode));
                    })
                .ForMember(dest => dest.OutputFormat,
                    opt => opt.MapFrom(src => OutputFormat.FindValue(src.OutputFormat)))
                .ForMember(dest => dest.TextType, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrEmpty(src.TextType));
                    opt.MapFrom(src => TextType.FindValue(src.TextType));
                });
        });
        _mapper = mapperConfiguration.CreateMapper();
    }
    
    public Structures.SynthesizeSpeechResponse SynthesizeSpeech(Structures.Credentials credentials, string region,
        Structures.SynthesizeSpeechRequest request)
    {
        Amazon.Polly.Model.SynthesizeSpeechRequest dtoRequest =
            _mapper.Map<Amazon.Polly.Model.SynthesizeSpeechRequest>(request);
        
        AmazonPollyClient pollyClient = GetAwsPollyClient(credentials, region);
        var synthesizeSpeechResponse = AsyncUtil.RunSync(() => pollyClient.SynthesizeSpeechAsync(dtoRequest));
        ParseResponse(synthesizeSpeechResponse);
        
        AmazonS3Client s3Client = GetAwsS3Client(credentials, region);

        using var audioStream = new MemoryStream();
        synthesizeSpeechResponse.AudioStream.CopyTo(audioStream);
        
        var putObjectRequest = new PutObjectRequest
        {
            BucketName = request.OutputS3BucketName,
            Key = request.OutputS3Key,
            InputStream = audioStream
        };
            
        var putObjectResponse = AsyncUtil.RunSync(() => s3Client.PutObjectAsync(putObjectRequest));
        ParseResponse(putObjectResponse);

        return new Structures.SynthesizeSpeechResponse
        {
            Key = request.OutputS3Key,
            Bucket = request.OutputS3BucketName,
            VersionId = putObjectResponse.VersionId
        };
    }
    
    private AmazonPollyClient GetAwsPollyClient(Structures.Credentials credentials,
        string region)
    {
        return new AmazonPollyClient(credentials.ToAwsCredentials(), RegionEndpoint.GetBySystemName(region));
    }

    private AmazonS3Client GetAwsS3Client(Structures.Credentials credentials, string region)
    {
        return new AmazonS3Client(credentials.ToAwsCredentials(), RegionEndpoint.GetBySystemName(region));
    }
    
    private void ParseResponse(AmazonWebServiceResponse response)
    {
        if (!(response.HttpStatusCode.Equals(HttpStatusCode.OK) || response.HttpStatusCode.Equals(HttpStatusCode.NoContent)))
            throw new Exception($"Error in operation ({response.HttpStatusCode})");
    }
}