// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EchoBot.Data;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Bot.Builder.Dialogs;
using EchoBot.BotBuilderSamples;
using Microsoft.Extensions.Logging;
using Microsoft.Bot.Builder.Teams;

namespace EchoBot.BotBuilderSamples.Bots
{
    public class EchoBot<T> : TeamsActivityHandler where T : Dialog
    {
        private CovidContext _context;
        protected readonly BotState ConversationState;
        protected readonly Dialog Dialog;
        protected readonly ILogger Logger;
        protected readonly BotState UserState;

        public EchoBot(CovidContext covidContext, ConversationState conversationState, UserState userState, T dialog, ILogger<EchoBot<T>> logger)
        {
            _context = covidContext;
            ConversationState = conversationState;
            UserState = userState;
            Dialog = dialog;
            Logger = logger;
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.OnTurnAsync(turnContext, cancellationToken);

            // Save any state changes that might have occurred during the turn.
            await ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            await UserState.SaveChangesAsync(turnContext, false, cancellationToken);
        }
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {

            if (turnContext.Activity.Text == "login" || turnContext.Activity.Text == "logout")
            {
                // Run the Dialog with the new message Activity.
                await Dialog.RunAsync(turnContext, ConversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);
            }
            else
            {
                var comuna = await _context.EstadoComunas.Include(e => e.IdComunaNavigation).Include(e => e.IdFaseNavigation).Where(e => e.IdComunaNavigation.StrDescripcion.Contains($"{turnContext.Activity.Text}")).ToListAsync();


                if (comuna.Count == 0)
                {
                    await turnContext.SendActivityAsync("No se han encontrado resultados de la ciudad ingresada", cancellationToken: cancellationToken);
                }
                else
                {
                    await turnContext.SendActivityAsync($"El estado de {comuna.FirstOrDefault()?.IdComunaNavigation.StrDescripcion}  es {comuna.FirstOrDefault()?.IdFaseNavigation.Nombre}", cancellationToken: cancellationToken);
                }

                await turnContext.SendActivityAsync("Ingresa una ciudad para conocer el estado covid", cancellationToken: cancellationToken);
            }

            
        }
    }
}
