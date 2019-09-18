using Common.Activatable;

namespace Base.ScreenLocker
{
	public interface IScreenLocker : IActivatable
	{
		LockerType LockerType { get; }
	}
}