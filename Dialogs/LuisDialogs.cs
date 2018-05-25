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
            string reply = $"Sorry {username}! I am unable to understand your question.  enter a vaild question.\n----------Notice---------\nThis chatbot can only reply quesition for software development major!\nFor search paper by Paper ID (such as 'COMP500')which you can get when you search major first(such as 'list paper for sd') ";
            await context.PostAsync(reply);
        }

        [LuisIntent("greeting")]
        public async Task QueryQuestion(IDialogContext context, LuisResult result)
        {
            var username = context.Activity.From.Name;
            string message = $"Hello {username}! What is your question?\n----------Notice---------\n Search major question first please! \n To know how to use this chatbot, You can enter any word such as 'notice'";
            await context.PostAsync(message);
        }

        [LuisIntent("Location")]
        public async Task Location(IDialogContext context, LuisResult result)
        {
            string message = $"fetch data from location.";
            await context.PostAsync(message);
        }

        [LuisIntent("getNextPaper")]
        public async Task getNextPaper(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            string data = "";
            EntityRecommendation majorEntity;

            if (result.TryFindEntity("papername", out majorEntity))
            {
                data = majorEntity.Entity;
                context.Call(new MajorSearch(data, "getNextPaper"), this.ResumeAfterMajorList);
            }        
            else
            {
                await context.PostAsync(Notice());
            }
            
        }
        [LuisIntent("getPaperAfterFailed")]
        public async Task getPaperAfterFailed(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            string data = "";
            EntityRecommendation majorEntity;
            if (result.TryFindEntity("papername", out majorEntity))
            {
                data = majorEntity.Entity;
               
                context.Call(new MajorSearch(data, "getPaperAfterFailed"), this.ResumeAfterMajorList);
            }
            else
            {
                await context.PostAsync(Notice());
            }
            

        }
        [LuisIntent("getPaperByJob")]
        public async Task getPaperByJob(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            string data = "";
            EntityRecommendation majorEntity;
            if (result.TryFindEntity("jobname", out majorEntity))
            {
                data = majorEntity.Entity;
                context.Call(new MajorSearch(data, "getPaperByJob"), this.ResumeAfterMajorList);
            }
            else
            {
                await context.PostAsync(Notice());
            }
        }
        [LuisIntent("getPaperByMajor")]
        public async Task getPaperByMajor(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            string data = "";
            EntityRecommendation majorEntity;
            if (result.TryFindEntity("majorname", out majorEntity))
            {
                data = majorEntity.Entity;
                context.Call(new MajorSearch(data, "getPaperByMajor"), this.ResumeAfterMajorList);
            }
            else
            {
                await context.PostAsync(Notice());
            }
            
        }
        [LuisIntent("getRequisite")]
        public async Task getRequisite(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
           
            EntityRecommendation majorEntity;
            if (result.TryFindEntity("papername", out majorEntity))
            {
                
                var data = majorEntity.Entity;
                context.Call(new MajorSearch(data, "getRequisite"), this.ResumeAfterMajorList);
            }
            else
            {
                await context.PostAsync(Notice());
            }

        }
        [LuisIntent("getSemester")]
        public async Task getSemester(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            string data = "";
            EntityRecommendation majorEntity;
            if (result.TryFindEntity("papername", out majorEntity))
            {
                data = majorEntity.Entity;
                context.Call(new MajorSearch(data, "getSemester"), this.ResumeAfterMajorList);
            }
            else
            {
                await context.PostAsync(Notice());
            }

        }
        private async Task ResumeAfterMajorList(IDialogContext context, IAwaitable<object> result)
        {
            new NotImplementedException();
        }

        private string Notice()
        {
            return $"Sorry, I can't find the information for this question!"+" \n "+"Please check the words spelling or ask the question by other way!";
        }
    }
}