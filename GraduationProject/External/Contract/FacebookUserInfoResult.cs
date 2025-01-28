using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace GraduationProject.External.Contract
{
    public class FaceBookUserInfoResult
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("Picture")]
        public FacebookPicture Picture { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }


    }
    public class FacebookPicture
    {
        [JsonProperty("data")]
        public FaebookPitureData Data { get; set; }
    }
    public class FaebookPitureData
    {
        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("is_silhouette")]
        public bool IsSilhouette { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }
}

