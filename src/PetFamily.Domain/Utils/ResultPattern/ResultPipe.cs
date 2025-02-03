namespace PetFamily.Domain.Utils.ResultPattern;

public sealed class ResultPipe
{
    private Error? _error;

    /// <summary>
    /// Method adds error if check returns true. If pipe already has error then returns pipe with current error without next checking.
    /// </summary>
    /// <param name="check">Condition that is processed.</param>
    /// <param name="error">Error to add.</param>
    /// <returns>returns pipe with modified error state. Or if error exists returns with current error without next check.</returns>
    public ResultPipe Check(bool check, Error error)
    {
        if (_error != null)
            return this;

        if (check)
            _error = error;

        return this;
    }

    /// <summary>
    /// Method process additional action in result pipe. If pipe has errors then action isn't proceed
    /// </summary>
    /// <param name="action">Action</param>
    /// <returns>Returns pipe with its current error state.</returns>
    public ResultPipe WithAction(Action action)
    {
        if (_error != null)
            return this;

        action();
        return this;
    }

    /// <summary>
    /// Method returns Result that is given as a parameter. If pipe has errors then returns failure result.
    /// </summary>
    /// <param name="process">Method that creates instance of result. Will be modified if pipe contains error.</param>
    /// <returns>Returns result that is given as a parameter or modified result if error persists.</returns>
    public Result FromPipe(Func<Result> process) => _error == null ? process.Invoke() : _error;

    public Result<T> FromPipe<T>(Func<T> process) => _error == null ? process.Invoke() : _error;

    public Result<T> FromPipe<T>(T expected) => _error == null ? expected : _error;
}
