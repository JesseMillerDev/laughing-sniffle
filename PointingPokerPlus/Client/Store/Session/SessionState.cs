using Fluxor;
using PointingPokerPlus.Shared;

namespace PointingPokerPlus.Client.Store.Session
{
	public class SessionState
	{
		public PointingPokerPlus.Shared.Session Session{ get; }
		public SessionState(PointingPokerPlus.Shared.Session session)
		{
			Session = session;
		}
	}

	public class Feature : Feature<SessionState>
	{
		public override string GetName() => "Session";
		protected override SessionState GetInitialState() =>
			new SessionState(session: null);
	}

	#region Session Actions
	
	public class LoadSessionAction
	{
		public User User { get; set; }
		public PointingPokerPlus.Shared.Session Session { get; set; }

		public LoadSessionAction(User user, PointingPokerPlus.Shared.Session session)
		{
			User = user;
			Session = session;
		}
	}

	public class ReceivePointsAction
	{
		public string UserId { get; set; }
		public int Points { get; set; }

		public ReceivePointsAction(string userId, int points)
		{
			UserId = userId;
			Points = points;
		}
	}

	public class UserJoinedAction
	{
		public User User { get; set; }

		public UserJoinedAction(User user)
		{
			User = user;
		}
	}

	public class SetActiveUserAction
	{
		public User User { get; set; }
		
		public SetActiveUserAction(User user)
		{
			User = user;
		}
	}

	#endregion

	public static class SessionReducers
	{
		[ReducerMethod]
		public static SessionState ReduceLoadSessionAction(SessionState state, LoadSessionAction action)
		{
			var newState = action.Session;
			newState.ActiveUser = action.User;
			return new SessionState(session: newState);
		}

		[ReducerMethod]
		public static SessionState ReduceUserJoinedAction(SessionState state, UserJoinedAction action)
		{
			var newState = state.Session;
			newState.Users.Add(action.User);
			return new SessionState(session: newState);
		}

		[ReducerMethod]
		public static SessionState ReduceSetActiveUserAction(SessionState state, SetActiveUserAction action)
		{
			var newState = state.Session;
			newState.ActiveUser = action.User;
			return new SessionState(session: newState);
		}

		[ReducerMethod]
		public static SessionState ReduceReceivePointsAction(SessionState state, ReceivePointsAction action)
		{
			var newState = state.Session;
			//maybe use an id instead of name so you can have people with the same name?
			newState.Users.Find(u => u.Id == action.UserId).Points = action.Points;
			return new SessionState(session: newState);
		}
	}

}
