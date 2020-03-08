FROM mcr.microsoft.com/dotnet/core/runtime:3.1
COPY dobro-bot/bin/Debug/netcoreapp3.1/ app/

ENTRYPOINT ["dotnet", "app/dobro-bot.dll"]
ARG BOT_TOKEN
ENV BOT_TOKEN=$BOT_TOKEN
