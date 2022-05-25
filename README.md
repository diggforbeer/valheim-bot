# Valheim-Bot
A .Net valhiem bot to post message to discord.


# Configuration

The docker is configured with 3 environment variables.
1. discordWebHook: url for the discord webhook
1. steamApiKey: api key for calling steam for player information
1. logFolder: the folder that the ghcr.io/lloesche/valheim-server docker outputs its logs to

## Docker Compose Example
version: "3.7"
```
services: 
  valheim: 
    image: ghcr.io/lloesche/valheim-server
    cap_add:
      - sys_nice
    volumes: 
      - /mnt/gluster/dockerdata2/valheim/config:/config
      - /mnt/gluster/dockerdata2/valheim/data:/opt/valheim
      - /mnt/gluster/dockerdata2/valheim/logs:/var/log/supervisor 
    ports: 
      - "2456-2457:2456-2457/udp"
    environment:
      - SERVER_NAME=redacted
      - WORLD_NAME=redacted
      - SERVER_PASS=redacted
      - SERVER_PUBLIC=true
   stop_grace_period: 3m
    deploy:
      replicas: 1
      restart_policy:
        condition: any
  valeim_bot:
    image: diggforbeer/valheim-bot
    environment:
      - discordWebHook=redacted
      - steamApiKey=redacted
      - logFolder=/server_logs
    volumes: 
      - /mnt/gluster/dockerdata2/valheim/logs:/server_logs
    ```
