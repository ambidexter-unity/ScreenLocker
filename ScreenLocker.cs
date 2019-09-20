using System;
using Common.Activatable;
using UnityEngine;

namespace Base.ScreenLocker
{
	[DisallowMultipleComponent]
	public abstract class ScreenLocker : MonoBehaviour, IScreenLocker
	{
		public abstract void Activate(bool immediately = false);
		public abstract void Deactivate(bool immediately = false);
		public abstract ActivatableState ActivatableState { get; protected set; }
		public abstract event ActivatableStateChangedHandler ActivatableStateChangedEvent;
		public abstract LockerType LockerType { get; }
	}

	public class ScreenLocker<TDerived> : ScreenLocker where TDerived : ScreenLocker<TDerived>
	{
		private ActivatableState _activatableState = ActivatableState.Inactive;

		public override void Activate(bool immediately = false)
		{
			throw new NotImplementedException();
		}

		public override void Deactivate(bool immediately = false)
		{
			throw new NotImplementedException();
		}

		public override LockerType LockerType => throw new NotImplementedException();

		public override ActivatableState ActivatableState
		{
			get => _activatableState;
			protected set
			{
				if (value == _activatableState) return;
				_activatableState = value;
				ActivatableStateChangedEvent?.Invoke(this, _activatableState);
			}
		}

		protected virtual void OnDestroy()
		{
			if (ActivatableStateChangedEvent != null)
			{
				// ReSharper disable once PossibleInvalidCastExceptionInForeachLoop
				foreach (ActivatableStateChangedHandler handler in ActivatableStateChangedEvent.GetInvocationList())
				{
					ActivatableStateChangedEvent -= handler;
				}
			}
		}

		public override event ActivatableStateChangedHandler ActivatableStateChangedEvent;
	}
}