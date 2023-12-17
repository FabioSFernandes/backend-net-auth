using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TokenMan.Serializeable
{
    [JsonSerializable(typeof(TokenDto[]))]
    [JsonSerializable(typeof(TokenRequestDto[]))]
    [JsonSerializable(typeof(TokenResponseDto[]))]
    [JsonSerializable(typeof(LoginDto[]))]
    [JsonSerializable(typeof(LoginResponseDto[]))]
    public partial class AppJsonSerializerContext : JsonSerializerContext
    {

    }
}
