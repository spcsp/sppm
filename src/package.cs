using System.CodeDom.Compiler;
using System.Collections.Generic;
using Newtonsoft.Json;
using static Newtonsoft.Json.Required;
using static Newtonsoft.Json.NullValueHandling;

namespace PackageJson
{
    [GeneratedCode("NJsonSchema", "10.4.4.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class Author
    {
        [JsonProperty("name", Required = DisallowNull, NullValueHandling = Ignore)]
        public string Name { get; set; }

        [JsonProperty("email", Required = DisallowNull, NullValueHandling = Ignore)]
        public string Email { get; set; }

        [JsonProperty("url", Required = DisallowNull, NullValueHandling = Ignore)]
        public string Url { get; set; }

        private IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.4.4.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class PublishConfig
    {
        [JsonProperty("access", Required = DisallowNull, NullValueHandling = Ignore)]
        public string Access { get; set; }

        private IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.4.4.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class Root
    {
        [JsonProperty("name", Required = DisallowNull, NullValueHandling = Ignore)]
        public string Name { get; set; }

        [JsonProperty("version", Required = DisallowNull, NullValueHandling = Ignore)]
        public string Version { get; set; }

        [JsonProperty("private", Required = DisallowNull, NullValueHandling = Ignore)]
        public bool Private { get; set; }

        [JsonProperty("description", Required = DisallowNull, NullValueHandling = Ignore)]
        public string Description { get; set; }

        [JsonProperty("keywords", Required = DisallowNull, NullValueHandling = Ignore)]
        public ICollection<string> Keywords { get; set; }

        [JsonProperty("license", Required = DisallowNull, NullValueHandling = Ignore)]
        public string License { get; set; }

        [JsonProperty("author", Required = DisallowNull, NullValueHandling = Ignore)]
        public Author Author { get; set; }

        [JsonProperty("repository", Required = DisallowNull, NullValueHandling = Ignore)]
        public Author Repository { get; set; }

        [JsonProperty("main", Required = DisallowNull, NullValueHandling = Ignore)]
        public string Main { get; set; }

        [JsonProperty("publishConfig", Required = DisallowNull, NullValueHandling = Ignore)]
        public PublishConfig PublishConfig { get; set; }

        private IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }
    }
}
