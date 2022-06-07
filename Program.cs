using System;
using System.Threading.Tasks;
using System.IO;
using Discord;
using Discord.WebSocket;
using Discord.Audio;
using Sodium;
using Xabe.FFmpeg;
using Concentus;



class Program
{
    private static Thread musicThread = null;
    private static IVoiceChannel channel = null;

    readonly string token = "OTgzMjc3NTQxNDQ2MDIxMTMx.GKGcSD.aRzwQeRtyWEaRKoaCODWsEhOYdXDYdD5wJ3xIw";
    private DiscordSocketClient client;
    public static async Task Main()
    {
        var client = new DiscordSocketClient();
        client.MessageReceived += CommandHandler;
        client.Log += PrintLog;
        await client.LoginAsync(TokenType.Bot, "OTgzMjc3NTQxNDQ2MDIxMTMx.GKGcSD.aRzwQeRtyWEaRKoaCODWsEhOYdXDYdD5wJ3xIw");
        await client.StartAsync();
        Console.ReadKey();
        
    }
    private static async void StartStream(object data)
    {
        IVoiceChannel channel = (IVoiceChannel) data;
        IAudioClient audioClient = null;
        audioClient = await channel.ConnectAsync(true);
    }
    private static Task PrintLog(LogMessage logMessage)
    {
        Console.WriteLine(logMessage.ToString());
        return Task.CompletedTask;
    }

    private static async Task CommandHandler(SocketMessage msg)
    {
        if (msg.ToString().Length == 0)
        {
            return;
        }
        if (!msg.Author.IsBot && msg.ToString()[0] == '-')
            switch(msg.Content)
            {
                case "-play":
                    
                    channel = channel ?? (msg.Author as IGuildUser).VoiceChannel;
                    if (channel == null) { await msg.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }
                    if (musicThread == null)
                    {
                        musicThread = new Thread(StartStream);
                        musicThread.Start(channel);
                    }
                    else
                        await msg.Channel.SendMessageAsync("Уже запущен, брат.");


                    //audioClient.CreatePCMStream(AudioApplication.Mixed);
                    break;
                case "-help":
                    string strOut = "```";
                    foreach (var elem in File.ReadAllLines("..//..//..//help.txt"))
                        strOut += elem + "\n";
                    strOut += "```";
                    await msg.Channel.SendMessageAsync(strOut);
                    break;
                case "-gachi":
                    var rnd1 = new Random();
                    
                    var photos1 = Directory.GetFiles("..//..//..//gachi");
                    await msg.Channel.SendFileAsync(photos1[rnd1.Next(0, photos1.Length)]);
                    break;
                case "-kenny9":
                    var rnd3 = new Random();
                    var photos3 = Directory.GetFiles("..//..//..//kenny9");
                    await msg.Channel.SendFileAsync(photos3[rnd3.Next(0, photos3.Length)]);
                    break;
                case "-dora":
                    var rnd2 = new Random();

                    var photos2 = Directory.GetFiles("..//..//..//dora");
                    await msg.Channel.SendFileAsync(photos2[rnd2.Next(0, photos2.Length)]);
                    break;
                case "-hui" or "-chlen" or "-dick":
                    var rnd4 = new Random();

                    var photos4 = Directory.GetFiles("..//..//..//dick");
                    await msg.Channel.SendFileAsync(photos4[rnd4.Next(0, photos4.Length)]);
                    break;
                case "-roll":
                    var rnd5 = new Random();
                    await msg.Channel.SendMessageAsync(rnd5.Next(0,100).ToString());
                    break;

                case "-stop":
                    if (musicThread != null)
                    {
                        musicThread.Join();
                        musicThread = null;
                        channel.DisconnectAsync();
                        channel = null;
                    }
                       
                    else
                        await msg.Channel.SendMessageAsync("Молчу, брат.");
                    break;
                default:
                    await msg.Channel.SendMessageAsync("Не знаю такой команды, брат.");
                    break;
            }
        await Task.CompletedTask;
    }
    
    
}
    

