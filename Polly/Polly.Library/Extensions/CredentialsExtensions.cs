using Amazon.Runtime;
using Without.Systems.Polly.Structures;

namespace Without.Systems.Polly.Extensions;

public static class CredentialsExtensions
{
    public static AWSCredentials ToAwsCredentials(this Credentials credentials)
    {
        if (!credentials.IsValid)
            throw new ArgumentException("Access Key or Secret Access Key not set");

        if (string.IsNullOrEmpty(credentials.SessionToken))
            return new BasicAWSCredentials(credentials.AccessKey, credentials.SecretAccessKey);
        
        return new SessionAWSCredentials(credentials.AccessKey, credentials.SecretAccessKey,
            credentials.SessionToken);
    }
    
}