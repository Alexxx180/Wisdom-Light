using Newtonsoft.Json;
using System;

namespace WisdomLight.ViewModel.Data.Files.Processors.Serialization.Json
{
    public class JsonExpressionsConverter<TConcrete> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            //assume we can convert to anything for now
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //explicitly specify the concrete type we want to create
            return serializer.Deserialize<TConcrete>(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //use the default serialization - it works fine
            serializer.Serialize(writer, value);
        }
    }

    //public class JsonExpressionsConverter
    //{
    //    class BitArrayDTO
    //    {
    //        public byte[] Bytes { get; set; }
    //        public int Length { get; set; }
    //    }

    //    public override BitArray Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
    //    {
    //        BitArrayDTO dto = JsonSerializer.Deserialize<BitArrayDTO>(ref reader);

    //        if (dto is null)
    //            return null;

    //        var bitArray = new BitArray(dto.Bytes);
    //        bitArray.Length = dto.Length;

    //        return bitArray;
    //    }

    //    public override void Write(Utf8JsonWriter writer, BitArray value, JsonSerializerOptions options)
    //    {
    //        byte[] serializable = new byte[(value.Length - 1) / 8 + 1];
    //        value.CopyTo(serializable, 0);

    //        BitArrayDTO dto = new BitArrayDTO
    //        {
    //            Bytes = serializable,
    //            Length = value.Length
    //        };

    //        JsonSerializer.Serialize(writer, dto);
    //    }
    //}
}
