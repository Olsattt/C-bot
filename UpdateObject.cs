using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RebootBot
{
    internal class UpdateObject
    {
        private static string typeTask;
        public async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
           
           var message = update.Message;
            if (message.Text != null)
            {
                var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                                   {
                        new KeyboardButton[] { "Создать задание","Просмотреть задания"},
                        new KeyboardButton[] { "Что может бот","Как бот работает"}
                    })
                {
                    ResizeKeyboard = true
                };
                var sentMessage = await botClient.SendTextMessageAsync(
                       chatId: message.Chat.Id,
                       text: "Выберите команду:",
                       replyMarkup: replyKeyboardMarkup);
                if (message.Text == "Создать задание")
                {
                     sentMessage = await botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Опишите задание",
                        replyMarkup: replyKeyboardMarkup);
                    return;
                }

                if (message.Text == "Просмотреть задания")
                {
                    await using Stream stream = System.IO.File.OpenRead("C:\\Users\\Oleg\\Desktop\\Заказ.txt");
                    message = await botClient.SendDocumentAsync(
                       chatId: message.Chat.Id,
                       document: InputFile.FromStream(stream: stream, fileName: "Заказ.txt"));
                }

                if (message.Text == "Что может бот")
                {
                    WhatBotJust(message, botClient);
                }

                if (message.Text == "Как бот работает")
                {
                    HowAreBotWork(message,botClient);
                }

                if (sentMessage != null)
                {
                    // Сохранение текста задачи
                    await SaveMessageToFile(message, botClient);
                    await botClient.SendTextMessageAsync(message.Chat.Id,"Задание создано");
                }
            }
        }


        public async static Task CreateTask(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
           

        }

        public async static Task SaveMessageToFile(Message message, ITelegramBotClient botClient)
        {
            string textFilePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Заказ.txt";

            using StreamWriter streamWriter = new StreamWriter(textFilePath, true);
            await streamWriter.WriteLineAsync("Username: " + "@" + message.Chat.Username);
            await streamWriter.WriteLineAsync("Имя: " + message.Chat.FirstName);
            await streamWriter.WriteLineAsync("Описание заказа: " + message.Text);
            streamWriter.Close();
            return;

        }

            public async static Task HowAreBotWork(Message message, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Бот работает следующим образом: пользователь оставляет задание которое ему нужно выполнить,затем форммируется документ и отправляется другим пользователям бота");
            return;
        }
        public async static Task WhatBotJust(Message message, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Данный бот может сохранять получаемые сообщения в виде файла формируя заказ и отправляя его всем пользователям бота");
            return;
        }

    }

}

    

