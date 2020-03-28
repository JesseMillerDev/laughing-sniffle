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
        [Parameter]
        public string SessionId { get; set; }

        [Inject]
		private IState<SessionState> SessionState { get; set; }
        [Inject]
        public IDispatcher Dispatcher { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }

        private HubConnection _hubConnection;

        protected string _userName;

        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(NavManager.ToAbsoluteUri("/sessionHub"))
                .Build();

            await _hubConnection.StartAsync();

            
               
        }

        async Task JoinSession()
        {
            var user = new User { Id = RandomGenerators.GenerateRandomId(), Name = _userName };
            
            if (SessionId == null)
            {
                var action = new CreateSessionAction(user);
                Dispatcher.Dispatch(action);
                Send(SessionState.Value.Session);
            }
            else
            {
                var session = await _hubConnection.InvokeAsync<Session>("JoinSession", SessionId);
                var action = new LoadSessionAction(user, session);
                Dispatcher.Dispatch(action);
            }

            NavManager.NavigateTo($"/activeSession/{SessionState.Value.Session.Id}");
        }

        Task Send(Session session) => _hubConnection.SendAsync("CreateSession", session);
    }
}
