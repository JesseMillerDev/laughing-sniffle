using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using PointingPokerPlus.Client.Store.Session;
using PointingPokerPlus.Shared;
using PointingPokerPlus.Shared.Utilities;
using System;
using System.Threading.Tasks;

namespace PointingPokerPlus.Client.Pages
{
	public partial class CreateSession
	{
		[Inject]
		private IState<SessionState> SessionState { get; set; }
        [Inject]
        public IDispatcher Dispatcher { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }

        protected string _myStringValue;

        protected override async Task OnInitializedAsync()
        {
        }

        void JoinSession()
        {
            var user = new User { Id = RandomGenerators.GenerateSessionId(), Name = _myStringValue };
            var action = new CreateSessionAction(user);
            Dispatcher.Dispatch(action);

            NavManager.NavigateTo($"/activeSession/{SessionState.Value.Session.Id}");
        }
    }
}
