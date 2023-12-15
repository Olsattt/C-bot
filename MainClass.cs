using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace RebootBot

{
    internal class MainClass
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            
            var client = new TelegramBotClient("6774056759:AAFtPJAxq-ZyE34J0W_JjdNLPIMu7bV2od4");
            client.StartReceiving(UpdateObject.Update, Error);
            
            
            Console.ReadLine();
        }



        async static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            
        }
    }

}