using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SP=Microsoft.SharePoint.Client;
namespace Bot_Application2.Dialogs
{
    [LuisModel("d63333ff-2b5b-4a3d-afff-4ea8d6824aca", "a47bc275380c48538bcc5c1c2644e19b")]
    [Serializable]
    public class LuisDialogs : LuisDialog<object>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            var username = context.Activity.From.Name;
            string reply = $"Hello {username}! I am unable to understand your question.";
            await context.PostAsync(reply);
        }

        [LuisIntent("greeting")]
        public async Task QueryQuestion(IDialogContext context, LuisResult result)
        {
            var username = context.Activity.From.Name;
            string message = $"Hello {username}! I am unable to understand your greeting.";
            await context.PostAsync(message);
        }

        [LuisIntent("Location")]
        public async Task Location(IDialogContext context, LuisResult result)
        {
            var username = context.Activity.From.Name;
            string message = $"Hello {username}! I am unable to understand your location.";
            await context.PostAsync(message);

        }


    }
}