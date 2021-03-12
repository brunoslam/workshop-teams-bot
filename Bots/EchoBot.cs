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

namespace Microsoft.BotBuilderSamples.Bots
{
    public class EchoBot : ActivityHandler
    {
        private CovidContext _context;
        public EchoBot(CovidContext covidContext)
        {
            _context = covidContext;
        }
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var replyText = $"Echo: {turnContext.Activity.Text}";

            var comuna = await _context.EstadoComunas.Include(e => e.IdComunaNavigation).Include(e => e.IdFaseNavigation).Where(e => e.IdComunaNavigation.StrDescripcion.Contains($"{turnContext.Activity.Text}")).ToListAsync();


            if (comuna.Count == 0)
            {
                await turnContext.SendActivityAsync("No se han encontrado resultados de la ciudad ingresada", cancellationToken: cancellationToken);
            }
            else
            {
                await turnContext.SendActivityAsync($"El estado de {comuna.FirstOrDefault()?.IdComunaNavigation.StrDescripcion}  es {comuna.FirstOrDefault()?.IdFaseNavigation.Nombre}", cancellationToken: cancellationToken);
            }

            await turnContext.SendActivityAsync("Ingresa tu ciudad para conocer el estado covid", cancellationToken: cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                    await turnContext.SendActivityAsync("Ingresa tu ciudad para conocer el estado covid", cancellationToken: cancellationToken);
                }
            }
        }
    }
}
