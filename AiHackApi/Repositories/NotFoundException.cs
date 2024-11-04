[Serializable] // Marca a classe como serializável para permitir que seja transmitida entre diferentes contextos, como por redes ou armazenamento
internal class NotFoundException : Exception // A classe herda da classe base Exception
{
    // Construtor padrão sem parâmetros
    public NotFoundException()
    {
    }

    // Construtor que aceita uma mensagem de erro
    public NotFoundException(string? message) : base(message)
    {
    }

    // Construtor que aceita uma mensagem de erro e uma exceção interna
    public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
