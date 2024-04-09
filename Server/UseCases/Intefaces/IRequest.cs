namespace Server.UseCases.Intefaces;

public interface IRequest<TResult, in TResquest>
{
    public Task<TResult> Handle(TResquest request);
}