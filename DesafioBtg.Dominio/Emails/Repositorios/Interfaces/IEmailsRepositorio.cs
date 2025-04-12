namespace DesafioBtg.Dominio.Emails.Repositorios.Interfaces;

public interface IEmailsRepositorio
{
    Task EnviarEmailAsync(string destinatario, string assunto, string mensagem);
}
