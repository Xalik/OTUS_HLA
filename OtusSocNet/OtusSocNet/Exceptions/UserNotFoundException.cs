namespace OtusSocNet.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(Guid userId) : base($"User not found by id {userId}") { }
}