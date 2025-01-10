# LogsProtocol - Advance

If you need advanced logs system for your BrokeProtocol server then you are in the right message, integrate discord webhooks with brokeprotocol events now is easier than ever!

This plugin depends of the [DiscordWebhook]() (made by me!!!)

## Events arguments for config:
```
// Player events
onJoin
  args:
    0: player username
    1: player id
onLeft:
  args:
    0: player username
    1: player id
onDeath:
  args:
    0: player username
    1: player id
    2: attacker username
    3: attacker id
onKill
  args:
    0: attacker username
    1: attacker id
    2: victim username
    3: victim id
onChatLocal:
  args:
    0: player username
    1: player id
    2: message
onChatGlobal:
  args:
    0: player username
    1: player id
    2: message
onCommand:
  args:
    0: player username
    1: player id
    2: message

// Manager events
onStart
  args:
    0: datetime
```

Example config file: [Click here](https://github.com/DevHaziel/LogsProtocolAdvanced/blob/master/Examples/config.json)
