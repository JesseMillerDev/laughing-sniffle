using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using PointingPokerPlus.Client.Store.Session;
using PointingPokerPlus.Shared;
using System.Threading.Tasks;

namespace PointingPokerPlus.Client.Pages
{
	public partial class ActiveSession : Fluxor.Blazor.Web.Components.FluxorComponent
    {
		[Inject]
		private IState<SessionState> SessionState { get; set; }
        [Inject]
        public IDispatcher Dispatcher { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }

        private HubConnection _hubConnection;

        [Parameter]
        public string SessionId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(NavManager.ToAbsoluteUri("/sessionHub"))
                .Build();

            _hubConnection.On<string, int>("ReceivePoints", (userId, points) =>
            {                
                var action = new ReceivePointsAction(userId, points);
                Dispatcher.Dispatch(action);
            });
            
            _hubConnection.On<User>("UserJoined", (user) =>
            {
                var action = new UserJoinedAction(user);
                Dispatcher.Dispatch(action);
            });

            await _hubConnection.StartAsync();
        }

        
        Task SendPoints(int points) => _hubConnection.SendAsync("SendPoints", SessionState.Value.Session.Id, SessionState.Value.Session.ActiveUser.Id, points);
        public bool IsConnected => _hubConnection.State == HubConnectionState.Connected;

        void SetActiveUser(User user)
        {
            var action = new SetActiveUserAction(user);
            Dispatcher.Dispatch(action);
        }
    }
}
