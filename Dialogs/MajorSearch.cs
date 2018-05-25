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
        public string key;
        public SearchResult searchResult;

        public MajorSearch(string queryname, string intentname)
        {
            this.majorEntity = queryname;
            key = intentname;
        }

        public async Task StartAsync(IDialogContext context)
        {
            string name = getEntity();
            await context.PostAsync("let's start searching");
            //await context.PostAsync("Tell me what you want to search");
            // context.Wait(MessageRecievedAsync);

            /*searchResult = await search.SearchByMajorName(name);
            if (searchResult.value.Length != 0)
            {
                reply = "Here is the search result:";
             
                for (var i = 0; i < searchResult.value.Length; i++)
                {
                    reply += " \n "+ searchResult.value[i].id +" "+ searchResult.value[i].Name;
                    
                }
                await context.PostAsync(reply);
            }
            else
            {
                await context.PostAsync($"{name} No data found");
            }
            */
            if (key.Equals("getNextPaper"))
            {
                await getNotFailNextPaper(context);
            }

            if (key.Equals("getRequisite"))
            {
                //await context.PostAsync(this.majorEntity);
                await getRequisitePaper(context);
            }

            if (key.Equals("getPaperByMajor"))
            {
                await getSuggestedPaper(context);
            }

            if (key.Equals("getPaperAfterFailed"))
            {
                await getFailNextPaper(context);
            }

            if (key.Equals("getPaperByJob"))
            {
                await getPaperByJob(context);
            }

            if (key.Equals("getSemester"))
            {
                await getSemester(context);
            }
            context.Done<object>(null);
            
        }

        private string getEntity()
        {
            return this.majorEntity;
        }

        private async Task getSemester(IDialogContext context)
        {
            string papername = getEntity();
            string searchPaperQuery = papername + "&searchFields=id";
            searchResult = await search.SearchByMajorName(searchPaperQuery);
            if (searchResult.value.Length != 0)
            {
                for (var i = 0; i < searchResult.value.Length; i++)
                {
                    reply = "The paper of " + searchResult.value[i].Name + " will start at: Semester "+ searchResult.value[i].Semester;
                }
                await context.PostAsync(reply);
            }
            else
            {
                await context.PostAsync($"No data found");
            }
        }

        private async Task getPaperByJob(IDialogContext context)
        {
            string papername = getEntity();
            string searchPaperQuery = papername + "&searchFields=name";
            searchResult = await search.SearchByMajorName(searchPaperQuery);
            reply = "The paper for "+papername+" job:";
            if (searchResult.value.Length != 0)
            {
                for (var i = 0; i < searchResult.value.Length; i++)
                {
                    if (searchResult.value[i].Type.Contains("1"))
                    {
                        reply += " \n " + searchResult.value[i].id + " " + searchResult.value[i].Name;
                    }
                }
                await context.PostAsync(reply);
            }
            else
            {
                await context.PostAsync($"No data found");
            }
        }

        private async Task getSuggestedPaper(IDialogContext context)
        {
            string majorname = getEntity();
            string first = "The first year for " + majorname + " major:";
            string second = "\n\n The secod year for " + majorname + " major:";
            string third = "\n\n The third year for " + majorname + " major:";
            string searchPaperQuery = "*&searchFields=year";
            searchResult = await search.SearchByMajorName(searchPaperQuery);
            if (searchResult.value.Length != 0)
            {
                for (var i = 0; i < searchResult.value.Length; i++)
                {
                    if (!searchResult.value[i].Type.Equals("2"))
                    {
                        if (searchResult.value[i].Year.Equals("1"))
                        {
                            first += " \n " + searchResult.value[i].id + " " + searchResult.value[i].Name;
                        }else if (searchResult.value[i].Year.Equals("2"))
                        {
                            second += " \n " + searchResult.value[i].id + " " + searchResult.value[i].Name;
                        }
                        else
                        {
                            third += " \n " + searchResult.value[i].id + " " + searchResult.value[i].Name;
                        }                        
                    }
                }
                reply = first + second + third;
                await context.PostAsync(reply);
            }
            else
            {
                await context.PostAsync($"No data found");
            }
        }

        private async Task getRequisitePaper(IDialogContext context)
        {
            string papername = getEntity();
            string searchPaperQuery = papername + "&searchFields=id";
            SearchResult searchResult2 = await search.SearchByMajorName(searchPaperQuery);
            if (searchResult2.value.Length != 0)
            {
                searchPaperQuery = papername + "&searchFields=requisite";
                searchResult = await search.SearchByMajorName(searchPaperQuery);
                reply = "The pre-requisites for "+ papername + " :";

                for (var i = 0; i < searchResult.value.Length; i++)
                {                   
                    reply += " \n " + searchResult.value[i].id + " " + searchResult.value[i].Name;
                }
                //await context.PostAsync(reply);

                reply += "\n \n The co-requisites for " + papername + " (Paper ID):";

                for (var j = 0; j < searchResult2.value.Length; j++)
                {
                    reply += " \n " + searchResult2.value[j].Requisite;
                }
                await context.PostAsync(reply);
            }
            else
            {
                await context.PostAsync($"No data found");
            }
        }

        

        private async Task getNotFailNextPaper(IDialogContext context)
        {
            string papername = getEntity();
            string searchPaperQuery = papername+"&searchFields=requisite";
            searchResult = await search.SearchByMajorName(searchPaperQuery);
            reply = "The list of next paper for Software Development:";
            if (searchResult.value.Length != 0)
            {                
                for (var i = 0; i < searchResult.value.Length; i++)
                {
                    if (!searchResult.value[i].id.Contains(papername))
                    {
                        reply += " \n " + searchResult.value[i].id + " " + searchResult.value[i].Name;
                    }

                }
                await context.PostAsync(reply);
            }
            else
            {
                await context.PostAsync($"No data found");
            }
        }

        private async Task getFailNextPaper(IDialogContext context)
        {

            string papername = getEntity();
            string searchPaperQuery ="1&searchFields=type";
            searchResult = await search.SearchByMajorName(searchPaperQuery);
            reply = "The list of next paper that you can still take for Software Development:";
            if (searchResult.value.Length != 0)
            {
                for (var i = 0; i < searchResult.value.Length; i++)
                {
                    if (!searchResult.value[i].id.Contains(papername) && !searchResult.value[i].Requisite.Contains(papername))
                    {
                        reply += " \n " + searchResult.value[i].id + " " + searchResult.value[i].Name;
                    }

                }
                await context.PostAsync(reply);
            }
            else
            {
                await context.PostAsync($"No data found");
            }
        }

        /*
        private async Task MessageRecievedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            try
            {
                SearchResult searchResult = await search.SearchByMajorName(message.Text);
                if(searchResult.value.Length != 1 )
                {
                    Activity reply = ((Activity)message).CreateReply();
                    reply.Text = "Here is the search result:" ;
                    for(var i= 0; i< searchResult.value.Length; i++)
                    {
                        if (searchResult.value[i].Requisite.Contains(message.Text))
                        {
                            reply.Text += " \n " + searchResult.value[i].Name + searchResult.value[i].Requisite;
                        }
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