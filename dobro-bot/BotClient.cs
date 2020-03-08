using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace dobro_bot
{
    public class BotClient
    {
        private static TelegramBotClient Bot;

        public BotClient()
        {
            Bot = new TelegramBotClient(Environment.GetEnvironmentVariable("BOT_TOKEN"));
            var cts = new CancellationTokenSource();
            var me = Bot.GetMeAsync().Result;
            Console.WriteLine(
                $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );
            Bot.OnMessage += BotOnMessageReceived;
            Bot.StartReceiving();
            while (true)
            {
                Thread.Sleep(Int32.MaxValue);
            }
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            Console.WriteLine($"Receive message type: {message.Type}");
            if (message.Type != MessageType.Text)
                return;

            switch (message.Text.Split(' ').First())
            {
                // send custom keyboard
                case "/keyboard":
                    ReplyKeyboardMarkup ReplyKeyboard = new[]
                    {
                        new[] {"1.3", "1.3"},
                    };
                    await Bot.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Choose",
                        replyMarkup: ReplyKeyboard
                    );
                    break;
            }

        }

        public static async Task HandleErrorAsync(Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException =>
                $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
        }
    }
}