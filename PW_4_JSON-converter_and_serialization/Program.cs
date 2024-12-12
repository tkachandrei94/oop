using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PW_4_simple_JSON
{
    public class MyEvent
    {
        public string Name { get; set; } = string.Empty;
        public int Tickets { get; set; }
        public DateTime EventDate { get; set; }
    }

    // Кастомный конвертер для DateTime
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private const string DateFormat = "dd MMMM yyyy";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Реализуем чтение в формате "dd MMMM yyyy"
            string? dateString = reader.GetString();
            if (DateTime.TryParseExact(dateString, DateFormat, null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                return date;
            }
            throw new JsonException($"Unable to convert \"{dateString}\" to DateTime.");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateFormat));
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var myEvent = new MyEvent
            {
                Name = "Birthday Party",
                Tickets = 100,
                EventDate = new DateTime(2024, 9, 21)
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            options.Converters.Add(new CustomDateTimeConverter());

            string json = JsonSerializer.Serialize(myEvent, options);
            Console.WriteLine("JSON с кастомным форматом даты:");
            Console.WriteLine(json);
        }
    }
}
