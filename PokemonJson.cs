using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Juego
{
    // Clases para deserializar la info de la api
    public class PokemonJson
    {
        [JsonPropertyName("id")]
        public int id {get; set;}
        [JsonPropertyName("name")]
        public string name {get; set;}
        [JsonPropertyName("types")]
        public List<typeInfo> types {get; set;}
    }

    public class typeInfo
    {
        [JsonPropertyName("type")]
        public typeName type {get; set;}
    }

    public class typeName
    {
        [JsonPropertyName("name")]
        public string name {get; set;}
    }
}