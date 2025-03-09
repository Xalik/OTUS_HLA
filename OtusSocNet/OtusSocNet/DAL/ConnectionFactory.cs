using Npgsql;

namespace OtusSocNet.DAL;

public interface IConnectionFactory
{
    public NpgsqlConnection CreateConnection();
}

public class ConnectionFactory: IConnectionFactory
{
    public ConnectionFactory(string connectionString)
    {
        this.connectionString = connectionString;
    }
    
    private readonly string connectionString;
    
    public NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(connectionString);
    }
}