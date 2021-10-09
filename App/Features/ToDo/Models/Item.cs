using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace App.Features.ToDo.Models
{
    public class Item
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("brewery_type")]
        public string Description { get; set; }
        [JsonProperty("street")]
        public string Street { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("longitude")]
        public string Longitude { get; set; }
        [JsonProperty("latitude")]
        public string Latitude { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("website_url")]
        public string Website_Url { get; set; }
        [JsonProperty("updated_at")]
        public DateTime Updated_At { get; set; }
        [JsonProperty("tag_list")]
        public List<object> Tag_List { get; set; }
    }
}
