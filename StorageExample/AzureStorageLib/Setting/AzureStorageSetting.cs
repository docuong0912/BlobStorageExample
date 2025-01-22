namespace AzureStorageLib.Setting
{
    public class AzureStorageSetting
    {
        public class AzureStorageSettings
        {
            public string DefaultEndpointsProtocol { get; set; }

            public string AccountName { get; set; }

            public string AccountKey { get; set; }

            public string EndpointSuffix { get; set; }

            public string ImageContainer { get; set; }

            public string BlobUri => $"{DefaultEndpointsProtocol}://{AccountName}.blob.{EndpointSuffix}/{ImageContainer}";
        }
    }
}
