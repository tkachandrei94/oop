using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PW_4_simple_JSON
{
    // Классы для результата
    public class Conference
    {
        public string ConferenceName { get; set; } = string.Empty;
        public List<Event> Events { get; set; } = new();
    }

    public class Event
    {
        // Start будет заполняться из StartTime
        public DateTime Start { get; set; }
        // Duration будет в секундах
        public int Duration { get; set; }
        public string Title { get; set; } = string.Empty;
        public Speaker Speaker { get; set; } = new();
    }

    // Спикер без изменений
    public class Speaker
    {
        public string Name { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
    }

    // Промежуточная модель для десериализации из исходного JSON,
    // чтобы потом преобразовать в нужный нам формат
    public class ConferenceRaw
    {
        public string ConferenceName { get; set; } = string.Empty;
        public List<EventRaw> Events { get; set; } = new();
    }

    public class EventRaw
    {
        public string Title { get; set; } = string.Empty;

        // Исходный формат содержит StartTime
        [JsonPropertyName("StartTime")]
        public DateTime StartTime { get; set; }

        // Исходный формат DurationInMinutes
        [JsonPropertyName("DurationInMinutes")]
        public int DurationInMinutes { get; set; }

        public Speaker Speaker { get; set; } = new();
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            string filePath = "conference.json";

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл {filePath} не найден.");
                return;
            }

            try
            {
                // Читаем содержимое файла
                string jsonFromFile = File.ReadAllText(filePath);

                // Десериализуем в промежуточную модель
                var rawConference = JsonSerializer.Deserialize<ConferenceRaw>(jsonFromFile);

                if (rawConference == null)
                {
                    Console.WriteLine("Не удалось десериализовать JSON из файла.");
                    return;
                }

                // Преобразуем raw модель в целевую модель
                var conference = new Conference
                {
                    ConferenceName = rawConference.ConferenceName,
                    Events = new List<Event>()
                };

                foreach (var e in rawConference.Events)
                {
                    var evt = new Event
                    {
                        Title = e.Title,
                        Start = e.StartTime, // Перекладываем StartTime в Start
                        Duration = e.DurationInMinutes * 60, // Конвертируем минуты в секунды
                        Speaker = e.Speaker
                    };
                    conference.Events.Add(evt);
                }

                // Выводим результат
                Console.WriteLine($"Conference: {conference.ConferenceName}");
                foreach (var ev in conference.Events)
                {
                    Console.WriteLine($"Title: {ev.Title}");
                    Console.WriteLine($"Start: {ev.Start}");
                    Console.WriteLine($"Duration (seconds): {ev.Duration}");
                    Console.WriteLine($"Speaker: {ev.Speaker.Name}, Experience: {ev.Speaker.ExperienceYears} years");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при обработке файла: {ex.Message}");
            }
        }
    }
}
