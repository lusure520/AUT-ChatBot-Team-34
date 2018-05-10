using Bot_Application2.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SP = Microsoft.SharePoint.Client;
namespace Bot_Application2.Dialogs
{
    [LuisModel("d63333ff-2b5b-4a3d-afff-4ea8d6824aca", "a47bc275380c48538bcc5c1c2644e19b")]
    [Serializable]
    public class LuisDialogs : LuisDialog<object>
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "entity")]
        public string Entity { get; set; }

        AzureSearchService search = new AzureSearchService();
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            var username = context.Activity.From.Name;
            string reply = $"Sorry {username}! I am unable to understand your question.  enter a vaild question";
            await context.PostAsync(reply);
        }

        [LuisIntent("greeting")]
        public async Task QueryQuestion(IDialogContext context, LuisResult result)
        {
            var username = context.Activity.From.Name;
            string message = $"Hello {username}! What is your question?.";
            await context.PostAsync(message);
        }

        [LuisIntent("Location")]
        public async Task Location(IDialogContext context, LuisResult result)
        {
            var username = context.Activity.From.Name;
            string message = $"fetch data from location.";
            await context.PostAsync(message);
        }

        [LuisIntent("SearchPapers")]
        public async Task SearchPapers(IDialogContext context, IAwaitable<IMessageActivity> activity , LuisResult result)
        {
            var message = await activity;
            string data = "";
             EntityRecommendation majorEntity;
            if (result.TryFindEntity("Software Engineering" , out majorEntity))
            {
                data = "Software Engineering";
            }
            context.Call(new MajorSearch(data), this.ResumeAfterMajorList);
        }

        private async Task ResumeAfterMajorList(IDialogContext context, IAwaitable<object> result)
        {
            new NotImplementedException();
        }

        [LuisIntent("showPapers")]
        public async Task ShowPapers(IDialogContext context, LuisResult result)
        {
            var username = context.Activity.From.Name;
            string message = $"I am unable to understand your show.";
            await context.PostAsync(message);
        }
    }
}