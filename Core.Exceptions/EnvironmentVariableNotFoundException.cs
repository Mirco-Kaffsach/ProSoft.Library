using System.Diagnostics.CodeAnalysis;

namespace ProSoft.Library.Core.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class EnvironmentVariableNotFoundException : Exception
{
	public EnvironmentVariableNotFoundException()
	{

	}

	public EnvironmentVariableNotFoundException(string message) : base(message)
	{

	}

	public EnvironmentVariableNotFoundException(string message, Exception innerException) : base(message, innerException)
	{

	}
}