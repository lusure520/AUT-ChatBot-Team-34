using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Bot_Application2.Models;
using Microsoft.Bot.Connector;

namespace Bot_Application2.Dialogs
{
    [Serializable]
    public class MajorSearch : IDialog<object>
    {
        AzureSearchService search = new AzureSearchService();

        public string majorEntity;
        public string reply;

        public MajorSearch(string majorEntity)
        {
            this.majorEntity = majorEntity;
        }

        public async Task StartAsync(IDialogContext context)
        {
            string name = getEntity();
            await context.PostAsync("let's start searching");
            //context.Wait(MessageRecievedAsync);
            
            SearchResult searchResult = await search.SearchByMajorName(name);
            if (searchResult.value.Length != 0)
            {
                reply = "Here is the search result:";
             
                for (var i = 0; i < searchResult.value.Length; i++)
                {
                    reply += searchResult.value[i].Name + ",  ";
                }
                await context.PostAsync(reply);
            }
            else
            {
                await context.PostAsync($"{name} No data found");
            }
            context.Done<object>(null);
        }

        private string getEntity()
        {
            return this.majorEntity;
        }

        /*private async Task MessageRecievedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            try
            {
                SearchResult searchResult = await search.SearchByMajorName(message.Text);
                if(searchResult.value.Length != 0)
                {
                    Activity reply = ((Activity)message).CreateReply();
                    reply.Text = "Here is the first search result:" ;
                    for(var i= 0; i< searchResult.value.Length; i++)
                    {
                        reply.Text += searchResult.value[i].Name + "  ";
                    }
                    await context.PostAsync(reply);
                }
                else
                {
                    await context.PostAsync($"No major found");
                }
            }catch(Exception e)
            {
                string x = e.Message;
            }
            context.Done<object>(null);
        }*/
    }
}