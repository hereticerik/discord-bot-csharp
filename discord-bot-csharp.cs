using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleDiscordBot
{
    class Program
    {
        private DiscordSocketClient client;
        private Dictionary<string, string> customCommands;
        private readonly HttpClient httpClient = new HttpClient();

        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            client = new DiscordSocketClient();

            client.Log += Log;

            string token = "INSERT_YOUR_BOT_TOKEN_HERE";
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            customCommands = LoadCustomCommands("commands.txt");

            client.MessageReceived += MessageReceived;

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Content.ToLower() == "!hello")
            {
                await message.Channel.SendMessageAsync("Hello, " + message.Author.Username + "!");
            }
            else if (message.Content.ToLower() == "!ping")
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                var reply = await message.Channel.SendMessageAsync("Pong!");
                watch.Stop();
                await reply.ModifyAsync(m => m.Content = $"Pong! Response time: {watch.ElapsedMilliseconds} ms.");
            }
            else if (message.Content.ToLower().StartsWith("!weather"))
            {
                string location = message.Content.Substring(8).Trim();
                if (!string.IsNullOrEmpty(location))
                {
                    string weather = await GetWeatherAsync(location);
                    await message.Channel.SendMessageAsync(weather);
                }
            }
            else if (customCommands.ContainsKey(message.Content.ToLower()))
            {
                await message.Channel.SendMessageAsync(customCommands[message.Content.ToLower()]);
            }
        }

        private async Task<string> GetWeatherAsync(string location)
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={location}&units=metric&appid=INSERT_YOUR_API_KEY_HERE";
            string result = "";

            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                dynamic data = await response.Content.ReadAsAsync<dynamic>();
                result = $"Current weather in {data.name}, {data.sys.country}: {data.weather[0].main}, {data.weather[0].description}. Temperature: {data.main.temp}°C. Humidity: {data.main.humidity}%.";
            }
            else
            {
                result = "Unable to retrieve weather data.";
            }

            return result;
        }

        private Dictionary<string, string> LoadCustomCommands(string fileName)
        {
            Dictionary<string, string> commands = new Dictionary<string, string>();

            if (!File.Exists(fileName))
            {
                return commands;
            }

            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                string[] parts = line.Split('=');

                if (parts.Length == 2)
                {
                    string command = parts[0].Trim().ToLower();
                    string response = parts[1].Trim();

                    if (!string.IsNullOrEmpty(command) && !string.IsNullOrEmpty(response))
                    {
                        commands[command] = response;
                    }
                }
            }

            return commands;
        }
    }
}




// Installation/Use Instructions

// This bot responds with "Hello, {user}!" whenever it receives a message that says "!hello".
// You can also use "!ping" and it will respond with "Pong!" and the response time.
// This bot responds to the !weather command with the current temperature and humidity in the specified city. The weather data is obtained from OpenWeatherMap API.
// This bot loads custom commands from a text file named commands.txt in the same directory as the executable file. The file format is one command per line, in the format of command=response. For example:
// !dice=Rolling the dice...
// !coinflip=Flipping a coin...
// To use this code, create a commands.txt file in the same directory as the executable file and add your custom commands to the file in the format of command=response.

// Here are the steps to create your bot:

// 1.) Create a new C# console application in Visual Studio.
// 2.) Install the Discord.NET library from the NuGet package manager.
// 3.) Copy and paste the code above into the Program.cs file.
// 4.) Replace "INSERT_YOUR_BOT_TOKEN_HERE" with your bot token, which you can obtain from the Discord Developer Portal.
// 5.) To use the weather feature, you need to sign up for an API key from OpenWeatherMap and replace INSERT_YOUR_API_KEY_HERE with your API key. 
// 6.) Run the program and your bot should be online and ready to respond to messages!