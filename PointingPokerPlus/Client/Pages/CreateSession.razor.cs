using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using PointingPokerPlus.Client.Store.Session;
using PointingPokerPlus.Shared;
using PointingPokerPlus.Shared.Utilities;
using System;
using System.Net.Http;
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
        [Inject]
        public HttpClient Http { get; set; }

        [Parameter]
        public string SessionId { get; set; }
        private User _activeUser;
        protected string _userName;

        async Task StartOrJoinSession()
        {
            _activeUser = new User { Id = RandomGenerators.GenerateRandomId(), Name = _userName };
            var request = new StartJoinSessionRequest { SessionId = SessionId, User = _activeUser };

            //Call Start Or Join
            var session = await Http.PostJsonAsync<Session>("Session/startOrJoin", request);

            var action = new LoadSessionAction(_activeUser, session);
            Dispatcher.Dispatch(action);

            NavManager.NavigateTo($"/activeSession/{SessionState.Value.Session.Id}");
        }
    }
}
