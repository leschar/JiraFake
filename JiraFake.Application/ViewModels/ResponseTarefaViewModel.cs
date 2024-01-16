﻿using System.Text.Json.Serialization;

namespace JiraFake.Application.ViewModels
{
    public class ResponseTarefaViewModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }
        [JsonPropertyName("dataCadastro")]
        public DateTime DataCadastro { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
