using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscordCLI.SerializableTypes
{
    public class IsoDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        private const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

        private DateTimeStyles _dateTimeStyles = DateTimeStyles.RoundtripKind;
        private string? _dateTimeFormat;
        private CultureInfo? _culture;

        public DateTimeStyles DateTimeStyles
        {
            get => _dateTimeStyles;
            set => _dateTimeStyles = value;
        }

        public string? DateTimeFormat
        {
            get => _dateTimeFormat ?? string.Empty;
            set => _dateTimeFormat = string.IsNullOrEmpty(value) ? null : value;
        }

        public CultureInfo Culture
        {
            get => _culture ?? CultureInfo.CurrentCulture;
            set => _culture = value;
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            var text = value.ToString(_dateTimeFormat ?? DefaultDateTimeFormat, Culture);
            writer.WriteStringValue(text);
        }

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateText = reader.GetString();
            return !string.IsNullOrEmpty(dateText) 
                ? DateTimeOffset.ParseExact(dateText, _dateTimeFormat ?? DefaultDateTimeFormat, Culture, _dateTimeStyles) 
                : default;
        }

        public static readonly IsoDateTimeOffsetConverter Singleton = new IsoDateTimeOffsetConverter();
    }
}