using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using LINETuchi.Util;
using LINETuchi.Api;
using LINETuchi.Entity.Line;
using System.Collections.Generic;

namespace LINETuchi
{
    public static class Function
    {
        private static bool IsDefaultImg = false;
        /// <summary>
        /// メッセージを受け取ったことをトリガーに起動する関数
        /// </summary>
        [FunctionName("Function")]
        public static async Task<IActionResult>Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"request!!");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var isAuthOk = Auth.IsSingatureOk(
                req.Headers["X-Line-Signature"],
                requestBody,
                Environment.GetEnvironmentVariable("LINE_CHANNEL_SECRET"));

            if (!isAuthOk)
            {
                return new BadRequestResult();
            }
            var data = JsonConvert.DeserializeObject<LineResponse>(requestBody);

            // 返信処理
            if(data.Events.Count == 0)
            {
                log.LogInformation($"no event");
                return new BadRequestResult();
            }

            var quickReply = new quickReply()
            {
                items = new List<item>()
                {
                    new item()
                    {
                        type = "action",
                        action = new action()
                        {
                            type = "message",
                            label = "にゃん!!(喜)",
                            text = "にゃん!!(喜)"
                        }
                    },
                    new item()
                    {
                        type = "action",
                        action = new action()
                        {
                            type = "message",
                            label = "にゃん💢(怒)",
                            text = "にゃん💢(怒)"
                        }
                    },
                    new item()
                    {
                        type = "action",
                        action = new action()
                        {
                            type = "message",
                            label = "にゃん・・(哀)",
                            text = "にゃん・・(哀)"
                        }
                    },
                    new item()
                    {
                        type = "action",
                        action = new action()
                        {
                            type = "message",
                            label = "ｽﾔｧ(眠)",
                            text = "ｽﾔｧ(眠)"
                        }
                    },new item()
                    {
                        type = "action",
                        action = new action()
                        {
                            type = "message",
                            label = "にゃッ(驚)",
                            text = "にゃッ(驚)"
                        }
                    },
                }
            };

            var replyToken = data.Events[0].ReplyToken;
            var message = data.Events[0].Message.text;
            log.LogInformation($"message:{message}");
            var emotion = new Emotion(message);

            var imgUrl = string.Empty;
            if (IsDefaultImg)
            {
                var unsplashResult = await new Unsplash(log)
                    .GetRandomCatPhoto(emotion.KeyWord);
                if (!unsplashResult.IsSuccess)
                {
                    log.LogInformation($"detail:{unsplashResult.HttpResponseMessage}");
                    return new BadRequestResult();
                }
                imgUrl = unsplashResult.Url;
                IsDefaultImg = false;
            }
            else
            {
                imgUrl = emotion.ImgPaths[new Random().Next(emotion.ImgPaths.Count)];
                IsDefaultImg = true;
            }

            var result = await new SendMessageAsync(log).
                SendMessage(replyToken, new List<IMessage> { 
                    new MessageImage(imgUrl) ,
                    new MessageText(message, quickReply)
                }
                );

            if (!result.IsSuccess)
            {
                log.LogInformation($"errorMessage:{result.Message} detail:{result.HttpResponseMessage}");
                return new BadRequestResult();
            }

            return new OkResult();
        }
    }
}
