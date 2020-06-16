# UwpCommunity.WebApi

## Db Schema
![logo](/Docs/DbSchema.png)

## Setup
1. Right click on `UwpCommunity.WebApi` > `Manage User Secrets`
2. Paste 
```
{
  "Discord": {
    "Client": "",
    "Secret": "",
    "BotToken": "",
    "GuildId": ""
  }
}
```

## How To Use Discord Token
1. Go to `https://uwpcommunity.com/`
2. Login
3. Run in the console `localStorage.getItem("discordAuthData");`
4. Copy the `access_token`
5. Paste in `Nightingale` (or another rest client)