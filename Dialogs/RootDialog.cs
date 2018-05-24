using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis;
using System.Collections.Generic;

namespace Bot_Application2.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            PromptDialog.Choice(context, this.AfterPromptSelection, new List<string>() { "ShowAllMajors", "SearchByName" }, "How can I help you?");

            
        }

        private async Task AfterPromptSelection(IDialogContext context, IAwaitable<string> result)
        {
            var optionSelected = await result;

            switch(optionSelected)
            {
                case "ShowAllMajors":
                    await context.PostAsync("That this dialog is not implemented yet, typing anything to show the selection again!");
                    context.Wait(MessageReceivedAsync);
                    break;
                case "SearchByName":
                    context.Call(new MajorSearch(), ResumeAfterOptionDialog);
                    break;
            }
        }

       private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceivedAsync);
        }
    }
}