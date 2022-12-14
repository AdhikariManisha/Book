namespace Book.Application.Contracts.Connections;

public interface IDbConnection
{
    System.Data.IDbConnection GetConnection();
}