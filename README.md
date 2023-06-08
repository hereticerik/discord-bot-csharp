# discord-bot-csharp
A very simple discord bot written in c#

## To run the bot, you need to:
<br>
1.) Install the .NET Core SDK on your machine if you haven't already. You can download it from the .NET website: https://dotnet.microsoft.com/download.<br>
2.) Copy the script to a file and save it with a .cs file extension.<br>
3.) Open a command prompt or terminal window and navigate to the directory where the file is saved.<br>
4.) Run the following command to compile the script:<br>
`dotnet build`<br>
5.) Run the following command to start the bot:<br>
`dotnet run`
<br>
The bot should now be running and ready to respond to commands in your Discord server. You can invite it to your Discord server by generating an invite link using the Discord Developer Portal.
<br><br>

## Notes / Usage

This bot responds with "Hello, {user}!" whenever it receives a message that says "!hello".<br>

You can also use "!ping" and it will respond with "Pong!" and the response time.<br>

This bot responds to the !weather command with the current temperature and humidity in the specified city. The weather data is obtained from OpenWeatherMap API.<br>

This bot loads custom commands from a text file named commands.txt in the same directory as the executable file. The file format is one command per line, in the format of command=response. For example:<br>
`!dice=Rolling the dice...`<br>
`!coinflip=Flipping a coin...`<br>

To use this code, create a commands.txt file in the same directory as the executable file and add your custom commands to the file in the format of command=response.


**This Discord Bot was made for Educational Purposes as an Example**
